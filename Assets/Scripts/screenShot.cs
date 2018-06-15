using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class screenShot : MonoBehaviour {
	private bool isProcessing = false;
	public string ScreenshotName = "screenshot.png";
	public string subject,ShareMessage,url;

	public void Share(){
		if(!isProcessing)
			StartCoroutine( CallSocialShareRoutine() );
	}

	public struct ConfigStruct
	{
		public string title;
		public string message;
	}
	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);
	public struct SocialSharingStruct
	{
		public string text;
		public string url;
		public string image;
		public string subject;
	}
	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);
	public void CallSocialShare(string title, string message)
	{
		ConfigStruct conf = new ConfigStruct();
		conf.title = title;
		conf.message = message;
		showAlertMessage(ref conf);
		isProcessing = false;
	}
	public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
	{
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = defaultTxt; 
		conf.url = url;
		conf.image = img;
		conf.subject = subject;
		showSocialSharing(ref conf);
	}
	IEnumerator CallSocialShareRoutine()
	{
		isProcessing = true;
		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		ScreenCapture.CaptureScreenshot(ScreenshotName);
		yield return new WaitForSeconds(1f);
		CallSocialShareAdvanced(ShareMessage, subject, url, screenShotPath);
	}
}
