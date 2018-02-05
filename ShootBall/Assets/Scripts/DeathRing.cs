using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRing : MonoBehaviour {

	//Rotate the Deathring
	void FixedUpdate(){
		transform.Rotate (new Vector3 (0, 0, 20.0f * Time.deltaTime));
	}

	//When Ball collides with an outer Ring
	void OnCollisionEnter2D(Collision2D col){
		GameControl.instance.StopFlying ();
		Transform trans = GameControl.instance.LastCircle;
		col.gameObject.transform.position = trans.position;
	}
}
