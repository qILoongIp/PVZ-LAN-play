using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ZombieReady : NetworkBehaviour
{
    public bool isReady;
    private GameObject cancel;
    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        cancel = transform.Find("Cancel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        Debug.Log("Now" + isReady);
        if (isReady == false && !IsOwner && IsClient)
        {
            SetReadyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetReadyServerRpc()
    {
        GameManager.Instance.SetZombieReadyServerRpc(true);
        isReady = true;
        UpdateReadyStatusClientRpc(true); // 通知所有客户端更新状态
    }

    [ClientRpc]
    private void UpdateReadyStatusClientRpc(bool status)
    {
        isReady = status;
        Color newcolor = Color.white;
        newcolor.a = 0;
        GetComponent<Image>().color = newcolor;
        newcolor = Color.white;
        newcolor.a = 1;
        cancel.GetComponent<Image>().color = newcolor;
    }
}
