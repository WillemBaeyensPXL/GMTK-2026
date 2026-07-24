using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrewFollower : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AlignToGrid()
    {
        Vector3 position = transform.position;
        position.x = MathF.Round(position.x * 32f) / 32f;
        position.y = MathF.Round(position.y * 32f) / 32f;
        transform.position = position;
    }

}
