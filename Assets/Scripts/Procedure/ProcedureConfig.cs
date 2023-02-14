using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MyGameFramework
{
    public partial class ProcedureConfig : ProcedureBase
    {
        /// <summary>
        /// 存储正在加载的表
        /// </summary>
        private List<string> LoaderConfigNameList = new List<string>();

        public int curForm;
        
        /// <summary>
        /// 是否加载完成
        /// </summary>
        private bool mIsLoaderFinish = false;
        
        public const string ConfigPath = "Assets/Resources/DataTable/{0}.bytes";
        
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            //流程初始化
            Debug.LogError("流程打印==>  表配置流程初始化");
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.LogError("流程打印==>  表配置流程 进入打印");
            //注册加载成功回调
            MainGame.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoaderDataTableSuccess);
            //注册加载失败回调
            MainGame.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoaderDataTableFailure);
            
            curForm = MainGame.GameUI.OpenUI(Const.LoaderConfigUIView, EnumType.UIGroupEnum.Main,false);
            for (int i = 0; i < ConfigNameList.Count; i++)
            {
                InitConfigData(ConfigNameList[i]);
            }

            if (ConfigNameList.Count <= 0)
            {
                ChangeState<ProcedureLogin>(procedureOwner);
            }
        }


        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (mIsLoaderFinish)
            {
                //切换到下一个流程
                ChangeState<ProcedureLogin>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Debug.LogError("流程打印==>  表配置流程 离开打印");
            // 表加载成功回调
            MainGame.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoaderDataTableSuccess);
            // 表加载失败回调
            MainGame.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoaderDataTableFailure);
            mIsLoaderFinish = false;
            
            //流程结束 关闭资源加载界面
            MainGame.GameUI.CloseUI(curForm);
        }

        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            // 游戏退出时执行
            Debug.LogError("流程打印==>  表配置流程 退出打印");
        }
        
        public void InitConfigData(string vDataTableName)
        {
            string vDataTableAssetName = GetDataTableAssetName(vDataTableName);
            LoaderConfigNameList.Add(vDataTableAssetName);

            if (MainGame.DataTable.HasDataTable(vDataTableName, vDataTableAssetName, null))
            {
                MainGame.DataTable.UnLoadDataTable(vDataTableName, vDataTableAssetName, null);
            }
            MainGame.DataTable.LoadDataTable(vDataTableName, vDataTableAssetName, null);
        }
        
        private string GetDataTableAssetName(string vDataTableName)
        {
            return string.Format(ConfigPath, vDataTableName);
        }
        
        /// <summary>
        /// 加载成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaderDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs vEventArgs = e as LoadDataTableSuccessEventArgs;
            Log.Info(vEventArgs.DataTableAssetName + "表加载成功");
            LoaderConfigNameList.Remove(vEventArgs.DataTableAssetName);
            // UnLoaderConfigNameList.Add(vEventArgs.DataTableAssetName);
            // mLoadingDataManager.SetLastLoadConfigCount(LoaderConfigNameList.Count);
            // mLoadingDataManager.SetLoadingStep(EnumDataDefine.LoadingStep.LoadingConfig);
            if (LoaderConfigNameList.Count <= 0)
            {
                Log.Info("表全部加载完成");
                mIsLoaderFinish = true;
            }
        }
        
        private void OnLoaderDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs vEventArgs = e as LoadDataTableFailureEventArgs;
            Log.Error(vEventArgs.DataTableAssetName + "表加载失败");
        }

        
        
        public void OpenUIFormSuccess(object sender, GameEventArgs e)
        {
            Debug.LogError("界面成功打开");
        }
        
        public void OnOpenUIFormFailure(object sender, GameEventArgs e)
        {
            Debug.LogError("界面打开失败");
        }
    }
}