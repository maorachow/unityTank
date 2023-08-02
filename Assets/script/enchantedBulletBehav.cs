using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enchantedBulletBehav : bulletBehav
{

    public override void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject==fireSource){
  
    return;
  }
    if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<playermove>()!=null){
        if(other.gameObject.GetComponent<playermove>().isWudi==true){
    other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*3f);
      ExplodeAndClearBullet();
        }else{
    //  Instantiate(ExploEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
       GameObject x=ObjectPools.playerCrushEffectPool.Get();
       x.transform.position=transform.position;
      x.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
    //  Destroy(other.gameObject);
     if(other.gameObject.activeInHierarchy==true){
      WorldGen.enemyPool.Release(other.gameObject);
     }
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
      //Instantiate(enemyCrushEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
     // Destroy(other.gameObject);
     if(other.gameObject.activeInHierarchy==true){
      WorldGen.enemyPool.Release(other.gameObject);
     }
     
      
        }
      }else{
      //  Destroy(other.gameObject);
      ExplodeAndClearBullet();
      }
      
    }
  }
  public override void ExplodeAndClearBullet(){
     GameObject a=ObjectPools.exploEffectPool.Get();
      a.transform.position=transform.position;
      a.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
      GameObject b=ObjectPools.hitAudioPool.Get();
      b.transform.position=transform.position;
      b.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);
   // Instantiate(ExploEffect,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
   // Instantiate(hitAudio,transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
   if(this.gameObject.activeInHierarchy==true){
     ObjectPools.enchantedBulletPool.Release(this.gameObject); 
    }
  }

   public override void BulletCollideWithWallEvent(Vector3Int collidePos){

    if(WorldGen.tm.GetTile(collidePos)==WorldGen.stoneTile){
       ExplodeAndClearBullet();
    }else if(WorldGen.tm.GetTile(collidePos)==WorldGen.brickTile){
          GameObject x=ObjectPools.hugeExlploPool.Get();
       x.transform.position=collidePos;
      x.transform.rotation=Quaternion.Euler(0.0f,0.0f,Random.Range(0f,180f));
     
      WorldGen.tm.SetTile(collidePos,null);
      
    //  ExplodeAndClearBullet();
    }
  }
 //  Debug.Log(info.collider);
// Vector3Int tmp=GetCollidePos();
 // if(tmp!=new Vector3Int(-10,-10,-10)){
    
//if(WorldGen.tm.GetTile(tmp)==WorldGen.stoneTile){
    //  WorldGen.tm.SetTile(GetCollidePos(),null);
    //  breakingLeavesCD=2f;
     // Instantiate(hugeExlplo,GetCollidePos(),Quaternion.Euler(0.0f,0.0f,0.0f));
   //   ExplodeAndClearBullet();
  //  }else if(WorldGen.tm.GetTile(tmp)==WorldGen.brickTile){
   //   WorldGen.tm.SetTile(tmp,null);
    //  breakingLeavesCD=2f;
  //    Instantiate(hugeExlplo,tmp+new Vector3(0.5f,0.5f,0f),Quaternion.Euler(0.0f,0.0f,0.0f));
    //  ExplodeAndClearBullet();
 //   }
//  }

    
}
