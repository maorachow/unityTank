using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class bulletBehav : MonoBehaviour
{public float movespeed;

public Vector2 originMovitation;
public static GameObject ExploEffect;
public static GameObject hugeExlplo;
public AudioSource hitSound;
public static GameObject hitAudio;
public static GameObject leavesCrackAudio;
public static GameObject leavesCrackAudio2;
public static GameObject exploAudio;
public GameObject fireSource;

public static GameObject enemyCrushEffect;
public static GameObject playerCrushEffect;
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
        playerCrushEffect=Resources.Load<GameObject>("prefabs/playercrush");
        AudioClip hitclip = Resources.Load<AudioClip>("textures/hit");

        hitSound=GetComponent<AudioSource>();
        hitSound.clip=hitclip;
    }

    // Update is called once per frame
    public virtual void ExplodeAndClearBullet(){
      GameObject a=ObjectPools.exploEffectPool.Get();
      a.transform.position=transform.position;
      a.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
      GameObject b=ObjectPools.hitAudioPool.Get();
      b.transform.position=transform.position;
      b.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
   // Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
   // Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
   if(this.gameObject.activeInHierarchy==true){
     WorldGen.bulletPool.Release(this.gameObject); 
    }
    }

  public virtual void BulletCollideWithWallEvent(Vector3Int collidePos){
    if(WorldGen.tm.GetTile(collidePos)==WorldGen.stoneTile){
       ExplodeAndClearBullet();
    }else if(WorldGen.tm.GetTile(collidePos)==WorldGen.brickTile){
      WorldGen.tm.SetTile(collidePos,null);
      ExplodeAndClearBullet();
    }
  }
  public virtual void OnTriggerEnter2D(Collider2D other){


    if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<playermove>()!=null){
        if(other.gameObject.GetComponent<playermove>().isWudi==true){
    
      ExplodeAndClearBullet();
        }else{
          
     // Instantiate(playerCrushEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
      GameObject x=ObjectPools.playerCrushEffectPool.Get();
       x.transform.position=transform.position;
      x.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
      //Destroy(other.gameObject);
      if(other.gameObject.activeInHierarchy==true){
        WorldGen.playerPool.Release(other.gameObject);
      }
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
        GameObject a=ObjectPools.enemyCrushEffectPool.Get();
       a.transform.position=transform.position;
      a.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
     // Instantiate(enemyCrushEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
    //  Destroy(other.gameObject);
    //MyObjectPool.poolInstance.Remove(other.gameObject);
    if(other.gameObject.activeInHierarchy==true){
     WorldGen.enemyPool.Release(other.gameObject); 
    }
    
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
   if(WorldGen.tm.GetTile(new Vector3Int((int)transform.position.x,(int)transform.position.y,0))!=null){
    BulletCollideWithWallEvent(new Vector3Int((int)transform.position.x,(int)transform.position.y,0));
   }
   
   if(fireSource!=null){
    if((this.gameObject.transform.position-fireSource.GetComponent<Transform>().position).magnitude>=20f){
      ExplodeAndClearBullet();

   }
   }


    }

}
