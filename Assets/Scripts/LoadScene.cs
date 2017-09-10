using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

	public Text scene;


	void Awake()
	{
		scene.text="Game";
	}

	public void LoadGame()
	{
		SceneManager.LoadScene (scene.text);
	
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene ("Menu");

	}

	public void setText(Text t)
	{
		scene.text = t.text;
	}


}
