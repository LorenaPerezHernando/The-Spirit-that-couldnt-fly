using Spirit;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeliverObject : MonoBehaviour
{
    [Header("Objective")]
    [SerializeField] private Collider2D _objectiveTrigger;      // zona de entrega (Trigger)
    [SerializeField] private GameObject _objectiveFeedback; // icono/child del objetivo a encender
    [SerializeField] private GameObject _puertaCerrada;
    [SerializeField] private GameObject _puertaAbierta;

    [Header("Progress")]
    [SerializeField] private float valueToAdd = 0.7f;

    // Estado
    private bool insideObjective;
    private bool isPossessed;
    private bool delivered;
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();               
    }

    public void SetPossessed(bool value) => isPossessed = value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Enter");
        if (other == _objectiveTrigger)
        {
            insideObjective = true;
        }


        else insideObjective = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == _objectiveTrigger)
        {
            insideObjective = false;
        }
        
    }

    public bool CanDeliver => isPossessed && insideObjective && !delivered;

    public void TryDeliver(MonoBehaviour requester)  
    {
        if (!CanDeliver) return;
        requester.StartCoroutine(DeliverRoutine());
    }

    private IEnumerator DeliverRoutine()
    {
        delivered = true;
        print("delivered true");
        if (_objectiveFeedback) _objectiveFeedback.SetActive(true);
        if(_objectiveTrigger) _objectiveTrigger.GetComponent<SpriteRenderer>().enabled = false;

        AbrirPuerta();

        var collider = GetComponent<Collider2D>();
        if (collider) collider.enabled = false;
        insideObjective = false;

        GameController.Instance.HeavenAura(valueToAdd);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void AbrirPuerta()
    {
        if(_puertaCerrada != null)
        {
            _puertaCerrada.GetComponent<Collider2D>().enabled = false;
            _puertaCerrada.GetComponent<SpriteRenderer>().enabled = false;
            _puertaAbierta.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
