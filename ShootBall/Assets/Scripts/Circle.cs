using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	public Transform DragBox, TextCanvas;
	public int value = 5;

	private Vector2 touchCache, startpoint, dragpoint;
	private bool touched = false;
	private bool flying = true;
	private bool done = false;
	private float width, height;

	void Start () {
		if (GetComponent<CircleCollider2D> ())
			width = transform.GetComponent<CircleCollider2D> ().radius;
		else
			width = 0.45f;
		startpoint = Vector2.zero;
	}

	void Update () {


		//If in Editor mode use Keyboard Inputs
		#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0)){

			Vector2 startingdrag = DragBox.position;
			Vector3 mouseCache = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.nearClipPlane));
			if(mouseCache.x >= (startingdrag.x - width*2) && mouseCache.x <= (startingdrag.x + width*2) && mouseCache.y >= (startingdrag.y - width*2) && mouseCache.y <= (startingdrag.y + width*2)){
				if(startpoint == Vector2.zero)
					startpoint = Camera.main.WorldToScreenPoint (mouseCache);
				touched = true;
				flying = false;
			}
		}

		if(Input.GetMouseButtonUp(0)){

			touched = false;
		}
		#endif

		//Else use Touch as Input
		if(Input.touchCount>0){

			Touch touch = Input.GetTouch(0);
			switch (touch.phase) {

			case TouchPhase.Began:
				Vector2 startingdrag = DragBox.position;
				Vector3 mouseCache = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,Camera.main.nearClipPlane));

				if (mouseCache.x >= (startingdrag.x - width*2) && mouseCache.x <= (startingdrag.x + width*2) && mouseCache.y >= (startingdrag.y - width*2) && mouseCache.y <= (startingdrag.y + width*2)) {
					if (startpoint == Vector2.zero) {
						startpoint = Camera.main.WorldToScreenPoint (mouseCache);
					}
					touched = true;
					flying = false;
				}
				break;
			case TouchPhase.Ended:
				touched = false;
				break;
			}
		}
	}

	//Check if ball should be shot
	void FixedUpdate(){

		if (touched) {
			dragpoint = Input.mousePosition;
		}

		if (!touched && !flying) {
			Shooting ();
		}
	}

	public void Init(){
	
		this.done = false;
		TextCanvas.gameObject.SetActive (true);
		if (!transform.parent.name.Equals ("NormalCircle")) {
			transform.parent.GetChild (1).transform.GetComponent<DeathRing> ().enabled = true;
			transform.parent.GetChild (1).transform.GetComponent<PolygonCollider2D> ().enabled = true;

			switch (transform.parent.name) {

			case "Half":
				transform.parent.GetChild (1).transform.GetComponent<SpriteRenderer> ().sprite = GameControl.instance.half;
				break;
			case "Quarter":
				transform.parent.GetChild (1).transform.GetComponent<SpriteRenderer> ().sprite = GameControl.instance.quarter;
				break;
			}
		}
	}

	public void UpdateTime(){
	
		if (!done) {
			GameControl.instance.time += value;
			this.done = true;
		}
	}

	void Shooting(){
		this.flying = true;
		GameControl.instance.StartFlying(((dragpoint.x-startpoint.x)), ((dragpoint.y - startpoint.y)));
		startpoint = Vector2.zero;
		this.enabled = false;
	}

}
