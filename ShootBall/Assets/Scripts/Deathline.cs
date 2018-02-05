using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathline : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		GameControl.instance.StopFlying ();
		Transform trans = GameControl.instance.StartCircle;
		col.gameObject.transform.position = trans.position;
		GameControl.instance.LastCircle = GameControl.instance.StartCircle;
	}
}
