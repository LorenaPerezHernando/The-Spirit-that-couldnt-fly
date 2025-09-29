using Spirit;
using UnityEngine;

public class RecolectableBackground : MonoBehaviour
{
    [SerializeField] private int backgroundId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
    private void Collect()
    {
        GameController.Instance.RegisterBackgroundPickup(backgroundId);
        // SFX/FX locales…
        Destroy(gameObject);
    }
}
