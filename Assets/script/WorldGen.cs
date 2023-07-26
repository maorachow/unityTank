using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Tilemaps;
using UnityEngine.Pool;
public class WorldGen : MonoBehaviour
{   
    public static ObjectPool<GameObject> enemyPool;
    public static ObjectPool<GameObject> playerPool;
    public static ObjectPool<GameObject> bulletPool;
    //public static ObjectPool<GameObject> 
    public static List<effectItemBeh> effectItemList=new List<effectItemBeh>();
    public float addEffectItemCD=4f;
   public GameObject brick;
   public GameObject stone;
   public GameObject leaves;
    public static GameObject bullet;
   public static bool spawnItem=true;
   public static int playerLifeCount=11;
   public static int enemyCount;
   public static int enemyOnWorldCount;
   public static int enemyHomeCount=5;
   public static int playerHomeCount=5;
   public static GameObject tankPlayer;
   public GameObject playerPrefab;
   public GameObject enemyPrefab;
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
    public bool create;
    public GameObject CreateEnemy(){
        GameObject go=Instantiate(enemyPrefab,enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f),transform.rotation);
        return go;
    }
    void GetEnemy(GameObject gameObject){
        gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleaseEnemy(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyEnemy(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreatePlayer(){
        GameObject go=Instantiate(playerPrefab,playerHome[(int)Random.Range(-0.4f,playerHome.Count-0.5f)]+new Vector2(0.5f,0.5f),transform.rotation);
        return go;
    }
    void GetPlayer(GameObject gameObject){
        gameObject.transform.position=playerHome[(int)Random.Range(-0.4f,playerHome.Count-0.5f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleasePlayer(GameObject go){
        go.SetActive(false);
    }
    void DestroyPlayer(GameObject go){
        Destroy(go);
    }

    public static GameObject CreateBullet(){
        GameObject go=Instantiate(bullet,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    public static void GetBullet(GameObject go){
        go.SetActive(true);
    }
    public static void ReleaseBullet(GameObject go){
    go.SetActive(false);
    }
    public static void DestroyBullet(GameObject go){
    Destroy(go);
    }
    void Start()

    {
        bulletPool=new ObjectPool<GameObject>(CreateBullet,GetBullet,ReleaseBullet,DestroyBullet,true,10,50);
        playerPool=new ObjectPool<GameObject>(CreatePlayer,GetPlayer,ReleasePlayer,DestroyPlayer,true,10,50);
        enemyPool=new ObjectPool<GameObject>(CreateEnemy,GetEnemy,ReleaseEnemy,DestroyEnemy,true,10,50);
tm=GameObject.Find("Tilemap").GetComponent<Tilemap>();
brickTile=Resources.Load<Tile>("textures/bricktile");
leavesTile=Resources.Load<Tile>("textures/leavestile");
stoneTile=Resources.Load<Tile>("textures/stonetile");
leavesTileCracked=Resources.Load<Tile>("textures/leavescrackedtile");
playerHomeTile=Resources.Load<Tile>("textures/playerhometile");
enemyHomeTile=Resources.Load<Tile>("textures/enemyhometile");
bullet=Resources.Load<GameObject>("prefabs/bullet");
effectItem=Resources.Load<GameObject>("prefabs/effectitem");
if(worldwidth==0){
    worldwidth=10;
    }
if(enemyCount==0){
    enemyCount=20;
    }
        if(playerLifeCount==0){
        playerLifeCount+=5;
    }
        gameOverUI=GameObject.Find("Canvas").GetComponent<RectTransform>().GetChild(7).gameObject;
        

      
   
       
        playerRespawnCD=5.1f;
        lose=false;
      
       //  worldwidth=10;
        gameOverUI.SetActive(false);
         brick=Resources.Load<GameObject>("prefabs/brick");
         stone=Resources.Load<GameObject>("prefabs/stone");
         leaves=Resources.Load<GameObject>("prefabs/leaves");
         playerPrefab=Resources.Load<GameObject>("prefabs/player");
         enemyPrefab=Resources.Load<GameObject>("prefabs/enemy");
        //0=air,1=brick,2=stone,3=leaves,4enemyhome,5playerhome
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
    Vector2 tmpEnemyHomePos=new Vector2((int)Random.Range(1f,2*worldwidth-2),(int)Random.Range(1f,2*worldwidth-2));
    if(map[(int)tmpEnemyHomePos.x,(int)tmpEnemyHomePos.y]!=5){
        map[(int)tmpEnemyHomePos.x,(int)tmpEnemyHomePos.y]=4;
        enemyHome.Add(tmpEnemyHomePos);
        tmp--;
    }
}
int tmp2=enemyHomeCount;
while(tmp2>0){
    Vector2 tmpPlayerHomePos=new Vector2((int)Random.Range(1f,2*worldwidth-2),(int)Random.Range(1f,2*worldwidth-2));
    if(map[(int)tmpPlayerHomePos.x,(int)tmpPlayerHomePos.y]!=4){
        map[(int)tmpPlayerHomePos.x,(int)tmpPlayerHomePos.y]=5;
        playerHome.Add(tmpPlayerHomePos);
        tmp2--;
    }
}
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
             //       Instantiate(leaves,new Vector3(i,j,0),transform.rotation);
                    tm.SetTile(new Vector3Int((int)i,(int)j,0),enemyHomeTile);
                }else if(map[i,j]==5){
             //       Instantiate(leaves,new Vector3(i,j,0),transform.rotation);
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
        Ray2D ray=new Ray2D(randomAddPos,new Vector2(0f,0.001f));
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
    enemyOnWorldCount=GameObject.FindGameObjectsWithTag("enemy").Length;
    Debug.Log("enemyPool active count:"+enemyPool.CountActive);
Debug.Log("enemyPool inactive count:"+enemyPool.CountInactive);
Debug.Log("enemyPool countAll:"+enemyPool.CountAll);
if(create){
    enemyPool.Get();
    create=false;
}
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

if(enemyCount>0&&enemyOnWorldCount<enemyHomeCount){
   
       // Instantiate(enemyPrefab,enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f),transform.rotation); 
         enemyPool.Get();
  //  MyObjectPool.poolInstance.Get();
 
    enemyCount--;
   
}
    tankPlayer=GameObject.FindGameObjectWithTag("Player");
        if(tankPlayer==null&&lose==false){
        if(playerLifeCount>=0&&playerRespawnCD<=0f){
            playerRespawnCD=playerRespawnTime;
            playerPool.Get();
    //Instantiate(playerPrefab,playerHome[(int)Random.Range(-0.4f,playerHome.Count-0.5f)]+new Vector2(0.5f,0.5f),transform.rotation);
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

