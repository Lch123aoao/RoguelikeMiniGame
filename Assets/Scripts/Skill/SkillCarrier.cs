using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能载体（处理此技能应该以什么方式表现）
/// </summary>
public class SkillCarrier : Entity
{
    //技能数据
    private SkillData skillData;
    public SkillData _skillData { get { return skillData; } }
    //技能模板
    private SkillTemplateBase skillTemplate;

    private float duration;
    private byte curAttackCount;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        skillData = userData as SkillData;
        if (skillData == null)
        {
            Log.Error("Bullet data is invalid.");
            return;
        }
        curAttackCount = 0;
        InitSkill();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }
    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        if (skillTemplate != null)
        {
            skillTemplate.OnDisable();
            skillTemplate = null;
        }
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (skillData == null)
            return;
        if (skillTemplate != null)
        {
            skillTemplate.OnUpdate(elapseSeconds, realElapseSeconds);
        }
        duration += elapseSeconds;
        if (duration >= skillData.Duration)
        {
            //显示时间到了
            MainGame.Entity.HideEntity(this);
        }
    }

    //初始化
    private void InitSkill()
    {
        //生成一个技能模板
        skillTemplate = MainGame.SkillComponent.CreateSkillTemplateBaseById(skillData.SkillTemplateId);
        if (skillTemplate == null) return;
        skillTemplate.OnInit(this, skillData);
    }

    // 攻击到敌人
    public void AttackEnemy(Entity target)
    {
        int attackCount = skillData.UsageCount;
        curAttackCount++;
        if (attackCount != -1 && curAttackCount >= attackCount)
        {
            MainGame.Entity.HideEntity(this);
        }
    }
}
