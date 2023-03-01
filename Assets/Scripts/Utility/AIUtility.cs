using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtility
{

    /// <summary>
    /// 碰撞双方
    /// </summary>
    /// <param name="entity">自身</param>
    /// <param name="other">碰撞对象</param>
    public static void ExecuteCollision(AoiUnit entity, Entity other)
    {
        if (entity == null || other == null)
        {
            return;
        }

        AoiUnit target = other as AoiUnit;
        if (target != null)
        {
            AoiUnitData entityData = entity._unitData;
            AoiUnitData targetData = target._unitData;
            if (GetTwoUnitRelation(entityData.CampType, entityData.CampType) == EnumType.RelationType.Friendly)
            {
                return;
            }

            //敌人对自身造成的伤害
            int entityDamageHP = CalculateDamageHp(targetData.GetSelfAttack(false), entityData.Defense);
            //自身对敌人造成的伤害
            int targetDamageHP = CalculateDamageHp(entityData.GetSelfAttack(false), targetData.Defense);

            if (entityDamageHP > 0)
            {
                entity.ApplyDamage(target, entityDamageHP);
                entity.AttackEnemy();
            }
            if (targetDamageHP > 0)
            {
                target.ApplyDamage(entity, targetDamageHP);
                target.AttackEnemy();
            }
            return;
        }

        SkillCarrier carrier = other as SkillCarrier;
        if (carrier != null)
        {
            AoiUnitData entityData = entity._unitData;
            SkillData skillData = carrier._skillData;
            if (GetTwoUnitRelation(entityData.CampType, skillData.campType) == EnumType.RelationType.Friendly)
            {
                return;
            }
            //敌人对自身造成的伤害
            int entityDamageHP = CalculateDamageHp(skillData.AttackValue, entityData.Defense);

            if (entityDamageHP > 0)
            {
                entity.ApplyDamage(carrier, entityDamageHP);
                carrier.AttackEnemy(target);
            }
            return;
        }
    }

    /// <summary>
    /// 计算伤害值
    /// </summary>
    /// <param name="attack">自身攻击力</param>
    /// <param name="defense">敌人防御力</param>
    /// <returns></returns>
    public static int CalculateDamageHp(int attack, int defense)
    {
        if (attack <= 0)
        {
            return 0;
        }
        return attack - defense;
    }

    /// <summary>
    /// 获取敌对的敌人layerMask
    /// </summary>
    /// <param name="type">自身阵营</param>
    public static int GetHostilityLayerMask(EnumType.CampType type)
    {
        if (type == EnumType.CampType.Player)
        {
            return 1 << 7;
        }
        else if (type == EnumType.CampType.Enemy)
        {
            return 1 << 6;
        }
        return ~(1 << 0);
    }

    /// <summary>
    /// 获取双方关系  友方还是敌对
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    public static EnumType.RelationType GetTwoUnitRelation(EnumType.CampType first, EnumType.CampType second)
    {
        if (first == second)
            return EnumType.RelationType.Friendly;
        if (first == EnumType.CampType.Player && second == EnumType.CampType.Enemy)
            return EnumType.RelationType.Hostile;
        if (first == EnumType.CampType.Enemy && second == EnumType.CampType.Player)
            return EnumType.RelationType.Hostile;
        return EnumType.RelationType.Unknown;
    }
}
