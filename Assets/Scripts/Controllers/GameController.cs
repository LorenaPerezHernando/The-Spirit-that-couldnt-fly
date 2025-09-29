using Spirit.UI;
using UnityEngine;

namespace Spirit
{
    public class GameController : Singleton<GameController>
    {
        #region Properties
        [Header("GameProgress")]
        public GameProgress GameProgress => _gameProgress;
        public UIController UIController => _uiController;

        #endregion
        #region Fields

        [SerializeField] private GameProgress _gameProgress;
        [SerializeField] private UIController _uiController;

        #endregion


        public void HeavenAura(float aura)
        {
            _gameProgress.heavenAura += aura;
            _uiController.AddAuraHeavenSlider(_gameProgress.heavenAura);
        }
        public void HellAura(float aura)
        {
            _gameProgress.hellAura += aura;
            _uiController.AddAuraHellSlider(_gameProgress.hellAura);
        }

        public void CompleteLevel()
        {
            _gameProgress.levelsCompleted++;
        }

        public void AddRecolectableBackground(int recolectable)
        {
            _gameProgress.recolectablesBackground += recolectable;
        }

        public void RegisterBackgroundPickup(int backgroundId)
        {
            _gameProgress.AddPendingBackground(backgroundId);
            // TODO SaveSystem.Save(_gameProgress); 
        }

    }
}
