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

    private List<PosAndMovement> _pastPostitions = new List<PosAndMovement>(new PosAndMovement[waypointAmount]);

    private Vector2 _oldPos;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponentInChildren<SpriteRenderer>();

        for (int i = 0;i<waypointAmount;++i)
        {
            _pastPostitions[i] = new PosAndMovement(transform.position,_movement);
        }

        _animator = gameObject.GetComponentInChildren<Animator>();

        _oldPos = _rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = _movement.magnitude != 0;

        _animator.SetBool("IsMoving", isMoving);


        foreach (CrewFollower follower in _crewFollowers)
        {
            follower.isMoving = isMoving;
        }
        

        if(_movement.x != 0)
        {
            _sr.flipX = _movement.x < 0;
        }
    }

    private void FixedUpdate()
    {
        if (_movement == Vector2.zero) return;
        Vector2 movement = _movement * Time.fixedDeltaTime * movementSpeed;
        _rb.MovePosition(_rb.position + movement);
        Vector2 newPos = _rb.position;
        if ((newPos - _oldPos).sqrMagnitude <= 0.00001)
        {
            return;
        }

        _oldPos = newPos;

        if (_pastPostitions[^1].pos == transform.position)
        {
            return;
        }

        for (int i = 0; i < _pastPostitions.Count - 1; i++)
        {
            _pastPostitions[i] = _pastPostitions[i+1];
        }

        _pastPostitions[^1] = new PosAndMovement(transform.position,_movement);

        for(int i = 0 ; i<_crewFollowers.Count ; i++)
        {
            int index = waypointAmount - waypointsBetweenCrew * (i+1);
            _crewFollowers[i].transform.position = _pastPostitions[index].pos;
            _crewFollowers[i].AlignToGrid();
            _crewFollowers[i].movement = _pastPostitions[index].movement;
        }
    }



    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
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
