using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Walk : MonoBehaviour
{
    [SerializeField] private CharacterController _chc;
    [SerializeField] private float speed;

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
        _meshPlayer = tempPlayer.transform.GetChild(0);
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;

        inspectObj = GameObject.Find("Cartao");
        inspectPoint = GameObject.Find("PointInspect");

        screenWidth = Screen.width;
    }


    void Update()
    {
        inputx = _mngrJoystick.inputHorizontal();
        inputz = _mngrJoystick.inputVertical();
       
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
             if(touch.phase == TouchPhase.Began && touch.position.x > (screenWidth/2))
             {
                 initTouch = touch;
             }else if (touch.phase == TouchPhase.Moved && touch.position.x > (screenWidth / 2))
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
