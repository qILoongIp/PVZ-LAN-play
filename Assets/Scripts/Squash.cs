using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : MonoBehaviour
{
    [Header("伤害")]
    public float damage;
    //[Header("抛物线对称轴")]
    private float Axis;
    [Header("抛物线顶点")]
    public float Top;
    [Header("抛物线速度")]
    public float Speed;
    private GameObject parant;
    private List<GameObject> Zombies = new List<GameObject>();
    private float old_direction = 100f;
    private GameObject targetzombie;
    private Animator animator;
    bool attack = true;
    bool isjump = false;
    bool isattacked = true;
    float time;
    private float x, y, oldx, oldy;//x,y坐标
    // Start is called before the first frame update
    void Start()
    {
        parant = transform.parent.parent.parent.gameObject;
        animator = GetComponent<Animator>();
        x = 0;
        y = 0;
        oldx = 0;
        oldy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldy >= 0 && isjump)
        {
            Axis = ((targetzombie.GetComponent<BoxCollider2D>().offset.x + targetzombie.transform.position.x) - (this.transform.position.x + this.GetComponent<BoxCollider2D>().offset.x)) / 2;
            if(Axis >= 0)
            {
                x += Speed * Time.deltaTime;
            }
            else
            {
                x -= Speed * Time.deltaTime;
            }
            y = 15 * time - 20 * time * time;//-((x - Axis) * (x - Axis)) + Top;
            time += Time.deltaTime;
            Vector3 vector = new Vector3(x, y, 0) - new Vector3(oldx, oldy, 0);
            this.transform.Translate(vector);
            oldx = x;
            oldy = y;
            //Debug.Log("x:" + x + ",y:" + y + ",oldx:" + oldx + ",oldy:" +oldy+ ",Axis:" + Axis);
        }
        for (int i = 0;i < parant.transform.childCount;i++)
        {
            for(int j = 0;j < parant.transform.GetChild(i).GetChild(0).childCount; j++)
            {
                if(parant.transform.GetChild(i).GetChild(0).GetChild(j).tag == "Zombie")
                {
                    Zombies.Add(parant.transform.GetChild(i).GetChild(0).GetChild(j).gameObject);
                }
            }
        }
        if(Zombies.Count > 0 && attack)
        {
            foreach(GameObject zom in Zombies)
            {
                float direction = (zom.GetComponent<BoxCollider2D>().offset.x+zom.transform.position.x) - (this.transform.position.x+this.GetComponent<BoxCollider2D>().offset.x);
                if(Mathf.Abs(direction) < Mathf.Abs(old_direction))
                {
                    old_direction = direction;
                    targetzombie = zom;
                    //Debug.Log(old_direction);
                }
            }
            if (old_direction >= 0 && old_direction <= 2.4f && attack)
            {
                attack = false;
                SoundManager.Instance.PlaySound(SoundManager.Sounds.squash_hmm, true);
                animator.SetTrigger("right");
            }
            else if (old_direction < 0 && old_direction >= -2.4f && attack)
            {
                attack = false;
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Squash_hmm2, true);
                animator.SetTrigger("left");
            }
        }
    }
    public void OnLookLeftLastFrame()
    {
        animator.SetTrigger("attack");
    }
    public void OnLookRightLastFrame()
    {
        animator.SetTrigger("attack");
    }
    public void Jump()
    {
        isjump = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && isattacked && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if(collision.transform.parent.parent.IsChildOf(this.transform.parent.parent.parent))
            {
                isattacked = false;
                collision.gameObject.GetComponent<Zombie>().ChangeHealth(-damage, Zombie.Attacked_type.press);
                SoundManager.Instance.PlaySound(SoundManager.Sounds.gargantuar_thump, true);
            }
            
        }
    }
    public void OnLastFrame()
    {
        animator.enabled = false;
        Destroy(gameObject);
    }
}
