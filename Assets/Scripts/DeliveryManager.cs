using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class DeliveryManager : NetworkBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float recipeSpawnTimer = 4f;
    private float recipeSpawnTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfullRecipesAmount;

    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer) { return; }
        recipeSpawnTimer -= Time.deltaTime;
        if (recipeSpawnTimer <= 0f) 
        {
            recipeSpawnTimer = recipeSpawnTimerMax;
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax) 
            {
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
                SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);
            }
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex) 
    {
        RecipeSO recipeSO = recipeListSO.recipeSOList[waitingRecipeSOIndex];
        waitingRecipeSOList.Add(recipeSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) 
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) 
            {
                bool plateCOntentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) 
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) 
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) 
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) 
                    {
                        plateCOntentsMatchesRecipe = false;
                    }
                }
                if (plateCOntentsMatchesRecipe) 
                {
                    DeliverCorrectRecipeServerRpc(i);
                    return;
                }
            }
        }
        DeliverIncorrectRecipeServerRpc();
    }

    [ServerRpc(RequireOwnership =false)]
    private void DeliverIncorrectRecipeServerRpc() 
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc() 
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSOListIndex) 
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeSOListIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeSOListIndex) 
    {
        successfullRecipesAmount++;
        waitingRecipeSOList.RemoveAt(waitingRecipeSOListIndex);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() 
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfullRecipesAmount() 
    {
        return successfullRecipesAmount;
    }
}
