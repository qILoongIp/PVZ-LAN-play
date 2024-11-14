using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class InitManager : NetworkBehaviour
{
    public static GameManager Instance;
    [Header("主摄像机")]
    public Camera mainCamera;

    public TMP_InputField ipAddressInputField;
    public TMP_InputField portAddressInputField;
    // Start is called before the first frame update
    void Start()
    {
        string localIP = GetLocalIPAddress();
        ipAddressInputField.text = localIP;
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
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            Debug.Log("Server Started");
            // 服务器启动后加载场景
            LoadScene("Day");
        };
    }

    string GetLocalIPAddress()
    {
        string localIP = string.Empty;
        try
        {
            // 获取所有的网络接口（本地网络适配器）
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                // 判断是 IPv4 地址
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;  // 找到第一个 IPv4 地址就返回
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("获取本机 IP 地址时发生错误: " + ex.Message);
        }

        return localIP;
    }

    public void Update()
    {
        //Debug.Log("游戏对象："+GameObject.FindWithTag("DayBG"));
    }

    public void HostButton()
    {
        string ipAddress = ipAddressInputField.text;
        SetNetworkAddress(ipAddress); //设置网络地址
        int port = GetPortFromInputField();
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = (ushort)port;
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host.");
        }
        else
        {
            Debug.Log("Host fail.");
        }
        Debug.Log("IsClient" + this.IsClient);
        Debug.Log("IsOwner" + this.IsOwner);
        //var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        //Debug.Log("IP" + transport.ConnectionData.Port);
    }
    public void JoinButton()
    {
        string ipAddress = ipAddressInputField.text;
        SetNetworkAddress(ipAddress); //设置网络地址
        int port = GetPortFromInputField();
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = (ushort)port;
        if (NetworkManager.Singleton.IsClient)
        {
            try
            {
                NetworkManager.Singleton.Shutdown();
                Debug.Log("已关闭现有客户端连接。");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to shut down existing client: " + e.Message);
            }
        }
        try
        {
            bool success = NetworkManager.Singleton.StartClient();
            if(success)
            {
                Debug.Log("Client started successfully.");
            }
            else
            {
                Debug.LogError("Client failed to start.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error starting client: " + e.Message);
        }
        //Debug.Log("IsClient" + this.IsClient);
        //Debug.Log("IsOwner" + this.IsOwner);
        //var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        //Debug.Log("IP" + transport.ConnectionData.Port);
    }

    int GetPortFromInputField()
    {
        // 尝试将输入框中的文本转换为整数
        if (int.TryParse(portAddressInputField.text, out int port))
        {
            // 如果转换成功，返回该端口
            return port;
        }
        else
        {
            // 如果转换失败，返回默认端口
            Debug.LogWarning("Invalid port entered. Using default port 7777.");
            return 7777; // 默认端口
        }
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
