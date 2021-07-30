using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource AmbientAudioSource;

    public AudioSource FeedbackAudioSource;
    private List<AudioClip> AudioFeedbackList;
    void Start()
    {
        if(AmbientAudioSource.clip == null)
        {
            Resources.Load<AudioClip>("Audio/Ambient/RoC_Ambient");
        }
        AmbientAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        AmbientAudioSource.loop = true;
    }

    public void PlayCoinSound()
    {
        FeedbackAudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/Coin_01"));
    }

    public void PlayCardSound()
    {
        FeedbackAudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/DrawCardx3_01"));
    }

    public void PlayTradePhaseSound()
    {
        FeedbackAudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/Incoming Merchant (ShopBell)_01"));
    }

    public void PlayEndOfDaySound()
    {
        FeedbackAudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/SigningContract"));
    }
}
