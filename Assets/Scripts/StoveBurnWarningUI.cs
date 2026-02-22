using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.onProgressChanged += StoveCounter_OnprogressChanged;
        Hide();
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) 
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        if (show)
        {
            Show();
        }
        else 
        {
            Hide();
        }
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
