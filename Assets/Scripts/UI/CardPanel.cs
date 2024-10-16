using UnityEngine;
using UnityEngine.EventSystems;

public class CardPanel : MonoBehaviour
{
    private bool change;//�����ж��Ƿ���Ҫ�ı����
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
            // ����������������Ӷ���
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform childTransform = this.transform.GetChild(i);
                // ��ȡ EventTrigger ���
                eventTrigger = childTransform.GetComponent<EventTrigger>();
                // �Ƴ� Pointer Click �¼�
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
            // ���� EventTrigger ���¼��б�
            for (int i = 0; i < eventTrigger.triggers.Count; i++)
            {
                EventTrigger.Entry entry = eventTrigger.triggers[i];

                // ����Ƿ�Ϊ Pointer Click �¼�
                if (entry.eventID == EventTriggerType.PointerClick)
                {
                    // �Ƴ� Pointer Click �¼�
                    eventTrigger.triggers.RemoveAt(i);
                    break; // ���ֻ���Ƴ�һ���¼�������ʹ�� break
                }
            }
        }
    }
}
