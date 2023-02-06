using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class settingsUIBeh : MonoBehaviour
{
    public Text worldBrickDensityText;
    public Text playerLifeText;
    public Text worldWidthText;
    public static Slider playerLifeSlider;
    public static Slider worldBrickDensitySlider;
    public Button saveButton;
    public static GameObject settingsUIObject;
    public static Slider worldWidthSlider;
    //public StreamWriter worldConfigWriter;

    // Start is called before the first frame update
    void Start()

    {//worldConfigWriter=new StreamWriter("/gameData/gameWorldGenData.txt");
    if(File.Exists("gameWorldGenData.txt")==true){
        
    }else{
        File.Create("gameWorldGenData.txt");
       

    } 
    FileStream fs=new FileStream("gameWorldGenData.txt",FileMode.Truncate);
        fs.Dispose();
        startGameUI.isSettingChanged=true;
        worldWidthText=GameObject.Find("worldWidthText").GetComponent<Text>();
        settingsUIObject=GameObject.Find("settingsMenu");
        worldBrickDensityText=GameObject.Find("worldBrickDensityText").GetComponent<Text>();
         playerLifeText=GameObject.Find("playerLifeText").GetComponent<Text>();
         playerLifeSlider=GameObject.Find("Sliderpl").GetComponent<Slider>();
         worldBrickDensitySlider=GameObject.Find("Sliderwbd").GetComponent<Slider>();
         saveButton=GameObject.Find("saveButton").GetComponent<Button>();
         worldWidthSlider=GameObject.Find("Sliderww").GetComponent<Slider>();
         playerLifeSlider.onValueChanged.AddListener(playerLifeSliderChanged);
         worldBrickDensitySlider.onValueChanged.AddListener(worldBrickDensitySliderChanged);
         worldWidthSlider.onValueChanged.AddListener(worldWidthSliderChanged);
         saveButton.onClick.AddListener(saveButtonOnClick);
         worldWidthText.text=(2*(worldWidthSlider.value)).ToString()+" * "+(2*(worldWidthSlider.value)).ToString();
         playerLifeText.text=playerLifeSlider.value.ToString();
         worldBrickDensityText.text=worldBrickDensitySlider.value.ToString();


    }
    void saveButtonOnClick(){


   
    this.gameObject.SetActive(false);
    
}

    void playerLifeSliderChanged(float a){
playerLifeText.text=playerLifeSlider.value.ToString();
WorldGen.playerLifeCount=(int)playerLifeSlider.value;
    }

    void worldBrickDensitySliderChanged(float b){
worldBrickDensityText.text=worldBrickDensitySlider.value.ToString();
WorldGen.worldBrickDensity=worldBrickDensitySlider.value;
    }
    void worldWidthSliderChanged(float c){
 worldWidthText.text=(2*(worldWidthSlider.value)).ToString()+" * "+(2*(worldWidthSlider.value)).ToString();
WorldGen.worldwidth=(int)worldWidthSlider.value;
    }
    
}
