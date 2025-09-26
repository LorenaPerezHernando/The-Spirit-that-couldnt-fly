using Dreamteck.Splines;
using System.Collections;
using UnityEngine;

public class TrainTrafficLight : MonoBehaviour
{
    [SerializeField] private SplineFollower _follower;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartTrain());
        }
    }

    IEnumerator StartTrain()
    {
        _renderer.color = Color.yellow;
        yield return new WaitForSeconds(1);
        _renderer.color = Color.green;
        yield return new WaitForSeconds(1);
        _follower.enabled = true;
        _follower.wrapMode = SplineFollower.Wrap.PingPong;

        yield return new WaitForSeconds(1);
        this.enabled = false;
    }
}
