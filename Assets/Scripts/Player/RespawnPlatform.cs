using UnityEngine;

public class RespawnPlatform : MonoBehaviour
{
    [SerializeField] private Transform _respawnPos;
    [SerializeField] private string _respawnName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_respawnName))
        {
            collision.transform.position = _respawnPos.position;
        }
    }
}
