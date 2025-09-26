using Spirit;
using System.Collections;
using UnityEngine;

public class DeliverPossesedObj : MonoBehaviour
{
    [Header("Zona de Entrega")]
    [SerializeField] private Collider2D _objectiveTrigger;     // Trigger del ÁREA fija
    [SerializeField] private Transform _deliverPoint;           // punto del OBJETO (child). Si null, usa transform

    [Header("Feedback")]
    [SerializeField] private GameObject _objectiveFeedback;
    [SerializeField] private GameObject _puertaCerrada;
    [SerializeField] private GameObject _puertaAbierta;

    [Header("Progreso")]
    [SerializeField] private float valueToAdd = 0.7f;

    // Estado
    private bool isPossessed;
    private bool delivered;
    private bool auraGranted;

    private void Awake()
    {
        if (_deliverPoint == null) _deliverPoint = transform;
    }

    public void SetPossessed(bool value) => isPossessed = value;

    // Comprobación robusta: punto del OBJETO dentro del TRIGGER del área
    private bool IsInsideObjective()
    {
        if (!_objectiveTrigger || !_deliverPoint) return false;
        return _objectiveTrigger.OverlapPoint(_deliverPoint.position);
    }

    public bool CanDeliver => isPossessed && !delivered && IsInsideObjective();

    public void TryDeliver(MonoBehaviour requester)
    {
        if (!CanDeliver || auraGranted) return;
        auraGranted = true;
        requester.StartCoroutine(DeliverRoutine());
    }

    private IEnumerator DeliverRoutine()
    {
        delivered = true;

        if (_objectiveFeedback) _objectiveFeedback.SetActive(true);

        var srArea = _objectiveTrigger ? _objectiveTrigger.GetComponent<SpriteRenderer>() : null;
        if (srArea) srArea.enabled = false;

        AbrirPuerta();

        var myCol = GetComponent<Collider2D>();
        if (myCol) myCol.enabled = false;

        GameController.Instance.HeavenAura(valueToAdd);

        yield return null;
        Destroy(gameObject);
    }

    private void AbrirPuerta()
    {
        if (_puertaCerrada)
        {
            var c = _puertaCerrada.GetComponent<Collider2D>(); if (c) c.enabled = false;
            var s = _puertaCerrada.GetComponent<SpriteRenderer>(); if (s) s.enabled = false;
        }
        if (_puertaAbierta)
        {
            var s = _puertaAbierta.GetComponent<SpriteRenderer>(); if (s) s.enabled = true;
        }
    }
}
