using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceSingle;
    [SerializeField] private AudioSource _audioSourceLine;

    public void PlaySingleExplode()
    {
        if (_audioSourceSingle.isPlaying) return;

        _audioSourceSingle.PlayOneShot(_audioSourceSingle.clip);
    }
    public void PlayLineExplode()
    {
        _audioSourceLine.PlayOneShot(_audioSourceLine.clip);
    }
}
