using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectItemBeh : MonoBehaviour
{
    public int effectId=0;
    public Sprite resistanceIcon;
    public Sprite strengthIcon;
    public SpriteRenderer sr;
    public GameObject itemEatenEffect;
    public float itemLifeTime=10f;
    //0resistance1strength
    // Start is called before the first frame update
    void Start()
    {    
        WorldGen.effectItemList.Add(this);
        itemEatenEffect=Resources.Load<GameObject>("prefabs/itemeateneffectprefab");
        resistanceIcon=Resources.Load<Sprite>("textures/resistance");
        strengthIcon=Resources.Load<Sprite>("textures/strength");
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        itemLifeTime-=Time.deltaTime;
        sr.color=new Color(1,1,1,itemLifeTime/10f);
        if(itemLifeTime<=0f){
            Destroy(this.gameObject);
            Destroy(this);
        }
        switch(effectId){
            case 0:sr.sprite=resistanceIcon;break;
            case 1:sr.sprite=strengthIcon;break;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<playermove>().effectEatenId=effectId;
            other.gameObject.GetComponent<playermove>().effectTime=8f;
            Instantiate(itemEatenEffect,transform.position,transform.rotation);
            WorldGen.effectItemList.Remove(this);
            Destroy(this.gameObject);
            Destroy(this);
        }
        if(other.gameObject.tag=="enemy"){
            other.gameObject.GetComponent<enemymove>().effectEatenId=effectId;
            other.gameObject.GetComponent<enemymove>().effectTime=8f;
            if(itemEatenEffect!=null){
            Instantiate(itemEatenEffect,transform.position,transform.rotation);    
            }
            
            WorldGen.effectItemList.Remove(this);
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

}
