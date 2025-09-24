using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoodUI : MonoBehaviour
{
    [SerializeField] private Collider2D _objectivePos;
    [SerializeField] private SpriteRenderer _childOfObjective; //Feedback de que lo has hecho bien
    [SerializeField] private float _valueToAdd = 0.7f;

    [SerializeField] private Slider _goodSlider;

    private void Awake()
    {
        _goodSlider = FindFirstObjectByType<Slider>();
       // _childOfObjective = _objectivePos.GetComponentInChildren<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("InteractableObject") || collision.CompareTag("OnlyPossesObject") 
            == _objectivePos)
        {

            //TODO ADD TEXT QUE DIGA QUE AURA ++ 
            
            _objectivePos.GetComponentInChildren<SpriteRenderer>().enabled = true;
            StartCoroutine(DelayDestroy());
        }
    }



    IEnumerator DelayDestroy()
    {
        _goodSlider.value = _goodSlider.value + _valueToAdd;
        print("Aura ++");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
