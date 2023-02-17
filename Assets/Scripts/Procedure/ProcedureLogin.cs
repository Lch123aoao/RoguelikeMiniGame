using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;

namespace MyGameFramework
{
    /// <summary>
    /// 登录流程
    /// </summary>
    public class ProcedureLogin : ProcedureBase
    {
        private int curForm;
         protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            GameDataManager.GetDataManager<LoaderDataManager>().isLoginBtn = false;
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);;
            curForm = MainGame.GameUI.OpenUI(Const.LoginUIView, EnumType.UIGroupEnum.Main,false );

        }


        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (GameDataManager.GetDataManager<LoaderDataManager>().isLoginBtn)
            {
                GameDataManager.GetDataManager<LoaderDataManager>().isLoginBtn = false;
                //进入游戏流程
                ChangeState<ProcedureScene>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            MainGame.UI.CloseUIForm(curForm);
            GameDataManager.GetDataManager<LoaderDataManager>().isLoginBtn = false;
        }

        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
