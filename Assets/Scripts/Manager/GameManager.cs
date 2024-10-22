using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public GameObject ReadySetPlant;
    public NetworkVariable<bool> plantReady = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> zombieReady = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private GameObject AllCards;
    public bool isStart;
    private GameObject PlantCardPanel;
    private GameObject ZombieCardPanel;
    private bool flag;//用于作为只执行一次的代码的标志
    // Start is called before the first frame update
    [Header("阳光初始数量")]
    public int sunNum = 50;
    [Header("脑子初始数量")]
    public int brainNum = 50;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        isStart = false;
        AllCards = GameObject.FindWithTag("AllCards");
        PlantCardPanel = GameObject.FindWithTag("PlantCardPanel");
        ZombieCardPanel = GameObject.FindWithTag("ZombieCardPanel");
        flag = true;
    }
    void Start()
    {
        UIManager.instance.InitUI();
        SoundManager.Instance.PlayBGMs(SoundManager.BGMs.ChooseYourPlant, true);
    }
    // Update is called once per frame
    void Update()
    {
        if (plantReady.Value && zombieReady.Value && AllCards.activeSelf)
        {
            isStart = true;
            AllCards.SetActive(false);
            if (PlantCardPanel.transform.childCount == 0 && ZombieCardPanel.transform.childCount == 0 && !AllCards.activeSelf)
            {
                //GetComponent<AudioSource>().Stop();
                SoundManager.Instance.PlayBGMs(SoundManager.BGMs.ChooseYourPlant, false);
                Debug.Log("今天又是和平的一天！");
                flag = false;
            }
            else if (flag && (PlantCardPanel.transform.childCount != 0 || ZombieCardPanel.transform.childCount != 0) && !AllCards.activeSelf)
            {
                SoundManager.Instance.PlayBGMs(SoundManager.BGMs.ChooseYourPlant, false);
                Instantiate(ReadySetPlant);
                flag = false;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlantReadyServerRpc(bool ready)
    {
        plantReady.Value = ready;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetZombieReadyServerRpc(bool ready)
    {
        zombieReady.Value = ready;
    }

    public void ChangeSunNum(int changeNum)
    {
        sunNum += changeNum;
        if(sunNum <= 0)
        {
            sunNum = 0;
        }
        UIManager.instance.UpdateUI();
    }
    public void ChangeBrainNum(int changeNum)
    {
        brainNum += changeNum;
        if (brainNum <= 0)
        {
            brainNum = 0;
        }
        UIManager.instance.UpdateUI();
    }
}
