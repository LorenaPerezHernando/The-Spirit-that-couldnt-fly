using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Spirit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interactable Action")]
        [SerializeField] private GameObject _currentPossessed;
        [SerializeField] private Collider2D _currentPossesedCollider;
        [SerializeField] private GameObject _candidate;
        [SerializeField] private Transform _possesPos;
        [SerializeField] private bool _insidePossesTrigger;
        [SerializeField] private bool _possessed;
        private SpriteRenderer _playerSprite;

        [Header("Burn or Possess Only")]
        [SerializeField] private Slider _badSlider;
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
                _insidePossesTrigger = true;
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
                _insidePossesTrigger = true;
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
            _insidePossesTrigger = false;
            if (collider.CompareTag("InteractableObject") && _candidate == collider.gameObject)
            {
                print("EndTrigger");
                _candidate = null;
                _insidePossesTrigger = false;
                //Vfx para saber que puede interactuar
                //VFX Objeto Iluminar
            }
            if (collider.CompareTag("OnlyBurnableObject") && _candidatePossesOnly == collider.gameObject)
            {
                _candidateBurnOnly = null;
                
                //TODO Vfx red
            }
            if (collider.CompareTag("OnlyPossessObject") && _candidateBurnOnly == collider.gameObject)
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
                if(_candidate.GetComponentInChildren<ParticleSystem>() != null)
                _candidate.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(_candidate, 4f);
                _badSlider.value = _badSlider.value + 0.7f;
                //TODO Text con Aura -- 
                print("Aura --");
            }
            if(_candidateBurnOnly != null) //Only burn for lights
            {
                _candidateBurnOnly.GetComponent<SpriteRenderer>().enabled = true;
                //TODO Text con Aura ++
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
            if (_possessed && _currentPossessed != null)
            {
                var deliver = _currentPossessed.GetComponent<DeliverObject>();
                if (deliver != null && deliver.CanDeliver)
                {
                    deliver.TryDeliver(this);  // corre la corrutina en este Mono
                }
                ExitPossessed();               // soltar/terminar siempre tras pulsar
                return;
            }
            //print("Interact");
            //if (_possessed)
            //{
            //    GameObject newTarget = _candidatePossesOnly != null ? _candidatePossesOnly
            //        : (_candidate != null && _candidate != _currentPossessed ? _candidate : null);

            //    if (newTarget != null)
            //    {
            //       ExitPossessed();
            //       PossessObject(newTarget);

            //    }
            //    else
            //    {
            //        ExitPossessed();
            //    }
            //}
            if (_insidePossesTrigger)
            {
                if (_candidatePossesOnly != null)
                {
                    PossessObject(_candidatePossesOnly);
                    return;
                }
                if (_candidate != null)
                {
                    PossessObject(_candidate);
                    return;
                }
            }

            if (_insideSceneTrigger)
            {
                PortalTrigger();
                print("portal");
            }

        }


        private void PossessObject(GameObject obj)
        {
            print("Possesmethod");
            _currentPossessed = obj;
            _possessed = true;

            var deliver = _currentPossessed.GetComponent<DeliverObject>();
            if (deliver) deliver.SetPossessed(true);

            //_currentPossesedCollider = obj.GetComponent<Collider2D>();
            //if (_currentPossesedCollider) _currentPossesedCollider.enabled = false;

            _playerSprite.enabled = false;
            obj.transform.SetParent(_possesPos);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;


        }

        private void ExitPossessed()
        {
            _possessed = false;
            print("Soltar objeto");
            //_currentPossessed.transform.SetParent(null);

            if (_currentPossessed)
            {
                var deliver = _currentPossessed.GetComponent<DeliverObject>();
                if (deliver) deliver.SetPossessed(false);
            }

            if (_currentPossessed) _currentPossessed.transform.SetParent(null);
            if (_playerSprite) _playerSprite.enabled = true;
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

