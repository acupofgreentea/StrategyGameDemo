﻿using System.Collections.Generic;
using UnityEngine;

public abstract class StateControllerBase<T, T1> : MonoBehaviour where T : System.Enum where T1 : StateBase
{
    protected T1 CurrentState { get; private set; }

    protected Dictionary<T, T1> stateDictionary;
    protected abstract void CreateDictionary();

    public void ChangeState(T type)
    {
        CurrentState = stateDictionary.GetValueOrDefault(type);
    }
}