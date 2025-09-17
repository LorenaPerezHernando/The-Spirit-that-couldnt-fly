using UnityEngine;

public class RespawnPlatform : MonoBehaviour
{
    [SerializeField] private Transform _respawnPos;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = _respawnPos.position;
        }
    }
}
