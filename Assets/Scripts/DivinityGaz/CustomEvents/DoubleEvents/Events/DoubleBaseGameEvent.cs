using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CustomEvents.Double
{
    public abstract class DoubleBaseGameEvent<T, R> : ScriptableObject
    {
        private readonly List<IDoubleGameEventListener<T, R>> eventListeners = new List<IDoubleGameEventListener<T, R>>();

        public void Invoke (T firstItem, R secondItem)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventInvoked(firstItem, secondItem);
            }
        }

        public void RegisterListener(IDoubleGameEventListener<T, R> eventListener)
        {
            if (!eventListeners.Contains(eventListener))
            {
                eventListeners.Add(eventListener);
            }
        }

        public void UnregisterListener (IDoubleGameEventListener<T, R> eventListener)
        {
            if (!eventListeners.Contains(eventListener))
            {
                eventListeners.Remove(eventListener);
            }
        }
    }
}

