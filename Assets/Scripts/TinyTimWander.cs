using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TinyTim : MonoBehaviour
{
    [SerializeField]
    private float wanderRadius;
    [SerializeField]
    private float wanderCooldown;
    [SerializeField]
    private float wanderDuration;

    [SerializeField]
    private List<Transform> _wanderLocations = new List<Transform>();

    private NavMeshAgent _agent;
    private float _wanderTimer;
    private float _wanderDurationTimer;

    private State _currentState;

    private bool _targetCurrentlyUnreachable = false;

    private GameObject _target;

    private enum State
    {
        Travel,
        WanderAtLocation,
        Aggro
    }

    private int _lastWanderLocation = -1;

    // Use this for initialization
    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        StartTravel();
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case State.Travel:
                if (_agent.remainingDistance < 1f)
                {
                    StartWander();
                }
                break;
            case State.WanderAtLocation:
                _wanderTimer += Time.deltaTime;

                if (_wanderTimer >= wanderCooldown || _agent.remainingDistance < .5f)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    _agent.SetDestination(newPos);
                    _wanderTimer -= wanderCooldown;

                    _wanderDurationTimer += wanderCooldown;
                    if(_wanderDurationTimer > wanderDuration)
                    {
                        StartTravel();
                    }
                }
                break;
            case State.Aggro:
                SetDestinationToTarget();
                break;
            default:
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = origin + Random.insideUnitSphere * dist;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private int ChooseWanderLocation()
    {
        int randomIndex = Random.Range(0, _wanderLocations.Count);

        if (randomIndex == _lastWanderLocation)
        {
            return ChooseWanderLocation();
        }
        return randomIndex;
    }

    private void StartTravel()
    {
        _agent.SetDestination(_wanderLocations[ChooseWanderLocation()].position);

        _currentState = State.Travel;
        Debug.Log("TinyTim travel");
    }

    private void StartWander()
    {
        _wanderDurationTimer = 0;
        _wanderTimer = 0;
        _currentState = State.WanderAtLocation;
        Debug.Log("TinyTim wander");
    }

    public void StartAggro(GameObject target)
    {
        if (_currentState == State.Aggro) return;
        _target = target;
        if(!SetDestinationToTarget())
        {
            return;
        }
        _currentState = State.Aggro;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.SoundEffects.MonsterAggro);
        Debug.Log("TinyTim aggro");

    }

    private bool SetDestinationToTarget()
    {
        _agent.SetDestination(_target.transform.position);

        switch(_agent.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                return true;
            case NavMeshPathStatus.PathInvalid:
            case NavMeshPathStatus.PathPartial:
                StartWander();
                return false;
        }
        return false;
    }
}
