using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	private const string SAVE_SASSION_KEY = "firstSession18";
	public static SaveManager Instance { get;set; }
	public SaveState session_state;


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

		session_state.isPermissionRequested = true;

		PlayerPrefs.SetString(SAVE_SASSION_KEY, SaveHelper.Serialize<SaveState>(session_state));
	}
	public void SaveIntroState(){
		session_state.wasIntroShown = true;

		PlayerPrefs.SetString(SAVE_SASSION_KEY, SaveHelper.Serialize<SaveState>(session_state));
	}

	public void Load() {

		if(PlayerPrefs.HasKey(SAVE_SASSION_KEY)){
			session_state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString(SAVE_SASSION_KEY));
			session_state.isPermissionRequested = true;
		} else {
			session_state = new SaveState();
		}
			
	}
}
