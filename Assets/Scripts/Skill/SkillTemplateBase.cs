using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillTemplateBase
{
    void OnInit(SkillCarrier carrier, SkillData data);
    void OnUpdate(float elapseSeconds, float realElapseSeconds);
    void OnDisable();
}
