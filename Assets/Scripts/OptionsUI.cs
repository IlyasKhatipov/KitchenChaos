using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => 
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteract); });
        gamepadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadPause); });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OngameUnpaused += KitchenManager_OnGameUnpaused;
        UpdateVisual();
        HidePressToRebind();
        Hide();
    }

    private void KitchenManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual() 
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    public void Show(Action onCloseButtonAction) 
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebind() 
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebind()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) 
    {
        ShowPressToRebind();
        GameInput.Instance.RebindBinding(binding, () => 
        {
            HidePressToRebind();
            UpdateVisual();
        });
    }
}
