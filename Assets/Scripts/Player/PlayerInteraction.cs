using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spirit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Possess Action")]
        [SerializeField] private GameObject _currentPossessed;
        [SerializeField] private GameObject _candidate;
        [SerializeField] private Transform _possesPos;
        [SerializeField] private bool _insidePossesTrigger;
        [SerializeField] private bool _possessed;
        private SpriteRenderer _playerSprite;

        [Header("Next Scene")]
        [SerializeField] private bool _insideSceneTrigger;
         private SceneLoader _sceneLoaderPortal;
        


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
                _insidePossesTrigger = true;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
            if (collision.CompareTag("Portal"))
            {
                _sceneLoaderPortal = collision.gameObject.GetComponent<SceneLoader>();
                _insideSceneTrigger = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("InteractableObject"))
            {
                Debug.Log("Poses");
                _candidate = collision.gameObject;
                _insidePossesTrigger = true;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
            if (collision.CompareTag("Portal"))
            {
                _sceneLoaderPortal = collision.gameObject.GetComponent<SceneLoader>();
                _insideSceneTrigger = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("InteractableObject"))
            {
                print("EndTrigger");
                _candidate = null;
                _insidePossesTrigger= false;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
            if (collider.CompareTag("Portal"))
            {
                _insideSceneTrigger = false;
                _sceneLoaderPortal= null;
            }
        }

        public void BurnableObject() //OnButtonAttack
        {
            if(_insidePossesTrigger && _candidate != null)
            {
                _candidate.GetComponentInChildren<SpriteRenderer>().enabled = true;
                _candidate.GetComponent<Collider2D>().enabled = false;
                print("Aura --");
            }

        }


        public void Possess() //OnButton
        {
            print("Interact");
            if (_possessed)
            {
                ExitPossessed();
            }
            else if (_insidePossesTrigger && _candidate != null)
            {
                PossessObject(_candidate);
            }

            if(gameObject.CompareTag("I"))

            if (_insideSceneTrigger)
                PortalTrigger();

        }


        private void PossessObject(GameObject obj)
        {
            print("Possesmethod");
            _currentPossessed = obj;

            _playerSprite.enabled = false;
            obj.transform.SetParent(_possesPos);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            _possessed = true;


        }

        private void ExitPossessed()
        {

            //if (_currentPossessed == null) yield return;

            _possessed = false;
            print("Soltar objeto");
            _currentPossessed.transform.SetParent(null);

            _playerSprite.enabled = true;

            _currentPossessed = null;
            
        }



        private void PortalTrigger()
        {
            _sceneLoaderPortal.LoadSceneByIndex(_sceneLoaderPortal._sceneToLoad);
        }

        private void OnAttack(InputValue input)
        {

        }
    }
}

