using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Cancel : NetworkBehaviour
{
    private GameObject ready;
    // Start is called before the first frame update
    void Start()
    {
        Color newcolor = Color.white;
        newcolor.a = 0;
        GetComponent<Image>().color = newcolor;
        ready = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(ready.GetComponent<Ready>().isReady == true && IsOwner && IsClient)
        {
            SetCancelServerRpc();
        }
    }

    [ServerRpc]
    private void SetCancelServerRpc()
    {
        GameManager.Instance.SetPlantReadyServerRpc(false);
        ready.GetComponent<Ready>().isReady = false;
        UpdateCancelStatusClientRpc(false); // 通知所有客户端更新状态
    }

    [ClientRpc]
    private void UpdateCancelStatusClientRpc(bool status)
    {
        if (status == false)
        {
            Color newcolor = Color.white;
            newcolor.a = 0;
            GetComponent<Image>().color = newcolor;
            newcolor = Color.white;
            newcolor.a = 1;
            ready.GetComponent<Image>().color = newcolor;
        }
    }
}
