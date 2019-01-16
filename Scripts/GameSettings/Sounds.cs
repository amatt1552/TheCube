using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour 
{
	public static Sounds GC;

	public AudioMixerGroup master;
	public AudioMixerGroup music;
	public AudioMixerGroup soundEffects;

	public float masterVolume;
	public float musicVolume;
	public float soundEffectsVolume;

	AudioSource movingSource;
	AudioClip movingSound;

	AudioSource splashSource;
	AudioClip splashSound;

	AudioSource musicSource;
	AudioClip musicClip;

	AudioSource playingSource;
	AudioClip playingClip;

	AudioSource hitSource;
	AudioClip hitSound;

	AudioClip hoverClip;
	AudioSource hoverSource;

	AudioClip clickClip;
	AudioSource clickSource;

	AudioSource sizzleSource;
	AudioClip sizzleClip;

	void Awake () 
	{
		GC = this.GetComponent<Sounds>();

		musicClip = (AudioClip)Resources.Load("AudioClips/theCubeMenu-1");
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.outputAudioMixerGroup = music;
		musicSource.clip = musicClip;
		musicSource.loop = true;
		musicSource.playOnAwake = true;

		playingClip = (AudioClip)Resources.Load("AudioClips/theCubePlaying");
		playingSource = gameObject.AddComponent<AudioSource>();
		playingSource.outputAudioMixerGroup = music;
		playingSource.clip = playingClip;
		playingSource.loop = true;
		playingSource.playOnAwake = true;

		movingSound = (AudioClip)Resources.Load("AudioClips/cubeMoving");
		movingSource = gameObject.AddComponent<AudioSource>();
		movingSource.outputAudioMixerGroup = soundEffects;
		movingSource.clip = movingSound;
		movingSource.loop = true;
		movingSource.playOnAwake = false;

		hitSound = (AudioClip)Resources.Load("AudioClips/death");
		hitSource = gameObject.AddComponent<AudioSource>();
		hitSource.outputAudioMixerGroup = soundEffects;
		hitSource.clip = hitSound;
		hitSource.playOnAwake = false;
		hitSource.volume = 0.1f;
		hitSource.pitch = 4f;

		hoverClip = (AudioClip)Resources.Load("AudioClips/textHover");
		hoverSource = gameObject.AddComponent<AudioSource>();
		hoverSource.outputAudioMixerGroup = soundEffects;
		hoverSource.clip = hoverClip;
		hoverSource.loop = false;
		hoverSource.playOnAwake = false;

		clickClip = (AudioClip)Resources.Load("AudioClips/textClicked");
		clickSource = gameObject.AddComponent<AudioSource>();
		clickSource.outputAudioMixerGroup = soundEffects;
		clickSource.volume = 0.5f;
		clickSource.clip = clickClip;
		clickSource.loop = false;
		clickSource.playOnAwake = false;

		splashSound = (AudioClip)Resources.Load("AudioClips/Splash");
		splashSource = gameObject.AddComponent<AudioSource>();
		splashSource.outputAudioMixerGroup = soundEffects;
		splashSource.volume = 0.1f;
		splashSource.pitch = 1.5f;
		splashSource.clip = splashSound;
		splashSource.loop = false;
		splashSource.playOnAwake = false;

		sizzleClip = (AudioClip)Resources.Load("AudioClips/Sizzle");
		sizzleSource = gameObject.AddComponent<AudioSource>();
		sizzleSource.outputAudioMixerGroup = soundEffects;
		sizzleSource.clip = sizzleClip;
		sizzleSource.loop = false;
		sizzleSource.playOnAwake = false;
		sizzleSource.volume = 0.1f;
	}
	void Update()
	{
		masterVolume = Settings.masterVol;
		musicVolume = Settings.musicVol;
		soundEffectsVolume = Settings.soundEffectsVol;

		master.audioMixer.SetFloat("master", masterVolume);
		music.audioMixer.SetFloat("music", musicVolume);
		soundEffects.audioMixer.SetFloat("sfx", soundEffectsVolume);
	}
		
	public void changeMusic(string newSong)
	{
	
	}

	public void playSound (string sound) 
	{
		switch(sound)
		{
			case("menu"):
			musicSource.Play();
			break;

			case("playing"):
			playingSource.Play();
			break;

			case("playerHit"):
			hitSource.Play();
			break;

			case("playerMoving"):
			movingSource.Play();
			break;

			case("textHover"):
			hoverSource.Play();
			break;

			case("textClicked"):
			clickSource.Play();
			break;

			case("splash"):
			splashSource.Play();
			break;

			case("sizzle"):
			sizzleSource.Play();
			break;

			default:
			break;

		}
	}

	public void stopSound (string sound)
	{
		switch(sound)
		{
		case("menu"):
			musicSource.Stop();
			break;

		case("playerHit"):
			hitSource.Stop();
			break;

		case("playerMoving"):
			movingSource.Stop();
			break;

		case("hover"):
			hoverSource.Stop();
			break;

		case("clicked"):
			clickSource.Stop();
			break;

		case("splash"):
			splashSource.Stop();
			break;

		default:
			break;
		}
	}

	public AudioSource AddSource(GameObject newObject, string newClip, int mixer)
	{
		
		AudioClip clip;
		AudioSource newSource;
		clip = (AudioClip)Resources.Load("AudioClips/" + newClip);
		newSource = newObject.AddComponent<AudioSource>();
		switch(mixer)
		{
		case(0):
			newSource.outputAudioMixerGroup = music;
			break;
		case(1):
			newSource.outputAudioMixerGroup = soundEffects;
			break;

		}

		newSource.clip = clip;
		newSource.loop = false;
		newSource.playOnAwake = false;
		return newSource;
	}

	public AudioSource getSource(string source)
	{
		switch(source)
		{
		case("menu"):
			return musicSource;
			break;

		case("playerHit"):
			return hitSource;
			break;

		case("playerMoving"):
			return movingSource;
			break;
		
		case("hover"):
			return hoverSource;
			break;

		case("clicked"):
			return clickSource;
			break;

		case("splash"):
			return splashSource;
			break;
		case("death"):
			return hitSource;
			break;

		default:
			return null; 
			break;
		}
	}
}
