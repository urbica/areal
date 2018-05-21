namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

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

		void Start()
		{
			
			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < _locationStrings.Length; i++)
			{
				var locationString = _locationStrings[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
				_markerPrefab.transform.localScale = new Vector3 (0, 0, 0);


			}
		}

		private void Update()
		{


			if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
				Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit raycastHit;

				if (Physics.Raycast (raycast, out raycastHit)) {
					if (raycastHit.collider.name == "mcollider") {
						clicker.OnClickPin (_spawnedObjects);
					}
				}
			}


//			pinsShown = _spawnScale == 0 ? false : true;
			
			if (pinsShown) {
				Debug.Log ("pins shown arrka");
				int count = _spawnedObjects.Count;
				for (int i = 0; i < count; i++) {
					var spawnedObject = _spawnedObjects [i];
					var location = _locations [i];

					spawnedObject.transform.localPosition = _map.GeoToWorldPosition (location, true);
					float _x = spawnedObject.transform.position.x;
					float _z = spawnedObject.transform.position.z;
					spawnedObject.transform.localScale = new Vector3 (_spawnScale, _spawnScale, _spawnScale);
					spawnedObject.transform.localPosition = new Vector3 (_x, _map.transform.position.y, _z);
				}
				if (_spawnScale == 0) {
					pinsShown = false;
				}
			}

			if (_spawnScale != 0 )
				pinsShown = true;

		}

		public void showPinsOnMap(){
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects [i];
//				var location = _locations [i];
//
//				spawnedObject.transform.localPosition = _map.GeoToWorldPosition (location, true);
//				float _x = spawnedObject.transform.position.x;
//				float _z = spawnedObject.transform.position.z;
//				spawnedObject.transform.localScale = new Vector3 (_spawnScale, _spawnScale, _spawnScale);
//				spawnedObject.transform.localPosition = new Vector3 (_x, _map.transform.position.y, _z);

				var boxCollider = spawnedObject.AddComponent<BoxCollider> ();
				boxCollider.transform.localPosition = spawnedObject.transform.localPosition;
				boxCollider.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
				boxCollider.name = "mcollider";

				pinsShown = true;
			}
		}

		public void setPinsScale(float scaleValue){
			_spawnScale = scaleValue;
		}
	}
}