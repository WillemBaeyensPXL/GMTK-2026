using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 10;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private List<CrewFollower> _crewFollowers = new List<CrewFollower>();

    private static int waypointAmount = 35;

    private static int waypointsBetweenCrew = waypointAmount / 5;

    private List<Vector3> _pastPostitions = new List<Vector3>(new Vector3[waypointAmount]);

    private Vector2 _oldPos;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponentInChildren<SpriteRenderer>();

        for (int i = 0;i<waypointAmount;++i)
        {
            _pastPostitions[i] = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_movement == Vector2.zero) return;

        Vector2 movement = _movement * Time.fixedDeltaTime * movementSpeed;
        _rb.MovePosition(_rb.position + movement);
        Vector2 newPos = _rb.position;
        if( (newPos - _oldPos).sqrMagnitude <= 0.00001)
        {
            return;
        }

        _oldPos = newPos;

        if (_pastPostitions[^1] == transform.position) return;

        for (int i = 0; i < _pastPostitions.Count - 1; i++)
        {
            _pastPostitions[i] = _pastPostitions[i+1];
        }

        _pastPostitions[^1] = transform.position;

        for(int i = 0 ; i<_crewFollowers.Count ; i++)
        {
            int index = waypointAmount - waypointsBetweenCrew * (i+1);
            _crewFollowers[i].transform.position = _pastPostitions[index];
            _crewFollowers[i].AlignToGrid();
        }
    }

    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        if(_movement.x > 0)
        {
            _sr.flipX = false;
        }
        else if(_movement.x < 0)
        {
            _sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Crew"))
        {
            var follower = collision.gameObject.GetComponent<CrewFollower>();
            collision.isTrigger = false;
            collision.gameObject.layer = LayerMask.NameToLayer("Crew");
            _crewFollowers.Add(follower);
        }
    }

}
