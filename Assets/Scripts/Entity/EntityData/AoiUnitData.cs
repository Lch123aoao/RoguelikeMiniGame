using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;

[Serializable]
public class AoiUnitData : EntityData
{
    //等级
    [SerializeField]
    private int curLevel;

    //当前经验
    [SerializeField]
    private int curExp;

    //当前血量
    [SerializeField]
    private int curHp;

    //当前防御、护盾值
    [SerializeField]
    private int curDefense;

    //当前拥有的技能
    [SerializeField]
    private List<SkillData> skillList;

    //属于什么类型单位
    [SerializeField]
    private EnumType.CampType campType = EnumType.CampType.Unknown;

    public AoiUnitData(int entityId, int configId, EnumType.CampType type)
           : base(entityId, configId)
    {
        IDataTable<DRUnitData> unitConfigs = MainGame.DataTable.GetDataTable<DRUnitData>();
        DRUnitData data = unitConfigs.GetDataRow(configId);
        if (data == null)
        {
            Log.Error("Can not find unit id '{0}' from data table.", configId);
            return;
        }
        EntityId = data.EntityId;
        campType = type;
        curLevel = 1;
        curHp = data.HP;
        curExp = data.EXP;
        if (data.SkillList != null && data.SkillList != "")
        {
            var tempList = data.SkillList.Split(',');
            for (int i = 0; i < tempList.Length; i++)
            {
                var skillId = int.Parse(tempList[i]);
                ModifySkill(true, skillId, 0);
            }
        }
    }
    /// <summary>
    /// 角色阵营。
    /// </summary>
    public EnumType.CampType CampType
    {
        get
        {
            return campType;
        }
    }

    /// <summary>
    /// 当前等级
    /// </summary>
    public int Level
    {
        get
        {
            return curLevel;
        }
        set
        {
            curLevel = value;
        }
    }

    /// <summary>
    /// 当前经验值。
    /// </summary>
    public int Exp
    {
        get
        {
            return curExp;
        }
        set
        {
            curExp = value;
        }
    }

    /// <summary>
    /// 当前生命。
    /// </summary>
    public int HP
    {
        get
        {
            return curHp;
        }
        set
        {
            curHp = value;
        }
    }

    /// <summary>
    /// 当前防御值。
    /// </summary>
    public int Defense
    {
        get
        {
            return curDefense;
        }
        set
        {
            curDefense = value;
        }
    }

    /// <summary>
    /// 当前擁有的技能
    /// </summary>
    public List<SkillData> SkillList
    {
        get
        {
            return skillList;
        }
        set
        {
            skillList = value;
        }
    }
    /// <summary>
    /// 修改技能
    /// </summary>
    /// <param name="isAdd">是否添加</param>
    /// <param name="skillId">技能id</param>
    public void ModifySkill(bool isAdd, int skillId, int signId)
    {
        if (skillList == null)
            skillList = new List<SkillData>();
        if (isAdd)
        {
            var vData = new SkillData(0, skillId, MainGame.Entity.GenerateSignId(), campType);
            skillList.Add(vData);
        }
        else
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i].SkillId == skillId && skillList[i].SignId == signId)
                {
                    skillList.RemoveAt(i);
                    return;
                }
            }
        }
    }
    /// <summary>
    /// 获取当前自身攻击力
    /// </summary>
    /// <param name="calculateCoolingSkill">是否将正在冷却中的技能攻击力计算入内</param>
    public int GetSelfAttack(bool calculateCoolingSkill)
    {
        int attack = 0;
        for (int i = 0; i < skillList.Count; i++)
        {
            var skill = skillList[i];
            if (skill.EntityId == -1)
            {
                if (calculateCoolingSkill)
                {
                    attack += skill.AttackValue;
                    continue;
                }
                if (skill.remainAttackCD <= 0)
                    attack += skill.AttackValue;
            }
        }
        return attack;
    }

}
