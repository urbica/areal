using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	private const string SAVE_SASSION_KEY = "firstSession18";
	private const string SAVE_PERMISSION_REQUEST_KEY = "permission01";
	public static SaveManager Instance { get;set; }
	public SaveState.PermissionState permission_state;
	public SaveState session_state;

	public int SCENE = 1;

	private int MAIN_SCENE = 1;
	private int SUPPORT_SCENE = 0;

	private void Awake() {

		DontDestroyOnLoad(gameObject);
		Instance = this;
		Load();
	}

	public void SaveSassionEnter() {
		//
		session_state.isFirstEnter = false;
		PlayerPrefs.SetString(SAVE_SASSION_KEY, SaveHelper.Serialize<SaveState>(session_state));
	}
	public void SavePermissionRequest(){
		permission_state.isPermissionAsked = true;
		PlayerPrefs.SetString(SAVE_PERMISSION_REQUEST_KEY, SaveHelper.Serialize<SaveState.PermissionState>(permission_state));
	}

	public void Load() {
		if( SCENE == MAIN_SCENE) {
			if(PlayerPrefs.HasKey(SAVE_SASSION_KEY)){
				session_state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString(SAVE_SASSION_KEY));
			}else{
				session_state = new SaveState();
				session_state.isFirstEnter = true;
			}
			
		} else if (SCENE == SUPPORT_SCENE) {
			if(PlayerPrefs.HasKey(SAVE_PERMISSION_REQUEST_KEY)){
				permission_state = SaveHelper.Deserialize<SaveState.PermissionState>(PlayerPrefs.GetString(SAVE_PERMISSION_REQUEST_KEY));
			} else {
				permission_state = new SaveState.PermissionState();
				permission_state.isPermissionAsked = false;
			}	
		} 
	}
}
