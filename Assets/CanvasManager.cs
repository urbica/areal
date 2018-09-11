using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private Canvas main,info,capture;
	private const string animatorControllerParametr = "info_state";
	private const string mainCanvasButtonsExitAnimatorParametr = "info_state_enter";

	[SerializeField]
	private ShareFun shareScript;
	

	private int currentState = 0;

	public static bool SCENE_UNDER_CANVAS = false;

	public void switchCanvas(){
		bool isMainVisible = main.gameObject.activeInHierarchy;
		bool infoTransitionState = isMainVisible;

		if (isMainVisible){
			SCENE_UNDER_CANVAS = true;
			var anim = main.GetComponent<Animator>();
			if(SaveManager.Instance.session_state.isFirstEnter){
				
				checkState();
				anim.SetInteger("CanvasState",0);
			}			
			anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,1);
			Invoke("turnOfMain",0.5f);
			
		} else {
			SCENE_UNDER_CANVAS = false;
			Invoke("returnMainCanvas",0.5f);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,false);
		}
	}
	private void returnMainCanvas(){
		info.gameObject.SetActive(false);
		main.gameObject.SetActive(true);
		var anim = main.GetComponent<Animator>();
		
		anim.SetInteger(mainCanvasButtonsExitAnimatorParametr,2);
		if(SaveManager.Instance.session_state.isFirstEnter)
			anim.SetInteger("CanvasState",currentState);
	}
	private void turnOfMain(){
			main.gameObject.SetActive(false);
			info.gameObject.SetActive(true);
			info.GetComponent<Animator>().SetBool(animatorControllerParametr,true);

	}

	public void switchCaptureCanvas(bool show){
		if(show){
			main.GetComponent<CanvasController>().close_share();
			
		} 
		// else {
		// 	switchCanvases(show);
		// }
		switchCanvases(show);
		

	}

	public void switchCanvases(bool show){
		main.gameObject.SetActive(!show);
		capture.gameObject.SetActive(show);
		if (show){
			shareScript.showScreenShot();
		} else if (SaveManager.Instance.session_state.isFirstEnter && !main.GetComponent<CanvasController>().isModelScene) {
			main.GetComponent<CanvasController>().show_about_pins();
		}
	}


	private void checkState(){
		
			CanvasController ccontroller = main.gameObject.GetComponent<CanvasController>();
			if (ccontroller.find_surface_Panel.gameObject.activeInHierarchy){
				currentState = 2;
			}
			else if(ccontroller.about_map_Panel.gameObject.activeInHierarchy){
				currentState = 4;			
			}
			else if (ccontroller.about_pins_Panel.gameObject.activeInHierarchy){
				currentState = 6;
			}	
		
	}
}
