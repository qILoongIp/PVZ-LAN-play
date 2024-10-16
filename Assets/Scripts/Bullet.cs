using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("�ӵ������ٶ�")]
    public float speed;
    [Header("�ӵ���ʧʱ��")]
    public float Destroytime;
    [Header("�˺�")]
    public float damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie" || collision.tag == "tomb" || collision.tag == "Zombie_Target")
        {
            GameObject.Destroy(gameObject);
            collision.GetComponent<Zombie>().ChangeHealth(-damage);
        }
    }
}
