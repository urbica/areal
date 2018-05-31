namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using Lean.Touch;

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

		public clicker clicker;

		private bool pinsShown = false;
		private List<string>collidersID;
		private Vector3 currentPinsScale;
		private Vector3 startPinsScale;

		void Start()
		{
			
			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();
			startPinsScale = _markerPrefab.transform.localScale;
			currentPinsScale = startPinsScale;
			for (int i = 0; i < _locationStrings.Length - 1; i++)
			{
				var locationString = _locationStrings[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
//				instance.transform.localScale = new Vector3(0, 0, 0);
				instance.transform.localScale = _markerPrefab.transform.localScale;
				instance.AddComponent<LeanScale>();
				_spawnedObjects.Add(instance);
//				_markerPrefab.transform.localScale = new Vector3 (0, 0, 0);

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
						clicker.OnClickPin (_spawnedObjects,raycastHit.collider.name);
						
					}
				}
			}
			
			if (pinsShown) {
				int count = _spawnedObjects.Count;
				for (int i = 0; i < count; i++) {
					var spawnedObject = _spawnedObjects [i];
					var location = _locations [i];

					spawnedObject.transform.localPosition = _map.GeoToWorldPosition (location, true);
					float _x = spawnedObject.transform.position.x;
					float _z = spawnedObject.transform.position.z;
//					spawnedObject.transform.localScale = new Vector3 (_spawnScale, _spawnScale, _spawnScale);
					
					spawnedObject.transform.localPosition = new Vector3 (_x, _map.transform.position.y, _z);
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
				var boxCollider = spawnedObject.AddComponent<BoxCollider> ();
				boxCollider.transform.localPosition = spawnedObject.transform.localPosition;
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
				Debug.Log("scale: " + currentPinsScale.magnitude + "auruu");
				pin.transform.localScale = value ? 	currentPinsScale : new Vector3(0,0,0);
			}		
		}

		public void resetPinsScale(){
			currentPinsScale = startPinsScale;
		}
	}
}