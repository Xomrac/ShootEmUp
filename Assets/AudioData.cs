using UnityEngine;

namespace Xomrac.Shmups
{

	[CreateAssetMenu(fileName = "NewAudioData", menuName = "Sounds/ScriptableSound")]
	public class AudioData : ScriptableObject
	{
	#region Fields

		[SerializeField] private AudioClip clip;
		public AudioClip Clip => clip;

		[Range(0, 256)] [SerializeField] private int priority = 128;
		public int Priority => priority;

		[Range(0, 1)] [SerializeField] private float volume = 0.5f;
		public float Volume => volume;

		[Range(-3, 3)] [SerializeField] private float pitch = 1;
		public float Pitch => pitch;

		[Range(-1, 1)] [SerializeField] private float stereoPan = 0;
		public float StereoPan => stereoPan;

		[Range(0, 1)] [SerializeField] private float spatialBlend = 0;
		public float SpatialBlend => spatialBlend;

		[Range(0, 1.1f)] [SerializeField] private float reverbZoneMix = 1;
		public float ReverbZoneMix => reverbZoneMix;
		
	#endregion

	#region Methods

		public void SetSource(AudioSource source)
		{
			source.clip = clip;
			source.volume = volume;
			source.pitch = pitch;
			source.priority = priority;
			source.panStereo = stereoPan;
			source.spatialBlend = spatialBlend;
			source.reverbZoneMix = reverbZoneMix;
		}
		
		public void Play(AudioSource source)
		{
			source.clip = clip;
			source.volume = volume;
			source.pitch = pitch;
			source.priority = priority;
			source.panStereo = stereoPan;
			source.spatialBlend = spatialBlend;
			source.reverbZoneMix = reverbZoneMix;
			source.Play();
		}

		public void PlayOneShot(AudioSource source)
		{
			source.volume = volume;
			source.pitch = pitch;
			source.priority = priority;
			source.panStereo = stereoPan;
			source.spatialBlend = spatialBlend;
			source.reverbZoneMix = reverbZoneMix;
			source.PlayOneShot(clip);
		}
		public void PlayOneShot(AudioSource source,Vector2 randomPitch)
		{
			source.volume = volume;
			source.pitch = Random.Range(randomPitch.x,randomPitch.y);
			source.priority = priority;
			source.panStereo = stereoPan;
			source.spatialBlend = spatialBlend;
			source.reverbZoneMix = reverbZoneMix;
			source.PlayOneShot(clip);
		}
		
	#endregion
	}

}