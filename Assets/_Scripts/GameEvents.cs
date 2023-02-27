using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameEvents
{
    public static UnityAction OnCardsDeal;

    public static UnityAction OnCardPlay;

    public static void RaiseCardsDeal()
    {
        OnCardsDeal?.Invoke();
    }

    public static void RaiseCardPlay()
    {
        OnCardPlay?.Invoke();
    }
}
    
