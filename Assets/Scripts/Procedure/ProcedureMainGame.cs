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
        private SceneBase mSceneBase;
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            mSceneBase = GameObject.Find("SceneBase").GetComponent<SceneBase>();
            if(mSceneBase == null) return;
            mSceneBase.OnStart();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            mSceneBase.OnUpdate();
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            //关闭 相当于移除
            mSceneBase.OnDestroy();
        }
    }
}
