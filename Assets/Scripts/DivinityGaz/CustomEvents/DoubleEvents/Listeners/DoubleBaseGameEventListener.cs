using UnityEngine;
using UnityEngine.Events;

namespace DivinityGaz.CustomEvents.Double
{
    public abstract class DoubleBaseGameEventListener<T, R, E, UER> : MonoBehaviour,
        IDoubleGameEventListener<T, R> where E : DoubleBaseGameEvent<T, R> where UER : UnityEvent<T, R>
    {
        [SerializeField] private E gameEvent;

        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse;

        private void OnEnable ()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable ()
        {
            if (gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventInvoked (T firstItem, R secondItem)
        {
            unityEventResponse?.Invoke(firstItem, secondItem);
        }
    }
}

