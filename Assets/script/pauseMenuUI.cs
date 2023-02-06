using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class pauseMenuUI : MonoBehaviour
{
    public Button resumeButton;
   
    public Button mainMenuButton;
  
    void Start()
    {
        resumeButton=GameObject.Find("resumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(resumeOnClick);
       mainMenuButton=GameObject.Find("quitGameButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(mainMenuOnClick);
    }
    public void mainMenuOnClick(){
     SceneManager.LoadScene("MainMenu");
    }
    public void resumeOnClick(){
        Time.timeScale=1;
this.gameObject.SetActive(false);
    }
}
