using System;
using System.Collections.Generic;
using UnityEngine.Analytics;


namespace UnityEngine.XR.iOS
{
	public class UnityARGeneratePlane : MonoBehaviour,PlaneAppearDetector
	{

		public CanvasController controller;
		public GameObject planePrefab;
        private UnityARAnchorManager unityARAnchorManager;
		public GameObject hitParent;
		public UnityARCameraManager Camera_managerScrpt;
		public GameObject pointCloud;

		[SerializeField]
		private TimeCounter analytic;


		public void initStart(){
	
			analytic.time_startFindPlane = Time.time;
			planePrefab.SetActive (true);
			UnityARHitTestExample hitScript = hitParent.GetComponentInChildren<UnityARHitTestExample>();
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
			controller.hide_find_surface_info();
			var delta = Time.time - analytic.time_startFindPlane;

			AnalyticsEvent.Custom("plane_find",new Dictionary<string,object>{{"find_time",delta}});
			
		}

		public void reload_plane(){
			analytic.time_startFindPlane = Time.time;
			Debug.Log("FindReload generateplane");
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

