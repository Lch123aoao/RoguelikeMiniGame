using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MyGameFramework
{
    //初始化流程
    public class ProcedureInit : ProcedureBase
    {
        enum initType
        {
            UIRoot = 1,
        }
        /// <summary>
        /// 是否初始化完毕
        /// </summary>
        private int isInit = 0;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            isInit = 1;
            // 流程进入
            InitUIRoot();
        }

        /// <summary>
        /// 初始化UIRoot
        /// </summary>
        public void InitUIRoot()
        {
            LoadAssetCallbacks callBacks = new LoadAssetCallbacks(LoadRootAssetCallBacks);
            MainGame.Resource.LoadAsset("Assets/Resources/Prefabs/UI/Root/UIRoot.prefab",callBacks);
            isInit <<= (int)initType.UIRoot;
        }
        
        /// <summary>
        /// 创建UIRoot回调
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="asset"></param>
        /// <param name="duration"></param>
        /// <param name="userData"></param>
        public void LoadRootAssetCallBacks( string assetName,
            object asset,
            float duration,
            object userData)
        {
            //加载成功
            GameObject Root = GameObject.Instantiate<GameObject>((GameObject)asset);
            UIRoot uiRoot = Root.GetComponent<UIRoot>();
            if (uiRoot == null)
            {
                Log.Error("UIRoot出问题，找不到组件");
                return;
            }
            Object.DontDestroyOnLoad(Root.gameObject);
            MainGame.UI.SetInstanceRoot(Root.transform);
            MainGame.GameUI.SetUICamera(uiRoot.uiCamera);
            MainGame.GameUI.SetUIComponent(MainGame.UI);
            isInit >>= (int)initType.UIRoot;
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (isInit == 1)
            {
                ChangeState<ProcedureConfig>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            isInit = 0;
        }
    }
}
