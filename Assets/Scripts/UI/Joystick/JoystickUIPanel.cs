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


    }

    //移动摇杆
    private void JoystickOnMove(Vector2 vVector2, Vector2 vDragPos)
    {
        
    }
    
    private void JoystickOnTouchUp(Vector2 vVec2)
    {
        
    }
}

