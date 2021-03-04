using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.CustomEvents.Double
{
    public interface IDoubleGameEventListener<T, R>
    {
        void OnEventInvoked (T firstItem, R secondItem);
    }
}

