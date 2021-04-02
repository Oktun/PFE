using DivinityGaz.CustomEvents.Single;
using UnityEngine;

public class SingleCast : StateMachineBehaviour
{
    [SerializeField] private string EntryParamtersName = string.Empty;
    [SerializeField] private VoidEvent onCancel = null;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("forward") != 0f || animator.GetFloat("strafe") != 0f)
        {
            animator.SetBool(EntryParamtersName, false);
            onCancel?.Invoke();
        }
    }
}
