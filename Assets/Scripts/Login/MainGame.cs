using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public partial class MainGame : MonoBehaviour
{
    private static GameObject MainGameRoot;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.LogError("游戏启动");
        
        InitBuiltinComponents();
        InitCustomComponents();
        
        if (MainGameRoot == null)
        {
            MainGameRoot = new GameObject();
            MainGameRoot.transform.SetParent(null);
            MainGameRoot.name = "MainGameRoot";
        }
        DontDestroyOnLoad(MainGameRoot.gameObject);
        this.transform.parent = MainGameRoot.transform;
    }


    private T GetGameFrameworkComponent<T>() where T : GameFrameworkComponent
    {
        return GameEntry.GetComponent<T>();
    }
}
