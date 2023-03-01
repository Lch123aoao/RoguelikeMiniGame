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
    /// 切换场景流程
    /// </summary>
    public class ProcedureScene : ProcedureBase
    {
        private bool isScene = false;
        
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            //进行场景切换
            MainGame.Event.Subscribe(LoadSceneSuccessEventArgs.EventId,LoadSceneSuccess);
            //开始加载场景
            isScene = false;
            //进入场景
            MainGame.Scene.LoadScene("Assets/Scenes/1001.unity", 0);
        }

        private void LoadSceneSuccess(object sender, GameEventArgs e)
        {
            //场景加载完毕
            isScene = true;
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (isScene)
            {
                ChangeState<ProcedureMainGame>(procedureOwner);
                isScene = false;
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            MainGame.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId,LoadSceneSuccess);
            isScene = false;
        }
    }
}