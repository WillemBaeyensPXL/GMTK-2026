using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour
{
    [SerializeField]
    private float PPU = 32f;

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
        Vector3 position = transform.position;
        position.x = MathF.Round(position.x * PPU) / PPU;
        position.y = MathF.Round(position.y * PPU) / PPU;
        transform.position = position;
    }
}
