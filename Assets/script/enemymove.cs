using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{   
    public int effectEatenId=-1;
    public float effectTime=0f;
    public bool bulletIsEnchanted=false;
    public SpriteRenderer sr;
    public int enemydirection=0;
    public Vector2 enemyVec;
    public Sprite[] tankUp;
    public Sprite[] tankRight;
    public Sprite[] tankDown;
    public Sprite[] tankLeft;
    public float enemyspeed=49f;
    public Rigidbody2D enemyrigidbody;
   public Transform shooter;
    public GameObject bullet;
    public Transform bulletPos;
    public float bulletCD;
    public float playerDistance;
    public bool fire=false;
    public bool isWudi=false;
    public float WudiTime;
    public GameObject hugeExlplo;
 public Transform playerPos;
 public SpriteRenderer shooterSR;
 public Transform enemyHomePos;
 public  Vector2 lastpos;
 public Vector3 effectItemPos;
 public bool isOnClearRoadMode=false;
    //0up 1right 2down 3left
    void Start()
    {
    enemyHomePos=GameObject.Find("enemyhome2").GetComponent<Transform>();
    WudiTime=40f;
      enemyspeed=5f;
        tankUp=new Sprite[3];
        tankDown=new Sprite[3];
        tankRight=new Sprite[3];
        tankLeft=new Sprite[3];

tankUp[0]=Resources.Load<Sprite>("textures/enemy");
tankUp[1]=Resources.Load<Sprite>("textures/enemy1");
tankDown[0]=Resources.Load<Sprite>("textures/enemyx");
tankDown[1]=Resources.Load<Sprite>("textures/enemy1x");
tankRight[0]=Resources.Load<Sprite>("textures/enemyy");
tankRight[1]=Resources.Load<Sprite>("textures/enemy1y");
tankLeft[0]=Resources.Load<Sprite>("textures/enemyz");
tankLeft[1]=Resources.Load<Sprite>("textures/enemy1z");
        bulletCD=10f;
    bullet=Resources.Load<GameObject>("prefabs/bullet");
    hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
     sr=GetComponent<SpriteRenderer>();
     enemyrigidbody=GetComponent<Rigidbody2D>();
     shooter=this.transform.GetChild(0);
     if(GameObject.FindGameObjectWithTag("Player")!=null){
     playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
     }else{
        playerPos=null;
     }

    bulletPos=shooter.GetChild(0);
    shooterSR=shooter.GetComponent<SpriteRenderer>();
    InvokeRepeating("EnemyAI", 0.0f, 0.25f);
    InvokeRepeating("PlayerPosFind", 0.0f, 0.2f);
    }
void MoveToInterestPoint(Vector2 point){
    if(point!=null){
        if(Mathf.Abs(point.x-transform.position.x)>=Mathf.Abs(point.y-transform.position.y)){
            if(point.x-0.5f>transform.position.x){
            enemyVec=new Vector2(0.6f,0f);}else if(point.x+0.5f<transform.position.x){
            enemyVec=new Vector2(-0.6f,0f);
            }
        }else if(Mathf.Abs(point.x-transform.position.x)<Mathf.Abs(point.y-transform.position.y)){
           if(point.y+0.5f<transform.position.y){
            enemyVec=new Vector2(0f,-0.6f);
        }else if(point.y-0.5f>transform.position.y){
            enemyVec=new Vector2(0f,0.6f);
        }
        }else enemyVec=new Vector2(0f,0f);
   
    
    }
}
void DiretionChange(Transform gameobject){
       // Vector3 ms = Input.mousePosition;
       // ms = Camera.main.ScreenToWorldPoint(ms);
       Vector3 ms;
if(playerPos!=null){
    ms=playerPos.position;
}else{
    ms=new Vector3(0f,0f);
}  
        Vector3 gunPos = gameobject.transform.position;
        float fireangle;
        
        Vector2 targetDir = ms - gunPos;
        fireangle = Vector2.Angle(targetDir, Vector3.up);
        if (ms.x > gunPos.x)
        {
            fireangle = -fireangle;
        }
        gameobject.transform.eulerAngles = new Vector3(0, 0, fireangle);
}
   
bool checkIsMoving(){
   
    if ((new Vector2(transform.position.x,transform.position.y)-lastpos).magnitude>=0.3f){
        lastpos=transform.position;
    return true;
    }else{
         lastpos=transform.position;
    return false;
    }
        
}

   public void EnemyAI(){
  // Vector3 tmpEffectItemPos=new Vector3(100f,100f,0f);
 //   for(int i=0;i<WorldGen.effectItemList.Count;i++){
        
     //   if((tmpEffectItemPos-transform.position).magnitude>(WorldGen.effectItemList[i].gameObject.GetComponent<Transform>().position-transform.position).magnitude){
     //       tmpEffectItemPos=WorldGen.effectItemList[i].gameObject.GetComponent<Transform>().position;
    //    }   

   
   // }
   if(WorldGen.effectItemList.Count>0){
    int tmp=(int)Random.Range(0f,WorldGen.effectItemList.Count-0.5f);
    if(WorldGen.effectItemList[tmp]!=null){
      effectItemPos=WorldGen.effectItemList[tmp].gameObject.GetComponent<Transform>().position;  
    }
   }
   
    
   //effectItemPos=tmpEffectItemPos;
    if(playerDistance<=12f){
        fire=true;
    }

    if((enemyVec.x!=0f||enemyVec.y!=0f)&&checkIsMoving()==false){

      //  Ray2D ray=new Ray2D(gameObject.transform.position,bulletPos.position);
     //   RaycastHit2D info=Physics2D.Raycast(ray.origin, ray.direction,5.7f);
      //  if(info.collider.gameObject.tag!="enemy"){
         isOnClearRoadMode=true;
fire=true;   
      //  }

    }

    if(playerPos!=null){
        /*if(Mathf.Abs(playerPos.position.x-transform.position.x)>=Mathf.Abs(playerPos.position.y-transform.position.y)){
            if(playerPos.position.x-0.5f>transform.position.x){
            enemyVec=new Vector2(0.6f,0f);}else if(playerPos.position.x+0.5f<transform.position.x){
            enemyVec=new Vector2(-0.6f,0f);
            }
        }else if(Mathf.Abs(playerPos.position.x-transform.position.x)<Mathf.Abs(playerPos.position.y-transform.position.y)){
           if(playerPos.position.y+0.5f<transform.position.y){
            enemyVec=new Vector2(0f,-0.6f);
        }else if(playerPos.position.y-0.5f>transform.position.y){
            enemyVec=new Vector2(0f,0.6f);
        }
        }else enemyVec=new Vector2(0f,0f);*/
   MoveToInterestPoint(playerPos.position);
    
    }else {
         MoveToInterestPoint(effectItemPos);
    }/*if(Mathf.Abs(enemyHomePos.position.x-transform.position.x)>=Mathf.Abs(enemyHomePos.position.y-transform.position.y)){
            if(enemyHomePos.position.x-0.5f>transform.position.x){
            enemyVec=new Vector2(0.6f,0f);}else if(enemyHomePos.position.x+0.5f<transform.position.x){
            enemyVec=new Vector2(-0.6f,0f);
            }
        }else if(Mathf.Abs(enemyHomePos.position.x-transform.position.x)<Mathf.Abs(enemyHomePos.position.y-transform.position.y)){
           if(enemyHomePos.position.y+0.5f<transform.position.y){
            enemyVec=new Vector2(0f,-0.6f);
        }else if(enemyHomePos.position.y-0.5f>transform.position.y){
            enemyVec=new Vector2(0f,0.6f);
        }
        }else enemyVec=new Vector2(0f,0f);*/
    }



public void PlayerPosFind(){
      if(GameObject.FindGameObjectWithTag("Player")!=null){
     playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
     }else{
        playerPos=null;
     }
}
    void Update()
    {
      //  Debug.Log(playerPos);
      if(effectTime>0f){
            effectTime-=Time.deltaTime;
            switch(effectEatenId){
                case 0:WudiTime=effectTime;break;
                case 1:bulletIsEnchanted=true;break;
            }
        }else{

            bulletIsEnchanted=false;
            effectEatenId=-1;
        }
if(enemyspeed<12f){
    enemyspeed+=Time.deltaTime*55f;
}

if(WudiTime>0f){
    sr.color=Color.yellow;
    shooterSR.color=Color.yellow;
    WudiTime-=Time.deltaTime*60f;
}
if(WudiTime>0f){
    isWudi=true;
    
}else{
    sr.color=Color.white;
    shooterSR.color=Color.white;
    isWudi=false;
}
if(playerPos==null){
    if(WorldGen.lose!=true){
        if(GameObject.FindGameObjectWithTag("Player")!=null)
        {playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();}
          
playerDistance = 100f;
    }
  
}
if(playerPos==null){
      playerDistance=100f;
}else{playerDistance = (transform.position - playerPos.position).magnitude;}
  

    
        if(bulletCD>=0f){
        bulletCD=bulletCD-10f*Time.deltaTime;
    }
       

      if(fire==true&&bulletCD<=0f){
         
        if(isOnClearRoadMode==false){
            DiretionChange(shooter);
             shooter.eulerAngles+=new Vector3(0,0,(int)Random.Range(-10f,10f));
        }else{
           if(enemydirection==0){
shooter.eulerAngles=new Vector3(0f,0f,0f+(int)Random.Range(-40f,40f));
           }else if(enemydirection==1){
shooter.eulerAngles=new Vector3(0f,0f,270f+(int)Random.Range(-40f,40f));
           }else if(enemydirection==2){
shooter.eulerAngles=new Vector3(0f,0f,180f+(int)Random.Range(-40f,40f));
           }else if(enemydirection==3){
shooter.eulerAngles=new Vector3(0f,0f,90f+(int)Random.Range(-40f,40f));
           }
            isOnClearRoadMode=false;
        }
      GameObject a=Instantiate(bullet,bulletPos.position,shooter.rotation);
       a.GetComponent<bulletBehav>().fireSource=this.gameObject;
       a.GetComponent<bulletBehav>().originMovitation=enemyrigidbody.velocity;
      //Instantiate(bullet,bulletPos.position,shooter.rotation);
          enemyspeed-=6f;
        bulletCD+=1.5f;
        fire=false;
      }
     
        if(enemydirection==0){
        sr.sprite=tankUp[Mathf.Abs((int)(transform.position.y*10)%2)];
        }else if(enemydirection==1){
        sr.sprite=tankRight[Mathf.Abs((int)(transform.position.x*10)%2)];
        }else if(enemydirection==2){
            sr.sprite=tankDown[Mathf.Abs((int)(transform.position.y*10)%2)];
        }else if(enemydirection==3){
            sr.sprite=tankLeft[Mathf.Abs((int)(transform.position.x*10)%2)];
        }
   //  Vector2 position = transform.position; 
    // Debug.Log(enemyVec);
if(enemyVec.x==0f&&enemyVec.y==0f){
    return;
}
        if(Mathf.Abs(enemyVec.x)>=Mathf.Abs(enemyVec.y)&&enemyVec.x>=0){
            enemydirection=1;
            shooter.transform.position=new Vector2(transform.position.x-0.1f,transform.position.y+0f);
            enemyrigidbody.velocity=new Vector2(enemyVec.x*enemyspeed,0f);
            
        //     position.x = position.x + enemyspeed * enemyVec.x * Time.deltaTime; 
         //    enemyrigidbody.MovePosition(position);
            return;
        }else if(Mathf.Abs(enemyVec.x)>=Mathf.Abs(enemyVec.y)&&enemyVec.x<0){
            enemydirection=3;
            shooter.transform.position=new Vector2(transform.position.x+0.1f,transform.position.y+0f);
             enemyrigidbody.velocity=new Vector2(enemyVec.x*enemyspeed,0f);  

          //  position.x += enemyspeed * enemyVec.x * Time.deltaTime; 
          //  enemyrigidbody.MovePosition(position);
            return;
        }
        if(Mathf.Abs(enemyVec.x)<Mathf.Abs(enemyVec.y)&&enemyVec.y>=0){
            enemydirection=0;
            shooter.transform.position=new Vector2(transform.position.x-0f,transform.position.y-0.1f);
             enemyrigidbody.velocity=new Vector2(0f,enemyVec.y*enemyspeed);
             
          // position.y += enemyspeed * enemyVec.y * Time.deltaTime;  
        //   enemyrigidbody.MovePosition(position);
            return;
        }else if(Mathf.Abs(enemyVec.x)<Mathf.Abs(enemyVec.y)&&enemyVec.y<0){
            enemydirection=2;
            shooter.transform.position=new Vector2(transform.position.x-0f,transform.position.y+0.1f);
           enemyrigidbody.velocity=new Vector2(0f,enemyVec.y*enemyspeed);
             
        //    position.y += enemyspeed * enemyVec.y * Time.deltaTime;  
         // enemyrigidbody.MovePosition(position);
            return;
        }
    }
    private void OnDestroy() {

        WorldGen.enemyOnWorldCount--;
    }
}
