using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine.UI;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class ShareFun : MonoBehaviour, NativeGallery.PathGetter {
	[SerializeField]
	private Canvas canvas, shareCanvas;
	[SerializeField]
	private Button b1,b2;

	private Texture2D image;
	private Texture2D lastImage;
	private string mainPath;

	private string mCapturePath;
		private string ScreenShotText = "AReal — St Petersburg's architecture in augmented reality: https://itunes.apple.com/app/areal/id1373207530";

	public void onClickSS(){
		canvas.GetComponent<CanvasController>().setUIVisible(false);
	}
	public void playCoroutine(){
		StartCoroutine(takeCapture());
	}

	private IEnumerator takeCapture(){
		yield return new WaitForEndOfFrame();

		Texture2D img = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
		img.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
		img.Apply();
		image = img;

		NativeGallery.SaveImageToGallery( image,"AReal","maimga{}.png",this );

		lastImage = image;
		canvas.GetComponent<CanvasController>().screenShot_Flash();

	}


	public void showScreenShot(){
		if (lastImage != null) {
			Image image = shareCanvas.transform.GetChild(0).gameObject.GetComponent<Image>();
			var rect = new Rect(0,0,lastImage.width, lastImage.height);
			Sprite s = Sprite.Create(lastImage,rect, new Vector2(0.5f, 0.5f));
			image.sprite = s;
		}
		ShareScreenshotWithText(ScreenShotText);
		
	}

	// private void returnCanvas(){
	// 	shareCanvas.gameObject.SetActive(false);
	// 	canvas.gameObject.SetActive(true);

	// }


	public string ScreenshotName = "screenshot.png";


	public void ShareScreenshotWithText(string text)
	{

		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		if(File.Exists(screenShotPath)) File.Delete(screenShotPath);
	//	mainPath = screenShotPath;

		b1.gameObject.SetActive(false);
		b2.gameObject.SetActive(false);

		ScreenCapture.CaptureScreenshot(ScreenshotName);
		StartCoroutine(delayedShare(screenShotPath, text));
	//	NativeShare.Share(text, mainPath, "", "", "image/png", true, "");
	}
	// public void shareMyScreen(){
	// 	NativeShare.Share("Hello from Areal!", mainPath, "", "", "image/png", true, "");
	// }

	//CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
	//for it to save, or set a unique image name and check if the file has been created yet before sharing.
	IEnumerator delayedShare(string screenShotPath, string text)
	{
		Debug.Log("Sshare delayed");
		while(!File.Exists(screenShotPath)) {
			Debug.Log ("Sshare waiting yeild");
			yield return new WaitForSeconds(.05f);
		}
		NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
		Invoke("delayedUIVisibility",2f);
	}

	private void delayedUIVisibility()
	{
		b1.gameObject.SetActive(true);
		b2.gameObject.SetActive(true);
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
	// public void Screenshot()
	// {
	// 	// Short way
	// 	StartCoroutine(GetScreenshot());
	// }

	//---------- Get Screenshot ----------//
	// public IEnumerator GetScreenshot()
	// {
	// 	yield return new WaitForEndOfFrame();

	// 	// Get Screenshot
	// 	Texture2D screenshot;
	// 	screenshot = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
	// 	screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
	// 	screenshot.Apply();
	// 	lastImage = screenshot;

	// 	// Save Screenshot
	// 	Save_Screenshot(screenshot);
	// }

	//---------- Save Screenshot ----------//
// 	private void Save_Screenshot(Texture2D screenshot)
// 	{
// 		string screenShotPath = Application.persistentDataPath + "/" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + "_" + ScreenshotName;
// 	//	string screenShotPath = Application.persistentDataPath + "/" +  "Areal";
// 		mCapturePath = screenShotPath;
// 		File.WriteAllBytes(screenShotPath, screenshot.EncodeToPNG());

// 		// Native Share
// //		StartCoroutine(DelayedShare_Image(screenShotPath));
// 	}

	//---------- Clear Saved Screenshots ----------//
	// public void Clear_SavedScreenShots()
	// {
	// 	string path = Application.persistentDataPath;

	// 	DirectoryInfo dir = new DirectoryInfo(path);
	// 	FileInfo[] info = dir.GetFiles("*.png");

	// 	foreach (FileInfo f in info)
	// 	{
	// 		File.Delete(f.FullName);
	// 	}
	// }

	// public void ShareCapture(){
	// 	Debug.Log("sshare clicked");
	// 	StartCoroutine(DelayedShare_Image(mCapturePath));
	// }

	// //---------- Delayed Share ----------//
	// private IEnumerator DelayedShare_Image(string screenShotPath)
	// {
	// 	while (!File.Exists(screenShotPath))
	// 	{
	// 		Debug.Log("sshare file not exists");
	// 		yield return new WaitForSeconds(.05f);
	// 	}

	// 	// Share
	// 	NativeShare_Image(screenShotPath);
	// }

	// //---------- Native Share ----------//
	// private void NativeShare_Image(string screenShotPath)
	// {
	// 	string text = "";
	// 	string subject = "";
	// 	string url = "";
	// 	string title = "Select sharing app";

	// 	#if UNITY_ANDROID

	// 	subject = "Test subject.";
	// 	text = "Test text";
	// 	#endif

	// 	#if UNITY_IOS
	// 	subject = "Test subject.";
	// 	text = "Test text";
	// 	#endif

	// 	// Share
	// 	NativeShare.Share(text, screenShotPath, url, subject, "image/png", true, title);
	// }

    public void setImagePath(string path)
    {
		 mainPath = "file://" + path;//Path.GetFileName( path );
		 string pathq = "file://" + System.IO.Path.Combine(Application.persistentDataPath, "maimga{}.png");
		// mainPath = pathq;
		

    }
}