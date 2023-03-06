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

    /// <summary>
    /// 获取地图控制器组件
    /// </summary>
    public static MapComponent MapComponent
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取怪物
    /// </summary>
    public static MonsterSpawnComponent MonsterSpawnComponent
    {
        get;
        private set;
    }

    /// <summary>
    /// 技能总管理器
    /// </summary>
    /// <value></value>
    public static SkillComponent SkillComponent
    {
        get;
        private set;
    }

    /// <summary>
    /// 计时器
    /// </summary>
    /// <value></value>
    public static TimerComponent TimerComponent
    {
        get;
        private set;
    }

    public void InitCustomComponents()
    {
        GameUI = GetGameFrameworkComponent<GameUIComponent>();
        MapComponent = GetGameFrameworkComponent<MapComponent>();
        MonsterSpawnComponent = GetGameFrameworkComponent<MonsterSpawnComponent>();
        SkillComponent = GetGameFrameworkComponent<SkillComponent>();
        TimerComponent = GetGameFrameworkComponent<TimerComponent>();
    }
}
