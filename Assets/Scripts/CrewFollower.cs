using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrewFollower : MonoBehaviour
{

    private CrewVisualController _visual;

    public Vector2 movement;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        _visual = gameObject.GetComponentInChildren<CrewVisualController>();
    }

    // Update is called once per frame
    void Update()
    {
        _visual.movement = movement;
        _visual.isMoving = isMoving;
    }

    public void AlignToGrid()
    {
        Vector3 position = transform.position;
        position.x = MathF.Round(position.x * 32f) / 32f;
        position.y = MathF.Round(position.y * 32f) / 32f;
        transform.position = position;
    }

}
