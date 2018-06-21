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

	public GameObject modelText;

	private List<GameObject>pinsList;


	public GameObject modelsCollection;
	private List<GameObject>modelList;
	private GameObject currentModel;
	private Animator _animator;
	private bool enterState;
	private Vector3 modelParentStartScale;


	// Use this for initialization
	void Start () {
		enterState = true;
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

	public void OnClickPin(Transform transform){
		modelsCollection.GetComponent<LeanScale>().enabled = true;
		Vector3 mapPosition = map.transform.position;
		modelsCollection.transform.localPosition = transform.position;
		setMapActive(false);
//		currentModel = Instantiate(modelsCollection.transform.GetChild(Convert.ToInt32(id)).gameObject);


		int currentid = Convert.ToInt32(transform.GetComponent<BoxCollider>().name);
		currentModel = modelsCollection.transform.GetChild(currentid).gameObject;
		
		
		currentModel.transform.position = transform.position;
		Debug.Log("FindPosition position:" +currentModel.transform.position + " local:" + transform.localPosition );
	//	setTextModelPosition(modelText.transform.position, currentModel.transform.localScale.z);

		modelText.transform.GetChild(0).GetComponent<TextMesh>().text = currentModel.name;

		enterState = true;
		_animator.SetInteger("modelAnim",currentid + 1);
	//	currentModel.AddComponent<LeanScale>();
		currentModel.AddComponent<RotateSC>();

		ccontroller.hide_about_pins ();
		ccontroller.hide_reload_btn();

		ccontroller.show_back_Button ();
//		ccontroller.setModelName(currentModel.name);
//		ccontroller.show_about_model();

		//save enter
		SaveManager.Instance.Save();

	}

	public void hideCurrentModel(bool invokeMap){
		_animator.SetInteger("modelAnim",0);
//		ccontroller.hide_about_model();
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
		map.SetActive (value);
		map.transform.parent.gameObject.GetComponent<LeanScale>().enabled = value;

		if(value){
			ccontroller.show_reload_btn();
			ccontroller.hide_back_Button();
		}else{
			ccontroller.hide_reload_btn();
			ccontroller.show_back_Button();
		}

	}

	private void setTextModelPosition(Vector3 currentModelPosition,float zScale){
		float x = currentModelPosition.x;
		float y = currentModelPosition.y;
		float z = currentModelPosition.z;
		modelText.SetActive(true);
		modelText.transform.localPosition = new Vector3(x,y,z);


	}

}
