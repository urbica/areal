using System;
using System.Collections.Generic;
using Lean.Touch;
using Mapbox.Examples;
using UnityEngine.EventSystems;

namespace UnityEngine.XR.iOS
{
	public class UnityARHitTestExample : MonoBehaviour, PlaneAppearDetector
	{
		public Transform m_HitTransform;
		public float maxRayDistance = 30.0f;
		public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
		public CanvasController ccontroller;
		public UnityARGeneratePlane generate_script;
		public UnityARCameraManager camera_manager;

		public GameObject pointCloud;

		private GameObject MAP;


		private bool mapWasShown = false;
		private bool planeAppeared = false;
		private SpawnOnMap spawnScript;

		private static int SHOW_MAP_ANIM = 1;
		private static int HIDE_MAP_ANIM = 2;

/* example of correct scale with such distance between camera and plane. To get correct resultScale need use ratio of this const fields data 
 to calculate result*/
		private const float normalScale = 0.1f;
		private const float costantDistance = 0.4f;
		private float resultScale = normalScale;




        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
			var overScene = CanvasManager.SCENE_UNDER_CANVAS;
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
			if (hitResults.Count > 0 && !mapWasShown && planeAppeared && !overScene) {
				mapWasShown = true;

                foreach (var hitResult in hitResults) {
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
					m_HitTransform.localScale = new Vector3 (resultScale, resultScale, resultScale);

					Vector3 currAngle = m_HitTransform.eulerAngles;
					m_HitTransform.LookAt(Camera.main.transform);
					m_HitTransform.eulerAngles = new Vector3(currAngle.x,m_HitTransform.eulerAngles.y + 180f,currAngle.z);

					Transform map;
					for (int i = 0; i < m_HitTransform.childCount; i++) {
						map = m_HitTransform.GetChild (i);
						if (map.name == "Map") {

							MAP = map.gameObject;
							MAP.GetComponent<Animator>().SetInteger("mapAnimTransition",SHOW_MAP_ANIM);
							spawnScript = MAP.GetComponent<SpawnOnMap> ();
							m_HitTransform.gameObject.GetComponent<LeanScale>().enabled = true;
							if(CanvasController.isFirstSession){
								ccontroller.show_info_Button();
							}
							
						}
					}
					generate_script.getManager ().HidePrefabs ();

					ccontroller.hide_about_map_text();
					ccontroller.show_screenShot_btn ();
					ccontroller.show_reload_btn ();

					return true;
                }
            }
            return false;
        }
		
		void Update () {
			#if UNITY_EDITOR  
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayer)) {
					m_HitTransform.position = hit.point;
					m_HitTransform.rotation = hit.transform.rotation;
				}
			}
			#else
			if (Input.touchCount > 0 && m_HitTransform != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
						//ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingGeometry,
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        //ARHitTestResultType.ARHitTestResultTypeEstimatedHorizontalPlane, 
						//ARHitTestResultType.ARHitTestResultTypeEstimatedVerticalPlane, 
						//ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    };
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                            return;
                        }
                    }
				}
			}
			#endif

		}

		void PlaneAppearDetector.planeDetect(){
			planeAppeared = true;
			pointCloud.GetComponent<UnityPointCloudExample>().switchCloud(false);

			//calculate distancce
			Vector3 cameraPostiton = camera_manager.transform.position;
			Vector3 planePosition = new Vector3(0,0,0); 
			LinkedList<ARPlaneAnchorGameObject> o = generate_script.getManager().GetCurrentPlaneAnchors();
						foreach (ARPlaneAnchorGameObject arpag in o) {
				planePosition = arpag.gameObject.transform.position;
			}
			float distance = planePosition.magnitude - cameraPostiton.magnitude;
			camera_manager.m_camera.GetComponent<clicker>().setDistance(distance);
			calculateResultScale(distance);
			
		}

		//call from editor
		public void reload_map(){
			ccontroller.hide_reload_btn();
			ccontroller.hide_screenShot_btn();

			if(CanvasController.isFirstSession){
				ccontroller.resetAnimationState();
				ccontroller.hide_info_Button();
				if(SaveManager.Instance.session_state.isFirstEnter){
					Invoke("show_first_help",0.5f);
				}
				
			}

			generate_script.reload_plane();
			if (mapWasShown){
				SpawnOnMap component = MAP.GetComponent<SpawnOnMap>();
				component.switchPins(false);
			}

			mapWasShown = false;
			m_HitTransform.localScale = new Vector3 (0, 0, 0);
			MAP.GetComponent<Animator>().SetInteger("mapAnimTransition",0);
			
			pointCloud.GetComponent<UnityPointCloudExample>().switchCloud(true);

			camera_manager.ResetAr();
		}
			

		private void calculateResultScale(float distance){
			if(distance > costantDistance)
				resultScale = (normalScale * distance) / (2 * costantDistance);
		}
		
		private void show_first_help(){
			ccontroller.show_find_surface_info();
		}
	}
}

