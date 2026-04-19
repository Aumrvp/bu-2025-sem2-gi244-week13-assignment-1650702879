using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        Prewarm(obstacleBarrelPool, obstacleBarrelPrefab);
        Prewarm(obstacleBarrierPool, obstacleBarrierPrefab);
        Prewarm(obstacleStoneWallPool, obstacleStoneWallPrefab);
    }

    private void Prewarm(List<GameObject> pool, GameObject prefab)
    {
        if (prefab == null) return;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(prefab, transform);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    private List<GameObject> GetPool(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0: return obstacleBarrelPool;
            case 1: return obstacleBarrierPool;
            case 2: return obstacleStoneWallPool;
            default: return null;
        }
    }

    private GameObject GetPrefab(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0: return obstacleBarrelPrefab;
            case 1: return obstacleBarrierPrefab;
            case 2: return obstacleStoneWallPrefab;
            default: return null;
        }
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> pool = GetPool(obstacleType);
        if (pool == null) return null;

        for (int i = 0; i < pool.Count; i++)
        {
            GameObject go = pool[i];
            if (go != null && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }

        GameObject prefab = GetPrefab(obstacleType);
        if (prefab == null) return null;

        GameObject grown = Instantiate(prefab, transform);
        pool.Add(grown);
        grown.SetActive(true);
        return grown;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        if (obstacle == null) return;

        List<GameObject> pool = GetPool(obstacleType);
        if (pool == null) return;

        obstacle.SetActive(false);
        obstacle.transform.SetParent(transform, false);

        if (!pool.Contains(obstacle))
        {
            pool.Add(obstacle);
        }
    }
}
