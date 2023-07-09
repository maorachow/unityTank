using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehav : MonoBehaviour
{public float movespeed;
public Vector2 originMovitation;
public GameObject ExploEffect;
public GameObject hugeExlplo;
public AudioSource hitSound;
public GameObject hitAudio;
public GameObject leavesCrackAudio;
public GameObject leavesCrackAudio2;
public GameObject exploAudio;
public GameObject fireSource;
public Ray2D ray;
    // Start is called before the first frame update
    void Start()
    {
       
        movespeed=8f;
        ExploEffect=Resources.Load<GameObject>("prefabs/exploeffect");
        leavesCrackAudio=Resources.Load<GameObject>("prefabs/leavescracksound");
        leavesCrackAudio2=Resources.Load<GameObject>("prefabs/leavescracksound2");
        hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
        hitAudio=Resources.Load<GameObject>("prefabs/hitsound");
        exploAudio=Resources.Load<GameObject>("prefabs/explosound");
        AudioClip hitclip = Resources.Load<AudioClip>("textures/hit");

        hitSound=GetComponent<AudioSource>();
        hitSound.clip=hitclip;
    }

    // Update is called once per frame
    void Update()
    {

   transform.Translate((new Vector2(0f,1f)+originMovitation*0.01f)*movespeed*Time.deltaTime);
   ray=new Ray2D(transform.position,new Vector2(0f,1f));
   RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction,0.1f);
   if(fireSource!=null){
    if((this.gameObject.transform.position-fireSource.GetComponent<Transform>().position).magnitude>=20f){
    Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
 Destroy(this.gameObject);
        Destroy(this);

   }
   }
 //  Debug.Log(info.collider);
   if(info.collider!=null){
   // Debug.Log("leaveshit");
if(info.collider.gameObject.tag=="leaves"){
  
  if(info.collider.gameObject.GetComponent<leaveCrackBeh>().leavesDamageCD<=0f){
switch((int)Random.Range(0f,1.9f)){
  case 1:Instantiate(leavesCrackAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
  break;
  
  case 0:Instantiate(leavesCrackAudio2,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
  break;
  
  
}

    info.collider.gameObject.GetComponent<leaveCrackBeh>().leavesLife-=1;
    info.collider.gameObject.GetComponent<leaveCrackBeh>().leavesDamageCD+=4f;
  }
}
   }
    }
 private void OnCollisionEnter2D(Collision2D other) {


    if(other.gameObject.tag=="brick"){
      //  Debug.Log("brickhit");

Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
        Destroy(other.gameObject);

Transform brickPos=other.gameObject.GetComponent<Transform>();
WorldGen.map[(int)(brickPos.position.x+9.5f),(int)(brickPos.position.y+9.5f)]=0;
        Destroy(this.gameObject);
        Destroy(this);
    }



        if(other.gameObject.tag=="stone"){
      //  Debug.Log("stonehit");
    Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
      Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));

        Destroy(this.gameObject);
        Destroy(this);
    }



    if(other.gameObject.tag=="Player"){
      //  Debug.Log("playerhit");

    Transform otherPos=other.gameObject.GetComponent<Transform>();
   if(other.gameObject.GetComponent<playermove>().isWudi==false){
    Instantiate(exploAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Instantiate(hugeExlplo,otherPos.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Destroy(other.gameObject);
   }
       Instantiate(hitAudio,otherPos.position,Quaternion.Euler(0.0f,0.0f,0.0f));
 Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
 
        Destroy(this.gameObject);
        Destroy(this);
    }
        


  if(other.gameObject.tag=="enemy"){
     //   Debug.Log("enemyhit");

   Transform otherPos=other.gameObject.GetComponent<Transform>();
   if(other.gameObject.GetComponent<enemymove>().isWudi==false){
    Instantiate(exploAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Instantiate(hugeExlplo,otherPos.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Destroy(other.gameObject);
   }
        Instantiate(hitAudio,otherPos.position,Quaternion.Euler(0.0f,0.0f,0.0f));
 Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
 
        Destroy(this.gameObject);
        Destroy(this);
    }

    }
 // void OnDestroy() {
   //    Instantiate(ExploEffect,transform.position,transform.rotation);
   // }
}
