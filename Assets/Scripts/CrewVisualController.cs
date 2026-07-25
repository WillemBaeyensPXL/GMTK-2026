using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CrewVisualController : MonoBehaviour
{
    protected SpriteRenderer _sr;
    protected Animator _animator;

    public Vector2 movement;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        _sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        _animator = gameObject.GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        UpdateAnimator();
    }

    protected virtual void UpdateAnimator()
    {
        _animator.SetBool("IsMoving", isMoving);

        if (movement.x != 0)
        {
            _sr.flipX = movement.x < 0;
        }
    }
}
