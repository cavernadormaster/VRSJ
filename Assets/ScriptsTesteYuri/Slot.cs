using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image _cartao;
    public Image _controleAcesso;


    

    public void RightObject()
    {
        
    }
    public void ADDToInventory()
    {
        if (!Walk.Item_Cartao)
        {
            Debug.Log("cart");
            _cartao.enabled = true;
        }else if(!Walk.Item_Controle)
        {
            _cartao.enabled = true;
        }
    }
} 