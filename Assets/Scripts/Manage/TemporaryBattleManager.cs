using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TemporaryBattleManager
{
    
    private static TemporaryBattleManager instance = null;

    private TemporaryBattleManager() { }

    public static TemporaryBattleManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = new TemporaryBattleManager();
            }

            return instance;
        }
    }

    #region 人物移动相关

    /// <summary>
    /// 角色是否在运动
    /// </summary>
    public bool isPlayerRun;

    /// <summary>
    /// 人物运动方向
    /// </summary>
    public Vector2 playerPos;

    #endregion
}
