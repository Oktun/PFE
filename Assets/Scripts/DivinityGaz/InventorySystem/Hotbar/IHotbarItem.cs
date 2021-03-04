using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    public interface IHotbarItem
    {
        void Use (InputSystem inputSystem);
    }
}
