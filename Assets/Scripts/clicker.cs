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
	public CanvasController ccontroller;

	public RotateSC rotateScript;

	public GameObject modelText;

	private List<GameObject>pinsList;


	public GameObject modelsCollection;
	private List<GameObject>modelList;
	private GameObject currentModel;
	private Animator _animator;
	private Vector3 modelParentStartScale;


	// Use this for initialization
	void Start () {
		modelText.SetActive(false);
		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		_animator = modelsCollection.GetComponent<Animator>();

		//save default scale of models parent
		modelParentStartScale = modelsCollection.transform.localScale;
		modelsCollection.AddComponent<LeanScale>();
		modelsCollection.GetComponent<LeanScale>().enabled = false;
		
	}

	public void setPinsList(List<GameObject> list){
		pinsList = list;
	}
	void TaskOnClick()
	{
		hideCurrentModel(true);
		
	}

	public void OnClickPin(Transform mTransform,float resultScaleCoef){
		modelsCollection.GetComponent<LeanScale>().enabled = true;
		Vector3 mapPosition = map.transform.position;
		modelsCollection.transform.localPosition = mTransform.position;
		float x = resultScaleCoef / 0.12f;
		modelsCollection.transform.localScale = new Vector3(x,x,x);

		setMapActive(false);
//		currentModel = Instantiate(modelsCollection.transform.GetChild(Convert.ToInt32(id)).gameObject);


		int currentid = Convert.ToInt32(mTransform.GetComponent<BoxCollider>().name);
		currentModel = modelsCollection.transform.GetChild(currentid).gameObject;
		
		
		currentModel.transform.position = mTransform.position;

		modelText.transform.GetChild(0).GetComponent<TextMesh>().text = currentModel.name;

		_animator.SetInteger("modelAnim",currentid + 1);
		currentModel.AddComponent<RotateSC>();


		
		if(CanvasController.isFirstSession){
			ccontroller.hide_about_pins ();
			SaveManager.Instance.Save();
			Invoke("hide_pins_EVENT",0.5f);
		} else {
			ccontroller.show_modelName_Text(currentModel.name);		
		}

	}
	private void hide_pins_EVENT(){
		ccontroller.show_modelName_Text(currentModel.name);	
	}

	public void hideCurrentModel(bool invokeMap){
		_animator.SetInteger("modelAnim",0);
		ccontroller.hide_modelName_Text();
		modelText.SetActive(false);
		if(invokeMap)
			Invoke("hideModel_EVENT",0.4f);
	}
	public void hideModel_EVENT(){
		modelsCollection.transform.localScale = modelParentStartScale;
		modelsCollection.GetComponent<LeanScale>().enabled = false;
		setMapActive(true);
		map.GetComponent<Animator>().SetInteger("mapAnimTransition",1);
	}

	private void setMapActive(bool value){
		if(!value){
			map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;
			map.SetActive (value);
		} else {
			map.SetActive (value);
			map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;
		}


		if (value) {
			ccontroller.show_reload_btn();
			ccontroller.hide_back_Button();
		} else {
			ccontroller.hide_reload_btn();
			ccontroller.show_back_Button();
		}
	}
}
