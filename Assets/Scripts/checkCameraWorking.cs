using UnityEngine;


public class checkCameraWorking {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("firstSession18")){
			Debug.Log("FindSession is first");
		} else {
			Debug.Log("FindSession Not first");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
