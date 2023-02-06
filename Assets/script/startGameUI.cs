using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class startGameUI : MonoBehaviour
{public static bool isSettingChanged;
    public Button startGameButton;
    public Button settingsButton;
    public GameObject settingsUI;
    // Start is called before the first frame update
    void Start()
    {
        settingsUI=GameObject.Find("settingsMenu");
        settingsButton=GameObject.Find("settingsButton").GetComponent<Button>();
        startGameButton=GameObject.Find("startGameButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        settingsUI.SetActive(false);
    }
    void OpenSettings(){
settingsUI.SetActive(true);
    }
   void StartGame(){
    if(isSettingChanged==false){
        WorldGen.worldwidth=10;
        WorldGen.playerLifeCount=11;
        WorldGen.worldBrickDensity=0.45f;
    }
    SceneManager.LoadScene("DefaultMatchScene");
   }

}
