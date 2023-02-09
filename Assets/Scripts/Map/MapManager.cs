using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("玩家")]
    public Transform _player;
    [Header("生成地形的父对象")]
    public Transform _terrParent;
    [Header("生成地形的大小(单个预制体 长/宽)")]
    public Vector2 _terrainSize = new Vector2(10, 10);
    [Header("超出已设置的地形范围后，随机生成的地形")]
    public List<GameObject> _terrainObjs;
    [Header("离开地形区域后地形自动隐藏的时间")]
    public float _timer = 3f;

    private Dictionary<Vector2, TerrainData> _terrainLoadedFixed;
    private Dictionary<(int x, int y), TerrainData> _terrainLoaded;
    private Dictionary<(int x, int y), TerrainData> _dictTemp;
    private Dictionary<(int x, int y), GameobjAndCoroutine> _unloadTerrCountDown;
    private Dictionary<int, Stack<GameObject>> _terrainPool;
    private (int x, int y) _lastPos = (0, 0);

    struct GameobjAndCoroutine
    {
        public TerrainData data;
        public Coroutine Cor;
    }

    public struct TerrainData
    {
        public int index; //代表地图预制体集合里的第几个预制体（_terrainObjs）
        public GameObject terrain;
        public TerrainData(int _index, GameObject _terrain)
        {
            index = _index;
            terrain = _terrain;
        }
    }

    private void Awake()
    {
        _terrainLoadedFixed = new Dictionary<Vector2, TerrainData>();
        _terrainLoaded = new Dictionary<(int x, int y), TerrainData>();
        _dictTemp = new Dictionary<(int x, int y), TerrainData>();
        _unloadTerrCountDown = new Dictionary<(int x, int y), GameobjAndCoroutine>();
        _terrainPool = new Dictionary<int, Stack<GameObject>>();
    }

    private void Start()
    {
        FirstLoadTerrain();
    }

    private void FixedUpdate()
    {
        LoadTerrain();
    }

    /// <summary>
    /// 第一次加载地形
    /// </summary>
    private void FirstLoadTerrain()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (_terrainLoaded.TryGetValue((i, j), out TerrainData data))
                {
                    _dictTemp.Add((i, j), data);
                    _terrainLoaded.Remove((i, j));
                    data.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                    data.terrain.SetActive(true);

                }
                else
                {
                    if (_unloadTerrCountDown.TryGetValue((i, j), out GameobjAndCoroutine val))
                    {
                        StopCoroutine(val.Cor);
                        _dictTemp.Add((i, j), val.data);
                        _unloadTerrCountDown.Remove((i, j));
                        val.data.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                        val.data.terrain.SetActive(true);
                    }
                    else
                    {
                        var newTerr = GetTerrainNew(i, j, -1);
                        _dictTemp.Add((i, j), newTerr);
                        newTerr.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                        newTerr.terrain.SetActive(true);
                    }
                }
            }
        }
        (_terrainLoaded, _dictTemp) = (_dictTemp, _terrainLoaded);
    }

    /// <summary>
    /// 加载地形
    /// </summary>
    private void LoadTerrain()
    {
        if (_player != null)
        {
            (int x, int y) pos = (Mathf.RoundToInt(_player.position.x / _terrainSize.x), Mathf.RoundToInt(_player.position.y / _terrainSize.y));
            if (!(pos == _lastPos))//当位置发生改变时进行判断，玩家进入新区域
            {
                _lastPos = pos;
                _dictTemp.Clear();
                //围绕玩家当前进入的新地形的周围九宫格一圈进行检查
                for (int i = pos.x - 1; i < pos.x + 2; i++)
                {
                    for (int j = pos.y - 1; j < pos.y + 2; j++)
                    {
                        if (_terrainLoaded.TryGetValue((i, j), out TerrainData terr))//如果_terrainLoaded已经存储有该地形的游戏对象
                        {
                            _dictTemp.Add((i, j), terr);
                            _terrainLoaded.Remove((i, j));
                            terr.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                            terr.terrain.SetActive(true);
                        }
                        else//地图的生成脚本
                        {
                            if (_unloadTerrCountDown.TryGetValue((i, j), out GameobjAndCoroutine val))//若玩家进入新地形后，在原来的地形还未取消激活时又返回到该位置时，执行该部分！
                            {
                                StopCoroutine(val.Cor);
                                _dictTemp.Add((i, j), val.data);
                                _unloadTerrCountDown.Remove((i, j));
                                val.data.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                                val.data.terrain.SetActive(true);
                            }
                            else//玩家每次离开原来的位置，进入新地方时调用该方法
                            {
                                var data = GetTerrainNew(i, j, -1);
                                _dictTemp.Add((i, j), data);
                                data.terrain.transform.localPosition = new Vector2(i * _terrainSize.x, j * _terrainSize.y);
                                data.terrain.SetActive(true);
                            }
                        }
                    }
                }

                //遍历_terrainLoaded，此时的_terrainLoaded内的对象都不在角色当前九宫格一圈，所以遍历进行准备隐藏显示
                foreach (var item in _terrainLoaded)//利用循环检测 将除了正在使用的9块地形之外的全部取消激活
                {
                    _unloadTerrCountDown.Add(item.Key, new GameobjAndCoroutine
                    {
                        Cor = StartCoroutine(RemoveTerrDelay(item.Key)),// 开启取消地块显示的协程
                        data = item.Value
                    });
                }
                (_terrainLoaded, _dictTemp) = (_dictTemp, _terrainLoaded);
            }
        }
    }

    /// <summary>
    /// 等待一段时间后，将地形对象取消激活
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private IEnumerator RemoveTerrDelay((int x, int y) pos)
    {
        yield return new WaitForSeconds(_timer);
        if (_unloadTerrCountDown.TryGetValue(pos, out var v))
        {
            RecycleTerrain(v.data.index, v.data.terrain);
            _unloadTerrCountDown.Remove(pos);
        }
    }

    /// <summary>
    /// 回收地形
    /// </summary>
    /// <param name="t"></param>
    private void RecycleTerrain(int index, GameObject obj)
    {
        obj.SetActive(false);
        Push(index, obj);
    }

    /// <summary>
    /// 获取新地形
    /// </summary>
    /// <returns></returns>
    private TerrainData GetTerrainNew(int x, int y, int index)
    {
        if (_terrainLoadedFixed.TryGetValue(new Vector2(x, y), out TerrainData data))
        {
            GameObject obj = Pop(data.index);
            data.terrain = obj;
            return data;
        }
        else
        {
            int tempIndex = Random.Range(0, _terrainObjs.Count);//从随机地形中抽取一个地形生成并返回
            GameObject obj = Pop(tempIndex);
            var data1 = new TerrainData(tempIndex, obj);
            _terrainLoadedFixed.Add(new Vector2(x, y), data1);
            return new TerrainData(tempIndex, obj);
        }
    }

    //临时池子
    private GameObject Pop(int index)
    {
        GameObject item = null;
        if (_terrainPool.ContainsKey(index))
        {
            Stack<GameObject> pool = _terrainPool[index];
            if (pool.Count > 0)
            {
                item =  pool.Pop();
                return item;
            } 
            item = Instantiate(_terrainObjs[index]);
        }
        else
        {
            if (index == -1)
            {
                item = Instantiate(_terrainObjs[index]);//栈中没有该位置的地形，新建并返回一个新的地形对象！
            }
            else
            {
                item = Instantiate(_terrainObjs[index]);
            }
        }
        item.transform.SetParent(_terrParent,true);
        return item;
    }
    private void Push(int index, GameObject obj)
    {
        if (!_terrainPool.ContainsKey(index))
        {
            _terrainPool.Add(index, new Stack<GameObject>());
        }
        _terrainPool[index].Push(obj);
    }
}
