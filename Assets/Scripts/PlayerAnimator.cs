using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Run(float speed)
    {
        anim.SetFloat("Walk", speed);
    }
    public void Jump(bool isJumping)
    {
        anim.SetBool("Jumping",isJumping);
    }


}
