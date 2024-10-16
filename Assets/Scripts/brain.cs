using UnityEngine;

public class brain : MonoBehaviour
{
    [Header("��ʧʱ��")]
    public float time;
    [Header("�����߶Գ���")]
    public float Axis;
    [Header("�����߶���")]
    public float Top;
    [Header("�������ٶ�")]
    public float Speed;
    [Header("�ռ����λ��")]
    public Vector3 endposition;
    private float x, y, oldx, oldy;//x,y����
    [Header("�ƶ�ʱ��")]
    public float moveTime = 1.0f; // �ƶ��������ʱ��

    private Vector3 initialPosition; // ��ʼλ��
    private float startTime; // ��ʼ�ƶ���ʱ��

    private bool isMoving = false; // �Ƿ������ƶ�
    private bool flag;//ֻ�ܵ��һ�εı�־
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
        x = 0;
        y = 0;
        oldx = 0;
        oldy = 0;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldx >= 0 && oldy >= 0 && isMoving == false)
        {
            x += Speed * Time.deltaTime;
            y = -((x - Axis) * (x - Axis)) + Top;
            Vector3 vector = new Vector3(x, y, 0) - new Vector3(oldx, oldy, 0);
            this.transform.Translate(vector);
            oldx = x;
            oldy = y;
        }
        // ����Ƿ������ƶ�
        if (isMoving)
        {
            // �����Ѿ���ȥ��ʱ��
            float elapsedTime = Time.time - startTime;

            // �����ֵ������0��1֮�䣩
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            // ʹ�����Բ�ֵ�����µ�λ��
            transform.position = Vector3.Lerp(initialPosition, endposition, t);

            // ����Ѿ�����Ŀ��λ�ã�ֹͣ�ƶ�
            if (t >= 1.0f)
            {
                isMoving = false;
                Destroy(gameObject);
            }
        }
    }
    private void OnMouseDown()
    {
        if(flag)
        {
            GameManager.Instance.ChangeBrainNum(25);
            SoundManager.Instance.PlaySound(SoundManager.Sounds.getbrain, true);
            startTime = Time.time;
            initialPosition = transform.position;
            isMoving = true;
            flag = false;
        }
    }
}
