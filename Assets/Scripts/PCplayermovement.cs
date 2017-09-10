using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PCplayermovement : MonoBehaviour {

	private Vector3 startMarker,endMarker,nextMarker;

	List<Vector3> snakePosition = new List<Vector3>();
	List<Vector3> snakeendPosition = new List<Vector3>();
	List<Vector3> ladderStarts = new List<Vector3>();
	List<Vector3> ladderEnds = new List<Vector3>();

	List<int> snakescore = new List<int>();
	List<int> ladderscore = new List<int>();

	public float speed;
	public Animator Boy;
	public GameObject boyMesh;
	public GameObject gameBoard;
	public GameObject turnButton;
	private GameObject FinalCube, climbCube;
	private int diceValue;
	private int score=0;
	private bool check=false;
	private bool checkclimb=false;
	private bool checkclimbcube=false;
	private AudioSource appluase;
	//
	private bool checkladder=false;
	private bool positionCheck=false;
	private int y;
	private bool turnChanger=false;

	// // // // UI
	public GameObject smileimage;
	public GameObject sadimage;
	public GameObject gamewonpanel;
	public Text wonText;



	void Start() {

		endMarker = new Vector3 (100,0,0);

		//Initializing audio source
		appluase = GetComponent<AudioSource>();
		//Initializing snake positions
		snakePosition.Add(new Vector3(10,2,2));
		snakeendPosition.Add(new Vector3(8,0,0));
		snakescore.Add (5);
		snakePosition.Add(new Vector3(8,8,8));
		snakeendPosition.Add(new Vector3(10,4,4));
		snakescore.Add (26);
		snakePosition.Add(new Vector3(12,10,10));
		snakeendPosition.Add(new Vector3(18,6,6));
		snakescore.Add (31);
		snakePosition.Add(new Vector3(10,14,14));
		snakeendPosition.Add(new Vector3(6,8,8));
		snakescore.Add (44);
		snakePosition.Add(new Vector3(12,18,18));
		snakeendPosition.Add(new Vector3(18,14,14));
		snakescore.Add (71);
		snakePosition.Add(new Vector3(6,18,18));
		snakeendPosition.Add(new Vector3(4,12,12));
		snakescore.Add (63);


		//Initializing ladder positions
		ladderStarts.Add (new Vector3 (6, 0, 0));
		ladderEnds.Add (new Vector3 (6, 2, 2));
		ladderscore.Add (17);
		ladderStarts.Add (new Vector3 (14, 2, 2));
		ladderEnds.Add (new Vector3 (14, 6, 6));
		ladderscore.Add (33);
		ladderStarts.Add (new Vector3 (2, 4, 4));
		ladderEnds.Add (new Vector3 (2, 12, 12));
		ladderscore.Add (62);
		ladderStarts.Add (new Vector3 (14,12,12));
		ladderEnds.Add (new Vector3 (14, 16, 16));
		ladderscore.Add (88);

	}
	void Update() {


		if(FortuneWheelManager.startWalk2)
		{
			SpinMe ();
			FortuneWheelManager.startWalk2 = false;
			turnChanger = false;
		}

		if(check)
		{
			transform.Translate (0, 0, speed);
			Boy.SetBool ("Walk",true);

		}

		if(checkclimb)
		{
			transform.Translate (0, 0.006f, 0);
			if (nextMarker.y  < transform.position.y+0.1f && nextMarker.y > transform.position.y-0.1f)
			{
				checkclimb = false;
				Boy.SetBool ("Fclimb", false); 
				check = true;
				checkclimbcube = true;
			}
		}

		if(checkclimbcube)
		{
			CheckClimbCube();
		}



		if(checkladder){
			Boy.SetBool ("Lclimb",true);
			if( endMarker.y < transform.position.y+2.2f && endMarker.y > transform.position.y-2.2f)
			{

				transform.Translate (0, 0.016f, 0);
				if (transform.position.y >= (endMarker.y)) {
					Boy.SetBool ("Lclimb",false);
					checkladder = false;
					transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
					check = true;
					positionCheck = true;
				}
			}
			else
			{

				transform.Translate (0, 0.016f, 0);
				StartCoroutine (ChangeAngleOnLadder());
				if (transform.position.y >= (endMarker.y)) {
					Boy.SetBool ("Lclimb",false);
					checkladder = false;
					transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
					check = true;
					positionCheck = true;
				}
			}

		}

		WalkMe ();



	}




	void WalkMe()
	{
		if (transform.position.x > (endMarker.x-0.2f) && transform.position.x < (endMarker.x+0.2f) && transform.position.z < (endMarker.z+0.5f) && transform.position.z > (endMarker.z-0.5f) ) 
		{

			transform.position=endMarker;
			check = false;
			Boy.SetBool ("Walk",false);

			if (snakePosition.Contains (endMarker)) {
				int x = snakePosition.IndexOf (endMarker);
				endMarker = snakeendPosition [x];
				score = snakescore [x];
				sadimage.SetActive (true);
				StartCoroutine (SpawnAfterSnakeBite ());
				//ps1.Play ();
				//StartCoroutine (SpawnAfterSnakeBite());
				//Debug.Log (endMarker);
			} else if (ladderStarts.Contains (endMarker)) {

				int x = ladderStarts.IndexOf (endMarker);
				endMarker = ladderEnds [x];
				score = ladderscore [x];
				smileimage.SetActive (true);
				appluase.Play ();
				StartCoroutine (LadderWalk ());

			}
			else
			{
				if (!turnChanger) {
					StartCoroutine (ChangePlayerTurn());
					turnChanger = true;
				}
					

			}

			if (positionCheck) {
				int posy = (int)endMarker.y/2;
				if (posy % 2 == 0) {
					transform.rotation = Quaternion.AngleAxis (90, Vector3.up);
				} else {
					transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
				}
				positionCheck = false;
			}

		}			

	}


	IEnumerator LadderWalk()
	{
		yield return new WaitForSeconds (2f);
		smileimage.SetActive (false);
		transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
		transform.rotation = Quaternion.AngleAxis(15, Vector3.right);

		checkladder = true;

	}

	public void SpinMe()
	{
		diceValue = FortuneWheelManager.diceValue;
		int x = score + diceValue;

		if (x <= 100) 
		{
			score += diceValue;
			FinalCube = gameBoard.transform.FindChild ("Cube" + score).gameObject;
			endMarker = FinalCube.transform.position;
			endMarker = new Vector3 (endMarker.x, endMarker.y + 1, endMarker.z);

			check = true;
		}
		else 
		{
			turnChanger = false;
			if (!turnChanger) {
				StartCoroutine (ChangePlayerTurn());
				turnChanger = true;
			}
		}

	}

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag=="cptype1")
		{
			check = false;
			transform.position = other.transform.position;
			Boy.SetBool ("Walk",false);
			transform.rotation = Quaternion.AngleAxis(0, Vector3.up);

			if (transform.position == endMarker) {
				check = false;
				Boy.SetBool ("Walk", false);
			} 
			else
			{
				check = true;
			}

		}

		if(other.gameObject.tag=="cptype2")
		{

			climbCube = other.gameObject;
			check = false;
			Boy.SetBool ("Walk",false);
			Boy.SetBool ("Fclimb",true);

			nextMarker = climbCube.transform.position;
			nextMarker = new Vector3 (nextMarker.x,nextMarker.y+1,nextMarker.z);

			checkclimb = true;
			//StartCoroutine(StopFreeClimb());
		}

		if(other.gameObject.tag=="cptype3")
		{
			transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
			check = false;
			Boy.SetBool ("Walk",false);
			Boy.SetBool ("Dance",true);

			wonText.text = "PC Won!";
			gamewonpanel.SetActive  (true);


		}


	}




	void CheckClimbCube()
	{


		if (transform.position.z > (nextMarker.z - 0.2f) && transform.position.z < (nextMarker.z + 0.2f)) 
		{
			transform.position=nextMarker;

			Boy.SetBool ("Walk",false);
			checkclimbcube = false;
			if(transform.position.x==18)
			{
				transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
			}
			else
			{
				transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
			}

		}
	}


	IEnumerator SpawnAfterSnakeBite()
	{
		yield return new WaitForSeconds (0.5f);
		boyMesh.SetActive (false);
		yield return new WaitForSeconds (2f);
		sadimage.SetActive (false);

		if (endMarker.z == 0 || endMarker.z == 8 || endMarker.z ==4 || endMarker.z==12) {
			transform.rotation = Quaternion.AngleAxis (90, Vector3.up);
			transform.position = endMarker;
			boyMesh.SetActive (true);

		}
		else 
		{
			transform.rotation = Quaternion.AngleAxis (-90, Vector3.up);
			transform.position = endMarker;
			boyMesh.SetActive (true); 
		}
		yield return new WaitForSeconds (1f);



	}

	IEnumerator ChangeAngleOnLadder()
	{
		yield return new WaitForSeconds (1.5f);
		transform.rotation = Quaternion.AngleAxis(35, Vector3.right);
	}

	IEnumerator ChangePlayerTurn()
	{
		yield return new WaitForSeconds (2f);
		endMarker = new Vector3 (100,0,0);
		CharacterMovement.changeturn = false;
		GameManager.changecamera = true;
		turnButton.SetActive (true);
	}

}