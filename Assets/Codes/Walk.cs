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



    private void Start()
    {
       

        // desabilita o simbolo da mão e o botão na tela
        Mao.enabled = false;
        buton.SetActive(false);

        Cursor.visible = true;
        _mngrJoystick = GameObject.Find("ImageJoyStickBg").GetComponent<ManagerJoyStick>();
        GameObject tempPlayer = GameObject.Find("FPSController");
        _chc = tempPlayer.GetComponent<CharacterController>();
        _meshPlayer = tempPlayer.transform.GetChild(0);
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }


    void Update()
    {
        inputx = _mngrJoystick.inputHorizontal();
        inputz = _mngrJoystick.inputVertical();
       
    }
    private void FixedUpdate()
    {

        //movimentação
            v_movement = new Vector3(-inputz * speed, 0, inputx * speed);
       
        _chc.Move(v_movement);

        //rotação da mesh
        if (inputx != 0 || inputz != 0)
        {
            Vector3 lookdir = new Vector3(v_movement.x, 0, 0);
            //_meshPlayer.rotation = Quaternion.LookRotation(lookdir);
        }

        if(ManagerJoyStick.JoystickOn == true)
        {
            Debug.Log("funfou");
        }

        //movimentação de camera por touch na tela
        //verifica cada toque na tela
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began && ManagerJoyStick.JoystickOn == false)
            {
                initTouch = touch;
            }else if (touch.phase == TouchPhase.Moved)
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
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jogador"))
        {
            Mao.enabled = false;
            buton.SetActive(false);
        }else if (other.CompareTag("Walkable"))
        { 
            safe = false;
            isInSalt = false;
        }
    }
    public void DestroyCup()
    {
        Destroy(Caneca);
    }

    
}
