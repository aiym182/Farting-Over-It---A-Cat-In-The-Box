using System.Collections;
using UnityEngine;
using System;
using System.IO;

public class saveManager : MonoBehaviour {

public static saveManager Instance;

public saveData data;

public GameObject mainPlayer;

public gameManager GM;

  public Vector3 originalPos;
  public  Vector3 originalangle;


void Awake(){

if(Instance == null){

    originalPos = new Vector3(-98,33,69);
    originalangle = new Vector3(-2,-189,-13); 
Instance = this;
DontDestroyOnLoad(this);


}
else{
    if(this != Instance){
        Destroy(this.gameObject);

    }
    
}
 

    GM =GetComponent<gameManager>();


}

public void PlayerSave(int level,Vector3 position,Vector3 rotation,float playerT, bool normalComplete, bool hardComplete, float completeTime){
data = new saveData();
data.level = level;
data.playerPosX = position.x;
data.playerPosY = position.y;
data.playerPosZ = position.z;
data.playerTime = playerT;
data.completedTime = completeTime;
data.playerRotX = rotation.x;
data.playerRotY = rotation.y;
data.playerRotZ = rotation.z;
data.IsNormalComplete =normalComplete;
data.IsHardComplete = hardComplete;

 string filePath = Application.dataPath + "/saveData/";

 File.WriteAllText(filePath+ "savedFile.json",JsonUtility.ToJson(data));


}
public  void PlayerLoad(){

    string filePath = Application.dataPath + "/saveData/";

    

    mainPlayer = gameManager.Instance.cat;
  
    if(File.Exists(filePath + "savedFile.json")){
    data =  JsonUtility.FromJson<saveData>(File.ReadAllText(filePath + "savedFile.json"));

    mainPlayer.transform.position = new Vector3(data.playerPosX,data.playerPosY,data.playerPosZ);
    GM.diffIndex = data.level;
    mainPlayer.transform.localEulerAngles = new Vector3(data.playerRotX,data.playerRotY,data.playerRotZ);
    gameManager.Instance.isNormalComplete = data.IsNormalComplete;
    gameManager.Instance.isHardComplete =data.IsHardComplete;
    gameManager.Instance.completedTime =data.completedTime;

    }


    else{

  
    GM.diffIndex = 0;
    mainPlayer = gameManager.Instance.cat;
    mainPlayer.transform.position = originalPos;
    mainPlayer.transform.localEulerAngles = originalangle;
    gameManager.Instance.finalTime = 0;
    gameManager.Instance.isNormalComplete = false;
    gameManager.Instance.isHardComplete = false;
    gameManager.Instance.completedTime = 99999999999f;

    }
}






}






[Serializable]
public class saveData{

    public int level;

    public float playerPosX;
    public float playerPosY;

    public float playerPosZ;

    public float playerRotX;
    public float playerRotY;
    public float playerRotZ;

    public float playerTime;
    public float completedTime;
    public bool IsNormalComplete;
    public bool IsHardComplete;



}
