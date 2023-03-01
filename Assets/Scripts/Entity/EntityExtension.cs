using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;

public static class EntityExtension
{
    // 关于 EntityId 的约定：
    // 0 为无效
    // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
    // 负值用于本地生成的临时实体（如特效、FakeObject等）
    private static int s_SerialId = 0;
    public static int GenerateSerialId(this EntityComponent entityComponent)
    {
        return --s_SerialId;
    }

    public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
    {
        UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
        if (entity == null)
        {
            return null;
        }
        return (Entity)entity.Logic;
    }
    public static void HideEntity(this EntityComponent entityComponent, Entity entity)
    {
        entityComponent.HideEntity(entity.Entity);
    }

    public static void ShowPlayer(this EntityComponent entityComponent, AoiUnitData data)
    {
        entityComponent.ShowEntity(typeof(RoleAoiUnit), "MoveUnit", Constant.AssetPriority.PlayerAsset, data);
    }

    public static void ShowMonster(this EntityComponent entityComponent, AoiUnitData data)
    {
        entityComponent.ShowEntity(typeof(MonsterUnit), "MoveUnit", Constant.AssetPriority.MonsterAsset, data);
    }

    public static void ShowSkill(this EntityComponent entityComponent, SkillData data)
    {
        entityComponent.ShowEntity(typeof(SkillCarrier), "Effect", Constant.AssetPriority.SkillAsset, data);
    }

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="entityComponent"></param>
    /// <param name="logicType">实体类型</param>
    /// <param name="entityGroup">实体分组池子</param>
    /// <param name="priority"></param>
    /// <param name="data">实体数组，必须在实体资源配置表里添加对应的实体，必须赋予实体id</param>
    private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
    {
        if (data == null)
        {
            Log.Warning("Data is invalid.");
            return;
        }

        IDataTable<DREntity> dtEntity = MainGame.DataTable.GetDataTable<DREntity>();
        DREntity drEntity = dtEntity.GetDataRow(data.EntityId);
        if (drEntity == null)
        {
            Log.Warning("Can not load entity id '{0}' from data table.", data.EntityId.ToString());
            return;
        }

        entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, data);
    }
}
