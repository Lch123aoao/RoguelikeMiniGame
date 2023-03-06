using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillTemplateBase
{
    void OnShow(Transform unit, Transform carrier, SkillData data);
    void OnUpdate(float elapseSeconds, float realElapseSeconds);
    void OnDisable();
}
