using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStarter : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timeline;
    [SerializeField] private bool _startOnTrigger;
    [SerializeField] private bool _playOnce = true;
    [SerializeField] private bool _activated;

    public void PlayTimeLine()
    {
        if (_playOnce && _activated)
            return;
        
        _timeline.Play();
        _activated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_startOnTrigger)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayTimeLine();
            }
        }
    }
}
