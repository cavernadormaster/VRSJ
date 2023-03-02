using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private CharacterController _chc;
    [SerializeField] private float speed;

    private ManagerJoyStick _mngrJoystick;

    private float inputx;
    private float inputz;
    private Vector3 v_movement;

    private void Start()
    {
        Cursor.visible = true;
        _mngrJoystick = GameObject.Find("ImageJoyStickBg").GetComponent<ManagerJoyStick>();
        GameObject tempPlayer = GameObject.Find("FPSController");
        _chc = tempPlayer.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        inputx = _mngrJoystick.inputHorizontal();
        inputz = _mngrJoystick.inputVertical();
       
    }
    private void FixedUpdate()
    {
        v_movement = new Vector3(inputz * speed, 0, inputx * speed);
        _chc.Move(v_movement);
    }
}
