using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class playerController : MonoBehaviour {


	public GameObject crossHairPreFab;

	public GameObject dotPrefab;

	public static playerController Instance;

	Transform dotArray;

	// public float fallMultiplier;


	GameObject crossHair;
	GameObject[] dot;

	GameObject fartParticle;
	GameObject boxParticle;


	Vector3 playerPosition;
	Vector3 mousePos;
	Vector3 pointPos;
	Vector3 gravity;

	public Vector3 direction;

	public Rigidbody2D ballRB;

	public static bool launched;
	public static bool onClick;



//max magnitude
	[SerializeField][Range(0,15)]
	float maxMagnitude;
	float shootSpeed;

	//stopping magnitude
	public float stopMag =.1f;
	public float dotTimeStamp;
	public int numberOfDots;

	faceController faceController;
	static float timer;

	Animator catAnim;
	float shotMag;

	bool reached;
	Vector3 catRot;
	Vector3 catPos;

	bool firstTouch;

void Awake(){


	Instance = this;
	firstTouch = false;
	ballRB = GetComponent<Rigidbody2D>();
	
	faceController = GetComponent<faceController>();
	catAnim = GetComponent<Animator>();
	dotArray = transform.GetChild(3);
	fartParticle = transform.GetChild(4).gameObject;
	boxParticle = transform.GetChild(5).gameObject;
	catRot = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,transform.localEulerAngles.z);
	fartParticle.SetActive(false);
	boxParticle.SetActive(false);

	gravity = Vector2.up * -9.8f * 2;
	onClick = false;
	createDotAndCrossHair(numberOfDots);
	Time.timeScale = 1.8f;

	timer = 5f;
}

