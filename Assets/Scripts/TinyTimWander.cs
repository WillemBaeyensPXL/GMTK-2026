using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderingAI : MonoBehaviour
{
    //[SerializeField]
    //private float wanderRadius;
    //[SerializeField]
    //private float wanderTimer;

    [SerializeField]
    private List<Transform> wanderLocations = new List<Transform>();

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //timer = wanderTimer;

        agent.SetDestination(wanderLocations[Random.Range(0,wanderLocations.Count)].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < .1f)
        {
            agent.SetDestination(wanderLocations[Random.Range(0, wanderLocations.Count)].position);
        }
        //timer += Time.deltaTime;

        //if (timer >= wanderTimer)
        //{
        //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //    agent.SetDestination(newPos);
        //    timer = 0;
        //}
    }

    //public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    //{
    //    Vector3 randDirection = origin+ Random.insideUnitSphere * dist;

    //    NavMeshHit navHit;

    //    NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

    //    return navHit.position;
    //}
}
