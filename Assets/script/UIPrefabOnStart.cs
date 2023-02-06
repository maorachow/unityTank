using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrefabOnStart : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas=GameObject.Find("Canvas");
        transform.SetParent(canvas.GetComponent<RectTransform>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
