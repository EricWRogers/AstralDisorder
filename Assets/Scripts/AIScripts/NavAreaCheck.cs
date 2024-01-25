using UnityEngine;
using UnityEngine.AI;

public class NavAreaCheck : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(gameObject.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            
            int navMeshArea = hit.mask;
            Debug.Log("Agent is in NavMesh area: " + navMeshArea);
        }
        else
        {
            Debug.Log("Nothin. .");
        }
    }
}