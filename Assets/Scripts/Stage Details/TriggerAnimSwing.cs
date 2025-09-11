using UnityEngine;

public class TriggerAnimSwing : MonoBehaviour
{                       
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();                                                                                    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            animator.SetTrigger("Swing");
    }
}
