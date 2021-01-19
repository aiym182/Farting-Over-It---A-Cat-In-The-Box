using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceController : MonoBehaviour {

	
		playerController playerController;
		Renderer pupilRenderer;
		Renderer eyeRenderer;
		Renderer mustatchRenderer;
		Renderer mouthRenderer;

		Renderer bodyRenderer;

		Transform face;



		// Eye textures 
		public Texture2D[] pupilTextures;
		public Texture2D[] eyeBlinkingTextures;
		public Texture2D[] eyeFlyTextures;

		//mustatch textures
		public Texture2D[] mustatchTextures;
		//mouth textures (0 :fly  1 :idle 2,3 : crush anim )
		public Texture2D[] mouthTextures;



		//Blinking Variables;

		
		public float blinkTimer; //max Timer for blink
		float blinkCount;

		public float transition;
		


		//eye wiggle transition
		public float crushAnimTransition;

		public float mustatchTimer; //max timer for mustatch


		//pupil's maximum offset
		public Vector2 pupilMax;

		void Awake(){
			

			//eye (0),mouth(1),pupil(3),mustatch(4)
			
			face = transform.GetChild(2);
			pupilRenderer = face.transform.GetChild(3).GetComponent<Renderer>();
			eyeRenderer = face.transform.GetChild(0).GetComponent<Renderer>();
			mustatchRenderer = face.transform.GetChild(4).GetComponent<Renderer>();
			mouthRenderer = face.transform.GetChild(1).GetComponent<Renderer>();
			bodyRenderer = transform.GetChild(1).GetComponent<Renderer>();

			
			playerController = GetComponent<playerController>();

			blinkCount = blinkTimer;


		}

		void Start(){
			mustatch();
			idleFace();
			outLine(true);
		}
		void Update(){
			blink();
		}

		public void pupilMovement(){
			Vector2 pupilPos = new Vector2(playerController.direction.x,playerController.direction.y);
			if(pupilPos.x < -pupilMax.x || pupilPos.x > pupilMax.x){

				pupilPos.x = Mathf.Clamp(pupilPos.x,-pupilMax.x,pupilMax.x);
			}
			if(pupilPos.y < -pupilMax.y || pupilPos.y > pupilMax.y){

				pupilPos.y = Mathf.Clamp(pupilPos.y,-pupilMax.x,pupilMax.y);
			}
			
			pupilRenderer.material.SetTexture("_MainTex",pupilTextures[0]);
			pupilRenderer.material.SetTextureOffset("_MainTex",new Vector2(pupilPos.x,pupilPos.y));

		}


	public void idleFace(){
			pupilRenderer.material.SetFloat("_Transparency",1.4f);
			mouthRenderer.material.SetTexture("_MainTex",mouthTextures[1]);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[0]);
			pupilRenderer.material.SetTexture("_MainTex",pupilTextures[0]);


	}
	
	public void blink(){


		blinkCount -= Time.fixedDeltaTime;	
		if(blinkCount <= 0){
			StartCoroutine("blinkAnimation");
		}

		
	
	}

	public void mustatch(){
		StartCoroutine("mustatchAnimation");

	}



	public void jumpingFace(){
				mouthRenderer.material.SetTexture("_MainTex",mouthTextures[0]);
				eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[0]);
				pupilRenderer.material.SetTexture("_MainTex",pupilTextures[1]);
				pupilRenderer.material.SetTextureOffset("_MainTex",Vector2.zero);
	}

	public void outLine(bool outlineSwitch){
		
		if(outlineSwitch){
			bodyRenderer.material.SetFloat("_Enable",1);
		}
		else{
			bodyRenderer.material.SetFloat("_Enable",0);
		}
	}


// blinking coroutine
//moving 1-2-3-4-3-1
		 IEnumerator blinkAnimation(){

			 
			blinkCount = blinkTimer + (Random.Range(-1,1));

			
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[0]);
			yield return new WaitForSeconds(transition);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[1]);
			yield return new WaitForSeconds(transition);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[2]);
			yield return new WaitForSeconds(transition);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[3]);
			yield return new WaitForSeconds(transition);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[2]);
			yield return new WaitForSeconds(transition);
			eyeRenderer.material.SetTexture("_MainTex",eyeBlinkingTextures[0]);

		}

	
		IEnumerator mustatchAnimation(){
			mustatchRenderer.material.SetTexture("_MainTex",mustatchTextures[0]) ;
			yield return new WaitForSeconds(mustatchTimer);
			mustatchRenderer.material.SetTexture("_MainTex",mustatchTextures[1]) ;
			yield return new WaitForSeconds(mustatchTimer);
			StartCoroutine("mustatchAnimation");
		


		}

	

		
}
