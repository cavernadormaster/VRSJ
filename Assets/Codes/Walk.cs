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

    private GameObject cama; 
    public static GameObject cartao;
    public static GameObject controle;
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
    public static GameObject inspectPoint;
    public static bool Item_Cartao = false; 
    public static bool Item_Cartao_pego = false;
    public static bool Item_Controle = false;
    public static bool Item_Controle_pego = false;
    public static bool IsInspect = false;
    private Vector3 initialItemPosition;

    private void Start()
    {
       

        // desabilita o simbolo da mao e o botao na tela
        Mao.enabled = false;
        buton.SetActive(false);

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

        cama = GameObject.Find("Cama");
        cartao = GameObject.Find("Cartao");
        controle = GameObject.Find("ControledeAcesso");
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
                Slot.objectToInspect.transform.localScale = Vector2.one * (distanceChange/10);
            }
        }

        

        if (IsInspect && distanceChange >= 100 || IsInspect && distanceChange < -100)
        {
            Debug.Log("Distance between touches changed by: " + distanceChange);
            targetZoom -= distanceChange * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, Time.deltaTime * zoomSpeed);
        }else if(IsInspect)
        {
            Mao.enabled = true;
            buton.SetActive(true);

            float rotationSpeed = 10f;

            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = touch.deltaPosition;

                   Slot.objectToInspect.transform.Rotate(new Vector3(-touchDeltaPosition.y, touchDeltaPosition.x, 0) * rotationSpeed);
                }
            }

        }

        if(Slot.cathcCartão && Slot.cathControle)
        {
            GameObject porta = GameObject.Find("vertice");
            porta.transform.rotation = Quaternion.Euler(0, 94, 0);
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
           
        }else if(other.CompareTag("Cartao"))
        {
            Item_Cartao = true;
            Mao.enabled = true;
            buton.SetActive(true);
        }else if(other.CompareTag("Controle"))
        {
            Item_Controle = true;
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

        }else if (other.CompareTag("Walkable") && !Item_Cartao)
        { 
            safe = false;
            isInSalt = false;
        }else if(other.CompareTag("Cartao"))
        {
            Item_Cartao = false;
            Mao.enabled = false;
            buton.SetActive(false);

        }else if (other.CompareTag("Controle"))
        {
            
            Item_Controle = false;
            Mao.enabled = false;
            buton.SetActive(false);
        }
    }
    public void DestroyCup()
    {
        if(Item_Cartao)
        {
            Destroy(cartao);
            Mao.enabled = false;
            buton.SetActive(false);
            Item_Cartao = false;
            Item_Cartao_pego = true;
        }
        else if (!Item_Cartao && !Item_Controle && !IsInspect)
         {
             Destroy(cama);
             Mao.enabled = false;
             buton.SetActive(false);
             Debug.Log("GET");
         }else if(Item_Controle)
        {
            Destroy(controle);
            Mao.enabled = false;
            buton.SetActive(false);
            Item_Controle = false;
            Item_Controle_pego = true;
        }

    }

    
}
