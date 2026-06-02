using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playMultiPlayerPlayBtn;
    [SerializeField] private Button quitBTN;

    private void Awake()
    {
        playMultiPlayerPlayBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LobbyScene);
        });

        quitBTN.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
