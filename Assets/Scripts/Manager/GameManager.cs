using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject ReadySetPlant;
    public List<bool> bools = new List<bool>();
    private GameObject AllCards;
    public bool isStart;
    private GameObject PlantCardPanel;
    private GameObject ZombieCardPanel;
    private bool flag;//������Ϊִֻ��һ�εĴ���ı�־
    // Start is called before the first frame update
    [Header("�����ʼ����")]
    public int sunNum = 50;
    [Header("���ӳ�ʼ����")]
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
        if (bools.Count >= 2 && AllCards.activeSelf)
        {
            isStart = true;
            AllCards.SetActive(false);
            if (PlantCardPanel.transform.childCount == 0 && ZombieCardPanel.transform.childCount == 0 && !AllCards.activeSelf)
            {
                //GetComponent<AudioSource>().Stop();
                SoundManager.Instance.PlayBGMs(SoundManager.BGMs.ChooseYourPlant, false);
                Debug.Log("�������Ǻ�ƽ��һ�죡");
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
