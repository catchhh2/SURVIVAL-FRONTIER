using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [Tooltip("可掉落的战利品 Prefab 列表")]
    public GameObject[] lootPrefabs;

    [Range(0, 1), Tooltip("掉落概率 (0~1)")]
    public float dropChance = 0.2f;

    [Tooltip("离地高度偏移")]
    public float yOffset = 0.5f;

    // LootDrop.cs
    public void TryDrop()
    {
        Debug.Log($"[LootDrop] TryDrop() from {name}");

        if (lootPrefabs.Length == 0) { Debug.Log("No prefabs"); return; }

        if (Random.value <= dropChance)
        {
            int idx = Random.Range(0, lootPrefabs.Length);
            Vector3 pos = transform.position + Vector3.up * yOffset;
            var go = Instantiate(lootPrefabs[idx], pos, Quaternion.identity);
            Debug.Log($"[LootDrop] --> Spawn {go.name} at {pos}");
        }
        else
        {
            Debug.Log("[LootDrop] Roll failed");
        }
    }
}
