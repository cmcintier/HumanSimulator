using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementController : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer;

    Vector3 destinationPoint;
    bool walkpointSet;
    [SerializeField] float walkRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
    }

    void Wander() {
        if (walkpointSet) {
            agent.SetDestination(destinationPoint);

            if (Vector3.Distance(transform.position, destinationPoint) < 10) {
                walkpointSet = false;
            }
        }
        else {
            SearchForDestination();
        }
    }

    void SearchForDestination() {
        float z = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destinationPoint, Vector3.down, groundLayer)) {
            walkpointSet = true;
        }
    }
}
