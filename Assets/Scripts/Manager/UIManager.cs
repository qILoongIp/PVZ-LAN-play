using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("阳光字段对象")]
    public TMP_Text sunNumText;
    [Header("脑子字段对象")]
    public TMP_Text brainNumText;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitUI()
    {
        sunNumText.text = GameManager.Instance.sunNum.ToString();
        brainNumText.text = GameManager.Instance.brainNum.ToString();
    }
    public void UpdateUI()
    {
        sunNumText.text = GameManager.Instance.sunNum.ToString();
        brainNumText.text = GameManager.Instance.brainNum.ToString();
    }
}
