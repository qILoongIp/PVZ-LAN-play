using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ���볡������

public class win : MonoBehaviour
{
    [Header("ֲ�ｱ��")]
    public GameObject Plantawrad;
    [Header("��ʬ����")]
    public GameObject Zombieawrad;

    private bool plantAwardSpawned = false; // ����ֲ�ｱ���Ƿ�������
    private bool zombieAwardSpawned = false; // ���ٽ�ʬ�����Ƿ�������

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        int targetZombieCount = GameObject.FindGameObjectsWithTag("Zombie_Target").Length;

        if (targetZombieCount == 5)
        {
            SetAwardVisibility(false, false, false);
        }
        else if (targetZombieCount == 4)
        {
            SetAwardVisibility(true, false, false);
        }
        else if (targetZombieCount == 3)
        {
            SetAwardVisibility(true, true, false);
        }
        else if (targetZombieCount <= 2)
        {
            if (!plantAwardSpawned)
            {
                SetAwardVisibility(true, true, true);
                StartCoroutine(SpawnAndAnimatePlantAward());
                plantAwardSpawned = true; // ����Ϊ������
            }
        }

        // ����Ƿ���Ҫ���ɽ�ʬ����
        GenerateZombieAward();
    }

    void SetAwardVisibility(bool first, bool second, bool third)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = first;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = second;
        transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = third;
    }

    IEnumerator SpawnAndAnimatePlantAward()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0); // ���½�
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0; // ȷ��z��Ϊ0

        GameObject newAward = Instantiate(Plantawrad, worldPosition, Quaternion.identity);
        float duration = 2f; // ��������ʱ��
        float elapsedTime = 0f;
        Vector3 targetPosition = new Vector3(0, 0, 0); // ��������
        Vector3 originalScale = newAward.transform.localScale;
        Vector3 targetScale = originalScale * 5; // Ŀ������

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            newAward.transform.position = Vector3.Lerp(worldPosition, targetPosition, t);
            newAward.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newAward.transform.position = targetPosition; // ȷ�����λ��Ϊ����
        newAward.transform.localScale = targetScale; // ȷ���������ΪĿ��
        yield return new WaitForSeconds(2f); // �ȴ�2��
        RestartGame(); // ������Ϸ
    }

    void GenerateZombieAward()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject box0 = GameObject.Find("Box0"); // �ҵ���ΪBox0�Ķ���

        if (box0 != null && box0.transform.childCount > 0 && !zombieAwardSpawned)
        {
            // ��ȡBox0��һ���Ӷ������������
            Vector3 box0ChildWorldPos = box0.transform.GetChild(0).transform.position;

            foreach (GameObject zombie in zombies)
            {
                // ��ȡ��ʬ���������겢���бȽ�
                if (zombie.transform.position.x < box0ChildWorldPos.x - 1)
                {
                    StartCoroutine(SpawnAndAnimateZombieAward(zombie.transform.position));
                    zombieAwardSpawned = true; // ����Ϊ������
                    break; // һ�����ɣ�����ѭ��
                }
            }
        }
    }

    IEnumerator SpawnAndAnimateZombieAward(Vector3 spawnPosition)
    {
        Vector3 worldPosition = new Vector3(Screen.width, 0, 0); // ���½�
        Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(worldPosition);
        targetWorldPosition.z = 0; // ȷ��z��Ϊ0

        GameObject newAward = Instantiate(Zombieawrad, targetWorldPosition, Quaternion.identity);
        float duration = 2f; // ��������ʱ��
        float elapsedTime = 0f;
        Vector3 targetPosition = new Vector3(0, 0, 0); // ��������
        Vector3 originalScale = newAward.transform.localScale;
        Vector3 targetScale = originalScale * 5; // Ŀ������

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            newAward.transform.position = Vector3.Lerp(targetWorldPosition, targetPosition, t);
            newAward.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newAward.transform.position = targetPosition; // ȷ�����λ��Ϊ����
        newAward.transform.localScale = targetScale; // ȷ���������ΪĿ��
        yield return new WaitForSeconds(2f); // �ȴ�2��
        RestartGame(); // ������Ϸ
    }

    void RestartGame()
    {
        // ���¼��ص�ǰ������������Ϸ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
