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

		Debug.Log("SaveState - " + SaveHelper.Serialize<SaveState>(state));
	}

	public void Save() {
		PlayerPrefs.SetString("save", SaveHelper.Serialize<SaveState>(state));
	}

	public void Load() {
		if(PlayerPrefs.HasKey("save"))
		{
			
			state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
			Debug.Log("SaveState HasKey with value " + state.isFirstEnter.ToString());
		} else 
		{
			Debug.Log("SaveState Has NO Key");
			state = new SaveState();
			Save();
		}
	}

}
