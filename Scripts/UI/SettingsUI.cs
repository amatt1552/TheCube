using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityStandardAssets.ImageEffects;

public class SettingsUI : MonoBehaviour 
{
	public static SettingsUI GC;
	public GameObject[] playerSettings;
	public Slider red, green, blue;
	public Dropdown playerSkin;


	public GameObject[] audioSettings;
	public Slider master, music, soundEffects;

	public GameObject[] videoSettings;
	public Dropdown screenResolution;
	Vector2 resolution;
	public Slider videoQuality;

	public Toggle bloom;
	public Slider bloomIntensity;
	public Toggle fullScreen;
	public Toggle hdr;

	public GameObject[] controls;

	GameObject chosenObject;
	public GameObject playerSettingsButton, audioButton, videoButton, controlsButton;
	ColorBlock playerBlock, audioBlock, videoBlock, controlsBlock;
	Color temp;

	public InputField left1, left2;
	public InputField right1, right2;
	public InputField jump1, jump2;
	public InputField cameraMove1, cameraMove2;
	public InputField selfDestruct1, selfDestruct2;

	public RectTransform sliderColorRed, sliderColorGreen, sliderColorBlue;

	void Start()
	{
		print("hi");
		GC = this.GetComponent<SettingsUI>();
		Settings.control.Load();
		left1.text = Settings.left1;
		left2.text = Settings.left2;
		right1.text = Settings.right1;
		right2.text = Settings.right2;
		jump1.text = Settings.jump1;
		jump2.text = Settings.jump2;
		cameraMove1.text = Settings.cameraMove1;
		cameraMove2.text = Settings.cameraMove2;
		selfDestruct1.text = Settings.selfDestruct1;
		selfDestruct2.text = Settings.selfDestruct2;


		bloom.isOn = bloom;
		hdr.isOn = Settings.hdr;
		fullScreen.isOn = Settings.fullScreen;
		master.value = Settings.masterVol;
		music.value = Settings.musicVol;
		soundEffects.value = Settings.soundEffectsVol;


		temp = playerSettingsButton.GetComponent<Button>().colors.normalColor;
		playerBlock = playerSettingsButton.GetComponent<Button>().colors;
		playerBlock.normalColor = playerSettingsButton.GetComponent<Button>().colors.highlightedColor;

		audioBlock = audioButton.GetComponent<Button>().colors;
		videoBlock = videoButton.GetComponent<Button>().colors;
		controlsBlock = controlsButton.GetComponent<Button>().colors;

		red.value = Settings.normalPlayerColor.r;
		green.value = Settings.normalPlayerColor.g;
		blue.value = Settings.normalPlayerColor.b;
		playerSkin.value = Settings.currentSkin;
		print(Settings.currentSkin);

		fullScreen.isOn = Settings.fullScreen;
		hdr.isOn = Settings.hdr;
		bloom.isOn = Settings.bloom;
		bloomIntensity.value = Settings.bloomIntensity;

		screenResolution.value = Settings.resolutionVal;
		resolution = Settings.resolution;
		videoQuality.value = Settings.quality;

	}

	void Update()
	{

		playerSettingsButton.GetComponent<Button>().colors = playerBlock;
		audioButton.GetComponent<Button>().colors = audioBlock;
		videoButton.GetComponent<Button>().colors = videoBlock;
		controlsButton.GetComponent<Button>().colors = controlsBlock;

		Image sliderColorRed;
		Image sliderColorGreen;
		Image sliderColorBlue;

		sliderColorRed = this.sliderColorRed.GetComponent<Image>();
		sliderColorGreen = this.sliderColorGreen.GetComponent<Image>();
		sliderColorBlue = this.sliderColorBlue.GetComponent<Image>();

		sliderColorRed.color = new Color(red.value, 0, 0);
		sliderColorBlue.color = new Color(0, 0, blue.value);
		sliderColorGreen.color = new Color(0, green.value, 0);



		switch(playerSkin.value)
		{

		case 0:
			PlayerSettings.chosenTexture = null;
			break;

		case 1:
			print("kay.");
			PlayerSettings.chosenTexture = (Texture)Resources.Load("PlayerSkins/cubeTexture");
			break;

		case 2:
			PlayerSettings.chosenTexture = (Texture)Resources.Load("PlayerSkins/theCubeBox");
			break;

		
		default:
			break;
		}

	}
		
	public void PlayerSettingsUI()
	{
		playerBlock.normalColor = playerBlock.highlightedColor;
		audioBlock.normalColor = temp;
		videoBlock.normalColor = temp;
		controlsBlock.normalColor = temp;
		for(int i = 0; i < playerSettings.Length;)
		{
			On(playerSettings[i], playerBlock);
			Off(audioSettings[i], audioBlock);
			Off(videoSettings[i], videoBlock);
			Off(controls[i], controlsBlock);
			i++;
		}
	}

	public void AudioSettings()
	{
		playerBlock.normalColor = temp;
		audioBlock.normalColor = playerBlock.highlightedColor;
		videoBlock.normalColor = temp;
		controlsBlock.normalColor = temp;
		for(int i = 0; i < playerSettings.Length;)
		{
			
			Off(playerSettings[i], playerBlock);
			On(audioSettings[i], audioBlock);
			Off(videoSettings[i], videoBlock);
			Off(controls[i], controlsBlock);
			i++;
		}
	}

