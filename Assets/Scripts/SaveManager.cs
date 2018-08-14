using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	private const string SAVE_SASSION_KEY = "firstSession18";
	private const string SAVE_PERMISSION_REQUEST_KEY = "permission01";
	public static SaveManager Instance { get;set; }
	public SaveState.PermissionState permission_state;
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

		permission_state.isPermissionAsked = true;
		PlayerPrefs.SetString(SAVE_PERMISSION_REQUEST_KEY, SaveHelper.Serialize<SaveState.PermissionState>(permission_state));
	}

	public void Load() {
		Debug.Log("Checker manager started");

		if(PlayerPrefs.HasKey(SAVE_SASSION_KEY)){
			session_state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString(SAVE_SASSION_KEY));
			session_state.isPermissionRequested = true;
		} else {
			session_state = new SaveState();
			session_state.isFirstEnter = true;
			session_state.isPermissionRequested = false;
		}
			
	}
}
