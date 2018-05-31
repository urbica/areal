using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using Mapbox.Examples;
using System;
using Lean.Touch;

public class clicker : MonoBehaviour {

	public Button but;
	public GameObject map;
	public GameObject originObj;
	public CanvasController ccontroller;

	public RotateSC rotateScript;

	private List<GameObject>pinsList;


	public GameObject modelsCollection;
	private List<GameObject>modelList;
	private GameObject currentModel;


	// Use this for initialization
	void Start () {

		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void TaskOnClick()
	{
		SpawnOnMap script = map.GetComponent<SpawnOnMap> ();
		script.switchPins(true);
		
		setMapActive(true);
		destroyCurrentModel();

		ccontroller.hide_back_Button ();
		ccontroller.hide_info_btn ();

	}

	public void OnClickPin(List<GameObject> list,string id){
		pinsList = list;
		setMapActive(false);
		currentModel = Instantiate(modelsCollection.transform.GetChild(Convert.ToInt32(id)).gameObject);
		
		currentModel.transform.position = map.transform.position;
		currentModel.AddComponent<LeanScale>();
		currentModel.AddComponent<RotateSC>();

		ccontroller.show_back_Button ();
		ccontroller.hide_about_pins ();
	}

	public void destroyCurrentModel(){
		Destroy(currentModel);
	}

	private void setMapActive(bool value){
		map.SetActive (value);
		map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;
	}
}
