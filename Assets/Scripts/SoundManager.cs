using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) 
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsRefsSO.trash[UnityEngine.Random.Range(0, audioClipsRefsSO.trash.Length)], trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) 
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipsRefsSO.objectDrop[UnityEngine.Random.Range(0, audioClipsRefsSO.objectDrop.Length)], baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) 
    {
        PlaySound(audioClipsRefsSO.objectPickup[UnityEngine.Random.Range(0, audioClipsRefsSO.objectPickup.Length)], Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) 
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsRefsSO.chop[UnityEngine.Random.Range(0, audioClipsRefsSO.chop.Length)], cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) 
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) 
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume) 
    {
        PlaySound(audioClipsRefsSO.footStep[UnityEngine.Random.Range(0, audioClipsRefsSO.footStep.Length)], position, volume);
    }
}
