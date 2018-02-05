using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Transform Ball;
	private float offset = 3.0f;

	//Trace the ball / After ball Movement
	void LateUpdate () {

		float x = transform.position.x;
		float z = transform.position.z;

		transform.position = new Vector3 (x, Ball.position.y + offset,z);
	}
}
