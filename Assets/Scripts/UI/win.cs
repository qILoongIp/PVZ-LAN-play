using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理

public class win : MonoBehaviour
{
    [Header("植物奖杯")]
    public GameObject Plantawrad;
    [Header("僵尸奖杯")]
    public GameObject Zombieawrad;

    private bool plantAwardSpawned = false; // 跟踪植物奖杯是否已生成
    private bool zombieAwardSpawned = false; // 跟踪僵尸奖杯是否已生成

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
                plantAwardSpawned = true; // 设置为已生成
            }
        }

        // 检查是否需要生成僵尸奖杯
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
        Vector3 spawnPosition = new Vector3(0, 0, 0); // 左下角
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0; // 确保z轴为0

        GameObject newAward = Instantiate(Plantawrad, worldPosition, Quaternion.identity);
        float duration = 2f; // 动画持续时间
        float elapsedTime = 0f;
        Vector3 targetPosition = new Vector3(0, 0, 0); // 画面中心
        Vector3 originalScale = newAward.transform.localScale;
        Vector3 targetScale = originalScale * 5; // 目标缩放

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            newAward.transform.position = Vector3.Lerp(worldPosition, targetPosition, t);
            newAward.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newAward.transform.position = targetPosition; // 确保最后位置为中心
        newAward.transform.localScale = targetScale; // 确保最后缩放为目标
        yield return new WaitForSeconds(2f); // 等待2秒
        RestartGame(); // 重启游戏
    }

    void GenerateZombieAward()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject box0 = GameObject.Find("Box0"); // 找到名为Box0的对象

        if (box0 != null && box0.transform.childCount > 0 && !zombieAwardSpawned)
        {
            // 获取Box0第一个子对象的世界坐标
            Vector3 box0ChildWorldPos = box0.transform.GetChild(0).transform.position;

            foreach (GameObject zombie in zombies)
            {
                // 获取僵尸的世界坐标并进行比较
                if (zombie.transform.position.x < box0ChildWorldPos.x - 1)
                {
                    StartCoroutine(SpawnAndAnimateZombieAward(zombie.transform.position));
                    zombieAwardSpawned = true; // 设置为已生成
                    break; // 一旦生成，跳出循环
                }
            }
        }
    }

    IEnumerator SpawnAndAnimateZombieAward(Vector3 spawnPosition)
    {
        Vector3 worldPosition = new Vector3(Screen.width, 0, 0); // 右下角
        Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(worldPosition);
        targetWorldPosition.z = 0; // 确保z轴为0

        GameObject newAward = Instantiate(Zombieawrad, targetWorldPosition, Quaternion.identity);
        float duration = 2f; // 动画持续时间
        float elapsedTime = 0f;
        Vector3 targetPosition = new Vector3(0, 0, 0); // 画面中心
        Vector3 originalScale = newAward.transform.localScale;
        Vector3 targetScale = originalScale * 5; // 目标缩放

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            newAward.transform.position = Vector3.Lerp(targetWorldPosition, targetPosition, t);
            newAward.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newAward.transform.position = targetPosition; // 确保最后位置为中心
        newAward.transform.localScale = targetScale; // 确保最后缩放为目标
        yield return new WaitForSeconds(2f); // 等待2秒
        RestartGame(); // 重启游戏
    }

    void RestartGame()
    {
        // 重新加载当前场景以重启游戏
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
