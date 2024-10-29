using Unity.Netcode;
using UnityEngine;

public class SunBirth : NetworkBehaviour
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
        if(IsServer)
        {
            time += Time.deltaTime;
            if (time >= interval && this.transform.parent != null)
            {
                animator.SetBool("Ready", true);
            }
        }

    }
    public void OnLsatFrame()
    {
        animator.SetBool("Ready", false);
        time = 0;
    }
    public void OnGlow()
    {
        if(IsServer)
        {
            Vector3 spawnPosition = suntransform.position + new Vector3(0, 0, -1);
            SpawnSunServerRpc(spawnPosition); // 调用客户端生成阳光
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnSunServerRpc(Vector3 position)
    {
        // 所有客户端实例化阳光
        GameObject newSun = Instantiate(Sun, position, Quaternion.identity);
        newSun.GetComponent<NetworkObject>().Spawn(); // 确保该对象同步至所有客户端
    }
}
