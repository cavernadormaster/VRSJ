using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image cartao;
    private bool cathcCart�o;
    public Image controleAcesso;
    private int _countTouch;
    public GameObject Gamecartao;
    

    public void InspectButton()
    {
        _countTouch++;
        if(_countTouch == 2 )//&& cathcCart�o)
        {
            _countTouch = 0;
            Instantiate(Gamecartao, Walk.inspectPoint.transform.position, Quaternion.identity);
        }
    }
    public void ADDToInventory()
    {
        if(!Walk.Item_Cartao && Walk.Item_Cartao_pego)
        {
            cartao.enabled = true;
            Walk.Item_Cartao_pego = false;
            cathcCart�o = true;
        }else if(!Walk.Item_Controle && Walk.Item_Controle_pego)
        {
            controleAcesso.enabled = true;
            Walk.Item_Controle_pego = false;
        }
    }
} 