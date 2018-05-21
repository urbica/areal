using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class CanvasController : MonoBehaviour {

	public Button ok_intro;
//	public Text intro;


	public Button back_Button;
	public Button back_to_intro_Button;
	public Button reload_Button;
	public Button info_Button;
	public Button screenShot_Button;

	public GameObject intro_Panel;
	public GameObject about_map_Panel;
	public GameObject about_pins_Panel;
	public GameObject about_model_Panel;
	public GameObject about_isaac_Panel;


	public UnityARAnchorManager anchManager;


	private bool first_Enter = true;




	// Use this for initialization
	void Start () {
		if (first_Enter) {
			show_intro ();
		}
		else {
			close_intro ();
			}
		hide_back_Button ();
		hide_about_map_text ();
		hide_about_pins ();
		hide_about_model ();
		hide_back_toIntro_btn ();
		hide_reload_btn ();
		hide_about_Isaac_info ();
		hide_info_btn ();
		hide_screenShot_btn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void close_introduction(){
		close_intro ();
	}
	public void open_introduction(){
		show_intro ();
	}

	public void show_back_Button(){
		back_Button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	public void hide_back_Button(){
		back_Button.transform.localScale = new Vector3 (0, 0, 0);
	}

	public void hide_about_map_text(){
		about_map_Panel.SetActive (false);
	}
	public void show_about_map_text(){
		about_map_Panel.SetActive (true);
	}

	public void show_about_pins(){
		about_pins_Panel.SetActive (true);
	}
	public void hide_about_pins(){
		about_pins_Panel.SetActive (false);
	}

	public void show_about_model(){
		about_model_Panel.SetActive (true);
	}
	public void hide_about_model(){
		about_model_Panel.SetActive (false);
	}

	public void hide_back_toIntro_btn(){
		back_to_intro_Button.transform.localScale = new Vector3 (0,0,0);
	}
	public void show_back_toIntro_btn(){
		back_to_intro_Button.transform.localScale = new Vector3 (1f,1f,1f);
	}

	public void hide_reload_btn(){
		reload_Button.transform.localScale = new Vector3 (0,0,0);
	}
	public void show_reload_btn(){
		reload_Button.transform.localScale = new Vector3 (1f,1f,1f);
	}

	public void show_about_Isaac_info(){
		about_isaac_Panel.SetActive (true);
	}
	public void hide_about_Isaac_info(){
		about_isaac_Panel.SetActive (false);
	}

	public void hide_info_btn(){
		info_Button.transform.localScale = new Vector3 (0, 0, 0);
	}
	public void show_info_btn(){
		info_Button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	public void hide_screenShot_btn(){
		screenShot_Button.transform.localScale = new Vector3 (0, 0, 0);
	}
	public void show_screenShot_btn(){
		screenShot_Button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}




	private void close_intro(){
		ok_intro.transform.localScale = new Vector3 (0, 0, 0);

		intro_Panel.SetActive(false);
	}
	private void show_intro(){
		ok_intro.transform.localScale = new Vector3 (1f, 1f, 1f);
		intro_Panel.SetActive(true);
	}
		
}
