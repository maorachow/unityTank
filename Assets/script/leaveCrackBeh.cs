using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveCrackBeh : MonoBehaviour
{
    public int leavesLife;
    public Sprite[] leavesSprite;
    public SpriteRenderer sr;
    public float leavesDamageCD;
    // Start is called before the first frame update
    void Start()
    {
        sr=gameObject.GetComponent<SpriteRenderer>();
        leavesSprite=new Sprite[3];
        leavesSprite[1]=Resources.Load<Sprite>("textures/leaves");
        leavesSprite[0]=Resources.Load<Sprite>("textures/leavescracked");
        leavesLife=1;
    }



    // Update is called once per frame
    void Update()
    {
        if(leavesDamageCD>0f){
            leavesDamageCD-=Time.deltaTime*40f;
        }
        if(leavesLife>=0){
        sr.sprite=leavesSprite[leavesLife];
        }else{
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
