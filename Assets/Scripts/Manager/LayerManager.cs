using UnityEngine;
/*
-2:±≥æ∞
-1:∏ﬂ¡¡∑ΩøÈ
0:÷≤ŒÔ
1:÷≤ŒÔµƒ‘§¿¿Õº
2:Ω© ¨
3:Ω© ¨µƒ‘§¿¿Õº
60:Ãÿ–ß
*/
public class LayerManager : MonoBehaviour
{
    public static LayerManager Instance;
    private int layer;
    private int plantlayer;
    private int zombielayer;
    private int pre_plantlayer;
    private int pre_zombielayer;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        layer = 0;
        /*
        plantlayer = 0;
        pre_plantlayer = 1;
        zombielayer = 2;
        pre_zombielayer= 3;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddLayer(int layertype,GameObject addlayergameobject)
    {
        addlayergameobject.GetComponent<SpriteRenderer>().sortingOrder = layer;
        layer += 1;
        /*
        switch (layertype) 
        { 
            case 0:
                addlayergameobject.GetComponent<SpriteRenderer>().sortingOrder = plantlayer;
                plantlayer += 1;
                break; 
            case 1:
                addlayergameobject.GetComponent<SpriteRenderer>().sortingOrder = pre_plantlayer;
                pre_plantlayer += 1;
                break;
            case 2:
                addlayergameobject.GetComponent<SpriteRenderer>().sortingOrder = zombielayer;
                zombielayer += 1;
                break;
            case 3:
                addlayergameobject.GetComponent<SpriteRenderer>().sortingOrder = pre_zombielayer;
                pre_zombielayer += 1;
                break;
        }
        */
    }
}
