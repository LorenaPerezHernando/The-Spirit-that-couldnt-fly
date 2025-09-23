using UnityEngine;

public class RespawnPlatform : MonoBehaviour
{
    [SerializeField] private Transform _respawnPos;
    [SerializeField] private GameObject[] _books;
    [SerializeField] private Vector2[] _initialBookPos;

    private void Awake()
    {
        for(int i = 0;  i < _books.Length; i++)
        {
            _initialBookPos[i] = _books[i].transform.position;                                                                                                                  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = _respawnPos.position;
            ResetBooks();
        }
    }

    private void ResetBooks()
    {
        for(int i = 0; i < _books.Length; i++)
        {
            _books[i].transform.position = _initialBookPos[i];
            _books[i].GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }
    }
}
