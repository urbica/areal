using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class Anim_Intro_Event_Sender : MonoBehaviour {

	public UnityARGeneratePlane script;

	public CanvasController ccontroller;


	void show_findSurface_scene(){
		script.initStart();
		
	}
	void show_about_map(){
		ccontroller.show_about_map_text();
	}
}
