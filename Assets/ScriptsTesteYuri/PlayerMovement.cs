using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // velocidade de movimento do personagem

    // método chamado a cada quadro
    void Update()
    {
        // obtém a entrada do teclado para as teclas WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        // calcula o movimento do personagem
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // aplica o movimento ao personagem
        transform.Translate(movement, Space.Self);
    }
}