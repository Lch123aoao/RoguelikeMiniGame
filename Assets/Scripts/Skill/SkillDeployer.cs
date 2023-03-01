using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能播放器（单个unit的技能管理器）
/// </summary>
public class SkillDeployer 
{
    private List<SkillData> _skillDatas;
    private Dictionary<string, Coroutine> _coroutineDic;

    private float _elapseSeconds;

    public void OnInit(AoiUnit unit, List<SkillData> skillDatas)
    {
        _skillDatas = skillDatas;
    }
    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        _elapseSeconds += elapseSeconds;
        if (_elapseSeconds >= 1)
        {
            DeploySkill();
        }
    }
    public void OnDispose()
    {
        if (_skillDatas != null)
        {
            _skillDatas.Clear();
            _skillDatas = null;
        }
        if (_coroutineDic != null)
        {
            foreach (var item in _coroutineDic)
            {
                // StopCoroutine(item.Value);
            }
            _coroutineDic.Clear();
            _coroutineDic = null;
        }
    }

    //释放技能（主动释放，按照冷却释放）
    private void DeploySkill()
    {
        if (_skillDatas == null)
            return;
        for (int i = 0; i < _skillDatas.Count; i++)
        {
            SkillData data = _skillDatas[i];
            if (data.EntityId == -1 || data.remainCreateCD > 0)
            {
                continue;
            }
            GenerateSkill(data, true);
        }
    }

    //使用作用自身的技能（被动释放，被敌人撞到）
    public void ReleaseActingOnItselfSkill()
    {
        if (_skillDatas == null)
            return;
        for (int i = 0; i < _skillDatas.Count; i++)
        {
            SkillData data = _skillDatas[i];
            if (data.EntityId != -1 || data.remainCreateCD > 0)
            {
                continue;
            }
            GenerateSkill(data, false);
        }
    }
    //生成技能
    private void GenerateSkill(SkillData data, bool isAuto)
    {
        //获取配置数据,创建技能预制体
        if (isAuto)
        {
            MainGame.Entity.ShowSkill(data);
        }
        //开启技能冷却
        if (_coroutineDic == null)
        {
            _coroutineDic = new Dictionary<string, Coroutine>();
        }

        // var coroutine = StartCoroutine(CoolingTimeDown(data, isAuto));
        // var key = data.SkillId + "Skill" + data.Id;
        // if (_coroutineDic.ContainsKey(key))
        //     _coroutineDic.Add(key, coroutine);
        // else
        //     _coroutineDic[key] = coroutine;
    }
    //生成技能进入冷却
    private IEnumerator CoolingTimeDown(SkillData data, bool isAuto)
    {
        if (isAuto)
        {
            data.remainCreateCD = data.SkillCD;
            while (data.remainCreateCD > 0)
            {
                yield return new WaitForSeconds(1f);
                data.remainCreateCD -= 1;
            }
        }
        else
        {
            data.remainAttackCD = data.AttackCD;
            while (data.remainAttackCD > 0)
            {
                yield return new WaitForSeconds(1f);
                data.remainAttackCD -= 1;
            }
        }
    }

    //增加技能
    public void AddSkill(SkillData data)
    {
        for (int i = 0; i < _skillDatas.Count; i++)
        {
            if (_skillDatas[i].SkillId == data.SkillId)
            {
                return;
            }
        }
        _skillDatas.Add(data);
    }
    //删除技能
    public void RemoveSkill(SkillData data)
    {
        for (int i = 0; i < _skillDatas.Count; i++)
        {
            if (_skillDatas[i].SkillId == data.SkillId)
            {
                _skillDatas.RemoveAt(i);
                break;
            }
        }
    }
}
