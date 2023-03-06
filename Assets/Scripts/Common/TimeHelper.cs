using System;
using System.Collections.Generic;
using UnityEngine;

//与逻辑循环同步的Scheduler
public class LogicScheduler
{
    //逻辑帧对象
    class ScheduleUnit
    {
        public int endFrameIndex; //结束帧
        public Action callback; //事件
        public string key; //帧事件ID标识
    }
    //帧事件队列
    private List<ScheduleUnit> mScheduleList = new List<ScheduleUnit>();
    //当前帧
    private int currLogicLoopIndex;
    //Controller单例子
    private TimerComponent timerComponent;
    private ScheduleUnit tempScheduleUnit;
    //帧数逻辑循环
    public void OnLoginLoop(int logicFrameIndex = 0)
    {
        if (timerComponent == null)
        {
            timerComponent = MainGame.TimerComponent;
        }
        currLogicLoopIndex = timerComponent.logicLoopIndex;
        //达到结束帧时候触发事件
        for (int i = 0; i < mScheduleList.Count;)
        {
            tempScheduleUnit = mScheduleList[i];

            if (currLogicLoopIndex >= tempScheduleUnit.endFrameIndex)
            {
                //Debug.LogError(tempScheduleUnit.endFrameIndex+"@@@@@@@@@@@"+currLogicLoopIndex);    
                if (tempScheduleUnit.callback != null)
                {
                    tempScheduleUnit.callback();
                }

                mScheduleList.Remove(tempScheduleUnit);
            }
            else
            {
                i++;
            }
        }
    }
    /// <summary>
    /// //注册帧事件
    /// </summary>
    /// <param name="callback">事件回调</param>
    /// <param name="delay">延时 对实际时间作用范围 0.1- ~     小于0.1 即等于 1/60 帧</param>
    /// <param name="key">如果为空 是单独一帧  key 相同 进行叠加事件</param>
    public void Schedule(Action callback, float delay, string key)
    {
        if (callback == null)
        {
            return;
        }
        ScheduleUnit unit = null;
        if (key != "")
        {
            for (int i = 0; i < mScheduleList.Count; i++)
            {
                if (mScheduleList[i].key == key)
                {
                    unit = mScheduleList[i];
                    break;
                }
            }
        }
        if (unit == null)
        {
            unit = new ScheduleUnit();

        }
        unit.endFrameIndex = timerComponent.logicLoopIndex + TimeHelper.TotalCaculateLogicFrameCount(delay);
        unit.callback = callback;
        unit.key = key;
        mScheduleList.Add(unit);
    }

    //根据key 取消帧事件
    public void UnSchedule(string key)
    {
        if (key == "")
        {
            return;
        }

        int count = mScheduleList.Count;
        for (int i = 0; i < count; i++)
        {
            var unit = mScheduleList[i];
            if (unit.key == key)
            {
                mScheduleList.RemoveAt(i);
                break;
            }
        }
    }
}
public struct TimerCounter
{
    string logTag;
    DateTime orginTime;
    float threshold;

    public TimerCounter(string logTag, float threshold = 0.00f)
    {
        this.logTag = logTag;
        this.orginTime = TimeHelper.GetCurrentTime();
        this.threshold = threshold;
    }

    public void Log()
    {
        var currTime = TimeHelper.GetCurrentTime();

        var span = currTime - orginTime;

        var ms = span.TotalMilliseconds;
        //if (ms > this.threshold)
        //{
        Debug.LogWarningFormat("Invoke:{0} 耗时:{1}毫秒", logTag, ms);
        //}

    }

}
public class TimeHelper
{
    /// <summary>
    /// 基础默认为60帧
    /// </summary>
    private static int _gameIndex = 60; //当前帧率
    private const int _gameIndexMax = 60; //最大帧率
    private const int _gameIndexMin = 30; //最小帧率
    /// <summary>
    /// 获取游戏帧数 默认为60帧
    /// </summary>
    /// <value></value>
    public static int GameIndex
    {
        get
        {
            return _gameIndex;
        }
        set
        {
            _gameIndex = value;
        }
    }
    /// <summary>
    /// 动态设置帧率
    /// </summary>
    /// <param name="m_fps"></param>
    public static void SetGameIndexDynamic(float m_fps)
    {
        _gameIndex = Mathf.Clamp(Mathf.CeilToInt(m_fps), _gameIndexMin, _gameIndexMax);
    }

    //返回毫秒
    public static long GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (long)(ts.TotalMilliseconds);
    }
    //返回秒
    public static long GetTimeStampTicks()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (long)(ts.Ticks);

    }

    // //把时间换算成需要的逻辑循环次数
    // public static int CaculateLogicFrameCount(float duration)
    // {
    //     //float logicLoopInterval = MapController.instance.logicLoopInterval;
    //     return Mathf.CeilToInt(duration * GameController._instance.LogicLoopCountPersecond);
    // }
    // //把逻辑循环次数换算成时间
    // public static float CaculateTimeFromLogicFrameCount(int count)
    // {
    //     return count / GameController._instance.LogicLoopCountPersecond;
    // }

    public static int TotalCaculateLogicFrameCount(float duration)
    {
        //向上取整
        return Mathf.CeilToInt(duration * GameIndex);
    }

    //当前时间
    public static DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    //计算完成时间
    public static DateTime CaculateFinishTime(TimeSpan span)
    {
        return DateTime.Now + span;
    }

    //计算差距时间
    public static int CaculateGapTime(long span)
    {
        long gapTime = GetTimeStamp() - span;
        int time = (int)(gapTime / 1000);
        return time;
    }
    /// <summary>
    /// 时间戳转为C#格式时间
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTIme = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTIme);
        return dtStart.Add(toNow);
    }

    /// <summary>
    /// DateTime 转为时间戳
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ConvertDateTimeInt(DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return (long)(time - startTime).TotalSeconds;
    }
    public static DateTime ConverTicksToDateTime(long ticks)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return startTime.AddSeconds(ticks);
    }

    public static TimeSpan ConvertDateTimeToSpan(DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return (time - startTime);
    }

}