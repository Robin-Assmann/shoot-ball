using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public static GameControl instance;

	public float time = 30;
	public bool flying = false;
	public bool running = false;
	public Transform StartCircle, LastCircle;
	public GameObject Timer, Points, Prefab_Background, Backgrounds, EndScreen;
	public GameObject Pre_Normal, Pre_Half, Pre_Quarter, Pre_3Quarters, Pre_Barrier, Pre_Bounce;
	public Sprite normal,half,quarter;
	public float ScreenWidth, ScreenHeight;

	private float height = -3.0f;
	private int point = 0;
	private int shownpoints = 0;
	private Transform Ball;

	//Setup Game / Get Screen absolute Resolution
	void Start () {

		instance = this;
		DontDestroyOnLoad (transform);
		this.shownpoints = 0;
		float right = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;
		float left = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).x;
		ScreenWidth = right - (left);

		float up = Camera.main.ScreenToWorldPoint (new Vector3(0, Screen.height, Camera.main.nearClipPlane)).y;
		float down = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).y;
		ScreenHeight = up - (down);

		Ball = GameObject.FindGameObjectWithTag ("Ball").transform;
		StartCircle = GameObject.FindGameObjectWithTag ("StartCircle").transform;
		LastCircle = StartCircle;
		StopFlying ();
	}

	//After Ball Movement Update Point Score
	void LateUpdate(){
	
		float tp_height = Ball.transform.position.y;
		if(tp_height>height){
			point += (int)((tp_height - height) /0.01f);
			shownpoints = point / 40;
			height = tp_height;
			Points.GetComponent<Text> ().text = shownpoints + "";
		}
	}

	//Control Time(Timer)
	void FixedUpdate () {
		if (running) {
			int min = (int)time / 60;
			int sec = (int)time % 60;
			string secs = sec.ToString ("D2");
			Timer.GetComponent<Text> ().text = min + ":" + secs;

			time -= 0.02f;

			if (time <= 0) {
				StopGame ();
			}
		}
	}

	//Stop the Game and Show End Screen
	void StopGame(){
		StopFlying ();
		this.running = false;

		int high = PlayerPrefs.GetInt ("Score");
		if (shownpoints > high) {
			PlayerPrefs.SetInt ("Score", shownpoints);
			high = shownpoints;
		}
		EndScreen.SetActive (true);
		EndScreen.transform.Find ("Score").GetComponent<Text> ().text = high + "";
	}

	//Restart the Game / Destroy all generated Backgrounds(Objects) /Reset starting values
	public void Restart(){

		this.height = 0.0f;
		this.point = 0;
		this.shownpoints = 0;
		this.time = 30;
		Timer.GetComponent<Text> ().text ="0:30";
		Points.GetComponent<Text> ().text ="0";
		EndScreen.SetActive (false);
		Transform trans = GameControl.instance.StartCircle;


		GameObject[] cc = GameObject.FindGameObjectsWithTag ("Normal");
		foreach (GameObject game in cc) {
			game.GetComponent<Circle> ().Init ();
		}
		GameObject[] cc2 = GameObject.FindGameObjectsWithTag ("Death");
		foreach (GameObject game in cc2) {
			game.GetComponent<Circle> ().Init ();
		}

		GameObject[] backs = GameObject.FindGameObjectsWithTag ("Background");
		foreach (GameObject game in backs) {
			Destroy (game);
		}

		GameObject[] StartBacks = GameObject.FindGameObjectsWithTag ("StartBackground");
		foreach (GameObject game in StartBacks) {
			game.GetComponent<GenerateBackground> ().enabled = true;
			game.GetComponent<GenerateBackground> ().Init ();
		}
		Ball.transform.position = trans.position;

	}

	//Stop motion of the Ball
	public void StopFlying(){

		Ball.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
		Ball.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
		Ball.GetComponent<Rigidbody2D> ().rotation = 0.0f;
		this.flying = false;
	}

	//Shoot the Ball / Reduce the velocity if too high
	public void StartFlying(float xVel, float yVel){
		this.running = true;

		xVel *= (-0.02f);
		yVel *= (-0.035f);
		if (yVel > 10.0f)
			yVel = 10.0f;

		if (xVel > 5.0f)
			xVel = 5.0f;

		if (xVel < -5.0f)
			xVel = -5.0f;

		Ball.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
		Ball.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVel, yVel);
		this.flying = true;
	}

	//Close Application
	public void CloseApp(){
	
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit ();
	
	}
}
