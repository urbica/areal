using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CameraPermissionChecker : MonoBehaviour {
    [SerializeField] private Image warningBoard;
	[SerializeField] private GameObject backGround_Panel;

	[SerializeField] SupCanvasController ccontroller;
	

	private bool permissionAsked = false;


    private void SampleCallback(string permissionWasGranted)
    {       
        if (permissionWasGranted == "true" )
        {
			loadScene();
        }
        else
        {
			warningBoard.gameObject.SetActive(true);
			ccontroller.hide_backGround_Panel();
        }
		SaveManager.Instance.permission_state.isPermissionAsked = true;
		SaveManager.Instance.SavePermissionRequest();
    }

	void Start () {
		permissionAsked = SaveManager.Instance.permission_state.isPermissionAsked;
		Debug.Log("permissionAsked res - " + permissionAsked);
		ccontroller.setIntroVisible(!permissionAsked);

		if (permissionAsked){
			verifyPermission();
			
			
		} else {
			
			Debug.Log("NoPermission");
		}

		
	}

	public void verifyPermission(){
		iOSCameraPermission.VerifyPermission(gameObject.name, "SampleCallback");
	}

	private void loadScene(){
		SceneManager.LoadScene("ArealMainScene",LoadSceneMode.Single);
	}
}


