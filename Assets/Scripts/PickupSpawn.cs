using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    [Header("要生成的模块 Prefab 列表（红/绿）")]
    public GameObject[] pickupPrefabs;

    [Header("生成间隔（秒）")]
    public float spawnTime = 5f;
    private float timer = 0f;

    [Header("XZ 平面区域尺寸")]
    public Vector2 areaSize = new Vector2(50f, 50f);

    [Header("Y 轴高度（离地面高度）")]
    public float yOffset = 0.5f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer -= spawnTime;
            SpawnPickup();
        }
    }

    void SpawnPickup()
    {
        // 以自身 transform.position 为中心，XZ 平面内随机
        Vector3 center = transform.position;
        float dx = Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f);
        float dz = Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f);
        Vector3 spawnPos = new Vector3(center.x + dx, center.y + yOffset, center.z + dz);

        // 随机挑一个 Prefab 出来
        int idx = Random.Range(0, pickupPrefabs.Length);
        Instantiate(pickupPrefabs[idx], spawnPos, Quaternion.identity);
    }

    // 在 Scene 视图中画出生成区域（可视化）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 gizmoCenter = transform.position + Vector3.up * yOffset;
        Vector3 gizmoSize = new Vector3(areaSize.x, 0.1f, areaSize.y);
        Gizmos.DrawWireCube(gizmoCenter, gizmoSize);
    }
}
