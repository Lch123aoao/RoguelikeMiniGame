using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

//角色控制器
public class PlayerController : MonoBehaviour
{
    private RoleAoiUnit roleAoiUnit;
    private Rigidbody2D rigibody;

    public Vector2 Pos1;
    public  Vector3 pos3;
    public void Awake()
    {
        if (roleAoiUnit == null)
        {
            roleAoiUnit = this.GetComponent<RoleAoiUnit>();
        }

        if (!rigibody)
        {
            rigibody = this.GetComponent<Rigidbody2D>();
        }
    }

    public void FixedUpdate()
    {
        Debug.Log("摇杆状态  " + TemporaryBattleManager.Instance.isPlayerRun);
        if (TemporaryBattleManager.Instance.isPlayerRun)
        {
            // float moveX = Input.GetAxisRaw("Horizontal");
            // float moveY = Input.GetAxisRaw("Vertical");
            //移动速度
            Vector2 player = TemporaryBattleManager.Instance.playerPos;
            float curSpeed = 10f;
            Pos1 = player;
            Vector2 targetPos = this.transform.position + new Vector3(player.x,player.y) * Time.deltaTime * curSpeed;
            rigibody.MovePosition(targetPos);

        }
    }
}
