
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour {




	public static audioManager Instance;
	public sound[] sounds;


	void Awake(){

		Instance = this;
	
		// foreach(sound s in sounds){

		// 	s.source = gameObject.AddComponent<AudioSource>();
		// 	s.source.clip = s.file;
		// 	s.source.name = s.name;
		// 	s.source.volume = s.volume;
		// 	s.source.pitch = s.pitch;

		// }

		for (int i = 0; i <sounds.Length; i ++ ){

			sounds[i].source =gameObject.AddComponent<AudioSource>();
			sounds[i].source.clip = sounds[i].file;
			sounds[i].source.volume = sounds[i].volume;
			sounds[i].source.pitch = sounds[i].pitch;
			
		}
	}

	public void PlayFart(){

	if(!sounds[0].source.isPlaying){
		sounds[0].source.pitch = Random.Range(1f,3f);
		sounds[0].source.Play();
	}
	else{
		return;
	}
		
	}

	public void PlayButton(){
		if(!sounds[1].source.isPlaying){
			sounds[1].source.pitch =1;
			sounds[1].source.volume =1;
			sounds[1].source.Play();
		}
		else{
			return;
		}
	}

	public void PlayHit(){
		if(!sounds[2].source.isPlaying){
			sounds[2].source.pitch =Random.Range(1f,2f);
			sounds[2].source.volume =.5f;
			sounds[2].source.Play();
		}
		else{
			return;
		}

	}
	public void PlayChicken(){
		if(!sounds[3].source.isPlaying){
			sounds[3].source.pitch =1;
			sounds[3].source.volume =1;
			sounds[3].source.Play();
		}
		
		else{
			return;
		}
	}
	public void muteOnAndOff(int mute){
		for(int i =0; i<sounds.Length;i++){
			if(mute>0){

				sounds[i].source.mute =true;
			}
			else{
				sounds[i].source.mute = false;
			}

		}

	}
}

