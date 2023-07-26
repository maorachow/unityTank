using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPools : MonoBehaviour
{
    public static ObjectPool<GameObject> enemyCrushEffectPool;

    public static ObjectPool<GameObject> playerCrushEffectPool;
    public static ObjectPool<GameObject> exploEffectPool;
    public static ObjectPool<GameObject> hugeExlploPool;
    public static ObjectPool<GameObject> hitAudioPool;
    public static ObjectPool<GameObject> enchantedBulletPool;
    public static GameObject enchantedBullet;
    public static GameObject hitAudio;
    public static GameObject leavesCrackAudio;
    public static GameObject leavesCrackAudio2;
    public static GameObject exploAudio;
    public static GameObject enemyCrushEffect;
    public static GameObject playerCrushEffect;
    public static GameObject ExploEffect;
    public static GameObject hugeExlplo;
    public GameObject CreateenemyCrushEffect(){
        GameObject go=Instantiate(enemyCrushEffect,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GetenemyCrushEffect(GameObject gameObject){
      //  gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleaseenemyCrushEffect(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyenemyCrushEffect(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreateplayerCrushEffect(){
        GameObject go=Instantiate(playerCrushEffect,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GetplayerCrushEffect(GameObject gameObject){
      //  gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleaseplayerCrushEffect(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyplayerCrushEffect(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreateexploEffect(){
        GameObject go=Instantiate(ExploEffect,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GetexploEffect(GameObject gameObject){
       // gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleaseexploEffect(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyexploEffect(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreatehugeExlplo(){
        GameObject go=Instantiate(hugeExlplo,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GethugeExlplo(GameObject gameObject){
      //  gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleasehugeExlplo(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyhugeExlplo(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreatehitAudio(){
        GameObject go=Instantiate(hitAudio,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GethitAudio(GameObject gameObject){
       // gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleasehitAudio(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyhitAudio(GameObject gameObject){
        Destroy(gameObject);
    }
    public GameObject CreateenchantedBullet(){
        GameObject go=Instantiate(enchantedBullet,new Vector2(1000f,1000f),Quaternion.Euler(0f,0f,0f));
        return go;
    }
    void GetenchantedBullet(GameObject gameObject){
   //     gameObject.transform.position=enemyHome[(int)Random.Range(1f,enemyHome.Count-1f)]+new Vector2(0.5f,0.5f);
        gameObject.SetActive(true);
    }
    void ReleaseenchantedBullet(GameObject gameObject){
        gameObject.SetActive(false);
    }
    void DestroyenchantedBullet(GameObject gameObject){
        Destroy(gameObject);
    }
    void Start(){
        ExploEffect=Resources.Load<GameObject>("prefabs/exploeffect");
        hugeExlplo=Resources.Load<GameObject>("prefabs/hugeexploeffect");
        hitAudio=Resources.Load<GameObject>("prefabs/hitsound");
        exploAudio=Resources.Load<GameObject>("prefabs/explosound");
        enemyCrushEffect=Resources.Load<GameObject>("prefabs/enemycrush");
        playerCrushEffect=Resources.Load<GameObject>("prefabs/playercrush");
        enchantedBullet=Resources.Load<GameObject>("prefabs/enchantedbullet");
    playerCrushEffectPool=new ObjectPool<GameObject>(CreateplayerCrushEffect,GetplayerCrushEffect,ReleaseplayerCrushEffect,DestroyplayerCrushEffect,true,10,50);
    exploEffectPool=new ObjectPool<GameObject>(CreateexploEffect,GetexploEffect,ReleaseexploEffect,DestroyexploEffect,true,10,50);
    hugeExlploPool=new ObjectPool<GameObject>(CreatehugeExlplo,GethugeExlplo,ReleasehugeExlplo,DestroyhugeExlplo,true,10,50);
    hitAudioPool=new ObjectPool<GameObject>(CreatehitAudio,GethitAudio,ReleasehitAudio,DestroyhitAudio,true,10,50);
    enchantedBulletPool=new ObjectPool<GameObject>(CreateenchantedBullet,GetenchantedBullet,ReleaseenchantedBullet,DestroyenchantedBullet,true,10,50);
    enemyCrushEffectPool=new ObjectPool<GameObject>(CreateenemyCrushEffect,GetenemyCrushEffect,ReleaseenemyCrushEffect,DestroyenemyCrushEffect,true,10,50);
    }
}
