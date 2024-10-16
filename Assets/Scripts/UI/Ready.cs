using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour
{
    public bool isReady;
    private GameObject cancel;
    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        cancel = transform.Find("Cancel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(isReady == false)
        {
            GameManager.Instance.bools.Add(true);
            isReady = true;
            Color newcolor = Color.white;
            newcolor.a = 0;
            GetComponent<Image>().color = newcolor;
            newcolor = Color.white;
            newcolor.a = 1;
            cancel.GetComponent<Image>().color = newcolor;
        }
    }
}
