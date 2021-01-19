using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolingParticle : MonoBehaviour {




	public static poolingParticle Instance;
	public GameObject dustParticle;
	public List<GameObject> pooledDustList;
	public int dustAmount;




	void Awake(){

		Instance = this;

	}
	void Start () {
		dustParticleStart();
		
	}
	
	// Update is called once per frame


	void dustParticleStart(){

		pooledDustList = new List<GameObject>();

		for( int i = 0; i < dustAmount; i ++){

			GameObject obj = Instantiate(dustParticle)as GameObject;
			obj.transform.SetParent(gameObject.transform,true);
			obj.SetActive(false);
			pooledDustList.Add(obj);
		}
	}

	public GameObject getPooledDustParticle(Vector3 pos){
		for(int i = 0; i< pooledDustList.Count; i ++){
			if (!pooledDustList[i].activeInHierarchy){
				pooledDustList[i].transform.position = pos;
				return pooledDustList[i];

			}

		
		}


			GameObject obj = Instantiate(dustParticle)as GameObject;
			obj.transform.SetParent(gameObject.transform,true);
			obj.SetActive(false);
			pooledDustList.Add(obj);
			return obj;
		

	

		
	}

}
