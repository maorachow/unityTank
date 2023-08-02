using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAndEffectBeh : MonoBehaviour
{
    // Start is called before the first frame update
    public float effectLifeTime=3f;
    public ParticleSystem ps;
    public AudioSource AS;
    void Start()
    {
        ps=GetComponent<ParticleSystem>();
        AS=GetComponent<AudioSource>();
      //  Destroy(this.gameObject,3f);
        
    }
    void OnEnable(){
        ps=GetComponent<ParticleSystem>();
        AS=GetComponent<AudioSource>();
        effectLifeTime=3f;
        ps.Play();
        AS.Play();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        effectLifeTime-=Time.deltaTime;
        if(effectLifeTime<0f){
            if(gameObject.name=="enemycrush(Clone)"){
                ObjectPools.enemyCrushEffectPool.Release(gameObject);
            }else if(gameObject.name=="playercrush(Clone)"){
                ObjectPools.playerCrushEffectPool.Release(gameObject);
            }
        }
    }
}
