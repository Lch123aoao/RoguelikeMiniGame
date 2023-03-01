using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : AoiUnit
{
    private Transform player;
    private int monsterId;
    private object data;
    private float moveSpeed = 3;
    private bool isCanAttack;
    private float acttackCoolingTime = 3;//普通攻击冷却时间  读表  TODO
    public void Init(int monsterId, Transform player, object data)
    {
        this.player = player;
        this.monsterId = monsterId;
        this.data = data;
        isCanAttack = true;
        SpawnPosition();
    }
    public void OnDispose()
    {
        
    }

    private void SpawnPosition()
    {
        Vector2 pos = (Vector2)player.position + Random.insideUnitCircle * 50;
        transform.position = pos;
    }

    //攻击玩家，玩家收到的伤害
    public int ReturnAttackValue()
    {
        if (unitData.HP <= 0) return 0;
        if (!isCanAttack) return 0;
        isCanAttack = false;
       
        return 5;
    }
    //攻击冷却
    private void AttackCooling()
    {
        isCanAttack = true;
    }

    public void Update()
    {
        if (unitData.HP <= 0) return;
        Vector2 dir = (player.position - transform.position).normalized;
        transform.position = (Vector2)transform.position + dir * moveSpeed * Time.deltaTime;
    }
}
