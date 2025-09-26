using UnityEngine;
using UnityEngine.UI;

namespace Spirit.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Slider _heavenSlider;
        [SerializeField] private Slider _hellSlider;


        public void AddAuraHeavenSlider(float value)
        {
            _heavenSlider.value = value;
        }

        public void AddAuraHellSlider(float value)
        {
            _hellSlider.value = value;

        }
    }
}
