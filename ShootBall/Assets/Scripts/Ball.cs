using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float x,y;
	public float xleft, xright;

	void Start(){

		xleft = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).x;
		xright = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;
	}

	void FixedUpdate () {

		Vector3 now = Camera.main.WorldToScreenPoint (transform.position);

		if (GameControl.instance.flying) {

			if (now.x < 0) 
				transform.position = new Vector2 (xright, transform.position.y);

			if (now.x > Screen.width)
				transform.position = new Vector2 (xleft, transform.position.y);
		}
	}

}
