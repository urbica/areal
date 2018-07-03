namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using Lean.Touch;
	using UnityEngine.XR.iOS;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		// public clicker clicker;
		public Camera Camera;

		private bool pinsShown = false;
		private List<string>collidersID;
		private Vector3 currentPinsScale;
		private Vector3 startPinsScale;

		private bool pinsSpawned;
		private string clickedCollider;
		private Transform clickedTransform;
		private const float normalPinScale = 0.007f;

		void Start()
		{
			pinsSpawned = false;
		}

		public void setPinCoef(float x){
			var result = normalPinScale * x;
			startPinsScale = new Vector3(result,result,result);
		}

		public void spawnPins(){
			if(!pinsSpawned){
				_locations = new Vector2d[_locationStrings.Length];
				_spawnedObjects = new List<GameObject>();
//				startPinsScale = _markerPrefab.transform.localScale;
				
				currentPinsScale = startPinsScale;
				for (int i = 0; i < _locationStrings.Length - 1; i++)
				{
					var locationString = _locationStrings[i];
					_locations[i] = Conversions.StringToLatLon(locationString);
					var instance = Instantiate(_markerPrefab);
					instance.transform.localScale = new Vector3(0,0,0);
					instance.AddComponent<LeanScale>();
					_spawnedObjects.Add(instance);


				}
				Camera.GetComponent<clicker>().setPinsList(_spawnedObjects);
				showPinsOnMap();
				pinsSpawned = true;
			}
			else {
				switchPins(true);
			}			
		}

		private void Update()
		{
			if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
				Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit raycastHit;

				if (Physics.Raycast (raycast, out raycastHit)) {
					int touchs = Input.touchCount;
					if (collidersID.Contains(raycastHit.collider.name) && touchs < 2) {
						switchPins(false);
						clickedTransform = raycastHit.collider.transform;
						_map.GetComponent<Animator>().SetInteger("mapAnimTransition",2);
						
					}
				}
			}
			
			if (pinsShown) {
				int count = _spawnedObjects.Count;
				for (int i = 0; i < count; i++) {
					var spawnedObject = _spawnedObjects [i];
					var location = _locations [i];

					Vector3 pinLocalPosition = _map.GeoToWorldPosition (location, true);
					float _x = pinLocalPosition.x;
					float _z = pinLocalPosition.z;
					spawnedObject.transform.localPosition = new Vector3 (_x, _map.transform.position.y, _z);
					// spawnedObject.transform.rotation = UnityARMatrixOps.GetRotation(spawnedObject.transform.localToWorldMatrix);
					// spawnedObject.transform.LookAt (Camera.main.transform.position);
					// spawnedObject.transform.eulerAngles = new Vector3 (0, spawnedObject.transform.eulerAngles.y, 0);

					// Vector3 currAngle = spawnedObject.transform.eulerAngles;
					// if(i == 1)
					// Debug.Log("Angel before " + spawnedObject.transform.rotation.ToString());
					// spawnedObject.transform.LookAt(Camera.main.transform.position);
					// spawnedObject.transform.eulerAngles = new Vector3(currAngle.x,spawnedObject.transform.eulerAngles.y,currAngle.z);
					// if(i == 1)
					// Debug.Log("Angel after " + spawnedObject.transform.rotation.ToString());
					
				}
			} 
		}


		public void showPinsOnMap(){
			int count = _spawnedObjects.Count;
			collidersID = new List<string>();
			pinsShown = true;

			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects [i];				
				var boxCollider = spawnedObject.GetComponent<BoxCollider> ();
				boxCollider.name = i.ToString();
				collidersID.Add(boxCollider.name);
				
			}
			switchPins(true);
		}

		public void switchPins(bool value){
			pinsShown = value;
			foreach(GameObject pin in _spawnedObjects){
				pin.GetComponent<LeanScale>().enabled = value;
				if (!value){
					currentPinsScale = pin.transform.localScale;
				} 
				else {
					pin.GetComponent<Animator>().Play("new_star_anim");
				}
				pin.transform.localScale = value ? 	currentPinsScale : new Vector3(0,0,0);
			}		
		}

		public void resetPinsScale(){
			if(currentPinsScale != null && startPinsScale != null)
				currentPinsScale = startPinsScale;
		}
		public void clickPins(){
			var x = startPinsScale.x / normalPinScale;
			Camera.GetComponent<clicker>().OnClickPin (clickedTransform,x);
		}
		
	}
}