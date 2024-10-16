using UnityEngine;
using UnityEngine.UI;

public class Cancel : MonoBehaviour
{
    private GameObject ready;
    // Start is called before the first frame update
    void Start()
    {
        Color newcolor = Color.white;
        newcolor.a = 0;
        GetComponent<Image>().color = newcolor;
        ready = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(ready.GetComponent<Ready>().isReady == true)
        {
            GameManager.Instance.bools.RemoveAt(0);
            ready.GetComponent<Ready>().isReady = false;
            Color newcolor = Color.white;
            newcolor.a = 0;
            GetComponent<Image>().color = newcolor;
            newcolor = Color.white;
            newcolor.a = 1;
            ready.GetComponent<Image>().color = newcolor;
        }
    }
}
