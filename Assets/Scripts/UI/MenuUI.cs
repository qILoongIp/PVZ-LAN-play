using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("BGM的音量滑块")]
    public Slider mySlider1;
    float sliderValue1;
    [Header("音效的音量滑块")]
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
            // 获取滑条的当前值
            sliderValue1 = mySlider1.value;

            // 使用滑条的值进行其他操作
            SoundManager.BGMvolume = sliderValue1;
        }
        if (mySlider2 != null)
        {
            // 获取滑条的当前值
            sliderValue2 = mySlider2.value;

            // 使用滑条的值进行其他操作
            SoundManager.Soundvolume = sliderValue2;
        }
    }
}
