using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CrewFollower : MonoBehaviour
{
    private Transform _target;

    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            _collider.isTrigger = false;
            this.gameObject.layer = LayerMask.NameToLayer("Crew");
        }
    }

    [SerializeField]
    private float squaredFollowDistance = 4;
    [SerializeField]
    private float squaredStopFollowDistance = 2;
    [SerializeField]
    private float movementSpeed = 9.5f;

    private bool _isFollowing = false;
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if (!Target) return;

        //Bounds bounds = _collider.bounds;

        //Debug.DrawLine(transform.position, Target.position);

        //float sqrDistance = bounds.SqrDistance(Target.position);

        //Debug.Log(sqrDistance);

        //if (sqrDistance > squaredFollowDistance)
        //{
        //    _isFollowing = true;
        //}
        //else if (_isFollowing && sqrDistance < squaredStopFollowDistance)
        //{
        //    _isFollowing = false;
        //}

        //if (_isFollowing)
        //{
        //    Vector2 distanceVector = Target.position - transform.position;
        //    Vector2 position = _rb.position + distanceVector.normalized * movementSpeed * Time.fixedDeltaTime;

        //    _rb.MovePosition(position);
        //}

    }

    public void AlignToGrid()
    {
        Vector3 position = transform.position;
        position.x = MathF.Round(position.x * 32f) / 32f;
        position.y = MathF.Round(position.y * 32f) / 32f;
        transform.position = position;
    }

    private void LateUpdate()
    {

    }
}
