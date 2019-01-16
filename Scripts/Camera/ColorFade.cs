using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorFade : MonoBehaviour
{
	#region public variables
	public static ColorFade GC; 
	public Color startColor;
	public Color newColor;
	public float fadeTime;
	public bool start;
	public bool startInv;
	public bool finished;
	bool flipped;

	#endregion	

	#region private variables

	Image image;
	float r,g,b,a;

	#endregion	

	void Awake()
	{
		GC = GetComponent<ColorFade>();
		image = gameObject.GetComponent<Image>();

		image.color = startColor;
		r = image.color.r;
		g = image.color.g;
		g = image.color.b;
		a = image.color.a;
	}

	void FixedUpdate ()	
	{

		image.color = new Color(r, g, b, a);

		if(start)
		{
			Fade ();
			print ("starting");
		}
		if(startInv)
		{
			FadeInv ();
			print ("starting");
		}
		if(Mathf.Abs(image.color.r - newColor.r) < .1 && Mathf.Abs(image.color.g - newColor.g) < .1 && Mathf.Abs(image.color.b - newColor.b) < .1 && Mathf.Abs(image.color.a - newColor.a) < .1)
		{
			image.color = newColor;
			finished = true;
			start = false;
			print ("finished");
		}
		else
		{
			finished = false;
		}

		Limits();
	}

	void Limits()
	{
		if(r <= 0)
		{
			r = 0;
		}
		if(g <= 0)
		{
			g = 0;
		}
		if(b <= 0)
		{
			b = 0;
		}
		if(a <= 0)
		{
			a = 0;
		}

		if(r >= 1)
		{
			r = 1;
		}
		if(g >= 1)
		{
			g = 1;
		}
		if(b >= 1)
		{
			b = 1;
		}
		if(a >= 1)
		{
			a = 1;
		}
	}

	public void Fade()
	{
		float fadeTime = this.fadeTime;
		if(r > newColor.r)
		{
			r -= fadeTime * Time.deltaTime;
		}
		if(g > newColor.g)
		{
			g -= fadeTime * Time.deltaTime;
		}
		if(b > newColor.b)
		{
			b -= fadeTime * Time.deltaTime;
		}
		if(a > newColor.a)
		{
			a -= fadeTime * 1.5f * Time.deltaTime;

		}

		if(r < newColor.r)
		{
			r += fadeTime * Time.deltaTime;
		}
		if(g < newColor.g)
		{
			g += fadeTime * Time.deltaTime;
		}
		if(b < newColor.b)
		{
			b += fadeTime * Time.deltaTime;
		}
		if(a < newColor.a)
		{
			a += fadeTime *  1.5f * Time.deltaTime;

		}

	}

	public void FadeInv()
	{
		float fadeTime = this.fadeTime;
		Color temp;
		if(!flipped)
		{
			finished = false;
			temp = startColor;
			startColor = newColor;
			newColor = temp;
			flipped = true;
		}

		if(r < newColor.r)
		{
			r -= fadeTime * Time.deltaTime;
		}
		if(g > newColor.g)
		{
			g -= fadeTime * Time.deltaTime;
		}
		if(b > newColor.b)
		{
			b -= fadeTime * Time.deltaTime;
		}
		if(a > newColor.a)
		{
			a -= fadeTime * Time.deltaTime;
		}

		if(r < newColor.r)
		{
			r += fadeTime * Time.deltaTime;
		}
		if(g < newColor.g)
		{
			g += fadeTime * Time.deltaTime;
		}
		if(b < newColor.b)
		{
			b += fadeTime * Time.deltaTime;
		}
		if(a < newColor.a)
		{
			a += fadeTime * Time.deltaTime;
		}

	}
}