using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrushEffectBeh : ParticleAndEffectBeh
{
   public override void Update(){
      effectLifeTime-=Time.deltaTime;
        if(effectLifeTime<0f){

                ObjectPools.enemyCrushEffectPool.Release(gameObject);
            
        }
   }
}
