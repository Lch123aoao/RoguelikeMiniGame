using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能播放器（单个unit的技能管理器）
/// </summary>
public class SkillDeployer
{
    private List<SkillData> _skillDatas;
    private Dictionary<string, string> _coroutineDic;

    private float _elapseSeconds;

    private TimerComponent timerComponent;

    private AoiUnit mUnit;

    public void OnInit(AoiUnit unit, List<SkillData> skillDatas)
    {
        mUnit = unit;
        _skillDatas = skillDatas;
        timerComponent = MainGame.TimerComponent;
    }
    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        _elapseSeconds += elapseSeconds;
        if (_elapseSeconds >= 1)
        {
            _elapseSeconds = 0;
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
                timerComponent.UnSchedule(item.Value);
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
            GenerateSkill(data, 1);
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
            GenerateSkill(data, 0);
        }
    }
    /// <summary>
    /// 生成技能
    /// </summary>
    /// <param name="data">技能数据</param>
    /// <param name="isAuto">0:使用作用自身的技能（被动释放，被敌人撞到）</param>
    /// <param name="isAuto">1:释放技能,需要生成技能实体（主动释放，按照冷却释放）</param>
    private void GenerateSkill(SkillData data, byte isAuto)
    {
        //获取配置数据,创建技能预制体
        if (isAuto == 1)
        {
            data.remainCreateCD = data.SkillCD;
            data.deployer = mUnit.transform;
            data.Id = MainGame.Entity.GenerateSerialId();
            MainGame.Entity.ShowSkill(data);
        }
        else
        {
            data.remainAttackCD = data.AttackCD;
        }
        //开启技能冷却
        if (_coroutineDic == null)
        {
            _coroutineDic = new Dictionary<string, string>();
        }
        var key = data.SkillId + "Skill" + isAuto + data.Id;
        CoolingTimeDown(data, isAuto, key);
        if (!_coroutineDic.ContainsKey(key))
            _coroutineDic.Add(key, key);
    }
    //生成技能进入冷却
    private void CoolingTimeDown(SkillData data, byte isAuto, string key)
    {
        if (isAuto == 1)
        {
            data.remainCreateCD -= 1;
            if (data.remainCreateCD <= 0)
            {
                if (_coroutineDic.ContainsKey(key))
                    _coroutineDic.Remove(key);
                timerComponent.UnSchedule(key);
                return;
            }
        }
        else
        {
            data.remainAttackCD -= 1;
            if (data.remainAttackCD <= 0)
            {
                if (_coroutineDic.ContainsKey(key))
                    _coroutineDic.Remove(key);
                timerComponent.UnSchedule(key);
                return;
            }
        }
        timerComponent.Schedule(() => { CoolingTimeDown(data, isAuto, key); }, 1, key);
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
