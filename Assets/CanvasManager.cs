using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private Canvas main,info;
	private const string animatorContreollerParametr = "info_state";

	public void switchCanvas(){
		bool isMainVisible = main.gameObject.activeInHierarchy;
		bool infoTransitionState = isMainVisible ? true : false;
		if (isMainVisible){
			main.gameObject.SetActive(!isMainVisible);
			info.gameObject.SetActive(isMainVisible);

		} else {
			Invoke("returnMainCanvas",0.5f);
		}
		info.GetComponent<Animator>().SetBool(animatorContreollerParametr,infoTransitionState);
	}
	private void returnMainCanvas(){
		info.gameObject.SetActive(false);
		main.gameObject.SetActive(true);
	}


}
