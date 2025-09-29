using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueueRecolectables : MonoBehaviour
{
    private readonly Queue<IEnumerator> _q = new();
    private bool _playing;

    public void Enqueue(IEnumerator r)
    {
        _q.Enqueue(r);
        if (!_playing) StartCoroutine(PlayAll());
    }

    private IEnumerator PlayAll()
    {
        _playing = true;
        while (_q.Count > 0) yield return StartCoroutine(_q.Dequeue());
        _playing = false;
    }
}
