using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalController : MonoBehaviour
{
    public GameObject player;
    public GameObject portal;

    private bool playerIsOverlapping = false;
    // Start is called before the first frame update
    void Start()
    {
        portal = null;
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("PlayerOne");
        }
        transform.Rotate (new Vector3 (0, 20, 0) * Time.deltaTime);
        if(gameObject.tag == "portal1" && portal == null){
            portal = GameObject.FindGameObjectWithTag("portal2");
        }
        if(gameObject.tag == "portal2" && portal == null){
            portal = GameObject.FindGameObjectWithTag("portal1");
        }
        if(playerIsOverlapping){
            Debug.Log(portal.transform.position);
            player.transform.position = new Vector3(portal.transform.position.x, 0, portal.transform.position.z);
            if(gameObject.tag == "portal1"){
                Destroy(GameObject.FindGameObjectWithTag("portal2"));
                Destroy(GameObject.FindGameObjectWithTag("portal1"));
            }
            else{
                Destroy(GameObject.FindGameObjectWithTag("portal1"));
                Destroy(GameObject.FindGameObjectWithTag("portal2"));
            }
        }
    }

    void OnTriggerEnter (Collider other){
        if(other.tag == "Player"){
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            playerIsOverlapping = false;
        }
    }
}
