using System.Collections;
using UnityEngine;

namespace AquariusMax.Ancient {
public class Rotator2 : MonoBehaviour {

		public float x = 0f;
		public float y = 0f;
		public float z = 0f;

	void Update ()
	{
			transform.Rotate (x, y, z);
		}
	}
}
