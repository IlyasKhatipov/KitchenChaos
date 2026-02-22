using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUPText;
    [SerializeField] private TextMeshProUGUI keyMoveDOWNText;
    [SerializeField] private TextMeshProUGUI keyMoveLEFTText;
    [SerializeField] private TextMeshProUGUI keyMoveRIGHTText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAternateText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GAmeInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnSTateChanged;
        UpdateVisual();
        Show();
    }

    private void KitchenGameManager_OnSTateChanged(object sender, EventArgs e) 
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive()) 
        {
            Hide();
        }
    }

    private void GAmeInput_OnBindingRebind(object sender, EventArgs e) 
    {
        UpdateVisual();
    }

    private void UpdateVisual() 
    {
        keyMoveUPText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        keyMoveDOWNText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        keyMoveLEFTText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        keyMoveRIGHTText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        keyGamepadInteractAternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
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
