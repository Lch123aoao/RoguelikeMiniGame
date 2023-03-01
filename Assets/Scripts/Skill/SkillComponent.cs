using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SkillComponent : GameFrameworkComponent
{
    //所以技能模板
    private Dictionary<int, Type> _allSkillTemplate;

    protected override void Awake()
    {
        base.Awake();
    }
    public void Init_Skill()
    {
        if (_allSkillTemplate == null)
        {
            _allSkillTemplate = new Dictionary<int, Type>();
            IDataTable<DRSkillTemplate> configs = MainGame.DataTable.GetDataTable<DRSkillTemplate>();
            DRSkillTemplate[] datas = configs.GetAllDataRows();
            for (int i = 0; i < datas.Length; i++)
            {
                Type type = Type.GetType(datas[i].ClassName);
                _allSkillTemplate.Add(datas[i].Id, type);
            }
        }
    }

    /// <summary>
    /// 创建一个技能释放器
    /// </summary>
    /// <returns></returns>
    public SkillDeployer CreateSkillDeployer()
    {
        return Activator.CreateInstance(typeof(SkillDeployer)) as SkillDeployer;
    }

    /// <summary>
    /// 创建一个技能模板
    /// </summary>
    /// <param name="id">模板id</param>
    /// <returns></returns>
    public SkillTemplateBase CreateSkillTemplateBaseById(int id)
    {
        if (_allSkillTemplate.ContainsKey(id))
        {
            return Activator.CreateInstance(_allSkillTemplate[id]) as SkillTemplateBase;
        }
        return null;
    }

    //创建一个技能释放器


}
