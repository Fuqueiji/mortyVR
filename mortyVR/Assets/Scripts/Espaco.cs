using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espaco : MonoBehaviour

{


   public int total_asteroides = 100;

   public float speed = 1.0f;


   public GameObject asteroid;
   public GameObject[] asteroids;

   void Start()

   {
       spawn();
   }

   public void despawn(){
        asteroids = GameObject.FindGameObjectsWithTag("Asteroids");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
   }

    public void spawn(){
        for (int i=0; i<total_asteroides; i++) {
           float spawnPointX = Random.Range(-240.0f, -280.0f);
           float spawnPointY = Random.Range(18.0f, 25.0f);
           float spawnPointZ = Random.Range(200.0f, 240.0f);
           Vector3 spawnPos = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
           GameObject objeto = Instantiate(asteroid, spawnPos, Quaternion.identity);
           objeto.transform.parent = gameObject.transform;
           float spawnDirX = 0f;
           float spawnDirY = -1f;
           float spawnDirZ = 0f;
           float speed = Random.Range(-1.0f, 1.0f);
           Vector3 spawnDirection = new Vector3 (spawnDirX, spawnDirY, spawnDirZ);
           objeto.GetComponent<Rigidbody>().velocity = speed * spawnDirection;
       }  
    }
}