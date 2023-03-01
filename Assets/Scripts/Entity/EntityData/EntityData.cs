//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

[Serializable]
public abstract class EntityData
{
    [SerializeField]
    private int m_Id = 0;

    [SerializeField]
    private int m_ConfigId = 0;

    [SerializeField]
    private int m_EntityId = 0;

    [SerializeField]
    private Vector3 m_Position = Vector3.zero;

    [SerializeField]
    private Vector3 m_Scale = Vector3.one;

    public EntityData(int entityId, int typeId)
    {
        m_Id = entityId;
        m_ConfigId = typeId;
    }

    /// <summary>
    /// 实体逻辑编号。
    /// </summary>
    public int Id
    {
        get
        {
            return m_Id;
        }
    }

    /// <summary>
    /// 实体资源id编号。
    /// </summary>
    public int EntityId
    {
        get
        {
            return m_EntityId;
        }
        set
        {
            m_EntityId = value;
        }
    }

    /// <summary>
    /// 实体位置。
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return m_Position;
        }
        set
        {
            m_Position = value;
        }
    }

    /// <summary>
    /// 实体朝向。
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            return m_Scale;
        }
        set
        {
            m_Scale = value;
        }
    }
}
