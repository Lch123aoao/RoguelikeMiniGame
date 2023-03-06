using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能模板：直线飞行
/// </summary>
public class T_StraightFly : SkillTemplateBase
{
    private SkillData skillData;

    //技能载体
    private Transform _carrier;
    //单位实体
    private Transform _unit;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="carrier">技能载体</param>
    /// <param name="data">技能数据</param>
    public void OnShow(Transform unit, Transform carrier, SkillData data)
    {
        _carrier = carrier.transform;
        _unit = unit;
        skillData = data;
        if (skillData.TowardType == (int)EnumType.TowardType.launcherForward)
        {
            carrier.eulerAngles = unit.eulerAngles;
        }
        else if (skillData.TowardType == (int)EnumType.TowardType.towardNearestEnemy)
        {
            float z = 0;
            var target = FindNearestEnemy(carrier.transform.position, 10, skillData.campType, 0);
            if (target != null)
            {
                var startDir = (target.position - carrier.transform.position).normalized;
                if (startDir.x > 0)
                    z = -Vector3.Angle(Vector3.up, startDir);
                else
                    z = Vector3.Angle(Vector3.up, startDir);
                carrier.eulerAngles = new Vector3(0, 0, z);
            }
            else
            {
                carrier.eulerAngles = unit.eulerAngles;
            }
        }
        else
        {
            carrier.eulerAngles = unit.eulerAngles;
        }
    }

    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        if (_carrier == null || skillData == null)
            return;

        _carrier.Translate(Vector2.up * skillData.BaseMoveSpeed * elapseSeconds, Space.World);
    }

    public void OnDisable()
    {

    }

    /// <summary>
    /// 寻敌,此方法要脱离出此类  TODO
    /// </summary>
    /// <param name="originPos">起始点</param>
    /// <param name="radius">搜索半径</param>
    /// <param name="layer">碰撞层级</param>
    /// <param name="mode">搜索模式（0：圆形搜索；1：扇形搜索:）</param>
    public Transform FindNearestEnemy(Vector3 originPos, float radius, EnumType.CampType type, int mode)
    {
        Collider2D[] results = null;

        var layerMask = AIUtility.GetHostilityLayerMask(type);
        //圆形
        int count = Physics2D.OverlapCircleNonAlloc(originPos, radius, results, layerMask);
        if (count <= 0)
            return null;
        float lastDis = 0;
        Transform target = null;
        for (int i = 0; i < results.Length; i++)
        {
            float dis = Vector2.SqrMagnitude(originPos - results[i].transform.position);
            if (dis < lastDis)
            {
                target = results[i].transform;
                lastDis = dis;
            }
        }
        return target;
    }

}
