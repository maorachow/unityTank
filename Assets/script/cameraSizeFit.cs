using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSizeFit : MonoBehaviour
{
    public Camera mainCam;
   public float devWidth=20f;
   public float devHeight=20f;
   public float targetOrthoSize;
   public Vector2 newPos;
   public Transform playerTransform;
   public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mainCam=GetComponent<Camera>();
        player=GameObject.FindGameObjectWithTag("Player");
//this.gameObject.transform.position=new Vector2(WorldGen.worldwidth,WorldGen.worldwidth);
    }
 private void Awake()
    {
        //Adaptation();
    }
    // Update is called once per frame
   /* void FixedUpdate()
    {
        
       
      float screenHeight = Screen.height;
      //   Debug.Log("screenHeight = " + screenHeight);
        //摄像机的尺寸
         float orthographicSize = this.GetComponent<Camera>().orthographicSize;
         //宽高比
         float aspectRatio = Screen.width * 1.0f / Screen.height;
         //摄像机的单位宽度
         float cameraWidth = orthographicSize * 2 * aspectRatio;
 
         Debug.Log("cameraWidth = " + cameraWidth);
        //如果设备的宽度大于摄像机的宽度的时候  调整摄像机的orthographicSize
      if(Screen.width>=Screen.height){
        orthographicSize =  devWidth/ (2 * aspectRatio);
      }else if(Screen.width<Screen.height){
        orthographicSize =  devHeight/ (2 * aspectRatio);
      }
        
            

            Debug.Log("new orthographicSize = " + orthographicSize);
            this.GetComponent<Camera>().orthographicSize = orthographicSize;
        

}*/
void Update(){
   if(player==null){
    player=GameObject.FindGameObjectWithTag("Player");
   }
    
     newPos = mainCam.ScreenToWorldPoint (Input.mousePosition);
if(targetOrthoSize<=0f){
  targetOrthoSize=1f;
}
        //鼠标滚轮的效果
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
           
            if (targetOrthoSize<= 50f){

                targetOrthoSize += 2.2f;
                // transform.position = Vector2.Lerp (transform.position,  newPos, Time.deltaTime*5f);
                }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (targetOrthoSize >= 3f){
               targetOrthoSize-= 2.2f;
                //transform.position = Vector2.Lerp (transform.position,  newPos, Time.deltaTime*5f);
                }
        }
        mainCam.orthographicSize=Mathf.Lerp(mainCam.orthographicSize,targetOrthoSize,3f*Time.deltaTime);
        if(player!=null){
          if(WorldGen.isUsingKeboardShooterControl==true){
  if(Input.GetMouseButton(0)){
   float h = Input.GetAxis("Mouse X");
    float v = Input.GetAxis("Mouse Y");
//Vector3 moveDir=transform.position;
//moveDir+=mainCam.ScreenToWorldPoint (Input.mousePosition);
     //moveDir.z = 0;
     if(transform.position.x<100f&&transform.position.x>-100f&&transform.position.y<100f&&transform.position.y>-100f){
      transform.position -= new Vector3(h,v,0f);
     }else{
       transform.position=player.GetComponent<Transform>().position;
     }
            
            
}

}else{
  transform.position=Vector2.Lerp(transform.position,player.GetComponent<Transform>().position,3f*Time.deltaTime);
}
        }

   
 
   
 
}
}
