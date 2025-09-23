using Dreamteck.Splines;
using UnityEngine;

public class TrainActivator : MonoBehaviour
{
    [SerializeField] private SplineFollower _follower;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isStartTrigger; // true = inicio, false = final
    [SerializeField] private string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(_playerTag)) return;

        if (_isStartTrigger)
        {
            // Arranca hacia adelante
            _follower.direction = Spline.Direction.Forward;
            _follower.follow = true;
            _sprite.flipX = false; // ajusta según tu sprite
        }
        else
        {
            // Llega al final, vuelta atrás
            _follower.direction = Spline.Direction.Backward;
            _follower.follow = true;
            _sprite.flipX = true;
        }
    }

}
