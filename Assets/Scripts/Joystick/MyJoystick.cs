using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EJoystickType
{
    Fixed,      //�̶�ʽҡ��
    Floating,   //����ʽҡ��(���ݵ����Ļ��λ������ҡ�˿�����)
    Dynamic     //��̬ҡ��(ҡ�˿��Ա���̬��ק)
}

public class MyJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("ҡ������")]
    public EJoystickType EJoystickType = EJoystickType.Fixed;

    [Header("ҡ�˱���")]
    public RectTransform BgRect;

    [Header("ҡ���϶���ť")]
    public RectTransform HandleRect;

    [Header("�������ҡ������")]
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
    // ҡ�˴���״̬
    private bool IsJoystickShow = false;
    // ���������¼
    private int IsTouchJoystickCount = 0;
    // �϶�״̬
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
        // ���¼���
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

        Vector2 position = Camera.main.WorldToScreenPoint(BgRect.position);//��ui�����е�backgroundӳ�䵽��Ļ�е�ʵ������
        Vector2 radius = (BgRect.sizeDelta - HandleRect.sizeDelta) / 2;
        input = (eventData.position - position) / (radius * mCanvas.scaleFactor);//����Ļ�еĴ����background�ľ���ӳ�䵽ui�ռ���ʵ�ʵľ���

        HandleInput(input.magnitude, input.normalized, radius);        //�������������
        HandleRect.anchoredPosition = input * radius;

        JoystickOnMove?.Invoke(input, eventData.position - position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouchJoystickCount--;
        if (IsTouchJoystickCount < 0)
        {
            // �ݴ�ɣ�����ɸ���
            IsTouchJoystickCount = 0;
        }
        Vector2 position = Camera.main.WorldToScreenPoint(BgRect.position);//��ui�����е�backgroundӳ�䵽��Ļ�е�ʵ������
        RunTouchUp(eventData.position - position);
    }

    private void RunTouchUp(Vector2 vPos)
    {
        // ��������0�����ʾ������ָ�ڲ�������ִ��ҡ�˽���
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mBaseRect, screenPosition, Camera.main, out localPoint);
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
                // �ݴ�ɣ�����ɸ���
                IsTouchJoystickCount = 0;
                return;
            }

            RunTouchUp(Vector2.zero);
        }
#else
            var vTouchCount = Input.touchCount;
            
            if(vTouchCount >= IsTouchJoystickCount)  return;
            
            // ��ǰ����ָ����С�ڰ��µ�������ʱ��
            IsTouchJoystickCount = vTouchCount;
            RunTouchUp(Vector2.zero);
#endif
    }
}
