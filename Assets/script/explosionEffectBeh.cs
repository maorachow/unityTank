using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionEffectBeh : MonoBehaviour
{
    public Sprite[] explo;
    public SpriteRenderer sr;
    public float delayTime;
    public int frame=-1;
    
    // Start is called before the first frame update
    void Start()

    { explo=new Sprite[6];
        explo[0]=Resources.Load<Sprite>("textures/explo2");
    explo[1]=Resources.Load<Sprite>("textures/explo4");
    explo[2]=Resources.Load<Sprite>("textures/explo5");
    explo[3]=Resources.Load<Sprite>("textures/explo1");
    explo[4]=Resources.Load<Sprite>("textures/explo3");
        delayTime=0.1f;
       
        sr=GetComponent<SpriteRenderer>();
    InvokeRepeating("EffectFrame", 0.0f, 0.2f);
    }
    void OnEnable(){
    InvokeRepeating("EffectFrame", 0.0f, 0.2f);
    }
    void OnDisable(){
        frame=0;
        CancelInvoke("EffectFrame");
    }
void EffectFrame(){
    if(gameObject.activeInHierarchy!=true){
        return;
    }
    frame++;
        sr.sprite=explo[frame];
        if(frame>=5){
            if(gameObject.activeInHierarchy==true){
                if(gameObject.name=="exploeffect(Clone)"){
                   ObjectPools.exploEffectPool.Release(gameObject);  
                }else if(gameObject.name=="hugeexploeffect(Clone)"){
                    ObjectPools.hugeExlploPool.Release(gameObject);  
                }
               
            }
           // Destroy(this.gameObject);
          //  Destroy(this);
            
}}
    // Update is called once per frame
    void Update()
    {
       
    }
}
