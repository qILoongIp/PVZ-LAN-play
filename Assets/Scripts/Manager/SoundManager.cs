using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public static float BGMvolume = 1;
    public static float Soundvolume = 1;
    private float OldBGMvolume;
    private float OldSoundvolume;
    List<GameObject> BGMGameObjects = new List<GameObject>();
    List<GameObject> SoundGameObjects = new List<GameObject>();
    string[] BGMEumeNames;
    string[] SoundEumeNames;
    public enum BGMs
    {
        ChooseYourPlant = 0,//选择植物界面的BGM
        day,//白天草坪的BGM
    }
    public enum Sounds
    {
        seedlift = 0,//拖拽卡片的音效
        plant,//种植的音效
        buzzer,//卡片未准备好不能拖拽的音效
        choose,//选卡界面选卡的音效
        chomp,//僵尸吃植物的音效
        gulp,//僵尸吃掉植物的音效
        splat,//豌豆打中僵尸的音效
        full,//僵尸倒地的音效
        getbrain,//获得脑子的音效
        getsun,//获得阳光的音效
        readysetplant,//准备种植的音效
        splat2,//击中钢制物品的音效
        potato_mine,//土豆地雷爆炸的音效
        Imp,//小鬼出场的音效
        squash_hmm,//倭瓜向右看的音效
        Squash_hmm2,//倭瓜向左看的音效
        gargantuar_thump//砸扁的音效
    }
    private void Awake()
    {
        Instance = this;
        OldBGMvolume = BGMvolume;
        OldSoundvolume = Soundvolume;
        BGMEumeNames = Enum.GetNames(typeof(BGMs));
        SoundEumeNames = Enum.GetNames(typeof(Sounds));
        for (int i = 0; i < 100; i++)
        {
            BGMGameObjects.Add(null);
            SoundGameObjects.Add(null);
        }
        for (int i = 0; i < GameObject.Find("BGMs").transform.childCount; i++)
        {
            foreach (string name in BGMEumeNames)
            {
                if(GameObject.Find("BGMs").transform.GetChild(i).name == name)
                {
                    BGMGameObjects[(int)Enum.Parse<BGMs>(name)] = GameObject.Find("BGMs").transform.GetChild(i).gameObject;
                }
            }
        }
        for (int i = 0;i < GameObject.Find("Sounds").transform.childCount; i++)
        {
            foreach (string name in SoundEumeNames)
            {
                if (GameObject.Find("Sounds").transform.GetChild(i).name == name)
                {
                    SoundGameObjects[(int)Enum.Parse<Sounds>(name)] = GameObject.Find("Sounds").transform.GetChild(i).gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BGMvolume != OldBGMvolume)
        {
            foreach(GameObject bgmgameobject in BGMGameObjects)
            {
                if(bgmgameobject != null)
                {
                    if(bgmgameobject.GetComponent<AudioSource>().isPlaying)
                    {
                        bgmgameobject.GetComponent<AudioSource>().volume = BGMvolume;
                    }
                }
            }
            OldBGMvolume = BGMvolume;
        }
        if(Soundvolume != OldSoundvolume)
        {
            foreach (GameObject soundgameobject in SoundGameObjects)
            {
                if(soundgameobject != null)
                {
                    if (soundgameobject.GetComponent<AudioSource>().isPlaying)
                    {
                        soundgameobject.GetComponent<AudioSource>().volume = Soundvolume;
                    }
                }
            }
            OldSoundvolume = Soundvolume;
        }
    }
    public void PlayBGMs(BGMs bgmname,bool isPlay)
    {
        switch(bgmname)
        {
            case BGMs.ChooseYourPlant://选择植物界面的BGM
                if (isPlay)
                {
                    BGMGameObjects[(int)BGMs.ChooseYourPlant].GetComponent<AudioSource>().Play();
                    BGMGameObjects[(int)BGMs.ChooseYourPlant].GetComponent<AudioSource>().volume = BGMvolume;
                }
                else
                {
                    BGMGameObjects[(int)BGMs.ChooseYourPlant].GetComponent<AudioSource>().Stop();
                }
                break;
            case BGMs.day://白天草坪的BGM
                if(isPlay)
                {
                    BGMGameObjects[(int)BGMs.day].GetComponent<AudioSource>().Play();
                    BGMGameObjects[(int)BGMs.day].GetComponent<AudioSource>().volume = BGMvolume;
                }
                else
                {
                    BGMGameObjects[(int)BGMs.day].GetComponent<AudioSource>().Stop();
                }
                break;
        }
    }
    public void PlaySound(Sounds soundname,bool isPlay)
    {
        switch (soundname)
        {
            case Sounds.seedlift://拖拽卡片的音效
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.seedlift].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.seedlift].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.seedlift].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.plant://种植的音效
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.plant].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.plant].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.plant].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.buzzer://卡片未准备好不能拖拽的音效
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.buzzer].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.buzzer].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.buzzer].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.choose://选卡界面选卡的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.choose].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.choose].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.choose].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.chomp://僵尸吃植物的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.chomp].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.chomp].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.chomp].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.gulp://僵尸吃掉植物的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.gulp].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.gulp].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.gulp].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.splat://豌豆打中僵尸的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.splat].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.splat].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.splat].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.full://僵尸倒地的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.full].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.full].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.full].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.getbrain://获得脑子的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.getbrain].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.getbrain].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.getbrain].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.getsun://获得阳光的音效
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.getsun].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.getsun].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.getsun].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.readysetplant:
                if(isPlay)
                {
                    SoundGameObjects[(int)Sounds.readysetplant].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.readysetplant].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.readysetplant].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.splat2:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.splat2].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.splat2].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.splat2].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.potato_mine:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.potato_mine].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.potato_mine].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.potato_mine].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.Imp:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.Imp].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.Imp].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.Imp].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.squash_hmm:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.squash_hmm].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.squash_hmm].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.squash_hmm].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.Squash_hmm2:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.Squash_hmm2].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.Squash_hmm2].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.Squash_hmm2].GetComponent<AudioSource>().Stop();
                }
                break;
            case Sounds.gargantuar_thump:
                if (isPlay)
                {
                    SoundGameObjects[(int)Sounds.gargantuar_thump].GetComponent<AudioSource>().Play();
                    SoundGameObjects[(int)Sounds.gargantuar_thump].GetComponent<AudioSource>().volume = Soundvolume;
                }
                else
                {
                    SoundGameObjects[(int)Sounds.gargantuar_thump].GetComponent<AudioSource>().Stop();
                }
                break;
        }
    }
}
