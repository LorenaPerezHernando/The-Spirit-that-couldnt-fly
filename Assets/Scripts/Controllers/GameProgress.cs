using System.Collections.Generic;
using UnityEngine;

namespace Spirit
{
    public class GameProgress : MonoBehaviour
    {
        public float heavenAura;
        public float hellAura;

        public int levelsCompleted;

        public int recolectablesBackground;

        // Fondos desbloqueables por ID (int)
        [SerializeField] private List<int> _collectedBackgrounds = new();
        [SerializeField] private List<int> _pendingBackgrounds = new();

        public bool HasBackground(int id) => _collectedBackgrounds.Contains(id);
        public bool IsPendingBackground(int id) => _pendingBackgrounds.Contains(id);

        public void AddPendingBackground(int id)
        {
            if (!HasBackground(id) && !IsPendingBackground(id))
                _pendingBackgrounds.Add(id);
        }

        // Se usa al entrar al nivel 0
        public List<int> ConsumePendingBackgrounds()
        {
            var copy = new List<int>(_pendingBackgrounds);
            _pendingBackgrounds.Clear();
            return copy;
        }

        public void MarkBackgroundCollected(int id)
        {
            if (!HasBackground(id)) _collectedBackgrounds.Add(id);
        }
    }
}
