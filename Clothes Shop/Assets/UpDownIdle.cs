using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownIdle : StateMachineBehaviour
{
    Animator myAnimator;
    [Header("Parameter Names")]
    public string horitzontalParameterName;
    public string verticalParameterName;
    public string lastHorizontalParameterName;
    public string lastVerticalParameterName;

    private float lastHorizontal;
    private float lastVertical;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    myAnimator = animator;
    //}

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat(horitzontalParameterName) != 0.0f || animator.GetFloat(verticalParameterName) != 0.0f)
        {
            lastHorizontal = animator.GetFloat(horitzontalParameterName);
            lastVertical = animator.GetFloat(verticalParameterName);
            animator.SetFloat(lastHorizontalParameterName, animator.GetFloat(horitzontalParameterName));
            animator.SetFloat(lastVerticalParameterName, animator.GetFloat(verticalParameterName));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
