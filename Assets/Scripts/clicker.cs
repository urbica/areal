using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using Mapbox.Examples;
using System;
using Lean.Touch;
using UnityEngine.Analytics;

public class clicker : MonoBehaviour {

	public Button but;
	public GameObject map;
	public CanvasController ccontroller;

	[SerializeField]
	private GameObject Architecture;

	private GameObject currentModel;

	private float distance;
	public TimeCounter timerScript;
	private string modelName;


	// Use this for initialization
	void Start () {
		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		
	}

	void TaskOnClick()
	{
		hideCurrentModel(true);
		var delta = Time.time - timerScript.time_model;
		Analytics.CustomEvent("model_event",new Dictionary<string,object>{{"model_time",delta},
																			{"model_name",modelName}});

		ccontroller.isModelScene = false;															
	}

	public void OnClickPin(Transform mTransform,float resultScaleCoef){
		timerScript.time_model = Time.time;

		var id = Convert.ToInt32(mTransform.GetComponent<BoxCollider>().name);

		setMapActive(false);
		
		var prefab = Architecture.transform.GetChild(id).gameObject;
		currentModel = Instantiate(prefab);
		currentModel.transform.localPosition = mTransform.position;
		var scale = (2 * distance) / 3;
		currentModel.transform.localScale = new Vector3(scale,scale,scale);


		currentModel.AddComponent<RotateSC>();
		currentModel.AddComponent<LeanScale>();
		currentModel.GetComponent<LeanScale>().Relative = true;
		currentModel.GetComponent<Animator>().SetBool("showModel",true);


		modelName = getChildName(currentModel);

		if(CanvasController.isFirstSession){
			ccontroller.hide_about_pins ();
			SaveManager.Instance.SaveSassionEnter();
			Invoke("hide_pins_EVENT",0.5f);
		} else {
			ccontroller.show_modelName_Text(modelName);		
		}
		ccontroller.isModelScene = true;

	}

	private void hide_pins_EVENT(){
		ccontroller.show_modelName_Text(modelName);	
	}

	public void hideCurrentModel(bool invokeMap){
		currentModel.GetComponent<Animator>().SetBool("showModel",false);
		ccontroller.hide_modelName_Text();
		if(invokeMap)
			Invoke("hideModel_EVENT",0.4f);
	}
	public void hideModel_EVENT(){
		GameObject.Destroy(currentModel);
		setMapActive(true);
		map.GetComponent<Animator>().SetInteger("mapAnimTransition",1);
	}

	private void setMapActive(bool value){
		//GameObject
		if(!value){
			map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;
			map.SetActive (value);
		} else {
			map.SetActive (value);
			map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;
		}

		//UI
		if (value) {
			ccontroller.show_reload_btn();
			ccontroller.hide_back_Button();
		} else {
			ccontroller.hide_reload_btn();
			ccontroller.show_back_Button();
		}
	}
	private string getChildName(GameObject obj){
		return obj.transform.GetChild(0).gameObject.name;
	}
	public void setDistance(float d){
		distance = d;
	}
}
