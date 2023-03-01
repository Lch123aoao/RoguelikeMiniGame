using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

public class LevelGame : GameBase
{
    public override void Initialize(int levelId)
    {
        base.Initialize(levelId);
        //显示玩家
        MainGame.Entity.ShowPlayer(new AoiUnitData(MainGame.Entity.GenerateSerialId(), 10000, EnumType.CampType.Player)
        {
            Position = new Vector3(0, 0, -1),
        });
    }

    public override void InitOtherGameInfo()
    {
        base.InitOtherGameInfo();
        //初始化地图
        MainGame.MapComponent.InitMap(playerUnit.transform);
        //初始化场景怪物信息  TODO
        //初始化技能信息
        MainGame.SkillComponent.Init_Skill();
    }

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);
    }

    public override void Shutdown()
    {
        base.Shutdown();
    }
}
