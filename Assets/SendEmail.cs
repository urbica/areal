using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmail : MonoBehaviour {

	// Use this for initialization
public void sendEmail ()
 {
  string email = "hello@urbica.co";
  string subject = MyEscapeURL("My Subject");
  string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
  Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
 }
 string MyEscapeURL (string url)
 {
  return WWW.EscapeURL(url).Replace("+","%20");
 }
}
