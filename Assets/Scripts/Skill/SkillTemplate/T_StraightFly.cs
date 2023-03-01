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

    private int towardType;
    private Vector2 startDir;//起始发射方向

    private Transform _carrier;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="carrier">技能载体</param>
    /// <param name="data">技能数据</param>
    public void OnInit(SkillCarrier carrier, SkillData data)
    {
        _carrier = carrier.transform;
        skillData = data;
        if (skillData.TowardType == (int)EnumType.TowardType.launcherForward)
        {
            startDir = carrier.transform.forward;
        }
        else if (skillData.TowardType == (int)EnumType.TowardType.towardNearestEnemy)
        {
            var target = FindNearestEnemy(carrier.transform.position, 10, skillData.campType, 0);
            if (target == null)
                startDir = carrier.transform.forward;
            else
                startDir = (target.position - carrier.transform.position).normalized;
        }
    }

    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        if (_carrier == null || skillData == null)
            return;

        _carrier.Translate(startDir * skillData.BaseMoveSpeed * elapseSeconds, Space.World);
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
