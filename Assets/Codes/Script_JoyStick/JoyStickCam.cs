using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickCam : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image imgJoystickBg;
    private Image imgJoyStick;
    public static Vector2 posInput;
    public static bool JoystickOn;

    // pega as imagens no Canvas do JoyStick
    void Start()
    {
        imgJoystickBg = GetComponent<Image>();
        imgJoyStick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // pega a direcao das setas no Joystick para determinar a movimentacao
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoystickBg.rectTransform, eventData.position,
            eventData.pressEventCamera, out posInput))
        {
            posInput.x = posInput.x / (imgJoystickBg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgJoystickBg.rectTransform.sizeDelta.y);

            //normalize
            if (posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }

            //move Joystick
            imgJoyStick.rectTransform.anchoredPosition = new Vector2(posInput.x * (imgJoystickBg.rectTransform.sizeDelta.x / 2),
                posInput.y * (imgJoystickBg.rectTransform.sizeDelta.y / 2));
        }
    }

    // detecta se o jogador tocou no JoyStick
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        JoystickOn = true;
    }

    // detecta se o jogador tirou o dedo do joystick e para o personagem
    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imgJoyStick.rectTransform.anchoredPosition = Vector2.zero;
        JoystickOn = false;
    }

    // D�o valores para serem referidos no Script do personagem
    public float inputHorizontal()
    {
        if (posInput.x != 0)
        {
            return posInput.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float inputVertical()
    {
        if (posInput.y != 0)
        {
            return posInput.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
