using UnityEngine;

public struct PosAndMovement
{
    public PosAndMovement(Vector3 _pos, Vector2 _movement)
    {
        pos = _pos;
        movement = _movement;
    }

    public Vector3 pos;
    public Vector2 movement;
}
