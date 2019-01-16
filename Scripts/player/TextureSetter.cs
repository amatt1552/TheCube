using UnityEngine;
using System.Collections;
using System.IO;

public class TextureSetter : MonoBehaviour 
{
	Texture[] textures;

	public static Texture selectedTexture;

	void Awake()
	{
		
		textures = (Texture[])Resources.LoadAll("PlayerSkins");
	
	} 

	public void SetTexture(int currentTexture)
	{
		selectedTexture = textures[currentTexture];
	}

}
