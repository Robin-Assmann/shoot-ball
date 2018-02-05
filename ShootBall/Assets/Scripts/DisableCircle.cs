using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCircle : MonoBehaviour {

	public GameObject Circle;

	void OnTriggerExit2D(Collider2D col){

		if(Circle.GetComponent<CircleCollider2D> ())
		Circle.GetComponent<CircleCollider2D> ().enabled = true;
	}
}
