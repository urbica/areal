//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using UnityEngine.EventSystems;


public class CanvasController : MonoBehaviour {

	private const string CANVAS_ANIMATOR_STATE = "CanvasState";
	private const int HIDE_INTRO_PANEL = 1;
	private const int FIND_SURFACE_PANEL_ENTER = 2;
	private const int PUT_MAP_PANEL_ENTER = 4;
	private const int TOUCH_PIN_PANEL_ENTER = 6;
	private const int SCREENSHOT_ANIM = 8;
	private const int ANIM_EXIT = 0;

	private Animator _animator;


	private AnimationComponent component;

	public UnityARGeneratePlane generatePlaneScript;

	public Button ok_intro;
//	public Text intro;




	public Button back_Button;
	public Button reload_Button;
	public Button screenShot_Button;

	public Button info_Button;

	public GameObject intro_Panel;
	public GameObject about_map_Panel;
	public GameObject about_pins_Panel;
	public GameObject find_surface_Panel;
	public GameObject screenShot_Panel;


	public UnityARAnchorManager anchManager;


	private bool first_Enter = true;

	private bool clickedFromUI;

	[SerializeField]
	private Text modelName;





	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		component = new AnimationComponent();
		
		show_intro();

		hide_back_Button ();
		hide_reload_btn ();
		hide_info_Button();
		hide_screenShot_btn ();

		// 		if (SaveManager.Instance.state.isFirstEnter) {

// 			show_intro ();
// 		}
// 		else {
// //			close_intro ();
// 			startGeneratePlane();
// 			}
	}
	
	// Update is called once per frame


	public void close_introduction(){
		close_intro ();
	}
	public void open_introduction(){
		show_intro ();
	}
	private void close_intro(){
	setCanvasAnimatorParametr(HIDE_INTRO_PANEL);
	}
	private void show_intro(){
		ok_intro.transform.localScale = new Vector3 (1f, 1f, 1f);
		intro_Panel.SetActive(true);
	}

	public void show_find_surface_info(){
		find_surface_Panel.SetActive(true);
		setCanvasAnimatorParametr(FIND_SURFACE_PANEL_ENTER);
		startGeneratePlane();
	}
	public void hide_find_surface_info(){
		setCanvasAnimatorParametr(ANIM_EXIT);
		Invoke("EVENT_surface_info_exit",0.4f);
	}

	public void EVENT_surface_info_exit(){
		find_surface_Panel.SetActive(false);
		show_about_map_text();
	}

	public void show_about_map_text(){
		about_map_Panel.SetActive (true);
		setCanvasAnimatorParametr(PUT_MAP_PANEL_ENTER);
	}
	public void hide_about_map_text(){
		setCanvasAnimatorParametr(ANIM_EXIT);
	}
	public void EVENT_put_map_exit(){
		about_map_Panel.SetActive(false);
		show_about_pins();
	}

	public void show_about_pins(){
		about_pins_Panel.SetActive (true);
		setCanvasAnimatorParametr(TOUCH_PIN_PANEL_ENTER);
	}
	public void hide_about_pins(){
		setCanvasAnimatorParametr(ANIM_EXIT);
	}
	public void EVENT_about_pins_exit(){
		about_pins_Panel.SetActive(false);
	}

	public void show_info_Button(){
		info_Button.gameObject.SetActive(true);
	}

	public void hide_info_Button(){
		info_Button.gameObject.SetActive(false);
	}


	public void show_back_Button(){
		back_Button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	public void hide_back_Button(){
		back_Button.transform.localScale = new Vector3 (0, 0, 0);
	}

	public void hide_reload_btn(){
		reload_Button.transform.localScale = new Vector3 (0,0,0);
	}
	public void show_reload_btn(){
		reload_Button.transform.localScale = new Vector3 (1f,1f,1f);
		
	}
	public void hide_screenShot_btn(){
		screenShot_Button.transform.localScale = new Vector3 (0, 0, 0);
	}
	public void show_screenShot_btn(){
		screenShot_Button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	public void screenShot_Flash(){
		screenShot_Panel.SetActive(true);
		screenShot_Panel.GetComponent<Animator>().Play("screenCupture_anim");
		Invoke("screenshot_anim_EVENT",0.5f);
	}
	private void screenshot_anim_EVENT(){
		screenShot_Panel.SetActive(false);
	}

    private void initUI(){
         intro_Panel.SetActive(true);
    }

	public AnimationComponent getAnimScript(){return component;}

	void startGeneratePlane(){
		intro_Panel.SetActive(false);
		generatePlaneScript.initStart();
	}
	private void setCanvasAnimatorParametr(int transitionState){
		_animator.SetInteger(CANVAS_ANIMATOR_STATE,transitionState);
	}


	public void resetAnimationState(){
		setCanvasAnimatorParametr(ANIM_EXIT);
	}

	public void setModelName(string text){
		modelName.text = text;
	}


}
