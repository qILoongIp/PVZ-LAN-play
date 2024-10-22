using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CYP_PlantCard : NetworkBehaviour
{
    //[Header("²»¿ÉÓÃ×´Ì¬")]
    private GameObject Unavailable;
    //[Header("Î´×¼±¸ºÃ×´Ì¬")]
    private GameObject Unprepared;
    private GameObject CardPanel;//¸¸¿¨Æ¬À¸
    private GameObject CardPool;//¸¸¿¨³Ø
    // Start is called before the first frame update
    void Start()
    {
        CardPanel = GameObject.FindWithTag("PlantCardPanel");
        CardPool = GameObject.FindWithTag("PlantCardPool");
        Unavailable = transform.Find("Unavailable").gameObject;//GameObject.FindGameObjectWithTag("Unavailable");
        Unprepared = transform.Find("Unprepared").gameObject;//GameObject.FindGameObjectWithTag("Unprepared");
        Unprepared.GetComponent<Image>().fillAmount = 0;
        Color newcolor = Color.white;
        newcolor.a = 0;
        Unavailable.GetComponent<Image>().color = newcolor;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(BaseEventData data)
    {
        if(IsOwner && IsClient)
        {
            NotifyServerOfClickServerRpc();
        }             
    }

    [ServerRpc(RequireOwnership = false)]
    private void NotifyServerOfClickServerRpc(ServerRpcParams rpcParams = default)
    {
        if (transform.parent.gameObject == CardPool)
        {
            if (CardPanel.transform.childCount < 10)
            {
                MoveCardToPanel();
            }
        }
        else if (transform.parent.gameObject == CardPanel)
        {
            MoveCardToPool();
        }
    }

    private void MoveCardToPanel()
    {
        transform.SetParent(CardPanel.transform);
        Debug.Log("Card moved to panel.");
        SoundManager.Instance.PlaySound(SoundManager.Sounds.choose, true);
    }

    private void MoveCardToPool()
    {
        transform.SetParent(CardPool.transform);
        Debug.Log("Card moved to pool.");
        SoundManager.Instance.PlaySound(SoundManager.Sounds.choose, true);
    }
}