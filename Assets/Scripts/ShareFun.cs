using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class ShareFun : MonoBehaviour {
	[SerializeField]
	private Canvas canvas;

	public void onClickSS(){
		canvas.gameObject.SetActive(false);
		StartCoroutine(takeCapture());
	}

	private IEnumerator takeCapture(){
		yield return new WaitForEndOfFrame();

		Texture2D img = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
		img.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
		img.Apply();
		NativeGallery.SaveImageToGallery( img,"MyGallary","maimga{}.png" );

		Destroy(img);

		canvas.gameObject.SetActive(true);
		canvas.GetComponent<CanvasController>().screenShot_Flash();
	}

	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText(string text)
	{
		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		if(File.Exists(screenShotPath)) File.Delete(screenShotPath);

		ScreenCapture.CaptureScreenshot(ScreenshotName);

//		StartCoroutine(delayedShare(screenShotPath, text));
	}

	//CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
	//for it to save, or set a unique image name and check if the file has been created yet before sharing.
	IEnumerator delayedShare(string screenShotPath, string text)
	{
		while(!File.Exists(screenShotPath)) {
			yield return new WaitForSeconds(.05f);
		}

		NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
	}

	//---------- Helper Variables ----------//
	private float width
	{
		get
		{
			return Screen.width;
		}
	}

	private float height
	{
		get
		{
			return Screen.height;
		}
	}


	//---------- Screenshot ----------//
	public void Screenshot()
	{
		// Short way
		StartCoroutine(GetScreenshot());
	}

	//---------- Get Screenshot ----------//
	public IEnumerator GetScreenshot()
	{
		yield return new WaitForEndOfFrame();

		// Get Screenshot
		Texture2D screenshot;
		screenshot = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
		screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
		screenshot.Apply();

		// Save Screenshot
		Save_Screenshot(screenshot);
	}

	//---------- Save Screenshot ----------//
	private void Save_Screenshot(Texture2D screenshot)
	{
		string screenShotPath = Application.persistentDataPath + "/" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + "_" + ScreenshotName;
		File.WriteAllBytes(screenShotPath, screenshot.EncodeToPNG());

		// Native Share
//		StartCoroutine(DelayedShare_Image(screenShotPath));
	}

	//---------- Clear Saved Screenshots ----------//
	public void Clear_SavedScreenShots()
	{
		string path = Application.persistentDataPath;

		DirectoryInfo dir = new DirectoryInfo(path);
		FileInfo[] info = dir.GetFiles("*.png");

		foreach (FileInfo f in info)
		{
			File.Delete(f.FullName);
		}
	}

	//---------- Delayed Share ----------//
	private IEnumerator DelayedShare_Image(string screenShotPath)
	{
		while (!File.Exists(screenShotPath))
		{
			yield return new WaitForSeconds(.05f);
		}

		// Share
		NativeShare_Image(screenShotPath);
	}

	//---------- Native Share ----------//
	private void NativeShare_Image(string screenShotPath)
	{
		string text = "";
		string subject = "";
		string url = "";
		string title = "Select sharing app";

		#if UNITY_ANDROID

		subject = "Test subject.";
		text = "Test text";
		#endif

		#if UNITY_IOS
		subject = "Test subject.";
		text = "Test text";
		#endif

		// Share
		NativeShare.Share(text, screenShotPath, url, subject, "image/png", true, title);
	}
}