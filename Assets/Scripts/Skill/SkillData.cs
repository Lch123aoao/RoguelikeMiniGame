using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using Table;
using UnityEngine;
using UnityGameFramework.Runtime;

//技能数据
public class SkillData : EntityData
{
    public SkillData(int entityId, int skillId, int signId, EnumType.CampType type)
           : base(entityId, skillId)
    {
        //处理一下技能数据吧
        IDataTable<DRSkillData> configs = MainGame.DataTable.GetDataTable<DRSkillData>();
        DRSkillData data = configs.GetDataRow(skillId);
        if (data == null)
        {
            Log.Error("Can not find unit id '{0}' from skilldata table.", skillId);
            return;
        }
        SkillId = skillId;
        SignId = signId;
        EntityId = data.EntityId;
        SkillTemplateId = data.SkillTemplateId;
        TowardType = data.TowardType;
        AttackValue = data.AttackValue;
        SkillCD = data.SkillCD;
        AttackCD = data.AttackCD;
        BaseMoveSpeed = data.BaseMoveSpeed;
        UsageCount = data.UsageCount;
        Duration = data.Duration;

        campType = type;
    }

    // ---------------------- 配置数据常量 ----------------------
    //技能ID
    public int SkillId;
    //标志id
    public int SignId;
    //技能模板id
    public int SkillTemplateId;
    //朝向类型
    public int TowardType;
    //攻击力
    public int AttackValue;
    //技能生成CD
    public int SkillCD;
    //攻击间隔/CD
    public int AttackCD;
    //基本移动速度
    public int BaseMoveSpeed;
    //作用次数
    public int UsageCount;
    //持续时间
    public int Duration;


    // ---------------------- 辅助计算变量 ----------------------
    //技能剩余生成CD
    public int remainCreateCD;
    //技能剩余攻击间隔CD
    public int remainAttackCD;
    //释放此技能的阵营：玩家、怪物。。。
    public EnumType.CampType campType;
    //释放此技能的单位
    public Transform deployer;

    //技能作用目标tag
    // public string[] attackTargetTags;
}
