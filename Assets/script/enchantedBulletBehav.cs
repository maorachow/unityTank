using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enchantedBulletBehav : MonoBehaviour
{
public float movespeed=10f;
public Vector2 originMovitation;
public GameObject ExploEffect;
public GameObject hugeExlplo;
public AudioSource hitSound;
public GameObject hitAudio;
public GameObject leavesCrackAudio;
public GameObject leavesCrackAudio2;
public GameObject exploAudio;
public GameObject fireSource;
    void Start()
    {
        movespeed=8f;
        ExploEffect=Resources.Load<GameObject>("prefabs/exploeffect");
        leavesCrackAudio=Resources.Load<GameObject>("prefabs/leavescracksound");
        leavesCrackAudio2=Resources.Load<GameObject>("prefabs/leavescracksound2");
        hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
        hitAudio=Resources.Load<GameObject>("prefabs/hitsound");
        exploAudio=Resources.Load<GameObject>("prefabs/explosound");
        AudioClip hitclip = Resources.Load<AudioClip>("textures/hit");
        hitSound=GetComponent<AudioSource>();
        hitSound.clip=hitclip;
    }

  
    void Update()
    {
        transform.Translate((new Vector2(0f,1f)+0.01f*originMovitation)*movespeed*Time.deltaTime);
    }
   void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"&&other.gameObject!=fireSource){
            Instantiate(hugeExlplo,other.gameObject.GetComponent<Transform>().position,Quaternion.Euler(0f,0f,0f));
            hitSound.Play();
            Destroy(other.gameObject);
        }
         if(other.gameObject.tag=="enemy"&&other.gameObject!=fireSource){
            Instantiate(hugeExlplo,other.gameObject.GetComponent<Transform>().position,Quaternion.Euler(0f,0f,0f));
            hitSound.Play();
            Destroy(other.gameObject);
        }
         if(other.gameObject.tag=="brick"&&other.gameObject!=fireSource){
            Instantiate(hugeExlplo,other.gameObject.GetComponent<Transform>().position,Quaternion.Euler(0f,0f,0f));
            hitSound.Play();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="stone"&&other.gameObject!=fireSource){
            Instantiate(hugeExlplo,other.gameObject.GetComponent<Transform>().position,Quaternion.Euler(0f,0f,0f));
          //  hitSound.Play();
           // Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
   }
}
