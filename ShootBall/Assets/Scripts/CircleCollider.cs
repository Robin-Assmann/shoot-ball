using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour {

	//Start needed to make Script enableable
	void Start(){
	}

	//If the Ball hits the Circle / Update Ball Position / Exceptions for special Circles
	void OnCollisionEnter2D(Collision2D col){

		GameControl.instance.StopFlying ();
		col.gameObject.transform.position = transform.position;
		GameControl.instance.LastCircle = transform;

		gameObject.GetComponent<Circle> ().enabled = true;
		gameObject.GetComponent<CircleCollider2D> ().enabled = false;

		if (gameObject.tag != "StartCircle") {
			transform.parent.GetChild (0).gameObject.SetActive (false);
			if (gameObject.tag != "Normal") {
				transform.parent.GetChild (1).gameObject.GetComponent<DeathRing> ().enabled = false;
				transform.parent.GetChild (1).gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
				transform.parent.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().sprite = GameControl.instance.normal;
			}
			gameObject.GetComponent<Circle> ().UpdateTime ();
		}

		this.enabled = false;
	}
}
