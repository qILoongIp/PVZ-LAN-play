using UnityEngine;

public class SunBirth : MonoBehaviour
{
    [Header("����̫���ļ��ʱ��")]
    public float interval;
    [Header("����̫����λ��")]
    public Transform suntransform;
    [Header("����")]
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
