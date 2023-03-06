using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 负责受击，需要受伤的单位都需要挂这个脚本
/// </summary>
public class AoiUnit : Entity
{
    [SerializeField]
    protected AoiUnitData unitData = null;
    public AoiUnitData _unitData
    {
        get { return unitData; }
    }
    public bool IsDead
    {
        get
        {
            return unitData.HP <= 0;
        }
    }
    //技能控制器
    protected SkillDeployer skillDeployer;

    /// <summary>
    /// 攻击到敌人了
    /// </summary>
    public void AttackEnemy()
    {
        if (skillDeployer != null)
            skillDeployer.ReleaseActingOnItselfSkill();
    }
    public void ApplyDamage(Entity attacker, int damageHP)
    {
        unitData.HP -= damageHP;
        if (unitData.HP <= 0)
        {
            OnDead(attacker);
        }
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        unitData = userData as AoiUnitData;

        Init_Skill();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (skillDeployer != null)
        {
            skillDeployer.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        if (skillDeployer != null)
        {
            skillDeployer.OnDispose();
            skillDeployer = null;
        }
    }

    protected virtual void OnDead(Entity attacker)
    {
        MainGame.Entity.HideEntity(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity == null)
        {
            return;
        }

        if (entity is AoiUnit && entity.Id >= Id)
        {
            // 碰撞事件由 Id 小的一方处理，避免重复处理
            return;
        }

        AIUtility.ExecuteCollision(this, entity);
    }

    //初始化技能
    private void Init_Skill()
    {
        skillDeployer = MainGame.SkillComponent.CreateSkillDeployer();
        skillDeployer.OnInit(this, unitData.SkillList);
    }
}
