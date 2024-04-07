using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Xomrac.Shmups.Utilities.Patterns.Singleton;

namespace Xomrac.Shmups
{

	using UnityEngine;

	public class SoundManager : Singleton<SoundManager>
	{
		[SerializeField] private AudioSource _sfxSource;
		[SerializeField] private AudioSource _playerShotsSource;

		public void PlaySFX(AudioData clip)
		{
			if (clip == null)
			{
				return;
			}
			clip.PlayOneShot(_sfxSource);
		}
		
		public void PlayPlayerShot(AudioData clip)
		{
			if (clip == null)
			{
				return;
			}
			clip.PlayOneShot(_playerShotsSource);
		}
	}

}