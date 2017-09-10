using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
	private bool _isStarted,_isStarted2;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
	public GameObject TurnButton;
    public GameObject Circle; 
	public static bool startWalk=false;
	public static bool startWalk2=false;
	public static int diceValue = 0;




    public void TurnWheel ()
    {
    	// Player has enough money to turn the wheel

		TurnButton.SetActive (false);
		_currentLerpRotationTime = 0f;
    	
    	    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
		_sectorsAngles = new float[] { 60, 120,180, 240,300,360 };

    	int fullCircles = 5;
    	float randomFinalAngle = _sectorsAngles [UnityEngine.Random.Range (0, _sectorsAngles.Length)];
    	
    	    // Here we set up how many circles our wheel should rotate before stop
    	 _finalAngle = -(fullCircles * 360 + randomFinalAngle);
    	 _isStarted = true;
    
    	 
    	    // Animate coins
			//GiveAwardByAngle ();
    	
    }


	public void TurnWheel2 ()
	{
		// Player has enough money to turn the wheel

		_currentLerpRotationTime = 0f;

		// Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
		_sectorsAngles = new float[] { 60, 120,180, 240,300,360 };

		int fullCircles = 5;
		float randomFinalAngle = _sectorsAngles [UnityEngine.Random.Range (0, _sectorsAngles.Length)];

		// Here we set up how many circles our wheel should rotate before stop
		_finalAngle = -(fullCircles * 360 + randomFinalAngle);
		_isStarted2 = true;


		// Animate coins
		//GiveAwardByAngle ();

	}






    private void GiveAwardByAngle ()
    {
    	// Here you can set up rewards for every sector of wheel
    	switch ((int)_startAngle) {
		case 0:
			
			//cm.Spinwheel (1);
			diceValue = 1;
			startWalk = true;
    	    break;
		case -300:
			//cm.Spinwheel (2);
			diceValue = 2;
			startWalk = true;
			break;
		case -240:
			//cm.Spinwheel (3);
			diceValue = 3;
			startWalk = true;
			break;
		case -180:
			//cm.Spinwheel (4);
			diceValue = 4;
			startWalk = true;
			break;
		case -120:
			//cm.Spinwheel (5);
			diceValue = 5;
			startWalk = true;
			break;
		case -60:
			//cm.Spinwheel (6);
			diceValue = 6;
			startWalk = true;
			break;

        }
    }

	private void GiveAwardByAngle2 ()
	{
		// Here you can set up rewards for every sector of wheel
		switch ((int)_startAngle) {
		case 0:

			//cm.Spinwheel (1);
			diceValue = 1;
			startWalk2 = true;
			break;
		case -300:
			//cm.Spinwheel (2);
			diceValue = 2;
			startWalk2 = true;
			break;
		case -240:
			//cm.Spinwheel (3);
			diceValue = 3;
			startWalk2 = true;
			break;
		case -180:
			//cm.Spinwheel (4);
			diceValue = 4;
			startWalk2 = true;
			break;
		case -120:
			//cm.Spinwheel (5);
			diceValue = 5;
			startWalk2 = true;
			break;
		case -60:
			//cm.Spinwheel (6);
			diceValue = 6;
			startWalk2 = true;
			break;

		}
	}




    void Update ()
    {
//    	if (!_isStarted)
//    	    return;
//
//    	float maxLerpRotationTime = 4f;
//    
//    	// increment timer once per frame
//    	_currentLerpRotationTime += Time.deltaTime;
//    	if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
//    	    _currentLerpRotationTime = maxLerpRotationTime;
//    	    _isStarted = false;
//    	    _startAngle = _finalAngle % 360;
//			GiveAwardByAngle ();
//    	   
//    	}
//    
//    	// Calculate current position using linear interpolation
//    	float t = _currentLerpRotationTime / maxLerpRotationTime;
//    
//    	// This formulae allows to speed up at start and speed down at the end of rotation.
//    	// Try to change this values to customize the speed
//    	t = t * t * t * (t * (6f * t - 15f) + 10f);
//    
//    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
//    	Circle.transform.eulerAngles = new Vector3 (0, 0, angle);

		if(_isStarted)
		{
			float maxLerpRotationTime = 4f;
	    
	    	// increment timer once per frame
	    	_currentLerpRotationTime += Time.deltaTime;
	    	if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
	    	    _currentLerpRotationTime = maxLerpRotationTime;
	    	    _isStarted = false;
	    	    _startAngle = _finalAngle % 360;
				GiveAwardByAngle ();
	    	   
	    	}
	    
	    	// Calculate current position using linear interpolation
	    	float t = _currentLerpRotationTime / maxLerpRotationTime;
	    
	    	// This formulae allows to speed up at start and speed down at the end of rotation.
	    	// Try to change this values to customize the speed
	    	t = t * t * t * (t * (6f * t - 15f) + 10f);
	    
	    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
	    	Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
		}

		if(_isStarted2)
		{
			float maxLerpRotationTime = 4f;

			// increment timer once per frame
			_currentLerpRotationTime += Time.deltaTime;
			if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
				_currentLerpRotationTime = maxLerpRotationTime;
				_isStarted2 = false;
				_startAngle = _finalAngle % 360;
				GiveAwardByAngle2 ();
			}

			// Calculate current position using linear interpolation
			float t = _currentLerpRotationTime / maxLerpRotationTime;

			// This formulae allows to speed up at start and speed down at the end of rotation.
			// Try to change this values to customize the speed
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
			Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
		}


    }

   
}
