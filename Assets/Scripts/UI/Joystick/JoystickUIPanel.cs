using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUIPanel : MonoBehaviour
{
    public RectTransform mBaseRect;
    public Canvas mBaseCanvas;

    public MyJoystick MyJoystick;

    // Start is called before the first frame update
    void Start()
    {
        InitETCJoystick();
        //绑定按钮事件
        InitJoystickBind();
    }

    public void OnDestroy()
    {
        InitJoystickUnBind();
    }

    public void InitETCJoystick()
    {
        MyJoystick.SetData(mBaseRect, mBaseCanvas);
    }

    private void InitJoystickBind()
    {
        MyJoystick.JoystickOnTouchStart += JoystickOnTouchStart;
        MyJoystick.JoystickOnMove += JoystickOnMove;
        MyJoystick.JoystickOnTouchUp += JoystickOnTouchUp;
    }

    private void InitJoystickUnBind()
    {
        MyJoystick.JoystickOnTouchStart -= JoystickOnTouchStart;
        MyJoystick.JoystickOnMove -= JoystickOnMove;
        MyJoystick.JoystickOnTouchUp -= JoystickOnTouchUp;
    }

    private void JoystickOnTouchStart()
    {
        //通知摇杆移动
        TemporaryBattleManager.Instance.isPlayerRun = true;
        Debug.LogError("摇杆按下");

    }

    //移动摇杆
    private void JoystickOnMove(Vector2 vVector2, Vector2 vDragPos)
    {
        TemporaryBattleManager.Instance.playerPos = vVector2;
        //移动
        Debug.LogError("vVector2: "+vVector2.ToString() + "   vDragPos: "+vDragPos);
    }
    
    
    /// <summary>
    /// 抬起
    /// </summary>
    /// <param name="vVec2"></param>
    private void JoystickOnTouchUp(Vector2 vVec2)
    {
        //通知摇杆停止移动
        TemporaryBattleManager.Instance.isPlayerRun = false;
        Debug.LogError("摇杆抬起");
    }
}

