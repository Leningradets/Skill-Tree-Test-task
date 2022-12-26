using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsSound : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;

    private UIAudio _uIAudio;
    private Button _button;
    private EventTrigger _eventTrigger;

    private void Awake()
    {
        _uIAudio = FindObjectOfType<UIAudio>();
        _button = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();

        _button.onClick.AddListener(() => { PlaySound(_clickSound); });

        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { PlaySound(_hoverSound); });
        _eventTrigger.triggers.Add(entry);
    }

    private void PlaySound(AudioClip sound)
    {
        if (_uIAudio)
        {
            _uIAudio.PlaySound(sound);
        }
    }
}
