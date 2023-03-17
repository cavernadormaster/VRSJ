using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Walk : MonoBehaviour
{
    [SerializeField] private CharacterController _chc;
    [SerializeField] private float speed;

    public float zoomSpeed = 10f;
    public float minZoom = -5f;
    public float maxZoom = 20f;
    private float targetZoom;

    private Vector2 initialTouch1Pos, initialTouch2Pos; 
    private float initialDistance;

    private float distanceChange;

    public GameObject Caneca;
    public Image Mao;
    public GameObject buton;

    public static bool safe = true;
    public static bool isInSalt = true;

    private ManagerJoyStick _mngrJoystick;
    private JoyStickCam _mnJoyCam;

    private Touch initTouch = new Touch();
    public Camera cam;
    private float rotX;
    private float rotY;
    private Vector3 origRot;
    public float rotspeed= 0.5f;
    public float dirCam = -1;

    private Transform _meshPlayer;

    private float inputx;
    private float inputz;
    private Vector3 v_movement;


    // identifica o tamanho da tela
    public float screenWidth;

    private GameObject inspectObj;
    private GameObject inspectPoint;
    public bool Item = false;
    public bool IsInspect = false;
    public Image Mao2;
    public GameObject buton2;
    private Vector3 initialItemPosition;

    private void Start()
    {
       

        // desabilita o simbolo da mao e o botao na tela
        Mao.enabled = false;
        buton.SetActive(false);
        Mao2.enabled = false;
        buton2.SetActive(false);

        Cursor.visible = true;
        _mngrJoystick = GameObject.Find("ImageJoyStickBg").GetComponent<ManagerJoyStick>();
        GameObject tempPlayer = GameObject.Find("FPSController");
        _chc = tempPlayer.GetComponent<CharacterController>();
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;

        inspectObj = GameObject.Find("Cartao");
        inspectPoint = GameObject.Find("PointInspect");

        screenWidth = Screen.width;

        targetZoom = cam.fieldOfView;
    }


    void Update()
    {
        inputx = _mngrJoystick.inputHorizontal();
        inputz = _mngrJoystick.inputVertical();

        if (Input.touchCount >= 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialTouch1Pos = touch1.position;
                initialTouch2Pos = touch2.position;
                initialDistance = Vector2.Distance(initialTouch1Pos, initialTouch2Pos);
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {

                float newDistance = Vector2.Distance(touch1.position, touch2.position);
                distanceChange = newDistance - initialDistance;
                Debug.Log("Distance between touches changed by: " + distanceChange);
            }
        }

        

        if (IsInspect && distanceChange >= 100 || IsInspect && distanceChange < -100)
        {
            Debug.Log("Distance between touches changed by: " + distanceChange);
            targetZoom -= distanceChange * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, Time.deltaTime * zoomSpeed);
        }
    }


    private void FixedUpdate()
    {

        //movimenta��o
        if(!IsInspect)
            v_movement = new Vector3(-inputz * speed, 0, inputx * speed);
          

        _chc.Move(v_movement);

        //rota��o da mesh
        if (inputx != 0 || inputz != 0)
        {
            Vector3 lookdir = new Vector3(v_movement.x, 0, 0);
        }

        

         //movimenta��o de camera por touch na tela
         //verifica cada toque na tela
         foreach(Touch touch in Input.touches)
         {
             
             if(touch.phase == TouchPhase.Began && touch.position.x > (screenWidth/2) && !IsInspect)
             {
                 initTouch = touch;
             }else if (touch.phase == TouchPhase.Moved && touch.position.x > (screenWidth / 2) && !IsInspect)
             {
                 float deltaX = initTouch.position.x - touch.position.x;
                 float deltaY = initTouch.position.y - touch.position.y;
                 rotX -= deltaY * Time.deltaTime * rotspeed * dirCam;
                 rotY += deltaX * Time.deltaTime * rotspeed * dirCam;
                 rotX = Mathf.Clamp(rotX, -45f, 45f);
                 cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f); 
             }else if (touch.phase == TouchPhase.Ended)
             {
                 initTouch = new Touch();
             }
         }

        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Jogador"))
        {
            Mao.enabled = true;
            buton.SetActive(true);
        }
        if (other.CompareTag("Walkable")) 
        {
            safe = true;
           
        }else if(other.CompareTag("Cartao") && !IsInspect)
        {
            Item = true;
            Mao.enabled = true;
            buton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jogador"))
        {
            Mao.enabled = false;
            buton.SetActive(false);

        }else if (other.CompareTag("Walkable") && !Item)
        { 
            safe = false;
            isInSalt = false;
        }else if(other.CompareTag("Cartao"))
        {
            Item = false;
            Mao.enabled = false;
            buton.SetActive(false);
        }
    }
    public void DestroyCup()
    {
        if (!Item)
        {
            Destroy(Caneca);
            Mao.enabled = false;
            buton.SetActive(false);
            Debug.Log("GET");
        }

    }

    public void Inspect()
    {
        if(Item)
        {
            initialItemPosition = inspectObj.transform.position;
            inspectObj.transform.position = inspectPoint.transform.position;
            IsInspect = true;
            v_movement = new Vector3(0, 0, 0);
            Mao2.enabled = true;
            buton2.SetActive(true);

            Mao.enabled = false;
            buton.SetActive(false);
        }
        
    }
    public void DesInspect()
    {
        if (IsInspect)
        {
            Debug.Log("GET");
            Item = false;
            IsInspect = false;
            Mao2.enabled = false;
            buton2.SetActive(false);
            v_movement = new Vector3(-inputz * speed, 0, inputx * speed);
            inspectObj.transform.position = initialItemPosition;
        }
    }
    
}
