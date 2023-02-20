using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameFramework
{
    public partial class SceneBase : MonoBehaviour
    {
        /// <summary>
        /// 主角Aoi
        /// </summary>
        private RoleAoiUnit mainPlayerAoi;

        public RoleAoiUnit MainPlayerAoi
        {
            get
            {
                return mainPlayerAoi;
            }
        }
        
        /// <summary>
        /// 场景进入
        /// </summary>
        public virtual void OnStart()
        {
            //创建角色
            InitPlayerAoi();
            //创建摄像机
            //创建摇杆
            MainGame.GameUI.OpenUI(Const.JoystickUIPanel, EnumType.UIGroupEnum.Battle,false);
            
        }
        
        
        public virtual void OnUpdate()
        {
            
        }
        
        public virtual void OnDestroy()
        {
            
        }
    }
}
