using UnityEngine;

public class PotatoMine : MonoBehaviour
{
    [Header("准备时间")]
    public float waittime = 15;
    [Header("爆炸伤害")]
    public float damage = 1800;
    Animator animator;
    float time;
    bool isReady;
    bool isBomb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = 0;
        isReady = false;
        isBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= waittime)
        {
            animator.SetTrigger("rise");
        }
    }
    public void OnRiseLastFrame()
    {
        animator.SetTrigger("normal");
        isReady = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && isReady && isBomb)
        {
            isBomb = false;
            animator.SetTrigger("bomb");
            collision.gameObject.GetComponent<Zombie>().ChangeHealth(-damage, Zombie.Attacked_type.Bomb);
            SoundManager.Instance.PlaySound(SoundManager.Sounds.potato_mine, true);
        }
    }
    public void OnBombLastFrame()
    {
        animator.enabled = false;
        GameObject.Destroy(gameObject);
    }
}
