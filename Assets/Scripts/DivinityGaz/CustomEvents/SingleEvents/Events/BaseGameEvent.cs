using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CustomEvents.Single
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Invoke (T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventInvoked(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> eventListener)
        {
            if (!eventListeners.Contains(eventListener))
            {
                eventListeners.Add(eventListener);
            }
        }

        public void UnregisterListener (IGameEventListener<T> eventListener)
        {
            if (!eventListeners.Contains(eventListener))
            {
                eventListeners.Remove(eventListener);
            }
        }
    }
}

