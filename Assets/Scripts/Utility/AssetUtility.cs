using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

//资源路径
public static class AssetUtility
{

    public static string GetEntityAsset(string assetName)
    {
        return Utility.Text.Format("Assets/Resources/Prefabs/Entities/{0}.prefab", assetName);
    }
}
