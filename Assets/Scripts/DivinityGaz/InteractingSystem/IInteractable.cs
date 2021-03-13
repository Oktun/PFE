using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.Interactables
{
    public interface IInteractable
    {
        void OnEnterInteract ();
        void Interact (GameObject other);
        void OnExitInteract ();
    }
}

