using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcefieldController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter (Collider other){
        if(other.tag == "projectile"){
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
