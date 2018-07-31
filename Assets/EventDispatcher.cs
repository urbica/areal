using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EventDispatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void screenShotEvent(){
		Analytics.CustomEvent("screenshot_clicked");
	}
	public void infoEvent(){
		Analytics.CustomEvent("info_clicked");
	}
	public void emailEvent(){
		Analytics.CustomEvent("email_clicked");
	}
	public void websiteEvent(){
		Analytics.CustomEvent("weblink_clicked");
	}
	public void reloadEvent(){
		Analytics.CustomEvent("reload_clicked");
	}

}
