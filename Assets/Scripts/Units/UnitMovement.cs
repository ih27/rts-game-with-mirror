using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    #region Variables
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float chaseRange = 10f;

    #endregion

    #region Server
    [ServerCallback]
    private void Update()
    {
        Targetable target = targeter.GetTarget();
        // chasing logic
        if (target != null)
        {
            // Vector3.Distance() is slow
            if ((target.transform.position - transform.position).sqrMagnitude > chaseRange * chaseRange)
            {
                // chase
                agent.SetDestination(target.transform.position);
            }
            else if (agent.hasPath)
            {
                // stop chasing
                agent.ResetPath();
            }

            return;
        }

        if (!agent.hasPath) { return; }

        if (agent.remainingDistance > agent.stoppingDistance) { return; }

        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        targeter.ClearTarget();

        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client


    #endregion
}
