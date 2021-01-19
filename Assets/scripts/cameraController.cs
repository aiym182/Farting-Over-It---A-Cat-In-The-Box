using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {


	public Transform mainPlayer;
	Transform lightSource;

	playerController playerController;

	public float originalCameraSize;
	public float NewCameraSize;
	public float zoomSpeed;

	
	float smoothDampTime;
	public float followSpeed;
	public float xOffset;

	float yOffset;

// y is desired offset, x is initial offset
	public Vector2 YOffsetMinMax;

	
	
	Vector3 velocity = new Vector3(0,0,0);


	void Awake(){

		Camera.main.orthographicSize =originalCameraSize;
		lightSource = transform.GetChild(0).transform;
		yOffset = YOffsetMinMax.x;
		lights();
		

	
		
	}
	void Start(){
		mainPlayer = gameManager.Instance.cat.transform;
		transform.position = new Vector3(mainPlayer.position.x,mainPlayer.position.y+yOffset,transform.position.z);
		playerController = mainPlayer.gameObject.GetComponent<playerController>();

	}

	void FixedUpdate(){
		cameraMovement();
	}

	void cameraMovement(){
		if(mainPlayer != null){


		if(!playerController.onClick&&playerController.direction.x <0){
		transform.position = Vector3.SmoothDamp(transform.position,new Vector3(mainPlayer.position.x+xOffset,mainPlayer.position.y+yOffset,transform.position.z),ref velocity , followSpeed * Time.fixedDeltaTime);
		}
		else if (!playerController.onClick&& playerController.direction.x>0){
		transform.position = Vector3.SmoothDamp(transform.position,new Vector3(mainPlayer.position.x-xOffset,mainPlayer.position.y+yOffset,transform.position.z),ref velocity , followSpeed * Time.fixedDeltaTime);

		}
		else if (!playerController.onClick&&!playerController.launched){
		transform.position = Vector3.SmoothDamp(transform.position,new Vector3(mainPlayer.position.x,mainPlayer.position.y+yOffset,transform.position.z),ref velocity , followSpeed * Time.fixedDeltaTime);

		}
	
		if(playerController.onClick){
		Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize,NewCameraSize,ref smoothDampTime,zoomSpeed * Time.fixedDeltaTime);
		}
		else{
		Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize,originalCameraSize,ref smoothDampTime,zoomSpeed * Time.fixedDeltaTime);

		}
	

		}
	
	}

	void lights(){
		float height = Camera.main.orthographicSize *2f;

		float width = height * Camera.main.aspect;

		lightSource.position = new Vector3(transform.position.x+height*.5f,transform.position.y+width*.5f,transform.position.z + 1);
		lightSource.localScale = new Vector3 (width,height,lightSource.localScale.z);
	}
}
