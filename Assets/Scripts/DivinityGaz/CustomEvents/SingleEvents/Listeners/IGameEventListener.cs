using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CustomEvents.Single
{
    public interface IGameEventListener<T>
    {
        void OnEventInvoked (T item);
    }
}

