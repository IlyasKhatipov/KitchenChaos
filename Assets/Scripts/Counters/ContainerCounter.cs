using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) 
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            InteractLogisServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogisServerRpc() 
    {
        InteractLogisClientRpc();
    }

    [ClientRpc]
    private void InteractLogisClientRpc()
    {
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
