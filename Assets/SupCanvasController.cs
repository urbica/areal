using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;



public class SupCanvasController : MonoBehaviour {

	// Use this for initialization
	[SerializeField] GameObject util;
	[SerializeField] GameObject intro_Panel;

	[SerializeField] GameObject upPanel, botPanel,backGround;

	[SerializeField] Text warningTxt, linkTxt;
	[SerializeField] Button settingsButton;

	void Start(){
				if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadPro10Inch1Gen || 
		UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadPro10Inch2Gen || 
		UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad5Gen){
			upPanel.transform.position += new Vector3(0,400,0);
			botPanel.transform.position += new Vector3(0,400,0);
		} else if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadPro2Gen ||
		UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadPro1Gen){
			upPanel.transform.position += new Vector3(0,450,0);
			botPanel.transform.position += new Vector3(0,450,0);
		}
	}


	public void setIntroVisible(bool visibility){
		intro_Panel.SetActive(visibility);
	}

	public void close_Intro(){
		GetComponent<Animator>().SetBool("showIntro", true);
		SaveManager.Instance.SaveIntroState();
	}

	public void intro_anim_EVENT(){
		intro_Panel.SetActive(false);
		util.GetComponent<CameraPermissionChecker>().verifyPermission();	
	}
	public void showWarningText(){
		warningTxt.gameObject.SetActive(true);
		linkTxt.gameObject.SetActive(true);
		settingsButton.gameObject.SetActive(true);
	}

	public void openSettings(){
		NativeSettings.GetSettingsURL_Native();
	}

}
