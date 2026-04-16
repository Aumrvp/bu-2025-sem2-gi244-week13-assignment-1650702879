using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject obstaclePrefab;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        // 1.18 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        Instantiate(
            obstaclePrefab,
            spawnPoint.position,
            obstaclePrefab.transform.rotation
        );
    }
}
