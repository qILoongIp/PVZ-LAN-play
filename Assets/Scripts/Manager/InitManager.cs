using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InitManager : NetworkBehaviour
{
    public GameObject cardpanel;
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
        cardpanel = GameObject.FindWithTag("PlantCardPanel");
        if(this.IsClient && this.IsOwner)
        {
            Debug.Log("IsClient" + this.IsClient);
            Debug.Log("IsOwner" + this.IsOwner);
            foreach(Collider2D d in col)
            {

            }

        }
        else
        {
               
        }
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
    Vector3 ConvertMouseToWorld(Vector3 mousePosition)
    {
        if (mainCamera != null)
        {
            // 使用主摄像机将鼠标坐标转换为世界坐标
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f)); // 第三个参数是距离摄像机的距离
            return worldPosition;
        }
        else
        {
            Debug.LogError("Main Camera is not assigned!");
            return Vector3.zero; // 如果没有主摄像机，返回默认的Vector3.zero
        }
    }

    public void LoadScene(string sceneName)
    {
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
