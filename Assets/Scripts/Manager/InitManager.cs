using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitManager : NetworkBehaviour
{
    public static GameManager Instance;
    [Header("主摄像机")]
    public Camera mainCamera;

    public TMP_InputField ipAddressInputField;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            Debug.Log("A new client connected,id=" + id);
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            Debug.Log("A new client Disconnect,id=" +id);
        };

        NetworkManager.Singleton.OnServerStarted += () =>
        {
            Debug.Log("Server Started");
        };
        
    }

    public void Update()
    {
        //Debug.Log("游戏对象："+GameObject.FindWithTag("DayBG"));
    }

    public void HostButton()
    {
        string ipAddress = ipAddressInputField.text;
        //SetNetworkAddress(ipAddress); //设置网络地址
        if(NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host.");
        }
        else
        {
            Debug.Log("Host fail.");
        }
        LoadScene("Day");
        Debug.Log("IsClient" + this.IsClient);
        Debug.Log("IsOwner" + this.IsOwner);
        //var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        //Debug.Log("IP" + transport.ConnectionData.Port);
    }
    public void JoinButton()
    {
        string ipAddress = ipAddressInputField.text;
        //SetNetworkAddress(ipAddress); //设置网络地址
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client.");
        }
        else
        {
            Debug.Log("Client fail.");
        }
        //Debug.Log("IsClient" + this.IsClient);
        //Debug.Log("IsOwner" + this.IsOwner);
        //var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        //Debug.Log("IP" + transport.ConnectionData.Port);
    }

    private void SetNetworkAddress(string ipAddress)
    {
        var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        if(transport != null)
        {
            transport.ConnectionData.Address = ipAddress;
            Debug.Log("Connecting to IP:" + ipAddress);
        }
    }

    public void LoadScene(string sceneName)
    {
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
