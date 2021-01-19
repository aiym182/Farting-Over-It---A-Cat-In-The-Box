using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleController : MonoBehaviour {


	Rigidbody2D RB; 
	public float speed = 5f;



	void Awake(){
		RB = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){

			RB.velocity = Vector2.up * speed;
		}

	}
}
