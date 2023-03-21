using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image cartao;
    public Image controleAcesso;
    private int _countTouch;

    

    public void InspectButton()
    {
        _countTouch++;
        if(_countTouch == 2)
        {
            Debug.Log("Inst");
        }
    }
    public void ADDToInventory()
    {
        if(!Walk.Item_Cartao && Walk.Item_Cartao_pego)
        {
            cartao.enabled = true;
            Walk.Item_Cartao_pego = false;
        }else if(!Walk.Item_Controle && Walk.Item_Controle_pego)
        {
            controleAcesso.enabled = true;
            Walk.Item_Controle_pego = false;
        }
    }
} 