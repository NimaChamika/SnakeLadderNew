using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public Toggle t1;
	public static string audio;

	void Awake()
	{
		AudioListener.pause = false;
	}

	public void Toggle1()
	{
		if(t1.isOn)
		{
			
			AudioListener.volume = 1;
			audio="ON";
		
		}

		if(!t1.isOn)
		{
			AudioListener.volume = 0;
			audio="OFF";
		}
	}


}
