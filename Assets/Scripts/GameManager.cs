using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

	public GameObject player;
	private FortuneWheelManager fm;
	public Camera camera1;
	public Camera camera2;
	public static bool changecamera;

	void Start()
	{
		 fm= player.GetComponent<FortuneWheelManager> ();
		camera1.enabled = true;
		camera2.enabled= false;
		changecamera = false;
	}




	void Update()
	{
		if(changecamera)
		{
			if (!CharacterMovement.changeturn) {
				camera1.enabled = true;
				camera2.enabled= false;
				changecamera = false;
			}
			else 
			{
				camera1.enabled = false;
				camera2.enabled= true;
				StartCoroutine (Turnwheel2 ());
				changecamera=false;
			}
		}
	}

	IEnumerator Turnwheel2()
	{
		yield return new WaitForSeconds (1f);
		fm.TurnWheel2 ();
	}

}
