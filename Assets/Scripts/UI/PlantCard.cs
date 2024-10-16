using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlantCard : MonoBehaviour
{
    [Header("��������")]
    public Vector3 rvector = new Vector3(0,0,0);
    //[Header("������״̬")]
    private GameObject Unavailable;
    //[Header("δ׼����״̬")]
    private GameObject Unprepared;
    [Header("Ԥ��ͼ")]
    public GameObject previewimage;
    [Header("Ԥ����")]
    public GameObject gameobjectprefab;
    [Header("��ȴʱ��")]
    public float CD;
    [Header("����̫��")]
    public int costsun;
    [Header("�������")]
    public Camera mainCamera; // �� Inspector ��ͼ�з����������
    private float time;//��ʱ��
    private GameObject currentgameobject;//��ǰѡ�е�ֲ���ʬ
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
    //��ȴ������
    void UpdateProgress()
    {
        float per = Mathf.Clamp(time/CD,0,1);
        Unprepared.GetComponent<Image>().fillAmount = 1 - per;
    }
    //�Ƿ��ڼ���״̬
    void UpdateDarkBG()
    {
        if (GameManager.Instance.sunNum >= costsun)
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
        //�޷���ק�����
        if(Unavailable.GetComponent<Image>().color.a != 0 || Unprepared.GetComponent<Image>().fillAmount != 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sounds.buzzer, true);
            return;
        }
        SoundManager.Instance.PlaySound(SoundManager.Sounds.seedlift, true);
        PointerEventData pointerEventData = data as PointerEventData;
        currentgameobject = Instantiate(previewimage);
        LayerManager.Instance.AddLayer(1, currentgameobject);
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
                //�������ʮ�ָ���
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
                        //��������
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
            if (d.transform.childCount != 0) 
            { 
                Transform c = d.transform.GetChild(0);
                if (c.tag == "Land" && c.transform.childCount == 0)
                {
                    if (c.parent.transform.name == "Box0" || c.parent.transform.name == "Box1" || c.parent.transform.name == "Box2" || c.parent.transform.name == "Box3" || c.parent.transform.name == "Box4" || c.parent.transform.name == "Box5")
                    {
                        GameObject.Destroy(currentgameobject);
                        currentgameobject = Instantiate(gameobjectprefab);
                        LayerManager.Instance.AddLayer(0, currentgameobject);
                        currentgameobject.transform.parent = c.transform;
                        currentgameobject.transform.localPosition = Vector3.zero + rvector;
                        SoundManager.Instance.PlaySound(SoundManager.Sounds.plant, true);
                        currentgameobject = null;
                        //��������
                        foreach (GameObject onebox in box)
                        {
                            Color color = Color.white;
                            color.a = 0;
                            onebox.GetComponent<SpriteRenderer>().color = color;
                        }
                        time = 0;
                        GameManager.Instance.ChangeSunNum(-costsun);
                        break;
                    }
                }
            }
        }
        if(currentgameobject != null)
        {
            //��������
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
    // ���ߺ��������������ת��Ϊ��������
    Vector3 ConvertMouseToWorld(Vector3 mousePosition)
    {
        if (mainCamera != null)
        {
            // ʹ������������������ת��Ϊ��������
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f)); // �����������Ǿ���������ľ���
            return worldPosition;
        }
        else
        {
            Debug.LogError("Main Camera is not assigned!");
            return Vector3.zero; // ���û���������������Ĭ�ϵ�Vector3.zero
        }
    }
}
