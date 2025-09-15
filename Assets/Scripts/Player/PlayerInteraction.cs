using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spirit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interactable Action")]
        [SerializeField] private GameObject _currentPossessed;
        [SerializeField] private GameObject _candidate;
        [SerializeField] private Transform _possesPos;
        [SerializeField] private bool _insidePossesTrigger;
        [SerializeField] private bool _possessed;
        private SpriteRenderer _playerSprite;

        [Header("Burn or Possess Only")]
        [SerializeField] private GameObject _candidateBurnOnly;
        [SerializeField] private SpriteRenderer _candidateBurnChild;
        [SerializeField] private GameObject _candidatePossesOnly;

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

            if (collision.CompareTag("OnlyBurnableObject"))
            {
                _candidateBurnOnly = collision.gameObject;
                _candidateBurnChild = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
            }
            if (collision.CompareTag("OnlyPossessObject"))
            {
                _candidatePossesOnly = collision.gameObject;                                                                        
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
            if (collision.CompareTag("OnlyBurnableObject"))
            {
                _candidateBurnOnly = collision.gameObject;
                _candidateBurnChild = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
                //TODO Vfx red
            }
            if (collision.CompareTag("OnlyPossessObject"))
            {
                _candidatePossesOnly = collision.gameObject;
                //Todo vfx blue
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
            if (collider.CompareTag("OnlyBurnableObject"))
            {
                _candidateBurnOnly = null;
                
                //TODO Vfx red
            }
            if (collider.CompareTag("OnlyPossessObject"))
            {
                _candidatePossesOnly = null;
                //Todo vfx blue
            }
            if (collider.CompareTag("Portal"))
            {
                _insideSceneTrigger = false;
                _sceneLoaderPortal= null;
            }
        }



        public void BurnableObject() //OnButtonAttack
        {
            if(_insidePossesTrigger && _candidate != null) //Burnable & Possessable Object
            {
                _candidate.GetComponentInChildren<ParticleSystem>().Play();
                //_candidate.GetComponent<Collider2D>().enabled = false;
                Destroy(_candidate, 4f);
                
                print("Aura --");
            }
            if(_candidateBurnOnly != null) //Only burn for lights
            {
                _candidateBurnOnly.GetComponent<SpriteRenderer>().enabled = true;
                //Todo Aura ++
                StartCoroutine(DissableCollider());

            }

        }

        private IEnumerator DissableCollider()
        {
            yield return new WaitForSeconds(2f);
            _candidateBurnOnly.GetComponent<Collider2D>().enabled = false;
        }


        public void Possess() //OnButton
        {
            if(_candidatePossesOnly != null)
            {
                PossessObject(_candidatePossesOnly);
                return;
            }
            print("Interact");
            if (_possessed)
            {
                ExitPossessed();
            }
            else if (_insidePossesTrigger && _candidate != null)
            {
                PossessObject(_candidate);
                return;
            }

            if (_insideSceneTrigger && _candidate == null)
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

