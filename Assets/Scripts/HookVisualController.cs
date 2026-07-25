using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookVisualController : CrewVisualController
{
    protected override void  UpdateAnimator()
    {
        _animator.SetBool("IsMoving", isMoving);

        if (movement.x != 0)
        {
            _sr.flipX = movement.x < 0;
        }
    }
}
