using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSC : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public float rotateSpeed = 2f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.touches.Length == 1) {
			Touch t = Input.GetTouch (0);
			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			} else if (t.phase == TouchPhase.Moved) {
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				currentSwipe = new Vector3 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				float resultSpeed = secondPressPos.magnitude - firstPressPos.magnitude;
				resultSpeed = resultSpeed < 0 ? -resultSpeed : resultSpeed;
				resultSpeed = resultSpeed > rotateSpeed ? rotateSpeed : resultSpeed;

				currentSwipe.Normalize ();

				if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					transform.Rotate (new Vector3 (0,resultSpeed,0));
				}
				//swipe right
				if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					transform.Rotate (new Vector3 (0,-resultSpeed,0));
				}
			}
		}
	}
}
