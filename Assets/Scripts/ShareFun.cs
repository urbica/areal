using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine.UI;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class ShareFun : MonoBehaviour {
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

		NativeGallery.SaveImageToGallery( image,"AReal","maimga{}.png" );

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

	public string ScreenshotName = "screenshot.png";


	public void ShareScreenshotWithText(string text)
	{

		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		if(File.Exists(screenShotPath)) File.Delete(screenShotPath);

		b1.gameObject.SetActive(false);
		b2.gameObject.SetActive(false);

		ScreenCapture.CaptureScreenshot(ScreenshotName);
		StartCoroutine(delayedShare(screenShotPath, text));
	}

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
}