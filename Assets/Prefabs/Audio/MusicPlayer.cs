using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clipTransition;
    [SerializeField] private AudioClip _clipMusicLoop;
    private AudioSource _audioSource;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Transition audio will play on awake in lvl 2, if it stopped playing lets play music and loop
        if(!_audioSource.isPlaying)
        {
            _audioSource.clip = _clipMusicLoop;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
