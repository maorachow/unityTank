using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class WorldGen : MonoBehaviour
{
   public GameObject brick;
   public GameObject stone;
   public GameObject leaves;
   public static int playerLifeCount=11;
   public static int enemyCount;
   public static int enemyOnWorldCount;
   public static GameObject tankPlayer;
   public GameObject playerPrefab;
   public GameObject enemyPrefab;
   public static float playerRespawnCD;
   public static bool lose;
   public GameObject playerHome;
   public GameObject[] enemyHome;
   public static float worldBrickDensity=0.45f;
   public GameObject gameOverUI;
   public static int worldwidth=10;
public static int[,] map=new int[500,500];
public string worldGenData;
public string worldBrickDensityString;
public string playerLifeCountString;
public string worldwidthString;

    void Start()

    {
//int dataLineCount=1;
enemyCount=20;
 /*using (StreamReader sr = new StreamReader("gameWorldGenData.txt"))
            {
               
        string line;
                while ((line = sr.ReadLine()) != null)

                {
                    if(dataLineCount==1){
playerLifeCountString=line;
                    }
                    else if(dataLineCount==2){
                        worldBrickDensityString=line;
                    }
                    else if(dataLineCount==3){
                        worldwidthString=line;
                    }
                    dataLineCount++;
                }
            }*/

//Debug.Log(worldBrickDensityString);
//Debug.Log(playerLifeCountString);
///Debug.Log(worldwidthString);
//worldwidth=int.Parse(worldwidthString);
//playerLifeCount=int.Parse(playerLifeCountString);
///worldBrickDensity=float.Parse(worldBrickDensityString);
if(worldwidth==0){
    worldwidth=10;
    }

        if(playerLifeCount==0){
        playerLifeCount+=5;
    }
        enemyHome=new GameObject[3];
        gameOverUI=GameObject.Find("Canvas").GetComponent<RectTransform>().GetChild(7).gameObject;
        
        playerHome=GameObject.Find("mehome");
        enemyHome[0]=GameObject.Find("enemyhome");
        enemyHome[1]=GameObject.Find("enemyhome2");
        playerHome.GetComponent<Transform>().position=new Vector2(2f,2f);
        
           enemyHome[0].GetComponent<Transform>().position=new Vector2(2f*worldwidth-3f,2f*worldwidth-3f);  
        enemyHome[1].GetComponent<Transform>().position=new Vector2(2f,2f*worldwidth-3f); 
       
        playerRespawnCD=5.1f;
        lose=false;
        enemyCount=20;
       //  worldwidth=10;
gameOverUI.SetActive(false);
         brick=Resources.Load<GameObject>("prefabs/brick");
         stone=Resources.Load<GameObject>("prefabs/stone");
         leaves=Resources.Load<GameObject>("prefabs/leaves");
         playerPrefab=Resources.Load<GameObject>("prefabs/player");
         enemyPrefab=Resources.Load<GameObject>("prefabs/enemy");
        //0=air,1=brick,2=stone,3=leaves
        for (int i=0;i<=2*worldwidth-1;i++){
            for(int j=0;j<=2*worldwidth-1;j++){
                map[i,j]=2;
            }
        }
         for (int i=1;i<=2*worldwidth-2;i++){
            for(int j=1;j<=2*worldwidth-2;j++){
                map[i,j]=0;
            }
        }
         for (int i=1;i<=2*worldwidth-2;i++){
            for(int j=1;j<=2*worldwidth-2;j++){
                if(Random.Range(0f,1f)<=worldBrickDensity){
                   map[i,j]=1; 
                }else{
                    map[i,j]=0;
                }
                
            }
        }
        for (int i=1;i<=2*worldwidth-2;i++){
            for(int j=1;j<=2*worldwidth-2;j++){
                if(Random.Range(0f,1f)<=worldBrickDensity/2f&&map[i,j]==0){
                   map[i,j]=3; 
                }
            }
        }
for(int i = 0; i <2 ; i++) {
    int enemyHomeMapPosx=(int)(enemyHome[i].GetComponent<Transform>().position.x);
        int enemyHomeMapPosy=(int)(enemyHome[i].GetComponent<Transform>().position.y);
       
        map[enemyHomeMapPosx,enemyHomeMapPosy]=0;
       
}
         int playerHomeMapPosx=(int)(playerHome.GetComponent<Transform>().position.x);
        int playerHomeMapPosy=(int)(playerHome.GetComponent<Transform>().position.y);
         map[playerHomeMapPosx,playerHomeMapPosy]=0;
         for (int i=0;i<=2*worldwidth-1;i++){
            for(int j=0;j<=2*worldwidth-1;j++){
                if(map[i,j]==1){
                    Instantiate(brick,new Vector3(i,j,0),transform.rotation);
                }else if (map[i,j]==2){
                    Instantiate(stone,new Vector3(i,j,0),transform.rotation);
                }else if(map[i,j]==3){
                    Instantiate(leaves,new Vector3(i,j,0),transform.rotation);
                }else if(map[i,j]==0){
                    continue;
                }
            }
        }
        Time.timeScale=1;
        //Instantiate(enemyPrefab,new Vector2(transform.position.x-4.5f,transform.position.y-4.5f),transform.rotation);
       // Instantiate(enemyPrefab,new Vector2(transform.position.x-4.5f,transform.position.y-4.5f),transform.rotation);
    }
  void FixedUpdate() {
   // Debug.Log(playerRespawnCD);
    if(playerRespawnCD>0f&&tankPlayer==null){
        playerRespawnCD-=Time.deltaTime;
    }
if(enemyCount==0&&enemyOnWorldCount==0){
    Debug.Log("你赢了");
    gameOverUI.SetActive(true);
}
 for(int i=0;i<2;i++){
if(enemyCount>0&&enemyOnWorldCount<2){
   
        Instantiate(enemyPrefab,new Vector2(enemyHome[i].GetComponent<Transform>().position.x,enemyHome[i].GetComponent<Transform>().position.y),transform.rotation); 
         
    
   enemyOnWorldCount++;
    enemyCount--;
   
}}
    tankPlayer=GameObject.FindGameObjectWithTag("Player");
        if(tankPlayer==null&&lose==false){
        if(playerLifeCount>=0&&playerRespawnCD<=0f){
            playerRespawnCD=3.3f;
    Instantiate(playerPrefab,new Vector2(playerHome.GetComponent<Transform>().position.x,playerHome.GetComponent<Transform>().position.x),transform.rotation);
      playerLifeCount--;
      
    //return;
        }else if(playerLifeCount==0){
    Debug.Log("你输了");
    lose=true;
    gameOverUI.SetActive(true);
        }
    }else{
        return;
    }

}
   
   
    }

