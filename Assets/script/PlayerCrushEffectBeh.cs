using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrushEffectBeh : ParticleAndEffectBeh
{
   public override void Update(){
      effectLifeTime-=Time.deltaTime;
        if(effectLifeTime<0f){

                ObjectPools.playerCrushEffectPool.Release(gameObject);
            
        }
   }
}
