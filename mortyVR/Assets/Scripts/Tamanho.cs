using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Tamanho : MonoBehaviour {

    private SteamVR_LaserPointer steamVrLaserPointer;
    public GameObject Portal1;
    public GameObject Portal2;
    private GameObject portalExists;
    public GameObject Forcefield;
    private int portalCount;
    private int weaponMode;
    private CharacterController controller;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_ActionSet actionSet;
    private Vector3 playerVelocity;
    public SteamVR_Input_Sources hand;
    private Vector2 m;
    private Vector3 move;
    public SteamVR_Action_Boolean grabGrip;
    public SteamVR_Input_Sources rightInput = SteamVR_Input_Sources.RightHand;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
        portalCount = 0;
        weaponMode = 0;
    }

    private void Start(){
        controller = gameObject.GetComponent<CharacterController>();
        actionSet.Activate(hand);
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {

        if(weaponMode == 0){ // 0 == portal
            if(GameObject.FindGameObjectWithTag("forcefield") != null){
                GameObject.FindGameObjectWithTag("forcefield").SetActive(false);
            }
            portalExists = GameObject.FindGameObjectWithTag("portal1");
            if(portalExists == null){
                portalCount = 0;
            }
            if(portalCount == 0){
               Instantiate(Portal1, e.hit.point+ new Vector3(0, 1.2f, 0), steamVrLaserPointer.pointer.transform.rotation * Quaternion.Euler(90 , 0, 0));
                portalCount++;
            }
            else if(portalCount == 1){
               Instantiate(Portal2, e.hit.point+ new Vector3(0, 1.2f, 0), steamVrLaserPointer.pointer.transform.rotation * Quaternion.Euler(90 , 0, 0));
                portalCount++;
            }
        }
        else if(weaponMode == 1){ //1 == shrink
            if(GameObject.FindGameObjectWithTag("forcefield") != null){
                GameObject.FindGameObjectWithTag("forcefield").SetActive(false);
            }
            StartCoroutine(MoveFromTo(e.target.transform, e.target.transform.position, transform.position, 8.0f));
        }
        else if(weaponMode == 2){ //2 == campo de forca
            if(GameObject.FindGameObjectWithTag("forcefield") == null){
                Instantiate(Forcefield, e.hit.point, Quaternion.identity);
            }
            else{
                if(GameObject.FindGameObjectWithTag("forcefield").activeSelf == true){
                    GameObject.FindGameObjectWithTag("forcefield").SetActive(false);
                }
                else{
                    GameObject.FindGameObjectWithTag("forcefield").SetActive(true);
                }
            }
        }
    }
    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        // Debug.Log("laser saiu do objeto " + e.target.name);
    }
    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        // Debug.Log("laser entrou do objeto " + e.target.name);
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {
        Debug.Log(m);
        objectToMove.localScale = new Vector3(m.y,m.y,m.y)*0.5f + new Vector3(1f,1f,1f) ;
        yield return new WaitForFixedUpdate();
    }

    void Update(){
        // Posição do gamepad
        // move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m = moveAction[hand].axis;
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(rightInput)){
            if(weaponMode==2){
                weaponMode=0;
            }
            weaponMode++;
        }
    }






}
