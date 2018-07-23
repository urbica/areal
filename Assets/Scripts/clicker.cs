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

	[SerializeField]
	private GameObject redLight;
	[SerializeField]
	private GameObject whiteLight;
	[SerializeField]
	private GameObject Architecture;



	public GameObject modelText;



	private List<GameObject>modelList;
	private GameObject currentModel;
	private Animator _animator;
	private Vector3 modelParentStartScale;

	private float distance;


	// Use this for initialization
	void Start () {
		modelText.SetActive(false);
		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

		//save default scale of models parent
	//	 modelParentStartScale = modelsCollection.transform.localScale;
	//	 Architecture.AddComponent<LeanScale>();
	//	 Architecture.GetComponent<LeanScale>().enabled = false;
		
	}

	void TaskOnClick()
	{
		hideCurrentModel(true);
	}

	public void OnClickPin(Transform mTransform,float resultScaleCoef){
		var id = Convert.ToInt32(mTransform.GetComponent<BoxCollider>().name);

		// float x = resultScaleCoef / 0.12f;
		// modelsCollection.transform.localScale = new Vector3(x,x,x);

		setMapActive(false);
		Debug.Log("Clicked MAZAFAJA");
		
		var prefab = Architecture.transform.GetChild(id).gameObject;
		currentModel = Instantiate(prefab);
		currentModel.transform.localPosition = mTransform.position;
		var scale = (2 * distance) / 3;
		currentModel.transform.localScale = new Vector3(scale,scale,scale);


		currentModel.AddComponent<RotateSC>();
		currentModel.AddComponent<LeanScale>();
		currentModel.GetComponent<Animator>().SetBool("showModel",true);

		if(CanvasController.isFirstSession){
			ccontroller.hide_about_pins ();
			SaveManager.Instance.Save();
			Invoke("hide_pins_EVENT",0.5f);
		} else {
			ccontroller.show_modelName_Text(getChildName(currentModel));		
		}

	}

	private void hide_pins_EVENT(){
		ccontroller.show_modelName_Text(getChildName(currentModel));	
	}

	public void hideCurrentModel(bool invokeMap){
//		_animator.SetInteger("modelAnim",0);
//		currentModel.SetActive(false);
		currentModel.GetComponent<Animator>().SetBool("showModel",false);
		ccontroller.hide_modelName_Text();
		modelText.SetActive(false);
		if(invokeMap)
			Invoke("hideModel_EVENT",0.4f);
	}
	public void hideModel_EVENT(){
//		Architecture.transform.localScale = modelParentStartScale;
//		Architecture.GetComponent<LeanScale>().enabled = false;
//		Architecture.transform.position = new Vector3(10000,10000,10000);
		GameObject.Destroy(currentModel);
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
	private string getChildName(GameObject obj){
		return obj.transform.GetChild(0).gameObject.name;
	}
	public void setDistance(float d){
		distance = d;
	}
}
