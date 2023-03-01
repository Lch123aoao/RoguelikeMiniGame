using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 负责受击，需要受伤的单位都需要挂这个脚本
/// </summary>
public class TargetableObject : Entity
{
    [SerializeField]
    private AoiUnitData unitData = null;
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
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        unitData = userData as AoiUnitData;
        if (unitData == null)
        {
            Log.Error("unit data is invalid.");
            return;
        }
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);


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

        if (entity is TargetableObject && entity.Id >= Id)
        {
            // 碰撞事件由 Id 小的一方处理，避免重复处理
            return;
        }

        // AIUtility.ExecuteCollision(this, entity);
    }
}
