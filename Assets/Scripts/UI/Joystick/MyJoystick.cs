using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EJoystickType
{
    Fixed,      //固定式摇杆
    Floating,   //浮动式摇杆(根据点击屏幕的位置生成摇杆控制器)
    Dynamic     //动态摇杆(摇杆可以被动态拖拽)
}

public class MyJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("摇杆类型")]
    public EJoystickType EJoystickType = EJoystickType.Fixed;

    public Camera uiCamera;

    [Header("摇杆背景")]
    public RectTransform BgRect;

    [Header("摇杆拖动按钮")]
    public RectTransform HandleRect;

    [Header("点击唤醒摇杆区域")]
    public RectTransform DragArea;

    public float MoveThreshold = 8f;

    private RectTransform mBaseRect;
    private Vector2 input = Vector2.zero;
    private Vector2 center = new Vector2(0.5f, 0.5f);
    private Vector2 fixedPosition = Vector2.zero;
    private Canvas mCanvas;
    private float deadZone = 0;

    public Action JoystickOnTouchStart;
    public Action<Vector2, Vector2> JoystickOnMove;
    public Action<Vector2> JoystickOnTouchUp;
    // 摇杆触发状态
    private bool IsJoystickShow = false;
    // 点击次数记录
    private int IsTouchJoystickCount = 0;
    // 拖动状态
    private bool IsDrag = false;

    private bool mIsOpenDragSkill = false;

    private void OnEnable()
    {
        ResetUI();
    }

    private void OnDisable()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        IsTouchJoystickCount = 0;
        IsJoystickShow = false;
        IsDrag = false;
        SetMode();
    }

    private void InitUI()
    {
        BgRect.pivot = center;
        HandleRect.anchorMin = center;
        HandleRect.anchorMax = center;
        HandleRect.pivot = center;
        HandleRect.anchoredPosition = Vector2.zero;
        fixedPosition = BgRect.anchoredPosition;
        SetMode();
    }

    public void SetData(RectTransform vBaseRect, Canvas vCanvas)
    {
        mBaseRect = vBaseRect;
        mCanvas = vCanvas;
        InitUI(); 
    }


    private void SetMode()
    {
        if (EJoystickType == EJoystickType.Fixed)
        {
            BgRect.anchoredPosition = fixedPosition;
            BgRect.gameObject.SetActive(true);
        }
        else
        {
            BgRect.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下计数
        IsTouchJoystickCount++;
        JoystickOnTouchStart?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDrag)
        {
            IsDrag = true;
            BgRect.gameObject.SetActive(true);

            if (EJoystickType != EJoystickType.Fixed)
            {
                BgRect.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
                BgRect.gameObject.SetActive(true);
            }
        }

        Vector2 position = uiCamera.WorldToScreenPoint(BgRect.position);//将ui坐标中的background映射到屏幕中的实际坐标
        Vector2 radius = (BgRect.sizeDelta - HandleRect.sizeDelta) / 2;
        input = (eventData.position - position) / (radius * mCanvas.scaleFactor);//将屏幕中的触点和background的距离映射到ui空间下实际的距离

        HandleInput(input.magnitude, input.normalized, radius);        //对输入进行限制
        HandleRect.anchoredPosition = input * radius;

        JoystickOnMove?.Invoke(input, eventData.position - position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouchJoystickCount--;
        if (IsTouchJoystickCount < 0)
        {
            // 容错吧，别减成负的
            IsTouchJoystickCount = 0;
        }
        Vector2 position = uiCamera.WorldToScreenPoint(BgRect.position);//将ui坐标中的background映射到屏幕中的实际坐标
        RunTouchUp(eventData.position - position);
    }

    private void RunTouchUp(Vector2 vPos)
    {
        // 计数不归0，则表示还有手指在操作，不执行摇杆结束
        if (IsTouchJoystickCount > 0)
        {
            return;
        }


        if (EJoystickType != EJoystickType.Fixed)
        {
            BgRect.gameObject.SetActive(false);
        }
        input = Vector2.zero;
        HandleRect.anchoredPosition = Vector2.zero;

        if (IsDrag)
        {
            JoystickOnTouchUp?.Invoke(vPos);
        }
        else
        {
            JoystickOnTouchUp?.Invoke(Vector2.zero);
        }

        IsDrag = false;

    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
    {
        if (EJoystickType == EJoystickType.Dynamic && magnitude > MoveThreshold)
        {
            Vector2 difference = normalised * (magnitude - MoveThreshold) * radius;
            BgRect.anchoredPosition += difference;
        }
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }

    private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mBaseRect, screenPosition, uiCamera, out localPoint);
        return localPoint;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButton(0))
        {
            IsTouchJoystickCount--;
            if (IsTouchJoystickCount < 0)
            {
                // 容错吧，别减成负的
                IsTouchJoystickCount = 0;
                return;
            }

            RunTouchUp(Vector2.zero);
        }
#else
            var vTouchCount = Input.touchCount;
            
            if(vTouchCount >= IsTouchJoystickCount)  return;
            
            // 当前的手指数量小于按下的数量的时候
            IsTouchJoystickCount = vTouchCount;
            RunTouchUp(Vector2.zero);
#endif
    }
}
