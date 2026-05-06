using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{

    private void Start()
    {
        KitchenGameManager.Instance.OnMultiplayerGamePaused += KitchenGameManager_OnMulitplayerGamePaused;
        KitchenGameManager.Instance.OnMultiplayerGameUnpaused += KitchenGameManager_OnMulitplayerGameUnpaused;
        Hide();
    }

    private void KitchenGameManager_OnMulitplayerGamePaused(object sender, System.EventArgs e) 
    {
        Show();
    }

    private void KitchenGameManager_OnMulitplayerGameUnpaused(object sender, System.EventArgs e) 
    {
        Hide();
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
