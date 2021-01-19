using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleScreen : MonoBehaviour {


	void screenLevel(){

		gameManager.Instance.titleScreen();
		gameManager.Instance.yourBestTime();
	}
}
