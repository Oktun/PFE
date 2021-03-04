using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationHandler : MonoBehaviour
{
    private bool isAnimating = false;

    public bool IsAnimating { get { return isAnimating; } }

    
}
