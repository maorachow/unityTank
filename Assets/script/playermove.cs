using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 
public class playermove : MonoBehaviour 
{
    public SpriteRenderer sr;
    public int playerdirection=0;
    public Vector2 playerVec;
    public Sprite[] tankUp;
    public Sprite[] tankRight;
    public Sprite[] tankDown;
    public Sprite[] tankLeft;
    public float playerspeed=49f;
    public Rigidbody2D playerrigidbody;
    public GameObject shooter;
    public GameObject bullet;
    public GameObject bulletPos;
    public float bulletCD;
    public static Transform playerPos;
    public static bool isWudi=false;
    public float WudiTime;
     public GameObject hugeExlplo;
    public SpriteRenderer shooterSR;   
    public int shooterSensi; 
    //0up 1right 2down 3left
    // Start is called before the first frame update
    void Start()
    {
        shooterSensi=160;
        playerspeed=10f;
        WudiTime=40f;
        Application.targetFrameRate =60;
        tankUp=new Sprite[3];
        tankDown=new Sprite[3];
        tankRight=new Sprite[3];
        tankLeft=new Sprite[3];
tankUp[0]=Resources.Load<Sprite>("textures/tank");
tankUp[1]=Resources.Load<Sprite>("textures/tank1");
tankDown[0]=Resources.Load<Sprite>("textures/tankx");
tankDown[1]=Resources.Load<Sprite>("textures/tank1x");
tankRight[0]=Resources.Load<Sprite>("textures/tanky");
tankRight[1]=Resources.Load<Sprite>("textures/tank1y");
tankLeft[0]=Resources.Load<Sprite>("textures/tankz");
tankLeft[1]=Resources.Load<Sprite>("textures/tank1z");
        bulletCD=10f;
  //  bullet=Resources.Load<GameObject>("prefabs/bullet");
     sr=GetComponent<SpriteRenderer>();
     
     playerrigidbody=GetComponent<Rigidbody2D>();
     shooter=GameObject.Find("playerbulletshooter");
     bulletPos=GameObject.Find("bulletPos");
     bullet=Resources.Load<GameObject>("prefabs/bullet");
       hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
     shooterSR=shooter.GetComponent<SpriteRenderer>();
    }
void DiretionChange(GameObject gameobject){
     
    
       /* Vector3 ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);
       
        Vector3 gunPos = gameobject.transform.position;
        float fireangle;
        
        Vector2 targetDir = ms - gunPos;
        fireangle = Vector2.Angle(targetDir, Vector3.up);
        if (ms.x > gunPos.x)
        {
            fireangle = -fireangle;
        }
        gameobject.transform.eulerAngles = new Vector3(0, 0, fireangle);*/
        gameobject.transform.eulerAngles-=new Vector3(0,0,shooterSensi*Input.GetAxis("Rotate")*Time.deltaTime);
}
   
    
    void Update()
    {
playerPos=transform;
if(playerspeed<10f){
    playerspeed+=Time.deltaTime*55f;
}
if(WudiTime>0f){
    WudiTime-=Time.deltaTime*60f;

}
if(WudiTime>0f){
    isWudi=true;
    sr.color=Color.yellow;
    shooterSR.color=Color.yellow;
}else{
    isWudi=false;
      sr.color=Color.white;
    shooterSR.color=Color.white;
}
//Debug.Log(playerspeed);

        if(bulletCD>=0f){
        bulletCD=bulletCD-10f*Time.deltaTime;
    }
        
        playerVec=new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        DiretionChange(shooter);
      if(Input.GetButton("Jump")&&bulletCD<=0f){
        GameObject a=Instantiate(bullet,bulletPos.transform.position,shooter.transform.rotation);
       a.GetComponent<bulletBehav>().fireSource=this.gameObject;
        //Instantiate(bullet,bulletPos.transform.position,shooter.transform.rotation);
        playerspeed-=6f;
        bulletCD+=1.5f;
      }
        //Debug.Log(playerdirection);
      //  Debug.Log(bulletCD);
        if(playerdirection==0){
        sr.sprite=tankUp[Mathf.Abs((int)(transform.position.y*10)%2)];
        }else if(playerdirection==1){
        sr.sprite=tankRight[Mathf.Abs((int)(transform.position.x*10)%2)];
        }else if(playerdirection==2){
            sr.sprite=tankDown[Mathf.Abs((int)(transform.position.y*10)%2)];
        }else if(playerdirection==3){
            sr.sprite=tankLeft[Mathf.Abs((int)(transform.position.x*10)%2)];
        }
     //Vector2 position = transform.position; 
if(playerVec.x==0f&&playerVec.y==0f){
    return;
}
        if(Mathf.Abs(Input.GetAxis("Horizontal"))>=Mathf.Abs(Input.GetAxis("Vertical"))&&Input.GetAxis("Horizontal")>=0){
            playerdirection=1;
            shooter.transform.position=new Vector2(transform.position.x-0.2f,transform.position.y+0f);
            playerrigidbody.velocity=new Vector2(playerVec.x*playerspeed,0f);
            
          //   position.x = position.x + playerspeed * playerVec.x * Time.deltaTime; 
          //   playerrigidbody.MovePosition(position);
            return;
        }else if(Mathf.Abs(Input.GetAxis("Horizontal"))>=Mathf.Abs(Input.GetAxis("Vertical"))&&Input.GetAxis("Horizontal")<0){
            playerdirection=3;
            shooter.transform.position=new Vector2(transform.position.x+0.2f,transform.position.y+0f);
            playerrigidbody.velocity=new Vector2(playerVec.x*playerspeed,0f);  

           // position.x += playerspeed * playerVec.x * Time.deltaTime; 
           // playerrigidbody.MovePosition(position);
            return;
        }
        if(Mathf.Abs(Input.GetAxis("Horizontal"))<Mathf.Abs(Input.GetAxis("Vertical"))&&Input.GetAxis("Vertical")>=0){
            playerdirection=0;
            shooter.transform.position=new Vector2(transform.position.x-0f,transform.position.y-0.2f);
            playerrigidbody.velocity=new Vector2(0f,playerVec.y*playerspeed);
             
         //  position.y += playerspeed * playerVec.y * Time.deltaTime;  
        //   playerrigidbody.MovePosition(position);
            return;
        }else if(Mathf.Abs(Input.GetAxis("Horizontal"))<Mathf.Abs(Input.GetAxis("Vertical"))&&Input.GetAxis("Vertical")<0){
            playerdirection=2;
            shooter.transform.position=new Vector2(transform.position.x-0f,transform.position.y+0.2f);
           playerrigidbody.velocity=new Vector2(0f,playerVec.y*playerspeed);
             
         //   position.y += playerspeed * playerVec.y * Time.deltaTime;  
        //  playerrigidbody.MovePosition(position);
            return;
        }
    }
     
 
      
    
}
