using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MyGameFramework
{
    public class Login : UIFormLogic
    {
        public Button LoginBtn;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            LoginBtn.onClick.AddListener(OnClickLoginBtn);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            LoginBtn.onClick.RemoveListener(OnClickLoginBtn);
            base.OnClose(isShutdown, userData);
        }

        /// <summary>
        /// 点击登录按钮
        /// </summary>
        public void OnClickLoginBtn()
        {
            GameDataManager.GetDataManager<LoaderDataManager>().isLoginBtn = true;
        }
    }
}
