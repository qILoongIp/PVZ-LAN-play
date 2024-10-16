using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("行走方向")]
    public Vector3 direction = new Vector3(-1, 0, 0);
    [Header("行走速度")]
    public float speed = 1;
    [Header("伤害")]
    public float damage;
    [Header("攻击间隔")]
    public float damageInterval = 0.5f;
    [Header("生命值ֵ")]
    public float health;
    [Header("被击中的音效")]
    public Attacked_Sound attackedSound = Attacked_Sound.splat;
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    private float damageTimer;//伤害计时器
    private bool isWalk;//判断是否行走
    private Animator animator;
    private GameObject chomp;//吃
    private GameObject gulp;//吃掉
    private GameObject splat;//被击中
    private GameObject full;//倒地
    private bool ispress = false;
    public enum Attacked_Sound
    {
        splat = 0,
        splat2
    }
    public enum Attacked_type
    {
        normal,//正常死
        Bomb,//被炸死
        press//被压死
    }
    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (ispress)
        {
            if(transform.localScale.y <= 0.1)
            {
                Die();
            }
            else
            {
                transform.localScale -= new Vector3(0,0.02f,0);
                transform.position -= new Vector3(0, GetComponent<BoxCollider2D>().bounds.size.y * 0.02f / 2, 0);
            }
        }
    }
    private void Move()
    {
        if (isWalk)
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Plant")
        {
            isWalk=false;
            //chomp.GetComponent<AudioSource>().Play();
            SoundManager.Instance.PlaySound(SoundManager.Sounds.chomp, true);
            animator.SetBool("eat", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Plant")
        {
            isWalk = false;
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                Plant plant = collision.GetComponent<Plant>();
                float newplantHealth = plant.ChangeHealth(-damage);
                if (newplantHealth <= 0)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.gulp, true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Plant")
        {
            SoundManager.Instance.PlaySound(SoundManager.Sounds.chomp,false);
            //chomp.GetComponent<AudioSource>().Stop();
            animator.SetBool("eat", false);
            if(currentHealth > 0)
            {
                isWalk = true;
            }
        }
    }
    public float ChangeHealth(float num,Attacked_type attacked_Type = Attacked_type.normal)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if(transform.name == "Zombie_Target") animator.SetTrigger("Attack");
        if (attackedSound == Attacked_Sound.splat)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sounds.splat, true);
        }
        else if(attackedSound == Attacked_Sound.splat2)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sounds.splat2, true);
        }
        if (currentHealth <= 0)
        {
            if (transform.name == "Zombie_Target") Die();
            else
            {
                if (attacked_Type == Attacked_type.normal)
                {
                    isWalk = false;
                    animator.SetTrigger("die");
                    //chomp.GetComponent<AudioSource>().Stop();
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.chomp, false);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (attacked_Type == Attacked_type.Bomb)
                {
                    isWalk = false;
                    animator.SetTrigger("bomb");
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.chomp, false);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (attacked_Type == Attacked_type.press)
                {
                    ispress = true;
                }
            }
        }
        return currentHealth;
    }
    public void Die()
    {
        animator.enabled = false;
        GameObject.Destroy(gameObject);
    }
    public void Full()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.full, true);
    }
}
