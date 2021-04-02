using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    CharacterController cc;
    Animator anim;

    [System.Serializable]
    public class AnimationStrings
    {
        public string forward = "forward";
        public string strafe = "strafe";
        public string sprint = "sprint";
        public string aim = "aim";
        public string pull = "pullString";
        public string fire = "fire";
        public string axeAttack = "attackD";
        public string fistAttack = "attackFist";
    }
    [SerializeField] public AnimationStrings animStrings; 

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public void AnimationCharacter(float forward,float strafe)
    {
        anim.SetFloat(animStrings.forward, forward);
        anim.SetFloat(animStrings.strafe, strafe);
    }

    public void sprintCharacter(bool isSprinting) => anim.SetBool(animStrings.sprint, isSprinting);

    public void CharacterAim(bool aiming) => anim.SetBool(animStrings.aim, aiming);

    public void CharacterPullString(bool pull) => anim.SetBool(animStrings.pull, pull);

    public void CharacterFireArrow() => anim.SetTrigger(animStrings.fire);

    public void CharacterAttackWithAxe(bool attackAxe) => anim.SetBool(animStrings.axeAttack, attackAxe);

    public void CharacterFistAttack(bool fistAttack) => anim.SetBool(animStrings.fistAttack, fistAttack); 

    public void CharacterMining(string animationKeyString, bool state)
    {
        anim.SetBool(animationKeyString, state);
    }
}
