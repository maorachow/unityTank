using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBeh : MonoBehaviour
{
    public float width; 
    public float height; 
    public GameObject heartMask;
    public RectTransform rt;
    public RectTransform heartMaskrt;
    public List<GameObject> enemyLifeList;
    public GameObject enemyLifePrefab;
    public GameObject playerLifeText;
    public Text playerLifeTextText;
    public Text enemyLifeText;
    public Button pauseButton;
    public Button mobileFireButton;
    public static Slider mobileViewRangeSlider;
    public static Transform joystick1Pos;
    public static Transform joystick2Pos;
    public static GameObject playerMovementJoystick;
    public static GameObject shooterMovementJoystick;
    public static GameObject playerFireButton;
    public static Vector2 mobilePlayerVec;
    public static Vector2 mobilePlayerShooterVec;
    public RuntimePlatform platform = Application.platform;
    public bool isPaused;
    public GameObject pauseMenuUI;
    public GameObject respawnCDText;

    // Start is called before the first frame update
    void Start()
    {   
         platform=Application.platform;
         playerMovementJoystick=GameObject.Find("playermovejoystick");
         shooterMovementJoystick=GameObject.Find("shootermovejoystick");
         playerFireButton=GameObject.Find("firebutton");
         mobileViewRangeSlider=GameObject.Find("viewrangeslider").GetComponent<Slider>();

         if(platform!=RuntimePlatform.Android&&platform!=RuntimePlatform.IPhonePlayer){
              playerMovementJoystick.SetActive(false);
                shooterMovementJoystick.SetActive(false);
                playerFireButton.SetActive(false);
                mobileViewRangeSlider.gameObject.SetActive(false);
         }else{

                   mobileFireButton=playerFireButton.GetComponent<Button>();
  
       mobileFireButton.onClick.AddListener(FireButtonOnClick); 
    
       mobileViewRangeSlider.onValueChanged.AddListener(cameraSizeFit.mobileViewRangeSliderOnValueChanged);
      shooterMovementJoystick.GetComponent<ScrollRect>().onValueChanged.AddListener(ShooterMovementJoystickOnValueChanged);
      playerMovementJoystick.GetComponent<ScrollRect>().onValueChanged.AddListener(PlayerMovementJoystickOnValueChanged);
        joystick1Pos=playerMovementJoystick.transform.GetChild(0);
        joystick2Pos=shooterMovementJoystick.transform.GetChild(0);
         }

        enemyLifeText=GameObject.Find("enemyLifeText").GetComponent<Text>();
        respawnCDText=GameObject.Find("respawnTime");
        pauseMenuUI=GameObject.FindGameObjectWithTag("pauseMenuUI");
        pauseButton=GameObject.Find("Pause").GetComponent<Button>();
        pauseButton.onClick.AddListener(pauseButtonClick);
        playerLifeText=GameObject.Find("playerLifeText");
        playerLifeTextText=playerLifeText.GetComponent<Text>();
        enemyLifeList=new List<GameObject>();
        enemyLifePrefab=Resources.Load<GameObject>("prefabs/enemyLife");
        heartMask=GameObject.Find("backgroundImage");
        rt=GetComponent<RectTransform>();
        width= rt.rect.width;
        height=rt.rect.height;
        heartMaskrt=heartMask.GetComponent<RectTransform>();
        for(int i = 0; i < WorldGen.enemyCount; i++) {
        GameObject a=Instantiate(enemyLifePrefab,new Vector2(0f,0f)/*new Vector2(0f,0f)*/,transform.rotation);
        a.transform.SetParent(heartMaskrt);
        a.transform.localPosition=new Vector2(4f*i+17f,-8f);
        a.transform.localScale=new Vector3(0.2f,1.6f,1f);
        pauseMenuUI.SetActive(false);

   // Debug.Log(a.GetComponent<RectTransform>().pivot);
//Debug.Log(a.GetComponent<RectTransform>().anchoredPosition);
        enemyLifeList.Add(a);
        }


    }
    void ShooterMovementJoystickOnValueChanged(Vector2 v){
            mobilePlayerShooterVec=joystick2Pos.localPosition/100;
    }
    void PlayerMovementJoystickOnValueChanged(Vector2 v){
            mobilePlayerVec=joystick1Pos.localPosition/100;
    }
    void FireButtonOnClick(){
        if(playermove.isUsingMobileControl){
            if(GameObject.FindGameObjectWithTag("Player")!=null){
               GameObject.FindGameObjectWithTag("Player").GetComponent<playermove>().ShootBullet(); 
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
      //  if(playerMovementJoystick.activeInHierarchy==true&&shooterMovementJoystick.activeInHierarchy==true){
        //    mobilePlayerVec=playerMovementJoystick.GetComponent<ScrollRect>().normalizedPosition+new Vector2(0.5f,0.5f);
        //    mobilePlayerShooterVec=shooterMovementJoystick.GetComponent<ScrollRect>().normalizedPosition+new Vector2(0.5f,0.5f);
      //  }
   //   if(platform==RuntimePlatform.Android||platform==RuntimePlatform.IPhonePlayer){
   //           mobilePlayerVec=joystick1Pos.localPosition/100;
    //  mobilePlayerShooterVec=joystick2Pos.localPosition/100;
    //  }

  //      Debug.Log(mobilePlayerVec);
       // Debug.Log(mobilePlayerShooterVec);
        if(WorldGen.tankPlayer==null){
            respawnCDText.GetComponent<Text>().text=((int)WorldGen.playerRespawnCD).ToString();
           // respawnCDText.SetActive(true);
            
        }else{
            respawnCDText.GetComponent<Text>().text="   ";
          //  respawnCDText.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
        //   Debug.Log("pause");
            if(isPaused==false){
                Pause();
            }else{
                Resume();
            }
        }

        playerLifeTextText.text="Player Life:"+WorldGen.playerLifeCount;
        if(WorldGen.enemyCount>20){
        enemyLifeText.text="Enemy Life:"+WorldGen.enemyCount;
        for(int i=0;i<enemyLifeList.Count;i++){
            enemyLifeList[i].GetComponent<Image>().color=new Color(1f,1f,1f,0f);
        }
        }else{
                    for(int i=0;i<enemyLifeList.Count;i++){
            enemyLifeList[i].GetComponent<Image>().color=new Color(1f,1f,1f,1f);
        }
             enemyLifeText.text="Enemy Life:";
                 if(enemyLifeList.Count>WorldGen.enemyCount){
for(int i = 0; i <enemyLifeList.Count-WorldGen.enemyCount ; i++) {
    Destroy(enemyLifeList[enemyLifeList.Count-i-1]);
    enemyLifeList.Remove(enemyLifeList[enemyLifeList.Count-i-1]);

}}else if(enemyLifeList.Count<WorldGen.enemyCount){
    for(int i = 0; i < WorldGen.enemyCount-enemyLifeList.Count; i++) {
       GameObject a=Instantiate(enemyLifePrefab,new Vector2(0f,0f)/*new Vector2(0f,0f)*/,transform.rotation); 
        a.transform.SetParent(heartMaskrt);
    a.transform.localPosition=new Vector2(4f*(i+enemyLifeList.Count)+17f,-8f);
    a.transform.localScale=new Vector3(0.2f,1.6f,1f);
enemyLifeList.Add(a);
       // Debug.Log("success add");
    }
}  
        }

       }


  void pauseButtonClick(){
      //    Debug.Log("pause");
            if(isPaused==false){
               isPaused=true;
pauseMenuUI.SetActive(true);
Time.timeScale=0;
            }else{
               Time.timeScale=1;
        isPaused=false;
        pauseMenuUI.SetActive(false);
            }
       }

        void Pause(){
isPaused=true;
pauseMenuUI.SetActive(true);
Time.timeScale=0;
       }

        void Resume(){
        Time.timeScale=1;
        isPaused=false;
        pauseMenuUI.SetActive(false);
       }
      
 //      Debug.Log(heartMaskrt.pivot);
//Debug.Log(heartMaskrt.anchoredPosition);
//Debug.Log(heartMaskrt.localPosition);
      //  heartMaskrt.anchoredPosition=new Vector2(-width/2,height/2);
    //heartMaskrt.anchoredPosition .height=height/2;

    }
   

