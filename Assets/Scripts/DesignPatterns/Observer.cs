using System;
using System.Collections.Generic;
using UnityEngine;

public class Observer<T>
{
    private readonly List<Action<T>> listeners = new List<Action<T>>();
    public void Clear()
    {
        listeners.Clear();
    }
    public void Raise(T value)
    {
        foreach (var listener in listeners)
            listener?.Invoke(value);
    }
    public void Register(Action<T> listener)
    {
        if (listener != null && !listeners.Contains(listener))
            listeners.Add(listener);
    }
    public void Unregister(Action<T> listener)
    {
        if (listener != null && listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
