using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class bulletBehav : MonoBehaviour
{public float movespeed;
public float lifeTime;
public Transform thisTransform;
public Vector2 originMovitation;
public float bulletBreakLeavesCD;
public static GameObject ExploEffect;
public static GameObject hugeExlplo;
public AudioSource hitSound;
public AudioClip brickSound1;
public AudioClip leavesSound1;
public AudioClip brickSound2;
public AudioClip leavesSound2;
public static GameObject hitAudio;
public static GameObject leavesCrackAudio;
public static GameObject leavesCrackAudio2;
public static GameObject exploAudio;
public GameObject fireSource;

public static GameObject enemyCrushEffect;
public static GameObject playerCrushEffect;

    // Start is called before the first frame update

 void OnEnable(){
  lifeTime=0f;
  bulletBreakLeavesCD=0.9f;
  //fireSource=null;
 }
 void OnDisable(){
  lifeTime=0f;
 }

    void Start()
    {
       thisTransform=transform;
        movespeed=10f;
        ExploEffect=Resources.Load<GameObject>("prefabs/exploeffect");
        leavesCrackAudio=Resources.Load<GameObject>("prefabs/leavescracksound");
        leavesCrackAudio2=Resources.Load<GameObject>("prefabs/leavescracksound2");
        hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
        hitAudio=Resources.Load<GameObject>("prefabs/hitsound");
        exploAudio=Resources.Load<GameObject>("prefabs/explosound");
        enemyCrushEffect=Resources.Load<GameObject>("prefabs/enemycrush");
        playerCrushEffect=Resources.Load<GameObject>("prefabs/playercrush");
        leavesSound1=Resources.Load<AudioClip>("audios/Foliage02");
        leavesSound2=Resources.Load<AudioClip>("audios/Foliage04");
        brickSound1=Resources.Load<AudioClip>("audios/stone1");
        brickSound2=Resources.Load<AudioClip>("audios/stone2");

        hitSound=GetComponent<AudioSource>();
    //    hitSound.clip=hitclip;
    }

    // Update is called once per frame
    public virtual void ExplodeAndClearBullet(){
      GameObject a=ObjectPools.exploEffectPool.Get();
      a.transform.position=thisTransform.position;
      a.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
      GameObject b=ObjectPools.hitAudioPool.Get();
      b.transform.position=thisTransform.position;
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
       return;
    }else if(WorldGen.tm.GetTile(collidePos)==WorldGen.brickTile){
      WorldGen.tm.SetTile(collidePos,null);
      var tmpInt=(int)Random.Range(0f,1.99f);
      if(tmpInt==1){
 
      AudioSource.PlayClipAtPoint(brickSound1,thisTransform.position);
      }else if(tmpInt==2){
    
      AudioSource.PlayClipAtPoint(brickSound2,thisTransform.position);
      }
     // hitSound.clip=brickSound2;
     // hitSound.Play();
     // Invoke("StopAudio",2f);
       GameObject x=ObjectPools.hugeExlploPool.Get();
       x.transform.position=collidePos+new Vector3(0.5f,0.5f,0f);
      x.transform.rotation=Quaternion.Euler(0.0f,0.0f,Random.Range(0f,180f));

      ExplodeAndClearBullet();

    }else if(WorldGen.tm.GetTile(collidePos)==WorldGen.leavesTile){
      if(bulletBreakLeavesCD<=0f){
        WorldGen.tm.SetTile(collidePos,WorldGen.leavesTileCracked);
        var tmpInt=(int)Random.Range(0f,1.99f);
        if(tmpInt==1){
    
        AudioSource.PlayClipAtPoint(leavesSound1,thisTransform.position);
        }else if(tmpInt==2){
  
        AudioSource.PlayClipAtPoint(leavesSound2,thisTransform.position);
        }
      //hitSound.Play();
     // Invoke("StopAudio",1f);
        bulletBreakLeavesCD+=1.9f;
        return;
      }else{
        return;
      }
    }else if(WorldGen.tm.GetTile(collidePos)==WorldGen.leavesTileCracked){
      if(bulletBreakLeavesCD<=0f){
        WorldGen.tm.SetTile(collidePos,null);
        var tmpInt=(int)Random.Range(0f,1.99f);
        if(tmpInt==1){
        hitSound.clip=leavesSound1;
        AudioSource.PlayClipAtPoint(leavesSound1,thisTransform.position);
        }else if(tmpInt==2){
        hitSound.clip=leavesSound2;
        AudioSource.PlayClipAtPoint(leavesSound2,thisTransform.position);
        }
       //  hitSound.Play();
      //   Invoke("StopAudio",2f);
        bulletBreakLeavesCD+=1.9f;
        return;
      }else{
        return;
      }
    }
  }
  public virtual void OnTriggerEnter2D(Collider2D other){
  //  BulletCollideWithWallEvent(new Vector3Int((int)transform.position.x,(int)transform.position.z,0));
  if(other.gameObject==fireSource){
  
    return;
  }
    if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<playermove>()!=null){
        if(other.gameObject.GetComponent<playermove>().isWudi==true){
          other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up);
      ExplodeAndClearBullet();
        }else if(other.gameObject==fireSource){ other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up);}else{
          
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
        }else if(other.gameObject==fireSource){ other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up);}else{
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
      lifeTime+=Time.deltaTime;
      if(bulletBreakLeavesCD>=0f){
      bulletBreakLeavesCD-=Time.deltaTime;
    }

   thisTransform.Translate((new Vector2(0f,1f)+originMovitation*0.03f)*movespeed*Time.deltaTime);
   if(WorldGen.tm.GetTile(new Vector3Int((int)thisTransform.position.x,(int)thisTransform.position.y,0))!=null){
    BulletCollideWithWallEvent(new Vector3Int((int)thisTransform.position.x,(int)thisTransform.position.y,0));
   }
   
 
    //if((this.gameObject.thisTransform.position-fireSource.GetComponent<Transform>().position).magnitude>=20f){
      if(lifeTime>=10f){
      ExplodeAndClearBullet();  
      }
      

   //}
   


    }

}
