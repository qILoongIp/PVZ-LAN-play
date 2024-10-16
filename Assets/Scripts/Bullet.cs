using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子弹飞行速度")]
    public float speed;
    [Header("子弹消失时间")]
    public float Destroytime;
    [Header("伤害")]
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
