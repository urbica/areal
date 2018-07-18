using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour {
    public float RotationSpeed = 900;
    bool Forward = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime * ((Forward == true) ? 1 : (-1)));
	}
}
