using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Settings : MonoBehaviour 
{
	public static Settings control;
	public static int levelsComplete;
	public static Color normalPlayerColor;
	public static bool bloom;
	public static float bloomIntensity;
	public static bool fullScreen;
	public static bool hdr;
	public static Vector2 resolution;
	public static int resolutionVal;
	public static float masterVol;
	public static float musicVol;
	public static float soundEffectsVol;
	public static bool left, right, jump, cameraMovement, selfDestruct;
	public static string left1, left2;
	public static string right1, right2;
	public static string jump1, jump2;
	public static string cameraMove1, cameraMove2;
	public static string selfDestruct1, selfDestruct2;
	public static int quality;
	public static int currentSkin;
	public static bool firstStart;

	float timer, timer2;
	int autoSaveTime = 5;
	public static bool load = true;
	public static bool saving;

	void Awake ()
	{
		Load();
		//firstStart = false;
		if(!firstStart)
		{
			Save();
			quality = QualitySettings.GetQualityLevel();
			masterVol = -10;
			musicVol = -10;
			soundEffectsVol = -10;

			normalPlayerColor.r = 0.5f;
			normalPlayerColor.g = 0.7f;
			normalPlayerColor.b = 0.9f; 

			hdr = true;
			bloom = true;
			bloomIntensity = 0.9f;

			fullScreen = Screen.fullScreen;
			resolutionVal = 0;
		
			left1 = "a";
			left2 = "left";
			right1 = "d";
			right2 = "right";
			jump1 = "space";
			jump2 = "w";
			cameraMove1 = "f";
			cameraMove2 = "left ctrl";
			selfDestruct1 = "r";
			selfDestruct2 = "x";

			currentSkin = 0;
			levelsComplete = 2;
			firstStart = true;
		}
		Save();
	}

	void Start () 
	{
		
		control = this.GetComponent<Settings>();

	}

//----------------------------------------------------------------
	void Update () 
	{
		timer2 += 1/60 * Time.deltaTime;
		if(timer2 >= autoSaveTime)
		{
			Save();
			timer2 = 0;
		}
		
		left = Input.GetKey(left1) || Input.GetKey(left2);
		right = Input.GetKey(right1) || Input.GetKey(right2);
		jump = Input.GetKey(jump1) || Input.GetKey(jump2);
		cameraMovement = Input.GetKey(cameraMove1) || Input.GetKey(cameraMove2);
		selfDestruct = Input.GetKey(selfDestruct1) || Input.GetKey(selfDestruct2);

		Camera.main.allowHDR = hdr;
		Camera.main.GetComponent<Bloom>().enabled = bloom;
		Camera.main.GetComponent<Bloom>().bloomThreshold = bloomIntensity;
		QualitySettings.SetQualityLevel(quality);



		if(PlayerSettings.colorReverted)
		{
			PlayerSettings.playerMaterial.color = normalPlayerColor;
		}
		//print(left1);
	}

	public void Quit()
	{
		Save();
		Application.Quit();
	}


//----------------------------------------------------------------

	public void Save()
	{
		if(load)
		{
			saving = true;
			print("it IS saving..");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.data");

			PlayerData data = new PlayerData();
			data.firstStart = firstStart;
			data.r = normalPlayerColor.r;
			data.g = normalPlayerColor.g;
			data.b = normalPlayerColor.b;
			data.levelsComplete = levelsComplete;

			data.bloom = bloom;
			data.bloomIntensity = bloomIntensity;
			data.fullScreen = fullScreen;
			data.hdr = hdr;

			data.left = left;
			data.left1 = left1;
			data.left2 = left2;

			data.right = right;
			data.right1 = right1;
			data.right2 = right2;

			data.jump = jump;
			data.jump1 = jump1;
			data.jump2 = jump2;

			data.selfDestruct = selfDestruct;
			data.selfDestruct1 = selfDestruct1;
			data.selfDestruct2 = selfDestruct2;

			data.cameraMovement = cameraMovement;
			data.cameraMove1 = cameraMove1;
			data.cameraMove2 = cameraMove2;

			data.resolutionVal = resolutionVal;
			data.resolutionX = resolution.x; 
			data.resolutionY = resolution.y;

			data.masterVol = masterVol;
			data.musicVol = musicVol;
			data.soundEffectsVol = soundEffectsVol;

			data.quality = quality;

			data.currentSkin = currentSkin;

			bf.Serialize(file, data);
			file.Close ();
			saving = false;
		}
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/gameInfo.data") && !saving)
		{
			print("it IS loading..");
			load = false;
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.data", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close ();

			normalPlayerColor = new Color(data.r,data.g,data.b);
			levelsComplete = data.levelsComplete;
			firstStart = data.firstStart;

			bloom = data.bloom;
			bloomIntensity = data.bloomIntensity;
			fullScreen = data.fullScreen;
			hdr = data.hdr;

			left = data.left;
			left1 = data.left1;
			left2 = data.left2;

			right = data.right;
			right1 = data.right1;
			right2 = data.right2;

			jump = data.jump;
			jump1 = data.jump1;
			jump2 = data.jump2;

			cameraMovement = data.cameraMovement;
			cameraMove1 = data.cameraMove1;
			cameraMove2 = data.cameraMove2;

			selfDestruct = data.selfDestruct;
			selfDestruct1 =  data.selfDestruct1;
			selfDestruct2 = data.selfDestruct2;

			resolutionVal = data.resolutionVal;
			resolution.x = data.resolutionX; 
			resolution.y = data.resolutionY;

			masterVol = data.masterVol;
			musicVol = data.musicVol;
			soundEffectsVol = data.soundEffectsVol;

			quality = data.quality;

			currentSkin = data.currentSkin;

			load = true;
		}
	}
}

[Serializable]

class PlayerData
{
	public float r,g,b;
	public int levelsComplete;
	public bool bloom;
	public float bloomIntensity;
	public bool fullScreen;
	public bool hdr;
	public float resolutionX, resolutionY;
	public int resolutionVal;
	public float masterVol;
	public float musicVol;
	public float soundEffectsVol;
	public bool left, right, jump, cameraMovement, selfDestruct;
	public string left1, left2;
	public string right1, right2;
	public string jump1, jump2;
	public string cameraMove1, cameraMove2;
	public string selfDestruct1, selfDestruct2;
	public bool firstStart;
	public int quality;
	public int currentSkin;
}
