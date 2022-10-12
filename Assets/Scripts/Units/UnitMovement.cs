using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    #region Variables
    [SerializeField] private NavMeshAgent agent = null;

    #endregion

    #region Server

    [Command]
    public void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client


    #endregion
}
