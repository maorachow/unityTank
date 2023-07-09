using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gameOverUIBeh : MonoBehaviour
{
    public Button quitGameButton;
    public Button mainMenuButton;
    public Sprite backGround;
    public Text winUseTime;
    public GameObject winUseTimeobj;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(WorldGen.lose==false){
        winUseTime=GameObject.Find("winUsedTime").GetComponent<Text>();
        winUseTimeobj=GameObject.Find("winUsedTime");
        }

        winUseTimeobj.SetActive(false);
        if(WorldGen.lose==true){
        backGround=Resources.Load<Sprite>("textures/lose");
        }else{
        backGround=Resources.Load<Sprite>("textures/win");
winUseTimeobj.SetActive(true);
winUseTime.text="Time used: "+Time.timeSinceLevelLoad.ToString()+" second";
        }
        Time.timeScale=0;
       GameObject.Find("gameResultImage").GetComponent<Image>().sprite=backGround;
        quitGameButton=GameObject.Find("quitButton").GetComponent<Button>();
        mainMenuButton=GameObject.Find("mainMenuButton").GetComponent<Button>();
        quitGameButton.onClick.AddListener(QuitGame);
        mainMenuButton.onClick.AddListener(MainMenu);
    }


    void QuitGame(){
Application.Quit();
    }
    void MainMenu(){
SceneManager.LoadScene("MainMenu");
    }
}
