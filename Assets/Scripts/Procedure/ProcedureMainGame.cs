using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MyGameFramework
{
    /// <summary>
    /// 主游戏运行流程
    /// </summary>
    public class ProcedureMainGame : ProcedureBase
    {
        private GameBase currentGame;
        private SceneBase mSceneBase;
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            //添加一个游戏模式
            currentGame = new LevelGame();
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            mSceneBase = GameObject.Find("SceneBase").GetComponent<SceneBase>();
            if (mSceneBase == null) return;
            mSceneBase.OnStart();

            //初始化游戏流程
            int levelId = 1;
            //获取关卡数据
            currentGame.Initialize(levelId);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            // mSceneBase.OnUpdate();

            if (currentGame != null && !currentGame.GameOver)
            {
                currentGame.Update(elapseSeconds, realElapseSeconds);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            //关闭 相当于移除
            mSceneBase.OnDestroy();
            currentGame.Shutdown();
        }
    }
}
