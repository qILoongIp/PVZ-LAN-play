using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("ÉúÃüÖµ")]
    public float health = 100;
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if(currentHealth <= 0)
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
}
