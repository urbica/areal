using System;
using System.Runtime.InteropServices;

namespace UnityEngine.XR.iOS
{
	public class UnityARUtility
	{
		private MeshCollider meshCollider; //declared to avoid code stripping of class
		private MeshFilter meshFilter; //declared to avoid code stripping of class
		private static GameObject planePrefab = null;

		private float minX,minY = 10f;

		public static void InitializePlanePrefab(GameObject go)
		{
			planePrefab = go;
		}
		
		public static GameObject CreatePlaneInScene(ARPlaneAnchor arPlaneAnchor)
		{
			GameObject plane;
			if (planePrefab != null) {
				plane = GameObject.Instantiate(planePrefab);
			} else {
				plane = new GameObject (); //put in a blank gameObject to get at least a transform to manipulate
			}

			plane.name = arPlaneAnchor.identifier;

			ARKitPlaneMeshRender apmr = plane.GetComponent<ARKitPlaneMeshRender> ();
			if (apmr != null) {
				apmr.InitiliazeMesh (arPlaneAnchor);
			}

			return UpdatePlaneWithAnchorTransform(plane, arPlaneAnchor);

		}

		public static GameObject UpdatePlaneWithAnchorTransform(GameObject plane, ARPlaneAnchor arPlaneAnchor)
		{
			
			//do coordinate conversion from ARKit to Unity
			plane.transform.position = UnityARMatrixOps.GetPosition (arPlaneAnchor.transform);
			plane.transform.rotation = UnityARMatrixOps.GetRotation (arPlaneAnchor.transform);

			ARKitPlaneMeshRender apmr = plane.GetComponent<ARKitPlaneMeshRender> ();
			if (apmr != null) {
				apmr.UpdateMesh (arPlaneAnchor);
			}


			MeshFilter mf = plane.GetComponentInChildren<MeshFilter> ();
			Vector2 textureScale = plane.GetComponentInChildren<MeshRenderer>().material.mainTextureScale;
			Debug.Log("AroAr texture X: " + textureScale.x + " Y: " + textureScale.y);
			Debug.Log("AroAr arplaneanchor X: " + arPlaneAnchor.extent.x + " Y: " + arPlaneAnchor.extent.y + " Z:" + arPlaneAnchor.extent.z);

			int textureScaleX = (int)(arPlaneAnchor.extent.x * 50);
			int textureScaleY = (int)(arPlaneAnchor.extent.z * 50);
			Debug.Log("AroAr countedScale: X - " + textureScaleX + ", Y - " + textureScaleY);
			// textureScaleX = textureScaleX > 10 ? textureScaleX : 10;
			// textureScaleY = textureScaleY > 10 ? textureScaleY : 10;
			
			textureScale = new Vector2(textureScaleX,textureScaleY);
			plane.GetComponentInChildren<MeshRenderer>().material.mainTextureScale = textureScale;


			if (mf != null) {
				if (apmr == null) {
					//since our plane mesh is actually 10mx10m in the world, we scale it here by 0.1f
					mf.gameObject.transform.localScale = new Vector3 (arPlaneAnchor.extent.x * 0.1f, arPlaneAnchor.extent.y * 0.1f, arPlaneAnchor.extent.z * 0.1f);

	                //convert our center position to unity coords
	                mf.gameObject.transform.localPosition = new Vector3(arPlaneAnchor.center.x,arPlaneAnchor.center.y, -arPlaneAnchor.center.z);
				}

			}

			return plane;
		}
	}
}

