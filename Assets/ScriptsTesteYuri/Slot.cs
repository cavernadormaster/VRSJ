using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image cartao;
    private bool cathcCartão;
    public Image controleAcesso;
    private bool cathControle;
    private int _countTouch;
    public GameObject Gamecartao;
    public GameObject GameControle;
    

    public void InspectButtonCartao()
    {
        _countTouch++;
        if(cathcCartão && _countTouch >= 2)
        {
            _countTouch = 0;
            Instantiate(Gamecartao, Walk.inspectPoint.transform.position, Quaternion.identity);
            Walk.IsInspect = true;
        }
    }
    public void InspectControle()
    {
        _countTouch++;
        if (_countTouch >=2 && cathControle)
        {
            
            _countTouch = 0;
            Instantiate(GameControle, Walk.inspectPoint.transform.position, Quaternion.identity);
        }
    }
    public void ADDToInventory()
    {
        if(!Walk.Item_Cartao && Walk.Item_Cartao_pego)
        {
            cartao.enabled = true;
            Walk.Item_Cartao_pego = false;
            cathcCartão = true;
        }else if(!Walk.Item_Controle && Walk.Item_Controle_pego)
        {
            controleAcesso.enabled = true;
            Walk.Item_Controle_pego = false;
            cathControle = true;
        }
    }
} 