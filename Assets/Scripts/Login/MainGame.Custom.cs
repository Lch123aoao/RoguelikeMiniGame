using System.Collections;
using System.Collections.Generic;
using MyGameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 自定义组件类
/// </summary>
public partial class MainGame
{
    /// <summary>
    /// 获取资源组件
    /// </summary>
    public static GameUIComponent GameUI
    {
        get;
        private set;
    }
    
    public void InitCustomComponents()
    {
        GameUI = GetGameFrameworkComponent<GameUIComponent>();
    }
}
