using Dreamteck.Splines;
using UnityEngine;

public class StartTrain : MonoBehaviour
{
    [SerializeField] private SplineFollower _splineFollower;
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _splineFollower.enabled = true;
    }
}
