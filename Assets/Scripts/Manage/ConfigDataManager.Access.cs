using System.Collections;
using System.Collections.Generic;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace MyGameFramework
{
    /// <summary>
    /// Access解析
    /// </summary>
    public partial class ConfigDataManager : GameDataManager
    {
        public List<DRAccess> GetDRAccessAll()
        {
            var table = MainGame.DataTable.GetDataTable<DRAccess>();
            if (table == null)
            {
                Log.Error("DRRoleStyle is null");
                return null;
            }

            var list = new List<DRAccess>();
            foreach (var roleStyle in table)
            {
                if (roleStyle == null) continue;
                list.Add(roleStyle);
            }

            return list;
        }
        
        public DRAccess GetDRAccessById(int id)
        {
            var table = MainGame.DataTable.GetDataTable<DRAccess>();
            if (table == null)
            {
                Log.Error("DRRoleStyle is null");
                return null;
            }
            foreach (var item in table)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            Log.Error($"无法获得 {id} DRAccess 表数据");
            return null;
        }
    }
}
