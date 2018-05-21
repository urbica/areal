using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class UnityARGeneratePlane : MonoBehaviour,PlaneAppearDetector
	{

		public CanvasController controller;
		public GameObject planePrefab;
        private UnityARAnchorManager unityARAnchorManager;
//		public UnityARHitTestExample hitTest;
		public GameObject hitParent;
//		public GameObject map;
//		private UnityARHitTestExample scrpt;
		public UnityARCameraManager Camera_managerScrpt;


		public void initStart(){
//			planePrefab.SetActive (true);
			UnityARHitTestExample hitScript = hitParent.GetComponentInChildren<UnityARHitTestExample>();
//			scrpt = map.GetComponent<UnityARHitTestExample>();
			unityARAnchorManager = new UnityARAnchorManager(this,hitParent,hitScript,Camera_managerScrpt);
			UnityARUtility.InitializePlanePrefab (planePrefab);

		}

		public UnityARAnchorManager getManager(){
			return unityARAnchorManager;
		}

        void OnDestroy()
        {
            unityARAnchorManager.Destroy ();
        }

        void OnGUI()
        {
			IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
			foreach(var planeAnchor in arpags)
			{
                //ARPlaneAnchor ap = planeAnchor;
                //GUI.Box (new Rect (100, 100, 800, 60), string.Format ("Center: x:{0}, y:{1}, z:{2}", ap.center.x, ap.center.y, ap.center.z));
                //GUI.Box(new Rect(100, 200, 800, 60), string.Format ("Extent: x:{0}, y:{1}, z:{2}", ap.extent.x, ap.extent.y, ap.extent.z));
            }
        }
		void PlaneAppearDetector.planeDetect(){
			controller.show_about_map_text ();
			controller.hide_screenShot_btn ();
		}

		public void reload_plane(){
			planePrefab.SetActive (true);
			unityARAnchorManager.reload_plane ();
			Transform map;
			for (int i = 0; i < hitParent.transform.childCount; i++) {
				map = hitParent.transform.GetChild (i);
				if (map.name == "Map") {
					map.transform.localScale = new Vector3 (0, 0, 0);
				}
			}
		}
	}
}

