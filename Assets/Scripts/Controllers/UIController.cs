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
            CompareAura();
        }

        public void AddAuraHellSlider(float value)
        {
            _hellSlider.value = value;
            CompareAura();

        }

        private void CompareAura()
        {
            if(_heavenSlider.value > _hellSlider.value)
            {
                GameController.Instance.ChangeApearenceToAngel();
            }

            if(_hellSlider.value > _heavenSlider.value)
            {
                GameController.Instance.ChangeApearenceToDemon();
            }
        }
    }
}
