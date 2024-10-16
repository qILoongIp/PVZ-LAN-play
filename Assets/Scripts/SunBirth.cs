using UnityEngine;

public class SunBirth : MonoBehaviour
{
    [Header("产生太阳的间隔时间")]
    public float interval;
    [Header("产生太阳的位置")]
    public Transform suntransform;
    [Header("阳光")]
    public GameObject Sun;
    private float time;
    private Animator animator;
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
        if(time >= interval && this.transform.parent != null)
        {
            animator.SetBool("Ready", true);
        }
    }
    public void OnLsatFrame()
    {
        animator.SetBool("Ready", false);
        time = 0;
    }
    public void OnGlow()
    {
        Instantiate(Sun,suntransform.position + new Vector3(0,0,-1),Quaternion.identity);
    }
}
