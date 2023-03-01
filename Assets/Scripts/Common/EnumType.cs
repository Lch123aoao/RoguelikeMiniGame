using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举脚本
/// </summary>
public class EnumType
{
    /// <summary>
    /// UI层级枚举
    /// </summary>
    public enum UIGroupEnum
    {
        //战斗层
        Battle,
        Main,//主界面层
        UI,//UI系统层
        Atlas,//弹窗层
        Float,//飘字层
    }

    /// <summary>
    /// 阵营
    /// </summary>
    public enum CampType : byte
    {
        Unknown,

        //玩家类型
        Player,
        // 敌人类型
        Enemy,
    }
    /// <summary>
    /// 关系
    /// </summary>
    public enum RelationType : byte
    {
        Unknown ,
        Friendly,//友方
        Hostile,//敌方
    }


    //技能朝向类型
    public enum TowardType : byte
    {
        launcherForward,//发射者正方向
        towardNearestEnemy,//朝向距离最近的敌人
    }

}
