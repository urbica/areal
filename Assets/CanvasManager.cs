using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private Canvas main,info;
	private const string animatorControllerParametr = "info_state";
	private const string mainCanvasButtonsExitAnimatorParametr = "info_state_enter";

	public void switchCanvas(){
		bool isMainVisible = main.gameObject.activeInHierarchy;
		bool infoTransitionState = isMainVisible ? true : false;
		if (isMainVisible){
			var anim = main.GetComponent<Animator>();
			anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,1);
			Invoke("turnOfMain",0.5f);

// anim.Play("exit_buttons",anim.GetLayerIndex("info_exit"));
//			main.GetComponent<Animator>().SetInteger(mainCanvasButtonsExitAnimatorParametr,1);			
			// main.gameObject.SetActive(!isMainVisible);
			// info.gameObject.SetActive(isMainVisible);

		} else {
			Invoke("returnMainCanvas",0.5f);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,false);
		}

	}
	private void returnMainCanvas(){
		info.gameObject.SetActive(false);
		main.gameObject.SetActive(true);
		var anim = main.GetComponent<Animator>();
//		main.GetComponent<Animator>().SetInteger(mainCanvasButtonsExitAnimatorParametr,0);
//		anim.Play("appear_buttons",anim.GetLayerIndex("info_exit"));
		
		anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,2);
	}
	private void turnOfMain(){
			main.gameObject.SetActive(false);
			info.gameObject.SetActive(true);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,true);
	}
}
