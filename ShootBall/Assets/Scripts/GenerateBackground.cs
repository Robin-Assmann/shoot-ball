using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBackground : MonoBehaviour {

	public int value = 2;

	//boolean to check for first ball enter
	private bool go = true;
	private Vector2 PossibleSpawn;

	//Initialize new background
	public void Initialize(){

		int count = 0;
		int ob = 1;
		List<GameObject> circles = new List<GameObject> ();
		List<GameObject> obstacles = new List<GameObject> ();

		//Decide the difficulty of the next Background
		if (value <= 10) {
			count = 4;
		} else if(value <= 20){
			count = 3;
		} else if(value <= 30){
			count = 2;
		} else if(value > 30){
			count = 1;
		}

		//Count of generated objects depending on the difficulty
		switch (count) {
		case 1:
			ob = 4;
			break;
		case 2:
			ob = 3;
			break;
		case 3:
			ob = 2;
			break;
		}

		//Differnet Difficult Levels decide outcome of new object
		for (int i = 0; i < count; i++) {
			//circles.Add (GameControl.instance.Pre_Normal);
			int value = Random.Range (0, 100);
			switch (count) {
			case 1:
				if (value < 40) {
					circles.Add (GameControl.instance.Pre_Half);
					break;
				} else if (value < 70) {
					circles.Add (GameControl.instance.Pre_Quarter);
					break;
				} else if (value < 90) {
					circles.Add (GameControl.instance.Pre_3Quarters);
					break;
				} else if (value < 100) {
					circles.Add (GameControl.instance.Pre_Normal);
					break;
				}
				break;
			case 2:
				if (value < 30) {
					circles.Add (GameControl.instance.Pre_Half);
					break;
				} else if (value < 60) {
					circles.Add (GameControl.instance.Pre_3Quarters);
					break;
				} else if (value < 80) {
					circles.Add (GameControl.instance.Pre_Quarter);
					break;
				} else if (value < 100) {
					circles.Add (GameControl.instance.Pre_Normal);
					break;
				}
				break;
			case 3:
				if (value < 40) {
					circles.Add (GameControl.instance.Pre_Quarter);
					break;
				} else if (value < 80) {
					circles.Add (GameControl.instance.Pre_Normal);
					break;
				} else if (value < 95) {
					circles.Add (GameControl.instance.Pre_Half);
					break;
				} else if (value < 100) {
					circles.Add (GameControl.instance.Pre_3Quarters);
					break;
				}
				break;
			case 4:
				if (value < 70) {
					circles.Add (GameControl.instance.Pre_Normal);
					break;
				} else if (value < 90) {
					circles.Add (GameControl.instance.Pre_Quarter);
					break;
				} else if (value < 100) {
					circles.Add (GameControl.instance.Pre_Half);
					break;
				} else if (value > 100) {
					circles.Add (GameControl.instance.Pre_3Quarters);
					break;
				}
				break;
			}
		}

		for (int i = 0; i < ob; i++) {
			if (Random.value >= 0.5)
				obstacles.Add (GameControl.instance.Pre_Bounce);
			else
				obstacles.Add (GameControl.instance.Pre_Barrier);
		}


		if (count % 2 == 0) {

			float offset = GameControl.instance.ScreenHeight / (2.0f * count);
			int round = 1;
			for (int i = 1; i <= count; i++) {
				
				GameObject spawn = Instantiate (circles [i - 1]) as GameObject;
				spawn.transform.SetParent (transform);
				int x = Mathf.FloorToInt (GameControl.instance.ScreenWidth) / 2;
				int y = Mathf.FloorToInt (GameControl.instance.ScreenHeight) / 2;
				PossibleSpawn = transform.position;
				spawn.transform.position = new Vector3 (PossibleSpawn.x - Random.Range (-x, x), PossibleSpawn.y - offset * (Mathf.Pow (-1.0f, i) * round), 0);
				if (spawn.name.Equals ("NormalCircle(Clone)")) {
					int value = Random.Range (2, 5);
					spawn.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = value + "";
					spawn.transform.GetChild (2).GetComponent<Circle> ().value = value;
				}
				if (i % 2 == 0) {
					round += 2;
				}
			}
			round = 0;
			offset = GameControl.instance.ScreenHeight / (2.0f * ob);
			for (int i = 1; i <= ob; i++) {

				GameObject spawn = Instantiate (obstacles [i - 1]) as GameObject;
				spawn.transform.SetParent (transform);
				float x = Mathf.FloorToInt (GameControl.instance.ScreenWidth) / 2;
				int y = Mathf.FloorToInt (GameControl.instance.ScreenHeight) / 2;
				PossibleSpawn = transform.position;
				spawn.transform.position = new Vector3 (PossibleSpawn.x - Random.Range (-x, x), PossibleSpawn.y - offset * (Mathf.Pow (-1.0f, i) * round), 0);
				if (count == 4)
					spawn.transform.Rotate (new Vector3 (0, 0, Random.Range (-20, 20)));
				else 
					spawn.transform.Rotate (new Vector3 (0, 0, Random.Range (-95, 95)));

				if ((i+1) % 2 == 0) {
					round += 2;
				}
			}

		} else {

			float offset = GameControl.instance.ScreenHeight / (2.0f * count);
			int round = 0;
			for (int i = 1; i <= count; i++) {

				GameObject spawn = Instantiate (circles [i - 1]) as GameObject;
				spawn.transform.SetParent (transform);
				int x = Mathf.FloorToInt (GameControl.instance.ScreenWidth) / 2;
				int y = Mathf.FloorToInt (GameControl.instance.ScreenHeight) / 2;
				PossibleSpawn = transform.position;
				spawn.transform.position = new Vector3 (PossibleSpawn.x - Random.Range (-x, x), PossibleSpawn.y - offset * (Mathf.Pow (-1.0f, i) * round), 0);
				if (spawn.name.Equals ("NormalCircle(Clone)")) {
					int value = Random.Range (2, 5);
					spawn.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = value + "";
					spawn.transform.GetChild (2).GetComponent<Circle> ().value = value;
				}

				if ((i+1) % 2 == 0) {
					round += 2;
				}
			}

			round = 1;
			offset = GameControl.instance.ScreenHeight / (2.0f * ob);
			for (int i = 1; i <= ob; i++) {

				GameObject spawn = Instantiate (obstacles [i - 1]) as GameObject;
				spawn.transform.SetParent (transform);
				float x = Mathf.FloorToInt (GameControl.instance.ScreenWidth) / 2;
				int y = Mathf.FloorToInt (GameControl.instance.ScreenHeight) / 2;
				PossibleSpawn = transform.position;
				spawn.transform.position = new Vector3 (PossibleSpawn.x - Random.Range (-x, x), PossibleSpawn.y - offset * (Mathf.Pow (-1.0f, i) * round), 0);
				if (count == 3)
					spawn.transform.Rotate (new Vector3 (0, 0, Random.Range (-15, 15)));
				else 
					spawn.transform.Rotate (new Vector3 (0, 0, Random.Range (-95, 95)));
				

				if (i % 2 == 0) {
					round += 2;
				}
			}
		}
	}
	public void Init(){
		go = true;
	}

	// On first enter of the ball generate a new background offscreen
	void OnTriggerEnter2D(Collider2D other){
		if (!go)
			return;
		go = false;
		GameObject back = Instantiate (GameControl.instance.Prefab_Background) as GameObject;

		back.transform.SetParent (GameControl.instance.Backgrounds.transform);
		back.transform.localPosition = new Vector2 (0, 6.4f + 12.8f * value);
		back.GetComponent<GenerateBackground> ().value = value+2;
		if (value % 2 != 0) {
			back.transform.Rotate (new Vector3 (0, 0, -180));
		}
		back.GetComponent<GenerateBackground> ().Initialize ();
		this.enabled = false;
	
	}
}
