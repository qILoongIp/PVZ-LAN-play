using UnityEngine;

public class brain : MonoBehaviour
{
    [Header("消失时间")]
    public float time;
    [Header("抛物线对称轴")]
    public float Axis;
    [Header("抛物线顶点")]
    public float Top;
    [Header("抛物线速度")]
    public float Speed;
    [Header("收集后的位置")]
    public Vector3 endposition;
    private float x, y, oldx, oldy;//x,y坐标
    [Header("移动时间")]
    public float moveTime = 1.0f; // 移动所需的总时间

    private Vector3 initialPosition; // 初始位置
    private float startTime; // 开始移动的时间

    private bool isMoving = false; // 是否正在移动
    private bool flag;//只能点击一次的标志
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
        // 检查是否正在移动
        if (isMoving)
        {
            // 计算已经过去的时间
            float elapsedTime = Time.time - startTime;

            // 计算插值比例（0到1之间）
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            // 使用线性插值计算新的位置
            transform.position = Vector3.Lerp(initialPosition, endposition, t);

            // 如果已经到达目标位置，停止移动
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
