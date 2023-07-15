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
public GameObject enemyCrushEffect;
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
        enemyCrushEffect=Resources.Load<GameObject>("prefabs/enemycrush");
        AudioClip hitclip = Resources.Load<AudioClip>("textures/hit");

        hitSound=GetComponent<AudioSource>();
        hitSound.clip=hitclip;
    }

    // Update is called once per frame
    public virtual void ExplodeAndClearBullet(){
    Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    Destroy(this.gameObject);
        Destroy(this);
    }

    public virtual Vector3Int GetCollidePos(){
    ray=new Ray2D(transform.position,new Vector2(0f,1f));
   RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction,0.1f);
   if(info.collider!=null){
   // Debug.Log("leaveshit");
   //stone0 leaves1 leavescracked2 brick3
    Vector3Int hitPos=new Vector3Int((int)info.point.x,(int)info.point.y,0);
    if(WorldGen.tm.GetTile(hitPos)!=null){
    // WorldGen.tm.SetTile(new Vector3Int((int)info.point.x,(int)info.point.y,0),null);
      Debug.Log("brickHit");
      return hitPos;
          }}else{
        return new Vector3Int(-10,-10,-10);
          }
           return new Vector3Int(-10,-10,-10);
        }
  public virtual void OnTriggerEnter2D(Collider2D other){
    if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<playermove>()!=null){
        if(other.gameObject.GetComponent<playermove>().isWudi==true){
    
      ExplodeAndClearBullet();
        }else{
      Instantiate(ExploEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
      Destroy(other.gameObject);
      ExplodeAndClearBullet();
        }
      }else{
      //  Destroy(other.gameObject);
      ExplodeAndClearBullet();
      }
      
    }
    if(other.gameObject.tag=="enemy"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<enemymove>()!=null){
        if(other.gameObject.GetComponent<enemymove>().isWudi==true){
    
      ExplodeAndClearBullet();
        }else{
      Instantiate(enemyCrushEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
      Destroy(other.gameObject);
      ExplodeAndClearBullet();
        }
      }else{
      //  Destroy(other.gameObject);
      ExplodeAndClearBullet();
      }
      
    }
  }


    public virtual void Update()
    {

   transform.Translate((new Vector2(0f,1f)+originMovitation*0.01f)*movespeed*Time.deltaTime);
   
   if(fireSource!=null){
    if((this.gameObject.transform.position-fireSource.GetComponent<Transform>().position).magnitude>=20f){
      ExplodeAndClearBullet();

   }
   }
 //  Debug.Log(info.collider);
 Vector3Int tmp=GetCollidePos();
  if(tmp!=new Vector3Int(-10,-10,-10)){
    
if(WorldGen.tm.GetTile(tmp)==WorldGen.stoneTile){
    //  WorldGen.tm.SetTile(GetCollidePos(),null);
    //  breakingLeavesCD=2f;
     // Instantiate(hugeExlplo,GetCollidePos(),Quaternion.Euler(0.0f,0.0f,0.0f));
      ExplodeAndClearBullet();
    }else if(WorldGen.tm.GetTile(tmp)==WorldGen.brickTile){
      WorldGen.tm.SetTile(tmp,null);
    //  breakingLeavesCD=2f;
      Instantiate(hugeExlplo,tmp+new Vector3(0.5f,0.5f,0f),Quaternion.Euler(0.0f,0.0f,0.0f));
      ExplodeAndClearBullet();
    }
  }

    }

}
