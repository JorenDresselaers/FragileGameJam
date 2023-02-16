using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource _audioPlayer;
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();

    void Start()
    {
        //_audioPlayer = GetComponent<AudioSource>();

        //for (int i=0;i<4;i++)
        //{
        //	_audioSources.Add(new AudioSource());
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClip _clip)
    {
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (!_audioSources[i].isPlaying || _audioSources[i].clip==_clip)
            {
                _audioSources[i].clip = _clip;
                _audioSources[i].Play();
                break;
            }
        }
        //_audioPlayer.clip = _clip;
        //_audioPlayer.Play();
    }
}
