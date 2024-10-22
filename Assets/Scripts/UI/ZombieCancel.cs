using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ZombieCancel : NetworkBehaviour
{
    public GameObject ready;
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
        if (ready.GetComponent<ZombieReady>().isReady == true && !IsOwner && IsClient)
        {
            //Debug.Log("isReady");
            SetCancelServerRpc();
            //Debug.Log("isalReady");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetCancelServerRpc()
    {
        GameManager.Instance.SetZombieReadyServerRpc(false);
        UpdateCancelStatusClientRpc(false); // 通知所有客户端更新状态
    }

    [ClientRpc]
    private void UpdateCancelStatusClientRpc(bool status)
    {
        ready.GetComponent<ZombieReady>().isReady = false;
        if (status == false)
        {
            Debug.Log("here false");
            Color newcolor = Color.white;
            newcolor.a = 0;
            GetComponent<Image>().color = newcolor;
            newcolor = Color.white;
            newcolor.a = 1;
            ready.GetComponent<Image>().color = newcolor;
        }
    }
}
