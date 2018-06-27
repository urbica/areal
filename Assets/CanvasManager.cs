using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private Canvas main,info;

	public void switchCanvas(){
		bool isMainVisible = main.gameObject.activeInHierarchy;
		main.gameObject.SetActive(!isMainVisible);
		info.gameObject.SetActive(isMainVisible);
	}
}
