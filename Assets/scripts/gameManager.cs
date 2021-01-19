using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {


public static gameManager Instance;

public GameObject mainPlayer;

public GameObject cat;

public GameObject TitleObj;

public GameObject timerTextObj;

public GameObject UiDifficultyObj;
public GameObject returnButtonObj;

public GameObject MessageBoardObj;

public GameObject EndGameObj;
public GameObject curtainObj;

public GameObject chickenBucket;

float stopWatch;

public int diffIndex;

public int soundIndex;

public int timerIndex;

public float finalTime;

public float completedTime;
float lastSavedTime;
bool isPaused;
bool isDiffChanged;

public bool isNormalComplete;
public bool isHardComplete;


TextMeshProUGUI timerText;

TextMeshProUGUI difficultyText;

TextMeshProUGUI changingDiffText;

// timer on off in setting menu
TextMeshProUGUI settingTimerText;

// sound on off in setting menu

TextMeshProUGUI settingSoundText;

TextMeshProUGUI MessageBoardText;




string[] diffUI;
string[] soundUI;



void Awake(){

    Instance = this;

    initialize();

    diffUI = new string[2];

    soundUI = new string[2];


}
void OnEnable(){
    if(saveManager.Instance != null){
         createPlayer();
        saveManager.Instance.PlayerLoad();
    }
        else{
            Debug.Log("There is no saveManager gameObject");
        }
    soundIndex = PlayerPrefs.GetInt("sound");
    timerIndex = PlayerPrefs.GetInt("TimerUI");


}
void Start(){
    //this is for ios and android
    // returnSwitch();
    stopWatch = 0;
    lastSavedTime = saveManager.Instance.data.playerTime;
    soundBox(soundIndex);
    timerBox(timerIndex);

}
void Update(){
    timer();

}



void OnApplicationQuit(){
    saveManager.Instance.PlayerSave(diffIndex,cat.transform.position,cat.transform.localEulerAngles,finalTime,isNormalComplete,isHardComplete,completedTime);
        PlayerPrefs.SetInt("sound",soundIndex);
        PlayerPrefs.SetInt("TimerUI",timerIndex);
        PlayerPrefs.Save();

}



public void pauseButton(){
    audioManager.Instance.PlayButton();
    TitleObj.transform.GetChild(2).gameObject.SetActive(false);
    TitleObj.transform.GetChild(3).gameObject.SetActive(false);
    TitleObj.transform.GetChild(4).gameObject.SetActive(false);
    isPaused =true;
    Time.timeScale = 0f;
}



public void resumeButton(){
             audioManager.Instance.PlayButton();

    isPaused =false;
    Time.timeScale = 1.8f;
}

void initialize(){
    timerTextObj = transform.GetChild(0).gameObject;
    TitleObj = transform.GetChild(1).gameObject;
    UiDifficultyObj =transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;
    returnButtonObj =transform.GetChild(3).gameObject;
    MessageBoardObj =transform.GetChild(7).gameObject;
    EndGameObj = transform.GetChild(8).gameObject;
    curtainObj = transform.GetChild(transform.childCount-1).gameObject;

    timerText = timerTextObj.GetComponent<TextMeshProUGUI>();
    difficultyText = UiDifficultyObj.GetComponent<TextMeshProUGUI>();
    settingTimerText = transform.GetChild(2).gameObject.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>();
    settingSoundText = transform.GetChild(2).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
    MessageBoardText = MessageBoardObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


}

void timer(){

    if(!isPaused){
    stopWatch += Time.deltaTime *.555f;
    }
   

    finalTime = stopWatch + lastSavedTime;


    int seconds = (int)finalTime%60;

    int minuate = Mathf.FloorToInt((finalTime%3600)/60);

    int hour =(int) (finalTime /216000);

    timerText.text = hour.ToString("D2") + " : " + minuate.ToString("D2") + " : " + seconds.ToString("D2");
    }

 

    // difficulty text in setting menu
     void difficultyBox (int indexNum){
        
         diffUI[0] = "Diffyculty : Normal ";
         diffUI[1] = "Diffyculty : Hard ";
        //  diffUI[2] = "Diffyculty : Insane " ;

        difficultyText.text = diffUI[indexNum];

     }


    //setting Button
    public void Setting(){
        audioManager.Instance.PlayButton();
        soundBox(soundIndex);
        difficultyBox(diffIndex);
        timerBox (timerIndex);
    }
    void returnSwitch(){
   if(returnButtonObj != null){
        if(diffIndex>0){
            returnButtonObj.SetActive(false);
        }
        else{
            returnButtonObj.SetActive(true);
        }

    }
    else{
         Debug.Log("returnButtonObj is not there");

    }
    }

    //setting sound function
    void soundBox(int onAndOff){
        if(onAndOff >0){
            settingSoundText.text = "Sound : Off";
            audioManager.Instance.muteOnAndOff(soundIndex);

        }
        else{
            settingSoundText.text ="Sound : on";
            audioManager.Instance.muteOnAndOff(soundIndex);


        }
    }
    //setting timer function
     void timerBox(int OnAndOff){
    
        if(OnAndOff>0){
            settingTimerText.text = "Timer : On ";
            timerTextObj.SetActive(true);
        }
        else{
            settingTimerText.text = "Timer : Off ";
            timerTextObj.SetActive(false);
        }
     }

     void createPlayer(){
         cat = Instantiate(mainPlayer) as GameObject;
     }


    // difficulty toggle buttons
     public void DifficultyToggleRight(){
         audioManager.Instance.PlayButton();

         diffIndex++;
         if(diffIndex >diffUI.Length-1)
         diffIndex = 0;

        difficultyBox(diffIndex);

     }

     public void DifficultyToggleLeft(){
         audioManager.Instance.PlayButton();

         diffIndex--;
         if(diffIndex<0)
         diffIndex = diffUI.Length -1;

         difficultyBox(diffIndex);

     }


   


    // timer toggle buttons

    public void timerToggleRight(){
    audioManager.Instance.PlayButton();
    if(timerIndex >0){
        timerIndex =0;
    }
    else{
        timerIndex= 1;
    }
        timerBox(timerIndex);
    }

    public void timerToggleLeft(){
    audioManager.Instance.PlayButton();

 if(timerIndex >0){
        timerIndex =1;
    }
    else{
        timerIndex= 0;
    }
        timerBox(timerIndex);

    }

    //sound ON and OFF toggle


    public void soundToggleLeft(){

        if(soundIndex>0){
            soundIndex= 0;

        }
        else{
            soundIndex=1;
        }
        audioManager.Instance.PlayButton();
            soundBox(soundIndex);
    


    }
   
    public void soundToggleRight(){
           if(soundIndex>0){
            soundIndex= 0;

        }
        else{
            soundIndex=1;
        }
    audioManager.Instance.PlayButton();
         soundBox(soundIndex);
   


    }

     public void exitButton(){
             audioManager.Instance.PlayButton();

            if(diffIndex != saveManager.Instance.data.level){
                MessageBoardObj.SetActive(true);

                MessageBoardSwitch(1);
           }
    
     }

     public void yesButtonOnMessage(){
        audioManager.Instance.PlayButton();
        curtainObj.SetActive(true);
  
      
     }

     public void noButtonOnMessage(){
            audioManager.Instance.PlayButton();

         diffIndex = saveManager.Instance.data.level;
     }

     public void returnButton(){
         audioManager.Instance.PlayButton();
         playerController.Instance.returntoPrevious();
     }

    public void reload(){
        resumeButton();

        //this is for ios and android
        // returnSwitch(); 
        stopWatch = 0;
        finalTime = 0;
        isPaused =false;
        lastSavedTime =0;
        chickenBucket.SetActive(true);
        Camera.main.transform.position = new Vector3(saveManager.Instance.originalPos.x,saveManager.Instance.originalPos.y,Camera.main.transform.position.z);
        cat.transform.position =  saveManager.Instance.originalPos;
        cat.transform.localEulerAngles  =saveManager.Instance.originalangle;
        saveManager.Instance.PlayerSave(diffIndex,cat.transform.position,cat.transform.localEulerAngles,finalTime,isNormalComplete,isHardComplete,completedTime);

    }



    //startOver Button
    public void startOver(){
        audioManager.Instance.PlayButton();

        MessageBoardObj.SetActive(true);
        MessageBoardSwitch(2);

    }

  

    void MessageBoardSwitch(int switchNum){
        
        switch(switchNum){
            case 1 : MessageBoardText.text = "changing difficulty will void your current progress." +System.Environment.NewLine + "Do you wish to continue? ";
                    MessageBoardObj.transform.GetChild(1).gameObject.SetActive(true); // yesButton
                    MessageBoardObj.transform.GetChild(2).gameObject.SetActive(true); // NoButton
            break;

            case 2 : MessageBoardText.text= "this will void your current progress. "+System.Environment.NewLine +"Do you wish to continue? ";
                   MessageBoardObj.transform.GetChild(1).gameObject.SetActive(true); // yesButton
                    MessageBoardObj.transform.GetChild(2).gameObject.SetActive(true); // NoButton
            break;

            default :
            break;
        }

    }

    public void endGame(){
        isPaused = true;
        TextMeshProUGUI endTimerText = EndGameObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        int seconds = (int)finalTime%60;

        int minuate = Mathf.FloorToInt((finalTime%3600)/60);

        int hour =(int) (finalTime /216000);

        endTimerText.text = hour.ToString("D2") + " : " + minuate.ToString("D2") + " : " + seconds.ToString("D2");

    }


    public void ChangingLevelOnEndGame(){
        audioManager.Instance.PlayButton();
        if(diffIndex<1){
            diffIndex =1;
            EndGameObj.SetActive(false);
            curtainObj.SetActive(true);

        }
        else{
         TextMeshProUGUI levelChange = EndGameObj.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
         levelChange.text = "You already finished your hardest level";
        }

    }
    public void StartOverOnEndGame(){
        audioManager.Instance.PlayButton();
        EndGameObj.SetActive(false);
        curtainObj.SetActive(true);
    }

    public void yourBestTime(){
        if(isNormalComplete || isHardComplete){
        GameObject bestTime = TitleObj.transform.GetChild(4).gameObject;
        TextMeshProUGUI bestTimeText =bestTime.GetComponent<TextMeshProUGUI>();
        int seconds = (int)completedTime%60;

        int minuate = Mathf.FloorToInt((completedTime%3600)/60);

        int hour =(int) (completedTime /216000);

        bestTimeText.text = "best record" + System.Environment.NewLine +hour.ToString("D2") + " : " + minuate.ToString("D2") + " : " + seconds.ToString("D2");
        bestTime.SetActive(true);

        }
    }


    public void titleScreen(){
        
        if(isNormalComplete)
        TitleObj.transform.GetChild(2).gameObject.SetActive(true);
        else{
            TitleObj.transform.GetChild(2).gameObject.SetActive(false);
        }

        if(isHardComplete){
        TitleObj.transform.GetChild(3).gameObject.SetActive(true);
        }
        else{
            TitleObj.transform.GetChild(3).gameObject.SetActive(false);

        }
    }
}