void start(){
	catPos = gameManager.Instance.cat.transform.position;
	catRot = gameManager.Instance.cat.transform.localEulerAngles;

}



	void FixedUpdate(){

		// if(ballRB.velocity.y <0){

		// 	ballRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1)*Time.fixedDeltaTime;
		// }
		mouseControl();


	}

	// && !EventSystem.current.IsPointerOverGameObject()
	
	void mouseControl(){

		if(Input.GetMouseButton(0) && !launched && !EventSystem.current.IsPointerOverGameObject()){
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pointPos = new Vector3(mousePos.x,mousePos.y,0);
			direction = (pointPos - new Vector3(transform.position.x,transform.position.y,0)).normalized;
			faceController.pupilMovement();
				

			onClick = true;
			
	


			if(shotMag >= maxMagnitude){
				reached = true;
			}
			else if(shotMag <= 0)
			
			{
				reached = false;
			}


			if(!reached){
				shotMag +=.15f;
			}
			else{
				shotMag -=.15f;
			}
			
	
		
			// float shotMag = (pointPos-new Vector3(transform.position.x,transform.position.y,0)).magnitude;
			// shotMag = Mathf.Clamp(shotMag,0,maxMagnitude);
			// shootSpeed = shotMag;
			shootSpeed = shotMag;
			calculateDotsPos(numberOfDots);

			
		}

		// & !EventSystem.current.IsPointerOverGameObject()
		else if (Input.GetMouseButtonDown(0) && !launched&& !EventSystem.current.IsPointerOverGameObject()){

			onClick= true;
		}
// !EventSystem.current.IsPointerOverGameObject()
		else if (Input.GetMouseButtonUp(0) && !launched&& !EventSystem.current.IsPointerOverGameObject()){

			//jump start
			ballRB.velocity =( -direction * shootSpeed*4f);

			//sounds


			//particles
			StartCoroutine("fart");
			StartCoroutine("box");

			//face
			faceController.jumpingFace();

			//Ui
			dotDisappeared();

			//cat Anim
			if(shootSpeed >=maxMagnitude*.7f)
			catAnim.SetInteger("catMotions",1);
			shotMag = 0;
			faceController.outLine(false);
			

			//mark position and rotation

			
			catPos = transform.position;
			catRot = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,transform.localEulerAngles.z);
			firstTouch = true;
			onClick = false;
			launched =true;
		
			
		}


		else if(ballRB.velocity.magnitude<= stopMag && launched){


			timer-= Time.fixedDeltaTime;

			if(timer <= 0){
			ballRB.velocity = Vector2.zero;
			shootSpeed = 0;


			faceController.idleFace();
			faceController.outLine(true);
			catAnim.SetInteger("catMotions",0);
			
			onClick = false;
			launched = false;
			timer = 1.0f;
			}
			else if (timer <5 && timer  > 0 && ballRB.velocity.sqrMagnitude>= stopMag){
				timer =1.0f;
			}
		}
	
	}
	void createDotAndCrossHair(int numberOfDots){
		dot = new GameObject[numberOfDots];
		for(int i =0; i<numberOfDots; i ++){
			dot[i] = Instantiate(dotPrefab,dotArray) as GameObject;
			dot[i].SetActive(false);
		}
		crossHair = Instantiate(crossHairPreFab,dotArray) as GameObject;
		crossHair.SetActive(false);


	}
	Vector3 calculatePos(float elapsedTime){
		return gravity * elapsedTime * elapsedTime * .5f + (shootSpeed *-direction*4f) *elapsedTime + transform.position;
	}


	void calculateDotsPos(int dots){

		int level = gameManager.Instance.diffIndex;
		for(int i = 0; i < dots; i++){

			dot[i].transform.position = new Vector3(calculatePos(dotTimeStamp * i).x,calculatePos(dotTimeStamp * i).y,dot[i].transform.position.z);
			
			if(level <1)
			dot[i].SetActive(true);
			else{
			dot[i].SetActive(false);
			}
		}
		crossHair.transform.position = new Vector3(calculatePos(dotTimeStamp * dots).x,calculatePos(dotTimeStamp * dots).y,crossHair.transform.position.z);
		crossHair.transform.up = -direction;
		if(level<1)
		crossHair.SetActive(true);
		else{
		crossHair.SetActive(false);

		}

	}

	void dotDisappeared(){
		for(int i =0; i <numberOfDots; i ++){
			dot[i].SetActive(false);

		}
		crossHair.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag =="badak"){
			catAnim.SetInteger("catMotions",2);
			fartParticle.SetActive(false);


			if(ballRB.velocity.sqrMagnitude >10){

			ContactPoint2D[] cp = new ContactPoint2D[2];
			col.GetContacts(cp);
						audioManager.Instance.PlayHit();

			Vector3 pos = new Vector3(cp[0].point.x,cp[0].point.y,transform.position.z);
			GameObject dust = poolingParticle.Instance.getPooledDustParticle(pos)as GameObject;
			dust.SetActive(true);
			}
			else{
				return;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag =="theEnd"){
			audioManager.Instance.PlayChicken();
			if(gameManager.Instance.diffIndex == 0){
				gameManager.Instance.isNormalComplete = true;
			}

			if(gameManager.Instance.diffIndex ==1){
				gameManager.Instance.isHardComplete = true;
				gameManager.Instance.isNormalComplete =true;
			}
			gameManager.Instance.completedTime = gameManager.Instance.finalTime;
			if(gameManager.Instance.isNormalComplete || gameManager.Instance.isHardComplete){
				if(gameManager.Instance.completedTime >gameManager.Instance.finalTime){
				gameManager.Instance.completedTime = gameManager.Instance.finalTime;
				}

			}
			gameManager.Instance.EndGameObj.SetActive(true);
			other.gameObject.SetActive(false);
			gameManager.Instance.endGame();
			
		}

	}
	IEnumerator fart(){
		fartParticle.transform.up =-direction;

		fartParticle.SetActive(true);
		audioManager.Instance.PlayFart();

		//.19 is 2(max Seconds)/max(MaxMag)
		float fartTimer = (2/maxMagnitude);
		yield return new WaitForSeconds(fartTimer*shootSpeed);
		fartParticle.SetActive(false);


	}

	IEnumerator box(){


		boxParticle.SetActive(true);
		yield return new WaitForSeconds(.5f);
		boxParticle.SetActive(false);
		
	
	}
	public void returntoPrevious(){

		if(firstTouch){
		transform.position = catPos;
		transform.localEulerAngles = catRot;
		ballRB.velocity = Vector2.zero;
		shootSpeed = 0;
		}
	
		
	}


}
