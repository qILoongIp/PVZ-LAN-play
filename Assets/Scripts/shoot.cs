using UnityEngine;

public class shoot : MonoBehaviour
{
    [Header("�ӵ�")]
    public GameObject bullet;
    [Header("������ʱ��")]
    public float interval;
    [Header("�ӵ�����λ��")]
    public Transform bullet_position;
    Animator animator;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval && this.transform.parent != null)
        {
            animator.SetBool("shoot",true);
            //time = 0;
        }
    }
    public void OnLastFrame()
    {
        animator.SetBool("shoot",false);
        time = 0;
    }
    public void ShootBullet()
    {
        Instantiate(bullet, bullet_position.position, Quaternion.identity);
    }
}
