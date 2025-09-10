using UnityEngine;

namespace Spirit.Interaction
{
    public class PossessableObject : MonoBehaviour, IPossessable
    {
        [SerializeField] private SpriteRenderer _sprite;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _targetPos;


        public Sprite PossessSprite()
        {
            return _sprite != null ? _sprite.GetComponent<Sprite>() : null;
        }
        public void PossessStart()
        {
            if(_sprite) _sprite.enabled = false;
        }
        public void PossessEnd()
        {
            if (_targetPos != null &&
            Vector2.Distance(_playerTransform.position, _targetPos.position) < 0.5f)
            {
                //TODO IF PULSA EL BOTON
                // Si el player está dentro del área target --> snap
                transform.position = _targetPos.position;
                transform.rotation = _targetPos.rotation;
            }
            else
            {
                transform.position = _playerTransform.position;
            }
        }

    }
}
