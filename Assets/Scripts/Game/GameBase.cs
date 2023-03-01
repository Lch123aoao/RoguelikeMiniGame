using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

public class GameBase
{
    protected bool isInitGame = false;

    private int levelId;
    public int LevelId
    {
        get;
        protected set;
    }

    public bool GameOver
    {
        get;
        protected set;
    }

    protected RoleAoiUnit playerUnit = null;


    //初始化游戏（进入游戏过程状态）
    public virtual void Initialize(int levelId)
    {
        isInitGame = false;
        LevelId = levelId;
        MainGame.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        MainGame.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
    }

    /// <summary>
    /// 等待玩家加载好后，加载其他信息
    /// </summary>
    public virtual void InitOtherGameInfo()
    {

    }

    public virtual void Update(float elapseSeconds, float realElapseSeconds)
    {
        if (playerUnit == null) return;
        if (!isInitGame)
        {
            InitOtherGameInfo();
            isInitGame = true;
        }
    }

    //结束游戏（退出游戏过程状态,（接着进入结算状态））
    public virtual void Shutdown()
    {
        MainGame.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        MainGame.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
    }

    protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        if (ne.EntityLogicType == typeof(RoleAoiUnit))
        {
            playerUnit = (RoleAoiUnit)ne.Entity.Logic;
        }
    }

    protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
    {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
        Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
    }

}
