using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using LitJson;
public class SettingsData{
    public int worldWidth;
    public int playerLife;
    public float worldBrickDensity;
    public bool isUsingKeboardShooterControl;
    public int enemyCount;
    public int enemyOnWorldCount;

}
public class settingsUIBeh : MonoBehaviour
{   
    public static string dataStorageLocation;
        public string worldSettingsString;
    public Text worldBrickDensityText;
    public Text playerLifeText;
    public Text worldWidthText;
    public Text dataPathText;
    public static Slider playerLifeSlider;
    public static Slider worldBrickDensitySlider;
    public Button saveButton;
    public static GameObject settingsUIObject;
    public static Slider worldWidthSlider;
    public static Slider enemyCountSlider;
    public static Slider enemyOnWorldCountSlider;
    public Text enemyOnWorldCountText;
    public Text enemyCountText;
    public Toggle isUsingKeboardShooterControlToggle;
    public static SettingsData settingsDataReadFromDisk;
    public static SettingsData currentSettingsData=new SettingsData();
    void Start()

    {   switch(Application.platform){
        case RuntimePlatform.Android: dataStorageLocation=Application.persistentDataPath;break;
        case RuntimePlatform.WindowsEditor:dataStorageLocation="C:/unityTankData";break;
        case RuntimePlatform.WindowsPlayer: dataStorageLocation="C:/unityTankData";break;
        default: dataStorageLocation=Application.persistentDataPath;break;
    }
        if(!Directory.Exists(dataStorageLocation+"/Json")){
        Directory.CreateDirectory(dataStorageLocation);
        Directory.CreateDirectory(dataStorageLocation+"/Json");
        if(!File.Exists(dataStorageLocation+"/Json/Settings.json")){
            File.Create(dataStorageLocation+"/Json/Settings.json");
        }
    }
        
        currentSettingsData=new SettingsData();
        Debug.Log(currentSettingsData);
        startGameUI.isSettingChanged=true;
        enemyOnWorldCountText=GameObject.Find("enemyOnWorldCountText").GetComponent<Text>();
        enemyOnWorldCountSlider=GameObject.Find("Sliderenemyonworldcount").GetComponent<Slider>();
        dataPathText=GameObject.Find("dataPath").GetComponent<Text>();
        worldWidthText=GameObject.Find("worldWidthText").GetComponent<Text>();
        settingsUIObject=GameObject.Find("settingsMenu");
        worldBrickDensityText=GameObject.Find("worldBrickDensityText").GetComponent<Text>();
         playerLifeText=GameObject.Find("playerLifeText").GetComponent<Text>();
         enemyCountSlider=GameObject.Find("Sliderenemylife").GetComponent<Slider>();
         enemyCountText=GameObject.Find("enemyLifeText").GetComponent<Text>();
         playerLifeSlider=GameObject.Find("Sliderpl").GetComponent<Slider>();
         worldBrickDensitySlider=GameObject.Find("Sliderwbd").GetComponent<Slider>();
         saveButton=GameObject.Find("saveButton").GetComponent<Button>();
         worldWidthSlider=GameObject.Find("Sliderww").GetComponent<Slider>();
         playerLifeSlider.onValueChanged.AddListener(playerLifeSliderChanged);
         worldBrickDensitySlider.onValueChanged.AddListener(worldBrickDensitySliderChanged);
         worldWidthSlider.onValueChanged.AddListener(worldWidthSliderChanged);
         enemyCountSlider.onValueChanged.AddListener(enemyCountSliderChanged);
         enemyOnWorldCountSlider.onValueChanged.AddListener(enemyOnWorldCountSliderChanged);
         saveButton.onClick.AddListener(saveButtonOnClick);
         worldWidthText.text=(2*(worldWidthSlider.value)).ToString()+" * "+(2*(worldWidthSlider.value)).ToString();
         playerLifeText.text=playerLifeSlider.value.ToString();
         worldBrickDensityText.text=worldBrickDensitySlider.value.ToString();
        isUsingKeboardShooterControlToggle=GameObject.Find("isUsingKeyboardShooterControl").GetComponent<Toggle>();
        isUsingKeboardShooterControlToggle.onValueChanged.AddListener((bool isOn)=> { isUsingKeboardShooterControlToggleOnClick(isUsingKeboardShooterControlToggle,isOn); });





        worldSettingsString=File.ReadAllText(dataStorageLocation+"/Json/Settings.json");
        if(worldSettingsString!=""){
        settingsDataReadFromDisk=JsonMapper.ToObject<SettingsData>(worldSettingsString); 
        currentSettingsData=settingsDataReadFromDisk;
        }else{
            SettingsData tmp=new SettingsData();
            tmp.worldWidth=(int)worldWidthSlider.value;
            tmp.playerLife=(int)playerLifeSlider.value;
            tmp.worldBrickDensity=(int)worldBrickDensitySlider.value;
            tmp.enemyCount=(int)enemyCountSlider.value;
            tmp.enemyOnWorldCount=(int)enemyOnWorldCountSlider.value;
            settingsDataReadFromDisk=tmp;
        //    Debug.Log(JsonMapper.ToJson(settingsDataReadFromDisk));
            currentSettingsData=settingsDataReadFromDisk;
        }
        ApplyFileData(currentSettingsData);


        dataPathText.text="Game Settings Datapath:"+dataStorageLocation+"/Json";
    }
    public void ApplyFileData(SettingsData sd){
        if(playerLifeSlider!=null&&worldWidthSlider!=null&&worldBrickDensitySlider!=null&&sd!=null){
            playerLifeSlider.value=sd.playerLife;
        worldWidthSlider.value=sd.worldWidth;
        worldBrickDensitySlider.value=sd.worldBrickDensity;
        enemyCountSlider.value=sd.enemyCount;    
        isUsingKeboardShooterControlToggle.isOn=sd.isUsingKeboardShooterControl;
        enemyOnWorldCountSlider.value=sd.enemyOnWorldCount;
        }else{
            Debug.Log("null");
        }
    
    }



