using UnityEngine;
using UnityEngine.EventSystems;

public class CardPanel : MonoBehaviour
{
    private bool change;//用于判断是否需要改变组件
    private EventTrigger eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isStart && this.transform.childCount != 0 && change)
        {
            // 遍历父对象的所有子对象
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform childTransform = this.transform.GetChild(i);
                // 获取 EventTrigger 组件
                eventTrigger = childTransform.GetComponent<EventTrigger>();
                // 移除 Pointer Click 事件
                RemovePointerClickEventFromEventTrigger();
                if (childTransform.GetComponent<PlantCard>() != null)
                {
                    childTransform.GetComponent<PlantCard>().enabled = true;
                }
                if (childTransform.GetComponent<CYP_PlantCard>() != null)
                {
                    childTransform.GetComponent<CYP_PlantCard>().enabled = false;
                }
                if (childTransform.GetComponent<ZombieCard>() != null)
                {
                    childTransform.GetComponent<ZombieCard>().enabled = true;
                }
                if (childTransform.GetComponent<PUZ_ZombieCard>() != null)
                {
                    childTransform.GetComponent<PUZ_ZombieCard>().enabled = false;
                }
                if(i == this.transform.childCount - 1)
                {
                    change = false;
                }
            }
        }
    }
    private void RemovePointerClickEventFromEventTrigger()
    {
        if (eventTrigger != null)
        {
            // 遍历 EventTrigger 的事件列表
            for (int i = 0; i < eventTrigger.triggers.Count; i++)
            {
                EventTrigger.Entry entry = eventTrigger.triggers[i];

                // 检查是否为 Pointer Click 事件
                if (entry.eventID == EventTriggerType.PointerClick)
                {
                    // 移除 Pointer Click 事件
                    eventTrigger.triggers.RemoveAt(i);
                    break; // 如果只想移除一个事件，可以使用 break
                }
            }
        }
    }
}
