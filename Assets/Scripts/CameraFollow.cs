using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSnapToGrid : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0,0,-10);

    [SerializeField]
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 position = target.position;
        position += offset;
        position.x = MathF.Round(position.x * 32f) / 32f;
        position.y = MathF.Round(position.y * 32f) / 32f;
        transform.position = position;
    }
}
