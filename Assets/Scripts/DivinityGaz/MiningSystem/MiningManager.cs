using DivinityGaz.CustomEvents.Single;
using DivinityGaz.Interactables;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

namespace DivinityGaz.MiningSystem
{
    public class MiningManager : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private InputSystem inputSystem = null;

        private bool isMining = false;
        private string currentActiveAnimation = string.Empty;

        [SerializeField] private float miningDuration = 3f;
        private float timer = 0f;

        [Header("Events")]
        [Space]
        [SerializeField] private StringEvent onEnterInteract = null;
        [SerializeField] private VoidEvent onExitInteract = null;

        private Minable currentMinable = null;

        private void Update ()
        {
            if (isMining)
            {
                if (timer >= miningDuration)
                {
                    MiningCompleted();
                } 
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }

        public void StartMining (IInteractable interactable, string AnimationName)
        {
            inputSystem.StopMoving();
            currentMinable = interactable as Minable;
            currentActiveAnimation = currentMinable.AnimationToCast;
            isMining = true;
            movement.CharacterMining(AnimationName, true);
        }

        public void MiningCancelled ()
        {
            isMining = false;
            currentMinable = null;
            timer = 0f;
        }

        public void MiningCompleted ()
        {
            isMining = false;
            movement.CharacterMining(currentActiveAnimation, false);
            timer = 0f;

            currentMinable = null;
            print("Completed");
        }

        public void OnEnterInteraction (string interactionText)
        {
            onEnterInteract?.Invoke(interactionText);
        }

        public void OnExitInteraction () => onExitInteract?.Invoke();
    }
}