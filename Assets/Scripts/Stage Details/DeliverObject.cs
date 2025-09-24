using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeliverObject : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Collider2D objectiveTrigger;      // zona de entrega (Trigger)
    [SerializeField] private GameObject objectiveFeedback; // icono/child del objetivo a encender

    [Header("UI")]
    [SerializeField] private Slider goodSlider;
    [SerializeField] private float valueToAdd = 0.7f;

    // Estado
    private bool insideObjective;
    private bool isPossessed;
    private bool delivered;

    private void Awake()
    {
        if (goodSlider == null)
            goodSlider = FindFirstObjectByType<Slider>(FindObjectsInactive.Include);
    }

    public void SetPossessed(bool value) => isPossessed = value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Enter");
        if (other == objectiveTrigger) insideObjective = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == objectiveTrigger) insideObjective = false;
    }

    public bool CanDeliver => isPossessed && insideObjective && !delivered;

    public void TryDeliver(MonoBehaviour requester)  // la llama el PlayerInteraction
    {
        if (!CanDeliver) return;
        requester.StartCoroutine(DeliverRoutine());
    }

    private IEnumerator DeliverRoutine()
    {
        delivered = true;
        print("delivered true");
        if (objectiveFeedback) objectiveFeedback.SetActive(true);
        if (goodSlider) goodSlider.value += valueToAdd;

        // Evita re-triggers mientras destruyes
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
