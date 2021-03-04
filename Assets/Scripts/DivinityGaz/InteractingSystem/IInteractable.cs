using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.Interactables
{
    public interface IInteractable
    {
        void OnEnterInteract ();
        void interact (GameObject other);
        void OnExitInteract ();
    }
}

