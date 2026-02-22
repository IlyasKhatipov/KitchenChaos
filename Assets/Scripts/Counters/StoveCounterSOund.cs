using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounterSOund : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.onProgressChanged += StoveCounter_OnprogressChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedeventArgs e) 
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Pause();
        }
    }

    private void StoveCounter_OnprogressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void Update()
    {
        if (playWarningSound) 
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f) 
            {
                float warningTimerSoundMax = .2f;
                warningSoundTimer = warningTimerSoundMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
