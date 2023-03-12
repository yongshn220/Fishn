using System;
using UnityEngine;


public enum AnimatorType
{
    IsSwim,
    IsEat,
}

public class EntityAnimatorController : MonoBehaviour
{

    Animator animator;
    
    void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    public void Setup()
    {
        ResetAllState();
    }

    public void SetBoolAnimator(AnimatorType type, bool state)
    {
        ResetAllState();
        animator.SetBool(type.ToString(), state);
    }

    private void ResetAllState()
    {
        foreach (AnimatorType state in Enum.GetValues(typeof(AnimatorType)))
        {   
            animator.SetBool(state.ToString(), false);
        }
    }
}
