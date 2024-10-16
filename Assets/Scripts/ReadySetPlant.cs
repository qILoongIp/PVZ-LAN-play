using UnityEngine;

public class ReadySetPlant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<AudioSource>().Play();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.readysetplant,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLastFrame()
    {
        //background.GetComponent<AudioSource>().Play();
        SoundManager.Instance.PlayBGMs(SoundManager.BGMs.day, true);
        Destroy(gameObject);
    }
}
