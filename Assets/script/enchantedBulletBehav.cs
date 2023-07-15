using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enchantedBulletBehav : bulletBehav
{

    public override void OnTriggerEnter2D(Collider2D other){
    if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
      if(other.gameObject.GetComponent<playermove>()!=null){
        if(other.gameObject.GetComponent<playermove>().isWudi==true){
    
      ExplodeAndClearBullet();
        }else{
      Instantiate(ExploEffect,other.transform.position,Quaternion.Euler(0.0f,0.0f,0.0f));
      Destroy(other.gameObject);
    
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
      
        }
      }else{
      //  Destroy(other.gameObject);
      ExplodeAndClearBullet();
      }
      
    }
  }

   public override void Update()
    {

   transform.Translate((new Vector2(0f,1f)+originMovitation*0.01f)*movespeed*Time.deltaTime);
   
   if(fireSource!=null){
    if((this.gameObject.transform.position-fireSource.GetComponent<Transform>().position).magnitude>=40f){
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
    //  ExplodeAndClearBullet();
    }
  }

    }
}
