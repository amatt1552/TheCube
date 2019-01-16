using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour 
{
	int levelsComplete;
	int currentLevel;
	bool levelComplete;
	public static AsyncOperation aSync;
	public GameObject[] levels;
	public int level;
	public static LevelSettings GC;

	void Awake() 
	{
		GC = this.GetComponent<LevelSettings>();
		currentLevel = SceneManager.GetActiveScene().buildIndex;
		levelsComplete = Settings.levelsComplete;
	}

	void Start()
	{
		if(currentLevel == 0)
		{

			for(int i = 0; i < levels.Length; )
			{
				levels[i].GetComponent<Button>().interactable = false;
				if(i + 3 <= levelsComplete)
				{
					levels[i].GetComponent<Button>().interactable = true;
				}
				i++;
			}
		}
	}
	
	void Update () 
	{
		
		if(levelsComplete < currentLevel)
		{
			levelsComplete = currentLevel;
			Settings.levelsComplete = levelsComplete;
			Settings.control.Save();
		}
	}

	public void LoadNextLevel()
	{
		
			if(currentLevel == 1 || currentLevel >= SceneManager.sceneCountInBuildSettings)
			{
				
				aSync = SceneManager.LoadSceneAsync (0);
			}
			else
			{
				
				aSync = SceneManager.LoadSceneAsync (currentLevel + 1);
			}

	}

	public void LoadLevel(int levelIndex)
	{
		
		aSync = SceneManager.LoadSceneAsync(levelIndex);

	}

	public void NewGame()
	{
		
		Settings.levelsComplete = 2;
		levelsComplete = 2;
		Settings.control.Save();
		aSync = SceneManager.LoadSceneAsync(levelsComplete);

	}

	public void Continue()
	{
		
		aSync = SceneManager.LoadSceneAsync(levelsComplete);

	}

	public void Restart()
	{
		
		aSync = SceneManager.LoadSceneAsync(currentLevel);

	}

	public void LevelComplete()
	{
		Invoke("LoadNextLevel", 2);
		ColorFade.GC.fadeTime = 0.5f;
		ColorFade.GC.startInv = true;
	}

	public void SetLevelsComplete(int levelsComplete)
	{
		this.levelsComplete = levelsComplete;
	}

	//return methods
	public bool isLevelComplete()
	{
		return levelComplete;
	}
	public bool LoadingComplete()
	{
		return aSync.isDone;
	}

	public float LoadingProgress()
	{
		return aSync.progress;
	}
		
	public int GetCurrentLevel()
	{
		return currentLevel;
	}

	public int GetLevelsCompleted()
	{
		return levelsComplete;
	}
}
