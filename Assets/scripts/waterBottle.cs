using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBottle : MonoBehaviour {



	Rigidbody2D bottleRB;
	SpriteRenderer bottleSprite;

	public float power;

	Vector3 waterOriPos;
	Vector3 bottleDirection;

	public GameObject waterPref;
	public int waterAmount;
	GameObject[] waterParticles;
	bool isLanded;

	void Awake(){
		bottleRB = GetComponent<Rigidbody2D>();
		bottleSprite = GetComponent<SpriteRenderer>();
		Time.timeScale =1.5F;
		int childNum = transform.GetChild(1).childCount;
	}
	
	void Start(){
		waterParticles = new GameObject[waterAmount];
		for (int i= 0; i<waterAmount; i ++){
			waterParticles[i] = Instantiate(waterPref,transform.GetChild(1)) as GameObject;
		waterOriPos = waterParticles[0].transform.localPosition;
		}
	}
	void FixedUpdate () {

		if(Input.GetMouseButtonDown(0) && !isLanded){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 pointPos = new Vector3(mousePos.x,mousePos.y,0);
		Vector3 direction = (pointPos -new Vector3(transform.position.x,transform.position.y,0)).normalized;

		bottleDirection = direction;

		// Debug.Log(direction);
		

		// bottleRB.AddTorque(250f);
		}
		else if (Input.GetMouseButtonUp(0) && !isLanded){

			bottleRB.velocity = -bottleDirection * power * 10f;
			isLanded = true;
		}

	}

void OnCollisionEnter2D(Collision2D col){

	if(col.gameObject.tag == "badak"){
		// particleReposition();		

		isLanded = false;
	}
}

IEnumerator reposition(){

	particleReposition();
	yield return new WaitForSeconds(5f);
	StartCoroutine("reposition");
}
	void particleReposition (){
	Vector2	bottleOffset = new Vector2(bottleSprite.bounds.size.x *.5f,bottleSprite.bounds.size.x *.5f);
	Vector3 bottleCenter = bottleSprite.bounds.center;
		for(int i =0; i <waterParticles.Length; i++){
			if(waterParticles[i].transform.position.x<bottleCenter.x -bottleOffset.x ||waterParticles[i].transform.position.x>bottleCenter.x +bottleOffset.x ){
			waterParticles[i].transform.localPosition = waterOriPos;
			Debug.Log("repositioned : X");
			}

			if(waterParticles[i].transform.position.y <bottleCenter.y -bottleOffset.y || waterParticles[i].transform.position.y>bottleCenter.x +bottleOffset.y){
			waterParticles[i].transform.localPosition = waterOriPos;
			Debug.Log("repositioned : Y");

			}

		}

		 
	}
}
