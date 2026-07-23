using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private List<CrewFollower> _crewFollowers = new List<CrewFollower>();

    private List<Vector3> _pastPostitions = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();

        for(int i = 0;i<60;++i)
        {
            _pastPostitions.Add(transform.position);
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

        if (_pastPostitions[^1] == transform.position) return;

        for (int i = 0; i < _pastPostitions.Count - 1; ++i)
        {
            _pastPostitions[i] = _pastPostitions[i+1];
        }

        _pastPostitions[_pastPostitions.Count - 1] = transform.position;

        for(int i=0;i<_crewFollowers.Count;i++)
        {
            _crewFollowers[i].transform.position = _pastPostitions[51-i*8];
            _crewFollowers[i].AlignToGrid();
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
            Debug.Log("Crew collision");
            var follower = collision.gameObject.GetComponent<CrewFollower>();
            if(_crewFollowers.Count == 0)
            {
                follower.Target = transform;
            }
            else
            {
                follower.Target = _crewFollowers[_crewFollowers.Count - 1].gameObject.transform;
            }
            _crewFollowers.Add(follower);
        }
    }
}
