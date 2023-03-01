using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class MonsterSpawnComponent : GameFrameworkComponent
{
    //     //玩家信息
    //     public Transform playerTransform;

    //     //怪物缓存
    //     private int monsterKey = 0;//用于查找
    //     private int spawnCount_EveryTime = 10; // 每一次生成的数量
    //     private int spawnMaxCount = 100;//最多创建的数量
    //     private Dictionary<string, MonsterUnit> monsterDic;//场景上存在的怪物数量
    //     private Dictionary<int, GameObject> monsterPrefabs;

    //     //关卡数据
    //     private LevelData levelData;

    //     protected override void Awake()
    //     {
    //         base.Awake();
    //         Init();
    //     }

    //     /// <summary>
    //     /// 预加载怪物资源  TODO
    //     /// </summary>
    //     public void Init()
    //     {
    //         monsterDic = new Dictionary<string, MonsterUnit>();
    //         monsterPrefabs = new Dictionary<int, GameObject>();

    //         levelData = DBManager.instance.GetLevelInfo();
    //         LoadMonster(levelData.ordinaryMonsterIdList);
    //         LoadMonster(levelData.eliteMonsterIdList);
    //         LoadMonster(levelData.bossIdList);

    //         LoopSpawnMonster();
    //     }
    //     public void dispose()
    //     {
    //         GameController.instance.mLogicScheduler.UnSchedule("SpawnMonster");
    //     }

    //     public void LoopSpawnMonster()
    //     {
    //         if (monsterDic.Count > spawnMaxCount)
    //         {
    //             GameController.instance.mLogicScheduler.Schedule(LoopSpawnMonster, 2, "SpawnMonster");
    //             return;
    //         }
    //         for (int i = 0; i < spawnCount_EveryTime; i++)
    //         {
    //             int ranNum = Random.Range(0, levelData.ordinaryMonsterIdList.Count);
    //             int monsterId = levelData.ordinaryMonsterIdList[ranNum];

    //             MonsterUnit unit = Instantiate(monsterPrefabs[monsterId], transform).GetComponent<MonsterUnit>();
    //             UnitData data = new UnitData();
    //             data.level = 1;
    //             data.curExp = 10;
    //             unit.Init(monsterId, playerTransform, data);
    //             AddMonster(unit);
    //         }
    //         GameController.instance.mLogicScheduler.Schedule(LoopSpawnMonster, 2, "SpawnMonster");
    //     }
    //     //添加怪物到场景上
    //     public void AddMonster(MonsterUnit unit)
    //     {
    //         string key = "Monster_" + monsterKey;
    //         unit.gameObject.name = key;
    //         monsterDic.Add(key, unit);
    //         monsterKey++;
    //     }
    //     //将怪物从场景上移除
    //     public void RemoveMonster(string key)
    //     {
    //         if (monsterDic.ContainsKey(key))
    //         {
    //             monsterDic.Remove(key);
    //         }
    //     }
    //     //获取场景上的一只怪物
    //     public MonsterUnit GetMonster(string monsterName)
    //     {
    //         if (monsterDic.ContainsKey(monsterName))
    //         {
    //             return monsterDic[monsterName];
    //         }
    //         return null;
    //     }
    //     private void LoadMonster(List<int> monsterIds)
    //     {
    //         string path = null;
    //         for (int i = 0; i < monsterIds.Count; i++)
    //         {
    //             int id = monsterIds[i];
    //             //获取资源路径  TODO
    //             path = "Prefabs/Monster/" + "MonsterUnit";
    //             AssetsLoader.instance.LoadAssets(path, (GameObject go) =>
    //                                {
    //                                    monsterPrefabs.Add(id, go);
    //                                });
    //         }
    //     }
}