	public void VideoSettings()
	{
		playerBlock.normalColor = temp;
		audioBlock.normalColor = temp;
		videoBlock.normalColor = playerBlock.highlightedColor;
		controlsBlock.normalColor = temp;
		for(int i = 0; i < playerSettings.Length;)
		{
			
			Off(playerSettings[i], playerBlock);
			Off(audioSettings[i], audioBlock);
			On(videoSettings[i], videoBlock);
			Off(controls[i], controlsBlock);
			i++;
		}
	}

	public void Controls()
	{
		playerBlock.normalColor = temp;
		audioBlock.normalColor = temp;
		videoBlock.normalColor = temp;
		controlsBlock.normalColor = playerBlock.highlightedColor;
		for(int i = 0; i < playerSettings.Length;)
		{
			Off(playerSettings[i], playerBlock);
			Off(audioSettings[i], audioBlock);
			Off(videoSettings[i], videoBlock);
			On(controls[i], controlsBlock);
			i++;
		}
	}

	public void Default()
	{
		videoQuality.value = 4;
		master.value = -10;
		music.value = -10;
		soundEffects.value = -10;

		red.value = 0.5f;
		green.value = 0.7f;
		blue.value = 0.9f; 

		hdr.isOn = true;
		bloom.isOn = true;
		bloomIntensity.value = 0.9f;

		fullScreen.isOn = false;
		hdr.isOn = true;
		screenResolution.value = 1;

		left1.text = "a";
		left2.text = "left";
		right1.text = "d";
		right2.text = "right";
		jump1.text = "space";
		jump2.text = "w";
		cameraMove1.text = "f";
		cameraMove2.text = "left ctrl";
		selfDestruct1.text = "r";
		selfDestruct2.text = "x";

		playerSkin.value = 0;
	}

	public void Apply()
	{
		Settings.quality = (int)videoQuality.value;
		Settings.masterVol = master.value;
		Settings.musicVol = music.value;
		Settings.soundEffectsVol = soundEffects.value;

		Settings.normalPlayerColor = new Color(red.value, green.value, blue.value);

		Settings.fullScreen = fullScreen.isOn;
		Settings.hdr = hdr.isOn;
		Settings.bloom = bloom.isOn;
		Settings.bloomIntensity = bloomIntensity.value;

		Settings.resolution = resolution;
		Settings.resolutionVal = screenResolution.value;
		switch(screenResolution.value)
		{
		case 0:
			resolution = new Vector2(800, 600);
			break;

		case 1:
			resolution = new Vector2(1280, 720);
			break;

		case 2:
			resolution = new Vector2(1920, 1080);
			break;

		default:
			break;
		}

		Screen.SetResolution((int)resolution.x, (int)resolution.y, Settings.fullScreen, 60);

		Settings.left1 = left1.text;
		Settings.left2 = left2.text;
		Settings.right1 = right1.text;
		Settings.right2 = right2.text;
		Settings.jump1 = jump1.text;
		Settings.jump2 = jump2.text;
		Settings.cameraMove1 = cameraMove1.text;
		Settings.cameraMove2 = cameraMove2.text;
		Settings.selfDestruct1 = selfDestruct1.text;
		Settings.selfDestruct2 = selfDestruct2.text;
		Settings.currentSkin = playerSkin.value;
		Settings.control.Save();

	}

	public void Cancel()
	{
		Settings.control.Load();
		videoQuality.value = Settings.quality;
		master.value = Sounds.GC.masterVolume;
		music.value = Sounds.GC.musicVolume;
		soundEffects.value = Sounds.GC.soundEffectsVolume;

		red.value = Settings.normalPlayerColor.r;
		green.value = Settings.normalPlayerColor.g;
		blue.value = Settings.normalPlayerColor.b; 

		fullScreen.isOn = Settings.fullScreen;
		hdr.isOn = Settings.hdr;
		bloom.isOn = Settings.bloom;
		bloomIntensity.value = Settings.bloomIntensity;
		resolution = Settings.resolution;
		screenResolution.value = Settings.resolutionVal;

		left1.text = Settings.left1;
		left2.text = Settings.left2;
		right1.text = Settings.right1;
		right2.text = Settings.right2;
		jump1.text = Settings.jump1;
		jump2.text = Settings.jump2;
		cameraMove1.text = Settings.cameraMove1;
		cameraMove2.text = Settings.cameraMove2;
		selfDestruct1.text = Settings.selfDestruct1;
		selfDestruct2.text = Settings.selfDestruct2;
		playerSkin.value = Settings.currentSkin;
		Settings.control.Save();
	}

	public void RedChange()
	{
		Settings.normalPlayerColor.r = red.value;

	}
	public void GreenChange()
	{
		Settings.normalPlayerColor.g = green.value;

	}
	public void BlueChange()
	{
		Settings.normalPlayerColor.b = blue.value;

	}

	void Off(GameObject chosenObject, ColorBlock block)
	{
		chosenObject.SetActive(false);
	}
	void On(GameObject chosenObject, ColorBlock block)
	{
		this.chosenObject = chosenObject;
		Invoke("invoked", 0.1f);

	}
	void invoked()
	{
		chosenObject.SetActive(true);
	}

}
