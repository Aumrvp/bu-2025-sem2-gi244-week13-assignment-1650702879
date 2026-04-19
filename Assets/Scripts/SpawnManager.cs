using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public ObstacleObjectPool pool;

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

        int obstacleType = Random.Range(0, 3);
        GameObject obstacle = pool.Acquire(obstacleType);
        if (obstacle == null) return;

        obstacle.transform.position = spawnPoint.position;
        obstacle.transform.rotation = spawnPoint.rotation;

        Rigidbody rb = obstacle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        MoveLeft mover = obstacle.GetComponent<MoveLeft>();
        if (mover != null)
        {
            mover.obstacleType = obstacleType;
            mover.pool = pool;
            mover.speed = 10f;
        }
    }
}
