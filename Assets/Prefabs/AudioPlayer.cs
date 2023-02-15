using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource _audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClip _clip)
    {
        if (!_audioPlayer.isPlaying)
        {
            _audioPlayer.clip = _clip;
            _audioPlayer.Play();
        }
    }
}
