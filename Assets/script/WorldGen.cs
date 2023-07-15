using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Tilemaps;
public class WorldGen : MonoBehaviour
{   
    public static List<effectItemBeh> effectItemList=new List<effectItemBeh>();
    public float addEffectItemCD=4f;
   public GameObject brick;
   public GameObject stone;
   public GameObject leaves;
   public static bool spawnItem=true;
   public static int playerLifeCount=11;
   public static int enemyCount;
   public static int enemyOnWorldCount;
   public static GameObject tankPlayer;
   public GameObject playerPrefab;
   public GameObject enemyPrefab;
   public static int enemyHomeCount=4;
   public static int playerHomeCount=4;
   public static float playerRespawnCD;
   public static bool lose;
   public List<Vector2> playerHome=new List<Vector2>();
   public GameObject effectItem;
   public List<Vector2> enemyHome=new List<Vector2>();
   public static float worldBrickDensity=0.45f;
   public GameObject gameOverUI;
   public static int worldwidth=10;
   public static float playerRespawnTime=3f;
   public static bool isUsingKeboardShooterControl=false;
   public static int maxEffectItemCount=40;
public static int[,] map=new int[500,500];
public string worldGenData;
public string worldBrickDensityString;
public string playerLifeCountString;
public string worldwidthString;
public static Tilemap tm;
public static Tile brickTile;
public static Tile stoneTile;
public static Tile leavesTile;
public static Tile leavesTileCracked;
public static Tile playerHomeTile;
public static Tile enemyHomeTile;
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
tm=GameObject.Find("Tilemap").GetComponent<Tilemap>();
brickTile=Resources.Load<Tile>("textures/bricktile");
leavesTile=Resources.Load<Tile>("textures/leavestile");
stoneTile=Resources.Load<Tile>("textures/stonetile");
leavesTileCracked=Resources.Load<Tile>("textures/leavescrackedtile");
playerHomeTile=Resources.Load<Tile>("textures/playerhometile");
enemyHomeTile=Resources.Load<Tile>("textures/enemyhometile");
effectItem=Resources.Load<GameObject>("prefabs/effectitem");
if(worldwidth==0){
    worldwidth=10;
    }

        if(playerLifeCount==0){
        playerLifeCount+=5;
    }
        //enemyHome=new Transform[enemyHomeCount];
        gameOverUI=GameObject.Find("Canvas").GetComponent<RectTransform>().GetChild(7).gameObject;
        
      //  playerHome=GameObject.Find("mehome");
      //  enemyHome[0]=GameObject.Find("enemyhome");
       // enemyHome[1]=GameObject.Find("enemyhome2");
       // playerHome.GetComponent<Transform>().position=new Vector2(2f,2f);
        
