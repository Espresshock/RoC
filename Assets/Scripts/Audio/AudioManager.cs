using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource AmbientAudioSource;

    public List<AudioSource> AudioFeedbackList;
    void Start()
    {
        AmbientAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        AmbientAudioSource.loop = true;
    }
}
