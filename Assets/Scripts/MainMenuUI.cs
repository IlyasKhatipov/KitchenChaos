using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBTN;
    [SerializeField] private Button quitBTN;

    private void Awake()
    {
        playBTN.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });

        quitBTN.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
