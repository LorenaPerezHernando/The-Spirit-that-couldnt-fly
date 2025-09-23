using Dreamteck.Splines;
using UnityEngine;

public class StartTrain : MonoBehaviour
{
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private bool _rideStarted;
    private SpriteRenderer _sprite;
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        double p = _splineFollower.result.percent;
        if (p >= 0.999 & _rideStarted)
        {
            _splineFollower.direction = Spline.Direction.Backward;
            
        }
        else if (p <= 0.001)
        {
            _splineFollower.direction = Spline.Direction.Forward;
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Trigger Train");
            _splineFollower.enabled = true;
            _rideStarted = true;
            _splineFollower.wrapMode = SplineFollower.Wrap.PingPong;
        }
    }

    private void Flip() { _sprite.flipX = !_sprite.flipX; } 



}
