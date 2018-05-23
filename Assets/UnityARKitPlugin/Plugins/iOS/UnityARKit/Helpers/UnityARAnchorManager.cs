using System;
using System.Collections.Generic;
using System.Linq;
using Collections.Hybrid.Generic;

namespace UnityEngine.XR.iOS
{
	public class UnityARAnchorManager 
	{

		public bool shouldCreatePlane; //--modified


		private LinkedListDictionary<string, ARPlaneAnchorGameObject> planeAnchorMap;
		private UnityARGeneratePlane generatedPlane;
		private UnityARHitTestExample hitScript;
		private UnityARCameraManager Camera_managerScrpt;


		public UnityARAnchorManager (UnityARGeneratePlane plane,GameObject hitParent,UnityARHitTestExample sharp,UnityARCameraManager camera_manager)
		{
			shouldCreatePlane = true;
			planeAnchorMap = new LinkedListDictionary<string,ARPlaneAnchorGameObject> ();
			UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
			UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
			UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;
			generatedPlane = plane;
			hitScript = sharp;
			Camera_managerScrpt = camera_manager;
		}


		public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
		{
				
				GameObject go = UnityARUtility.CreatePlaneInScene (arPlaneAnchor);
				go.AddComponent<DontDestroyOnLoad> ();  //this is so these GOs persist across scene loads
				ARPlaneAnchorGameObject arpag = new ARPlaneAnchorGameObject ();
				arpag.planeAnchor = arPlaneAnchor;
				arpag.gameObject = go;
				planeAnchorMap.Add (arPlaneAnchor.identifier, arpag);


				//modified
//				hitObj.SetActive (true);
//				scriptik.enabled = true;
		

				Debug.Log ("arrku addAnchor");
				PlaneAppearDetector detector = generatedPlane;
				detector.planeDetect ();
				detector = hitScript;
				detector.planeDetect ();
				PlaneDetectorSwitcher switcher = Camera_managerScrpt;
				switcher.turn_on_Detector (false);
				shouldCreatePlane = false;
				


//				hitTest.enabled = true;
//			}

		}

		public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor)
		{
			if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
				ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
				GameObject.Destroy (arpag.gameObject);
				planeAnchorMap.Remove (arPlaneAnchor.identifier);
			}
		}

		public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
		{
			if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
				ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
				UnityARUtility.UpdatePlaneWithAnchorTransform (arpag.gameObject, arPlaneAnchor);
				arpag.planeAnchor = arPlaneAnchor;
				planeAnchorMap [arPlaneAnchor.identifier] = arpag;
			}
		}

        public void UnsubscribeEvents()
        {
            UnityARSessionNativeInterface.ARAnchorAddedEvent -= AddAnchor;
            UnityARSessionNativeInterface.ARAnchorUpdatedEvent -= UpdateAnchor;
            UnityARSessionNativeInterface.ARAnchorRemovedEvent -= RemoveAnchor;
        }

        public void Destroy()
        {
            foreach (ARPlaneAnchorGameObject arpag in GetCurrentPlaneAnchors()) {
                GameObject.Destroy (arpag.gameObject);
            }

            planeAnchorMap.Clear ();
            UnsubscribeEvents();
        }

		public void HidePrefabs(){
			foreach (ARPlaneAnchorGameObject arpag in GetCurrentPlaneAnchors()) {
				Debug.Log("Prefabs was hidden unityq");
				arpag.gameObject.SetActive (false);
			}
				
		}

		public LinkedList<ARPlaneAnchorGameObject> GetCurrentPlaneAnchors()
		{
			return planeAnchorMap.Values;
		}

		public void reload_plane(){
			shouldCreatePlane = true;
		}
	}

	interface PlaneAppearDetector{
		void planeDetect ();
	}
	interface PlaneDetectorSwitcher{
		void turn_on_Detector(bool on_off);
	}
}

