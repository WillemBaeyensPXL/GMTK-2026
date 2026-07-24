using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewVisualController : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private float _oldX;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _oldX = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newX = transform.position.x;
        if(Mathf.Abs(newX-_oldX) > 0.0001)
        {
            if(newX > _oldX)
            {
                _renderer.flipX = false;
            }
            else if(newX < _oldX)
            {
                _renderer.flipX = true;
            }
        }

        _oldX = newX;
    }
}
