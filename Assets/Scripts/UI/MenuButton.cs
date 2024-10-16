using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private GameObject MenuUI;
    // Start is called before the first frame update
    void Start()
    {
        MenuUI = GameObject.Find("MenuUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(MenuUI.activeSelf)
        {
            MenuUI.SetActive(false);
        }
        else
        {
            MenuUI.SetActive(true);
        }
    }
}
