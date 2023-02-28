using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

public class Walk : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;

    private void Start()
    {
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        float xMovement = joystick.Horizontal();
        float zMovement = joystick.Vertical();

        transform.position += new Vector3(zMovement, 0f, xMovement) * speed * Time.deltaTime;

    }
}
