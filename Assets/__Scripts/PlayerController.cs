using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController controller;
    private Animator animator;

    private bool forward = true;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            if(forward) {
                SetSpeedAnimation(1);
                PlayAnimation("Run");

                forward = false;
            }
            else {
                SetSpeedAnimation(-1);
                PlayAnimation("Run");

                forward = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            PlayAnimation("Idle");
        }
	}
    private void SetSpeedAnimation(float speedAnimation) {
        animator.SetFloat("Direction",speedAnimation);
    }
    private void PlayAnimation(string animationToPlay) {
        animator.SetTrigger(animationToPlay);
    }
}
