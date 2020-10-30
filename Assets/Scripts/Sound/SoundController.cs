using UnityEngine;

public class SoundController : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSourceSingle;
	[SerializeField] private AudioSource _audioSourceLine;
	[SerializeField] private AudioSource _audioSourceMusic;
	[SerializeField] private AudioSource _audioSourceButtonClick;
	public bool SoundOn;
	public bool MusicOn;

	private void Start()
	{
		PlayMusic();
	}
	public void ChangeSoundBool()
	{
		if (SoundOn)
		{
			SoundOn = false;
		}
		else
		{
			SoundOn = true;
			PlayButtonClick();
		}
	}
	public void ChangeMusicBool()
	{
		if (MusicOn)
		{
			MusicOn = false;
			_audioSourceMusic.Stop();
			PlayButtonClick();
		}
		else
		{
			MusicOn = true;
			PlayButtonClick();
			PlayMusic();
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
	public void PlayButtonClick()
	{
		if (SoundOn)
		{
			_audioSourceButtonClick.PlayOneShot(_audioSourceButtonClick.clip);
		}
	}
	public void PlayMusic()
	{
		if (MusicOn)
		{
			_audioSourceMusic.Play();
		}
	}
}
