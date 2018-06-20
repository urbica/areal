using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	public static SaveManager Instance { get;set; }
	public SaveState state;

	private void Awake() {

		DontDestroyOnLoad(gameObject);
		Instance = this;
		Load();
	}

	public void Save() {
		state.isFirstEnter = false;
		PlayerPrefs.SetString("let", SaveHelper.Serialize<SaveState>(state));
	}

	public void Load() {
		if(PlayerPrefs.HasKey("let"))
		{
			state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("let"));
		} else {
			state = new SaveState();
			state.isFirstEnter = true;
		}
		// else {
		// 	state = new SaveState();
		// 	state.setState(true);
		// 	Save();
		// }
	}

}
