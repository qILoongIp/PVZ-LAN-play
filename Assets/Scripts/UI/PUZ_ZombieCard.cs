using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PUZ_ZombieCard : MonoBehaviour
{
    //[Header("²»¿ÉÓÃ×´Ì¬")]
    private GameObject Unavailable;
    //[Header("Î´×¼±¸ºÃ×´Ì¬")]
    private GameObject Unprepared;
    private GameObject CardPanel;//¸¸¿¨Æ¬À¸
    private GameObject CardPool;//¸¸¿¨³Ø
    // Start is called before the first frame update
    void Start()
    {
        CardPanel = GameObject.FindWithTag("ZombieCardPanel");
        CardPool = GameObject.FindWithTag("ZombieCardPool");
        Unavailable = transform.Find("Unavailable").gameObject;//GameObject.FindGameObjectWithTag("Unavailable");
        Unprepared = transform.Find("Unprepared").gameObject;//GameObject.FindGameObjectWithTag("Unprepared");
        Unprepared.GetComponent<Image>().fillAmount = 0;
        Color newcolor = Color.white;
        newcolor.a = 0;
        Unavailable.GetComponent<Image>().color = newcolor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(BaseEventData data)
    {
        if(this.transform.parent.gameObject == CardPool)
        {
            if (CardPanel.transform.childCount < 10)
            {
                this.transform.SetParent(CardPanel.transform);
                SoundManager.Instance.PlaySound(SoundManager.Sounds.choose, true);
            }
        }
        else if(this.transform.parent.gameObject == CardPanel)
        {
            this.transform.SetParent(CardPool.transform);
            SoundManager.Instance.PlaySound(SoundManager.Sounds.choose, true);
        }
    }
}
