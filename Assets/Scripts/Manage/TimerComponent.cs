using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class TimerComponent : GameFrameworkComponent
{
    public LogicScheduler mLogicScheduler = new LogicScheduler();
    private int mLogicLoopIndex = 0;
    public int logicLoopIndex
    {
        get
        {
            return mLogicLoopIndex;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        OnLocalLoop();
        mLogicLoopIndex++;
    }
    private void OnLocalLoop()
    {
        mLogicScheduler.OnLoginLoop(mLogicLoopIndex);
    }

    public void Schedule(Action callback, float delay, string key)
    {
        mLogicScheduler.Schedule(callback, delay, key);
    }
    public void UnSchedule(string key)
    {
        mLogicScheduler.UnSchedule(key);
    }
}
