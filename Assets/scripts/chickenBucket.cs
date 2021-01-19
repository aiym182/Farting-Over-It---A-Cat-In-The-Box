using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenBucket : MonoBehaviour {


	public float turnSpeed;

	void Update () {
		transform.Rotate(new Vector2(0,1) *turnSpeed *Time.deltaTime);
	}
}
