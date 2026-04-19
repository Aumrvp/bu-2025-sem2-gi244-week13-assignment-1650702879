using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;
    public int obstacleType;
    public ObstacleObjectPool pool;

    void Start()
    {
        if (pool == null)
        {
            pool = FindFirstObjectByType<ObstacleObjectPool>();
        }
    }

    void Update()
    {
        // 1.17 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            speed = 0;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -15 && gameObject.CompareTag("Obstacle"))
        {
            if (pool != null)
            {
                pool.Release(gameObject, obstacleType);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