    void SaveSettings(){
      FileStream fs = new FileStream(dataStorageLocation+"/Json/Settings.json", FileMode.Truncate, FileAccess.ReadWrite, FileShare.None);
      fs.Close();
      string saveData=JsonMapper.ToJson(currentSettingsData);
    //  Debug.Log(Application.persistentDataPath);
  //    Debug.Log(Application.dataPath);
      File.AppendAllText(dataStorageLocation+"/Json/Settings.json",saveData);
    }


    public void saveButtonOnClick(){
        SaveSettings();

   
    this.gameObject.SetActive(false);
    
}
    void isUsingKeboardShooterControlToggleOnClick(Toggle toggle,bool isOn){
        WorldGen.isUsingKeboardShooterControl=isOn;
         if(currentSettingsData==null){
        Debug.Log("null");
    }else{
     currentSettingsData.isUsingKeboardShooterControl=isOn;}
    }
    void enemyOnWorldCountSliderChanged(float a){
        enemyOnWorldCountText.text=enemyOnWorldCountSlider.value.ToString();
    WorldGen.enemyHomeCount=(int)enemyOnWorldCountSlider.value;
    if(currentSettingsData==null){
        Debug.Log("null");
    }else{
     currentSettingsData.enemyOnWorldCount=(int)enemyOnWorldCountSlider.value;}
    }
    void enemyCountSliderChanged(float a){
    enemyCountText.text=enemyCountSlider.value.ToString();
    WorldGen.enemyCount=(int)enemyCountSlider.value;
    if(currentSettingsData==null){
        Debug.Log("null");
    }else{
     currentSettingsData.enemyCount=(int)enemyCountSlider.value;}
    }
    void playerLifeSliderChanged(float a){
    playerLifeText.text=playerLifeSlider.value.ToString();
    WorldGen.playerLifeCount=(int)playerLifeSlider.value;
    if(currentSettingsData==null){
        Debug.Log("null");
    }else{
     currentSettingsData.playerLife=(int)playerLifeSlider.value;   
    }
    
    }

    void worldBrickDensitySliderChanged(float b){
    worldBrickDensityText.text=worldBrickDensitySlider.value.ToString();
    WorldGen.worldBrickDensity=worldBrickDensitySlider.value;
        if(currentSettingsData==null){
        Debug.Log("null");
    }else{
    currentSettingsData.worldBrickDensity=worldBrickDensitySlider.value;}
    }
    void worldWidthSliderChanged(float c){
    worldWidthText.text=(2*(worldWidthSlider.value)).ToString()+" * "+(2*(worldWidthSlider.value)).ToString();
    WorldGen.worldwidth=(int)worldWidthSlider.value;
        if(currentSettingsData==null){
        Debug.Log("null");
    }else{
    currentSettingsData.worldWidth=(int)worldWidthSlider.value;}
    }
    
}