        //   enemyHome[0].GetComponent<Transform>().position=new Vector2(2f*worldwidth-3f,2f*worldwidth-3f);  
       // enemyHome[1].GetComponent<Transform>().position=new Vector2(2f,2f*worldwidth-3f); 
       
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
        //0=air,1=brick,2=stone,3=leaves,4=enemyHome,5=playerHome
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
        int tmp=enemyHomeCount;
        while(tmp>0){
         Vector2 tmpEnemyHomePos=new Vector2(Random.Range(1f,2*worldwidth-2),Random.Range(1f,2*worldwidth-2));
         if(map[(int)tmpEnemyHomePos.x,(int)tmpEnemyHomePos.y]!=5){
            map[(int)tmpEnemyHomePos.x,(int)tmpEnemyHomePos.y]=4;
            enemyHome.Add(new Vector2((int)tmpEnemyHomePos.x,(int)tmpEnemyHomePos.y));
            tmp--;
         }
        }
        int tmp2=playerHomeCount;
                while(tmp2>0){
         Vector2 tmpPlayerHomePos=new Vector2(Random.Range(1f,2*worldwidth-2),Random.Range(1f,2*worldwidth-2));
         if(map[(int)tmpPlayerHomePos.x,(int)tmpPlayerHomePos.y]!=4){
            map[(int)tmpPlayerHomePos.x,(int)tmpPlayerHomePos.y]=5;
            playerHome.Add(new Vector2((int)tmpPlayerHomePos.x,(int)tmpPlayerHomePos.y));
            tmp2--;
         }
        }
for(int i = 0; i <2 ; i++) {
    //int enemyHomeMapPosx=(int)(enemyHome[i].GetComponent<Transform>().position.x);
    //    int enemyHomeMapPosy=(int)(enemyHome[i].GetComponent<Transform>().position.y);
       
     //   map[enemyHomeMapPosx,enemyHomeMapPosy]=0;
       
}
    //     float playerHomeMapPosx=(playerHome.GetComponent<Transform>().position.x);
   //     float playerHomeMapPosy=(playerHome.GetComponent<Transform>().position.y);
  //       map[(int)playerHomeMapPosx,(int)playerHomeMapPosy]=0;
         for (int i=0;i<=2*worldwidth-1;i++){
            for(int j=0;j<=2*worldwidth-1;j++){
                if(map[i,j]==1){
               //     Instantiate(brick,new Vector3(i,j,0),transform.rotation);
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),brickTile);
                }else if (map[i,j]==2){
               //     Instantiate(stone,new Vector3(i,j,0),transform.rotation);
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),stoneTile);
                }else if(map[i,j]==3){
             //       Instantiate(leaves,new Vector3(i,j,0),transform.rotation);
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),leavesTile);
                }else if(map[i,j]==0){
                    continue;
                }else if(map[i,j]==4){
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),enemyHomeTile);
                }else if(map[i,j]==5){
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),playerHomeTile);
                }
            }
        }
        Time.timeScale=1;
        //Instantiate(enemyPrefab,new Vector2(transform.position.x-4.5f,transform.position.y-4.5f),transform.rotation);
       // Instantiate(enemyPrefab,new Vector2(transform.position.x-4.5f,transform.position.y-4.5f),transform.rotation);
    }
    void AddEffectItem(){
          while(true){
            Vector2 randomAddPos=new Vector2(Random.Range(1f,2*worldwidth-1f),Random.Range(1f,2*worldwidth-1f));
        Ray2D ray=new Ray2D(randomAddPos,new Vector2(0f,1f));
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction,0.01f);
        if(info.collider==null){
            GameObject a=Instantiate(effectItem,randomAddPos,Quaternion.Euler(0f,0f,0f));
            a.GetComponent<effectItemBeh>().effectId=(int)Random.Range(0f,1.99f);
            Debug.Log("suceed");
            return;
        }else{
            Debug.Log("failed");
            continue;
        }
          }
        
    }
  void FixedUpdate() {

    addEffectItemCD-=Time.deltaTime;

    if(addEffectItemCD<0f&&effectItemList.Count<=maxEffectItemCount){
        AddEffectItem();
        addEffectItemCD=Random.Range(1f,3f);
    }
   // Debug.Log(playerRespawnCD);
    if(playerRespawnCD>0f&&tankPlayer==null){
        playerRespawnCD-=Time.deltaTime;
    }
if(enemyCount==0&&enemyOnWorldCount==0){
    Debug.Log("你赢了");
    gameOverUI.SetActive(true);
}
 //for(int i=0;i<2;i++){
if(enemyCount>0&&enemyOnWorldCount<3){
   
        Instantiate(enemyPrefab,enemyHome[(int)Random.Range(0f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f),transform.rotation); 
         
    
   enemyOnWorldCount++;
    enemyCount--;
   
}//}
    tankPlayer=GameObject.FindGameObjectWithTag("Player");
        if(tankPlayer==null&&lose==false){
        if(playerLifeCount>=0&&playerRespawnCD<=0f){
            playerRespawnCD=playerRespawnTime;
    Instantiate(playerPrefab,playerHome[(int)Random.Range(0f,playerHome.Count-1f)]+new Vector2(0.5f,0.5f),transform.rotation);
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

