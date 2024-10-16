using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("BGM����������")]
    public Slider mySlider1;
    float sliderValue1;
    [Header("��Ч����������")]
    public Slider mySlider2;
    float sliderValue2;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        mySlider1.value = SoundManager.BGMvolume;
        mySlider2.value = SoundManager.Soundvolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (mySlider1 != null)
        {
            // ��ȡ�����ĵ�ǰֵ
            sliderValue1 = mySlider1.value;

            // ʹ�û�����ֵ������������
            SoundManager.BGMvolume = sliderValue1;
        }
        if (mySlider2 != null)
        {
            // ��ȡ�����ĵ�ǰֵ
            sliderValue2 = mySlider2.value;

            // ʹ�û�����ֵ������������
            SoundManager.Soundvolume = sliderValue2;
        }
    }
}
