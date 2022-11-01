using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    #region Variables

    [SerializeField] private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public event Action ServerOnDie;
    #endregion

    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damageAmount)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

        if (currentHealth != 0) { return; }

        // dead
        ServerOnDie?.Invoke();

        Debug.Log("We died!");
    }

    #endregion

    #region Client


    #endregion
}
