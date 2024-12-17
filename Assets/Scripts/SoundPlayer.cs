using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource soundSrc;
    [SerializeField] private AudioClip fruit, key, finishOpen;

    public void PlayPickupSound(bool isKey)
    {
        soundSrc.clip = isKey ? key : fruit;
        soundSrc.Play();
    }

    public void PlayFinishSound()
    {
        soundSrc.clip = finishOpen;
        soundSrc.Play();
    }
}
