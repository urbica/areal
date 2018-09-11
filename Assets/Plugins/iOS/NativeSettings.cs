using UnityEngine;
#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public static class NativeSettings {

	#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        public static extern string GetSettingsURL();
	#endif

	#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        public static extern void OpenSettings();
    #endif

	public static void GetSettingsURL_Native(){
		#if UNITY_IOS && !UNITY_EDITOR
			string url = GetSettingsURL();
			Application.OpenURL(url);
		#endif
	}
	public static void OpenSettings_Native(){
		#if UNITY_IOS && !UNITY_EDITOR
			OpenSettings();
		#endif
	}
}
