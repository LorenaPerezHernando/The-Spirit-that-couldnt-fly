using Spirit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMap : MonoBehaviour
{
    [System.Serializable]
    public class Map
    {
        public int id;                 // ID del background
        public SpriteRenderer targetToActivate;
        public Animator animator;      // Animador en la escena 0
        public string trigger = "Play";
        public float fallbackDuration = 2f;
    }

    [SerializeField] private List<Map> _bindings = new();
    [SerializeField] private AnimationQueueRecolectables _queue;

    private Dictionary<int, Map> _map;

    private void Awake()
    {
        _map = new Dictionary<int, Map>();
        foreach (var b in _bindings) if (!_map.ContainsKey(b.id)) _map.Add(b.id, b);
    }

    private void Start()
    {
        var gp = GameController.Instance.GameProgress;
        var pending = gp.ConsumePendingBackgrounds(); // <- “nuevos”
        foreach (var id in pending)
        {
            if (_map.TryGetValue(id, out var bind) && bind.animator)
                _queue.Enqueue(PlayUnlockRoutine(id, bind));
            else
                gp.MarkBackgroundCollected(id); // no repetir aunque no haya binding
        }
    }

    private IEnumerator PlayUnlockRoutine(int id, Map b)
    {
        b.animator.ResetTrigger(b.trigger);
        b.animator.SetTrigger(b.trigger);
        yield return new WaitForSeconds(b.fallbackDuration);

        GameController.Instance.GameProgress.MarkBackgroundCollected(id);
        // Aquí: activar la imagen, UI “nuevo fondo”, guardar, etc.
    }
}
