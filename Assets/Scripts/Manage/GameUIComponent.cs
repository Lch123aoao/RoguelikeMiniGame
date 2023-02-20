using GameFramework.Event;
using UnityEngine;
using MyGameFramework;

namespace UnityGameFramework.Runtime
{
    public class GameUIComponent : GameFrameworkComponent
    {
        /// <summary>
        /// UI相机
        /// </summary>
        private Camera UICamera;

        private UIComponent _uiComponent;
        
        public void SetUICamera(Camera vCamera)
        {
            UICamera = vCamera;
        }
        
        public Camera GetUICamera()
        {
            return UICamera;
        }
        
        public void SetUIComponent(UIComponent vUIComponent)
        {
            _uiComponent = vUIComponent;
        }
        
        protected override void Awake()
        {
            base.Awake();
          
        }

        protected override void Start()
        {
            base.Start();
            //注册加载成功回调
            MainGame.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OpenUIFormSuccess);
            //注册加载失败回调
            MainGame.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OpenUIFormFailure);
            //
            MainGame.Event.Subscribe(CloseUIFormCompleteEventArgs.EventId, CloseUIFormComplete);
        }


        /// <summary>
        /// 打开UI界面
        /// </summary>
        /// <param name="uiName"></param>
        /// <param name="vGroup"></param>
        /// <param name="pauseCoveredUIForm"></param>
        /// <returns></returns>
        public int OpenUI(string uiName,EnumType.UIGroupEnum vGroup, bool pauseCoveredUIForm)
        {
            if (uiName == string.Empty)
                return -1;

            string formName = Const.UI_Path + uiName + ".prefab";
            int formId = _uiComponent.OpenUIForm(formName,vGroup.ToString(),pauseCoveredUIForm);
            return formId;
        }

        public void CloseUI(int fromId)
        {
            if(fromId == 0)
                return;
            if (!_uiComponent.HasUIForm(fromId))
            {
                //不存在的界面
                return;
            }
           _uiComponent.CloseUIForm(fromId);
        }
        
        
        /// <summary>
        /// 打开界面成功回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        public void OpenUIFormSuccess(object sender, GameEventArgs gameEventArgs)
        {
            OpenUIFormSuccessEventArgs args = gameEventArgs as OpenUIFormSuccessEventArgs;
            if (args == null || args.UIForm == null)
                return;

            Canvas _canvas = args.UIForm.transform.GetComponent<Canvas>();
            _canvas.worldCamera = UICamera;
        }
        
        /// <summary>
        /// 打开界面失败回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        public void OpenUIFormFailure(object sender, GameEventArgs gameEventArgs)
        {
            OpenUIFormFailureEventArgs args = gameEventArgs as OpenUIFormFailureEventArgs;
            Log.Error("界面打开失败  name:" + args.ErrorMessage);
        }

        /// <summary>
        /// 关闭界面回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        public void CloseUIFormComplete(object sender, GameEventArgs gameEventArgs)
        {
            
        }
    }
}
