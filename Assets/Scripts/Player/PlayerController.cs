using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色控制器
public class PlayerController : MonoBehaviour
{
    private RoleAoiUnit roleAoiUnit;
    private Rigidbody2D rigibody;
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        //移动速度
        float curSpeed = 10f;
        Vector2 targetPos = this.transform.position + (new Vector3(moveX, moveY, 0) * Time.deltaTime * curSpeed);
        rigibody.MovePosition(targetPos);
    }
}
