using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IEventInfo { }

// Event class that contains 0 args
public class EventInfo : IEventInfo
{
    public UnityAction action;
}

// Event class that contains 1 args
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;
}

public class EventInfo<T, X> : IEventInfo
{
    public UnityAction<T, X> action;
}

public class EventCenter : MonoBehaviour
{
    private static EventCenter instance;
    public static EventCenter Instance
    {
        get
        {
            if (instance == null) instance = new EventCenter();
            return instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (this != instance) //Preserves only the first object created.
            Destroy(this);
    }

    void OnDestroy()
    {
        if (this == instance) // If a duplicate object is destroyed, it does not free the first instance created.
            instance = null;
    }

    private Dictionary<string, IEventInfo> EventDict = new Dictionary<string, IEventInfo>();

    // Add event with 0 args
    public void AddEventListener(string eventName, UnityAction action)
    {
        // If the dictionary contains the event, add the action to the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo).action += action;
        // Else, create a new event
        else
            EventDict.Add(eventName, new EventInfo() { action = action });
    }

    // Add event with 1 args
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        // If the dictionary contains the event, add the action to the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T>).action += action;
        // Else, create a new event
        else
            EventDict.Add(eventName, new EventInfo<T>() { action = action });
    }

    // Add event with 2 args
    public void AddEventListener<T, X>(string eventName, UnityAction<T, X> action)
    {
        // If the dictionary contains the event, add the action to the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T, X>).action += action;
        // Else, create a new event
        else
            EventDict.Add(eventName, new EventInfo<T, X>() { action = action });
    }

    // Remove event with 0 args
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        // If the dictionary contains the event, remove the action from the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo).action -= action;
    }

    // Remove event with 1 args
    public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        // If the dictionary contains the event, remove the action from the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T>).action -= action;
    }

    // Remove event with 2 args
    public void RemoveEventListener<T, X>(string eventName, UnityAction<T, X> action)
    {
        // If the dictionary contains the event, remove the action from the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T, X>).action -= action;
    }

    // Trigger event with 0 args
    public void TriggerEventListener(string eventName)
    {
        // If the dictionary contains the event, broadcast the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo).action?.Invoke();
    }

    // Trigger event with 1 args
    public void TriggerEventListener<T>(string eventName, T param)
    {
        // If the dictionary contains the event, broadcast the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T>).action?.Invoke(param);
    }

    // Trigger event with 2 args
    public void TriggerEventListener<T, X>(string eventName, T param1, X param2)
    {
        // If the dictionary contains the event, broadcast the event
        if (EventDict.ContainsKey(eventName))
            (EventDict[eventName] as EventInfo<T, X>).action?.Invoke(param1, param2);
    }

    // Clear event
    public void ClearEventListener()
    {
        EventDict.Clear();
    }
}
