using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain : MonoBehaviour {


	public void curtainSwitch(){
		this.gameObject.SetActive(false);
	}

	public void prep(){
		gameManager.Instance.reload();

	}
}
