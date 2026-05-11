using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;

    private void Awake()
    {
        createPublicButton.onClick.AddListener(() =>
            {
                KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
            }
        );
        
        createPrivateButton.onClick.AddListener(() =>
            {
                KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
            }
        );
        
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
