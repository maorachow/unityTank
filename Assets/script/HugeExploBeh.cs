using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeExploBeh : explosionEffectBeh
{
public override void EffectFrame(){
    if(gameObject.activeInHierarchy!=true){
        return;
    }
    frame++;
        sr.sprite=explo[frame];
        if(frame>=5){
                    if(gameObject.activeInHierarchy==true){
                    ObjectPools.hugeExlploPool.Release(gameObject);  }
                
               
            }
           // Destroy(this.gameObject);
          //  Destroy(this);
            
}}

