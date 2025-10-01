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
        [Header("Player")]
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _demonPlayer;
        [SerializeField] private GameObject _angelPlayer;

        [Header("GameProgress")]
        [SerializeField] private GameProgress _gameProgress;
        [SerializeField] private UIController _uiController;

        #endregion

        public void NextInitialPos(Transform posInitial)
        {
            _player.position = posInitial.position;
        }
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

        public void ChangeApearenceToDemon()
        {
            _demonPlayer.SetActive(true);
            _angelPlayer.SetActive(false);
        }

        public void ChangeApearenceToAngel()
        {
            _demonPlayer.SetActive(false);
            _angelPlayer.SetActive(true);
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
