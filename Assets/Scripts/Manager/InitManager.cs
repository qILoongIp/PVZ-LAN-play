using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InitManager : NetworkBehaviour
{
    public static GameManager Instance;
    [Header("主摄像机")]
    public Camera mainCamera;
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
        var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        Debug.Log("IP" + transport.ConnectionData.Port);
    }
    public void JoinButton()
    {
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

    public void LoadScene(string sceneName)
    {
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
