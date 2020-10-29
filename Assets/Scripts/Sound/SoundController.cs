using UnityEngine;

public class SoundController : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSourceSingle;
	[SerializeField] private AudioSource _audioSourceLine;
	public bool SoundOn;

	public void ChangeSoundBool()
	{
		if (SoundOn)
		{
			SoundOn = false;
		}
		else
		{
			SoundOn = true;
		}
	}
	public void PlaySingleExplode()
	{
		if (_audioSourceSingle.isPlaying) return;
		if (SoundOn)
		{
			_audioSourceSingle.PlayOneShot(_audioSourceSingle.clip);
		}
	}
	public void PlayLineExplode()
	{
		if (SoundOn)
		{
			_audioSourceLine.PlayOneShot(_audioSourceLine.clip);
		}
	}
}
