using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private Canvas main,info,capture;
	private const string animatorControllerParametr = "info_state";
	private const string mainCanvasButtonsExitAnimatorParametr = "info_state_enter";

	public void switchCanvas(){
		bool isMainVisible = main.gameObject.activeInHierarchy;
		bool infoTransitionState = isMainVisible ? true : false;
		if (isMainVisible){
			var anim = main.GetComponent<Animator>();
			anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,1);
			Invoke("turnOfMain",0.5f);

		} else {
			Invoke("returnMainCanvas",0.5f);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,false);
		}

	}
	private void returnMainCanvas(){
		info.gameObject.SetActive(false);
		main.gameObject.SetActive(true);
		var anim = main.GetComponent<Animator>();
		
		anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,2);
	}
	private void turnOfMain(){
			main.gameObject.SetActive(false);
			info.gameObject.SetActive(true);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,true);
	}

	public void showCaptureCanvas(){
		main.gameObject.SetActive(false);
		capture.gameObject.SetActive(true);
	}
}
