using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameFramework
{
    public class GameDataManager
    {
        private static GameDataManager mInstance;
        private static GameDataManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new GameDataManager();
                }
                return mInstance;
            }
        }
        
        /// <summary>
        /// 返回管理类
        /// </summary>
        /// <typeparam name="T">该管理类（必须继承GameDataManager）</typeparam>
        /// <param name="vDataManagerName">类名</param>
        /// <returns></returns>
        public static T GetDataManager<T>(string vDataManagerName = null) where T : GameDataManager
        {
            if (string.IsNullOrEmpty(vDataManagerName))
                vDataManagerName = typeof(T).ToString();
            var vDataManager = Instance.GetDataManager(vDataManagerName);
            if (vDataManager == null)
            {
                var temp = (GameDataManager)System.Activator.CreateInstance<T>();
                temp.SetDataManagerName(vDataManagerName);
                Instance.RegisterDataManager(temp);
                vDataManager = temp;
            }
            return (T)vDataManager;
        }
        
        
        /// <summary>
        /// 存储管理类的字典
        /// </summary>
        private Dictionary<string, GameDataManager> mDataManagerDic = new Dictionary<string, GameDataManager>();

        /// <summary>
        /// 返回管理类
        /// </summary>
        /// <param name="vDataManagerName">管理类名</param>
        /// <returns></returns>
        private GameDataManager GetDataManager(string vDataManagerName)
        {
            if (mDataManagerDic.ContainsKey(vDataManagerName))
            {
                return mDataManagerDic[vDataManagerName];
            }
            return null;
        }
        
        
        /// <summary>
        /// 注册管理类
        /// </summary>
        /// <param name="vGameDataManager"></param>
        private void RegisterDataManager(GameDataManager vGameDataManager)
        {
            if (mDataManagerDic.ContainsKey(vGameDataManager.DataManagerName))
            {
                mDataManagerDic[vGameDataManager.DataManagerName] = vGameDataManager;
            }
            else
            {
                mDataManagerDic.Add(vGameDataManager.DataManagerName, vGameDataManager);
            }
        }
        
        /// <summary>
        /// 当前数据管理类名
        /// </summary>
        private string DataManagerName;

        /// <summary>
        /// 设置当前数据管理类名
        /// </summary>
        /// <param name="vDataManagerName"></param>
        private void SetDataManagerName(string vDataManagerName)
        {
            DataManagerName = vDataManagerName;
        }

        public static void ClearData()
        {
            mInstance = null;
        }
    }
}
