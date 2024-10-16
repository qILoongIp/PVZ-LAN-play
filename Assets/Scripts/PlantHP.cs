using UnityEngine;

public class PlantHP : MonoBehaviour
{
    private GameObject parent;//最高层级父类
    private GameObject BG;//上一级父类，血条的背景图片
    private float health;
    private float currentHealth;
    // Start is called before the first frame update
    void Awake()
    {
        parent = transform.root.gameObject;
        BG = transform.parent.gameObject;
        health = parent.GetComponent<Plant>().health;
        currentHealth = parent.GetComponent<Plant>().CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SpriteRenderer>().sortingOrder = parent.GetComponent<SpriteRenderer>().sortingOrder + 2 + 100;
        BG.GetComponent<SpriteRenderer>().sortingOrder = parent.GetComponent<SpriteRenderer>().sortingOrder + 1 + 100;
        currentHealth = parent.GetComponent<Plant>().CurrentHealth;
        transform.localScale = new Vector3(1 * (currentHealth / health), 1, 0);
        transform.localPosition = new Vector3(-(1.45f * (1 - currentHealth / health)) / 2, 0, 0);
    }
}
