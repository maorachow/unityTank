using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeh : MonoBehaviour
{
    public AudioSource AS;
    public float audioLifeTime=3f;
    void Start()
    {

        AS=GetComponent<AudioSource>();
        AS.Play();

       // Destroy(this.gameObject,3f);
       // Destroy(this,3f);
        
    }
    void OnEnable(){
         AS=GetComponent<AudioSource>();
        audioLifeTime=3f;
        AS.Play();
    }
      //  public float audioLifeTime=3f;
      //  AS.Play();

    
    void Update(){
        audioLifeTime-=Time.deltaTime;
        if(audioLifeTime<0f){
            if(gameObject.name=="hitsound(Clone)"){
                ObjectPools.hitAudioPool.Release(gameObject);
            }
        }
    }

  
}
