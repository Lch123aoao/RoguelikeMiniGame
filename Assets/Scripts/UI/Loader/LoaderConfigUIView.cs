using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.UI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;
using Slider = UnityEngine.UI.Slider;

namespace MyGameFramework
{
    public class LoaderConfigUIView : UIFormLogic
    {
        public Slider _configSlider;
        public Text _configtext;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
    }
}
