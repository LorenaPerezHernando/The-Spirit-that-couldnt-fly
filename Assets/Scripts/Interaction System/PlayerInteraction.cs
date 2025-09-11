using Spirit.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Spirit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private IPossessable _possessableObject;
        private Sprite _originalSprite;

        private void Awake()
        {
            _originalSprite = GetComponent<Sprite>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("InteractableObject"))
            {
                Debug.Log("Poses");
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
                _possessableObject = collision.GetComponent<IPossessable>();
                if(_playerSprite == null)
                {
                    _playerSprite = collision.GetComponent<SpriteRenderer>().sprite;
                }
                


            }
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("InteractableObject"))
            {
                print("EndTrigger");
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
        }

        public void Possess()
        {
            print("Interact");
            PossessObject();
            _originalSprite = _playerSprite;
        }

        private void OnInteract(InputValue input)
        {
            print("Interact");
            if (!input.isPressed) return;
            PossessObject();
            print("Interact");
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _playerSprite;
            //if (_possessableObject == null)
            //{
            //    print("StartPossess");
                
            //    _possessableObject.PossessStart();

            //}
            //if (_possessableObject != null)
            //{
            //    print("EndPoses");
            //    // Soltar
            //    _possessableObject.PossessEnd();
            //    _possessableObject = null;

            //    _playerSprite = _originalSprite;
            //}
        }

        private void PossessObject()
        {
            print("Possesmethod");
        }

        private void OnAttack(InputValue input)
        {

        }
    }
}

