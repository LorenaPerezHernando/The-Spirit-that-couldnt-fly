using UnityEngine;
using UnityEngine.InputSystem;

namespace Spirit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _currentPossessed;
        [SerializeField] private GameObject _candidate;
        [SerializeField] private Transform _possesPos;
        [SerializeField] private bool _insideTrigger;
        [SerializeField] private bool _possessed;
        private SpriteRenderer _playerSprite;
        


        private void Awake()
        {
            _playerSprite = GetComponent<SpriteRenderer>();

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("InteractableObject"))
            {
                Debug.Log("Poses");
                _candidate = collision.gameObject;
                _insideTrigger = true;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("InteractableObject"))
            {
                print("EndTrigger");
                _candidate = null;
                _currentPossessed = null;
                _insideTrigger= false;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
        }

        public void Possess() //OnButton
        {
            print("Interact");
            if(_insideTrigger)
                PossessObject(_candidate);
            if( _possessed)
                ExitPossess();

        }


        private void PossessObject(GameObject obj)
        {
            print("Possesmethod");
            _currentPossessed = obj;

            _playerSprite.enabled = false;
            obj.transform.SetParent(_possesPos);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;


        }

        private void ExitPossess()
        {

        }

        private void OnAttack(InputValue input)
        {

        }
    }
}

