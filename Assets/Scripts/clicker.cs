using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using Mapbox.Examples;

public class clicker : MonoBehaviour {

	public Button but;
	public GameObject map;
	public GameObject originObj;
	public CanvasController ccontroller;


	// Use this for initialization
	void Start () {

		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
		{
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit raycastHit;

			if (Physics.Raycast(raycast, out raycastHit) ){

				if (raycastHit.collider.name == "Sphere") {

	//				OnClickPin ();

//					map.SetActive (false);
//					originObj.SetActive (true);
//					originObj.transform.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
//					originObj.transform.position = map.transform.position;
//
//					ccontroller.show_back_Button ();
//					ccontroller.show_about_model ();
//					ccontroller.show_info_btn ();
//
//					ccontroller.hide_about_pins ();
//					ccontroller.hide_reload_btn ();
				} 

			}
		}

	}

	void TaskOnClick()
	{
		map.SetActive(true);
//		map.transform.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
		switchPins (true);

		originObj.transform.localScale = new Vector3 (0,0,0);
		ccontroller.hide_about_model ();
		ccontroller.hide_back_Button ();
		ccontroller.hide_info_btn ();

		ccontroller.show_reload_btn ();
	}

	public void OnClickPin(List<GameObject> list){
		switchPins (false);

//		foreach (GameObject obj in pinsArray ) {
//			switchPins (false);
//		}

		map.SetActive (false);
//		map.transform.localScale = new Vector3 (0, 0, 0);
		originObj.SetActive (true);
		originObj.transform.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
		originObj.transform.position = map.transform.position;

		ccontroller.show_back_Button ();
//		ccontroller.show_about_model ();
//		ccontroller.show_info_btn ();

		ccontroller.hide_about_pins ();
//		ccontroller.hide_reload_btn ();
	}

	private void switchPins(bool value){
		SpawnOnMap script = map.GetComponent<SpawnOnMap> ();
		if (!value)
			script.setPinsScale (0);
		else
			script.setPinsScale (0.02f);
	}
}
