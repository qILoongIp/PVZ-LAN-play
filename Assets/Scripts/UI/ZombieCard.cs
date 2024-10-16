using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ZombieCard : MonoBehaviour
{
    [Header("坐标修正")]
    public Vector3 rvector = new Vector3(0,0,0);
    //[Header("不可用状态")]
    private GameObject Unavailable;
    //[Header("未准备好状态")]
    private GameObject Unprepared;
    [Header("预览图")]
    public GameObject previewimage;
    [Header("预制体")]
    public GameObject gameobjectprefab;
    [Header("冷却时间")]
    public float CD;
    [Header("消耗脑子")]
    public int costbrain;
    [Header("主摄像机")]
    public Camera mainCamera; // 在 Inspector 视图中分配主摄像机
    [Header("可否重叠放置")]
    public bool overlap = true;
    private float time;//计时器
    private GameObject currentgameobject;//当前选中的植物或僵尸
    // Start is called before the first frame update
    void Start()
    {
        Unavailable = transform.Find("Unavailable").gameObject;//GameObject.FindGameObjectWithTag("Unavailable");
        Unprepared = transform.Find("Unprepared").gameObject;//GameObject.FindGameObjectWithTag("Unprepared");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBG();
    }
    //冷却进度条
    void UpdateProgress()
    {
        float per = Mathf.Clamp(time/CD,0,1);
        Unprepared.GetComponent<Image>().fillAmount = 1 - per;
    }
    //是否处于激活状态
    void UpdateDarkBG()
    {
        if (GameManager.Instance.brainNum >= costbrain)
        {
            Color newcolor = Color.white;
            newcolor.a = 0;
            Unavailable.GetComponent<Image>().color = newcolor;
        }
        else
        {
            Color newcolor = Color.white;
            newcolor.a = 0.8f;
            Unavailable.GetComponent<Image>().color = newcolor;
        }
    }
    public void OnBeginDrag(BaseEventData data)
    {
        //无法拖拽的情况
        if (Unavailable.GetComponent<Image>().color.a != 0 || Unprepared.GetComponent<Image>().fillAmount != 0)
        {
            //buzzer.GetComponent<AudioSource>().Play();
            SoundManager.Instance.PlaySound(SoundManager.Sounds.buzzer,true);
            return;
        }
        //seedlift.GetComponent<AudioSource>().Play();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.seedlift, true);
        PointerEventData pointerEventData = data as PointerEventData;
        currentgameobject = Instantiate(previewimage);
        LayerManager.Instance.AddLayer(3, currentgameobject);
        currentgameobject.transform.position = ConvertMouseToWorld(pointerEventData.position) + rvector;
    }
    public void OnDrag(BaseEventData data) 
    { 
        if(currentgameobject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        currentgameobject.transform.position = ConvertMouseToWorld(pointerEventData.position) + rvector;
        Collider2D[] col = Physics2D.OverlapPointAll(ConvertMouseToWorld(pointerEventData.position) + rvector);
        foreach (Collider2D d in col)
        {
            if (d.transform.childCount != 0)
            { 
                Transform c = d.transform.GetChild(0);
                //鼠标所在十字高亮
                if (c.tag == "Land")
                {
                    GameObject[] box = GameObject.FindGameObjectsWithTag("Land");
                    foreach (GameObject onebox in box)
                    {
                        if (onebox.transform.position.x == c.position.x || onebox.transform.position.y == c.position.y)
                        {
                            Color color = Color.white;
                            color.a = 0.5f;
                            onebox.GetComponent<SpriteRenderer>().color = color;
                        }
                        //结束高亮
                        else
                        {
                            Color color = Color.white;
                            color.a = 0;
                            onebox.GetComponent<SpriteRenderer>().color = color;
                        }
                    }
                }
            }
        }
    }
    public void OnEndDrag(BaseEventData data)
    {
        if (currentgameobject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        Collider2D[] col = Physics2D.OverlapPointAll(ConvertMouseToWorld(pointerEventData.position) + rvector);
        GameObject[] box = GameObject.FindGameObjectsWithTag("Land");
        foreach (Collider2D d in col)
        {
            if(d.transform.childCount != 0)
            {
                Transform c = d.transform.GetChild(0);
                if (c.tag == "Land")
                {
                    if(overlap == false)
                    {
                        // 获取父游戏对象的所有子对象
                        Transform[] children = c.GetComponentsInChildren<Transform>();

                        // 遍历所有子对象，查找具有标签 "tomb" 的子对象
                        foreach (Transform child in children)
                        {
                            if (child.CompareTag("tomb"))
                            {
                                Destroy(currentgameobject);
                                //结束高亮
                                foreach (GameObject onebox in box)
                                {
                                    Color color = Color.white;
                                    color.a = 0;
                                    onebox.GetComponent<SpriteRenderer>().color = color;
                                }
                                return;
                            }
                        }
                    }
                    if (c.parent.transform.name == "Box6" || c.parent.transform.name == "Box7" || c.parent.transform.name == "Box8")
                    {
                        GameObject.Destroy(currentgameobject);
                        currentgameobject = Instantiate(gameobjectprefab);
                        LayerManager.Instance.AddLayer(2, currentgameobject);
                        currentgameobject.transform.parent = c.transform;
                        currentgameobject.transform.localPosition = Vector3.zero + rvector;
                        SoundManager.Instance.PlaySound(SoundManager.Sounds.plant, true);
                        currentgameobject = null;
                        //结束高亮
                        foreach (GameObject onebox in box)
                        {
                            Color color = Color.white;
                            color.a = 0;
                            onebox.GetComponent<SpriteRenderer>().color = color;
                        }
                        time = 0;
                        GameManager.Instance.ChangeBrainNum(-costbrain);
                        break;
                    }
                }
            }
        }
        if(currentgameobject != null)
        {
            //结束高亮
            foreach (GameObject onebox in box)
            {
                Color color = Color.white;
                color.a = 0;
                onebox.GetComponent<SpriteRenderer>().color = color;
            }
            GameObject.Destroy(currentgameobject);
            currentgameobject = null;
        }
    }
    // 工具函数：将鼠标坐标转换为世界坐标
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
}
