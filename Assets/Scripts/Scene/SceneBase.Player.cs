using System.Collections;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine;

namespace MyGameFramework
{
    public partial class SceneBase
    {
        /// <summary>
        /// 初始化角色
        /// </summary>
        public void InitPlayerAoi()
        {
            MainGame.Resource.LoadAsset(Const.PlayerPath, new LoadAssetCallbacks(AssetPlayerCallbacks));
        }
        
        /// <summary>
        /// 创建角色成功回调
        /// </summary>
        public void AssetPlayerCallbacks(string assetname, object asset, float duration, object userdata)
        {
            if (asset == null) return;
            GameObject playerObj = GameObject.Instantiate((GameObject)asset);
            if (playerObj == null)
                return;
            mainPlayerAoi = playerObj.GetComponent<RoleAoiUnit>();
        }
    }
}


