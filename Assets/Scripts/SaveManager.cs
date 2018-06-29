using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	private const string SAVE_SASSION_KEY = "firstSession9";
	public static SaveManager Instance { get;set; }
	public SaveState state;

	private void Awake() {

		DontDestroyOnLoad(gameObject);
		Instance = this;
		Load();
	}

	public void Save() {
		//
		state.isFirstEnter = false;
		PlayerPrefs.SetString(SAVE_SASSION_KEY, SaveHelper.Serialize<SaveState>(state));
	}

	public void Load() {
		if(PlayerPrefs.HasKey(SAVE_SASSION_KEY))
		{
			state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString(SAVE_SASSION_KEY));
		} else {
			state = new SaveState();
			state.isFirstEnter = true;
		}
	}

}
