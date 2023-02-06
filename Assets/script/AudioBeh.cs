using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeh : MonoBehaviour
{
    public AudioSource AS;
  
    void Start()
    {
        AS=GetComponent<AudioSource>();
        AS.Play();
        Destroy(this.gameObject,3f);
        Destroy(this,3f);
        
    }

  
}
