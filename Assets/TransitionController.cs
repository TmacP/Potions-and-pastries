using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public Animator transitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = transform.Find("Transition").GetComponent<Animator>();
    }

    public void ExitSceneTransition()
    {
        transitionAnimator.SetTrigger("ExitScene");
    }
}
