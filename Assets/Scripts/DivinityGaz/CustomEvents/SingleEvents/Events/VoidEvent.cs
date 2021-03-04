using UnityEngine;

namespace DivinityGaz.CustomEvents.Single
{
    [CreateAssetMenu(fileName ="Void_Event", menuName ="Custom Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Invoke () => Invoke(new Void());
    }
}