using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeInfoViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _captionText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private float _positionSpringness = 0.5f;
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private Player _player;

    private RectTransform _transform;
    private Image _image;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _image.CrossFadeAlpha(1, _fadeInDuration, false);
        var targetPosition = Input.mousePosition;
        _transform.position = Vector2.Lerp(_transform.position, targetPosition, _positionSpringness);

        var heightTreshold = Screen.height * 0.5f;
        var widthTreshold = Screen.width * 0.5f;
        var pivotPosition = new Vector2();

        if (targetPosition.y > Screen.height - _transform.rect.height)
        {
            pivotPosition += Vector2.up;
        }

        if(targetPosition.x > Screen.width - _transform.rect.width)
        {
            pivotPosition += Vector2.right;
        }

        _transform.pivot = Vector2.Lerp(_transform.pivot, pivotPosition, _positionSpringness + Time.deltaTime);
    }

    public void SetSkillNodeData(SkillNodeData skillNodeData)
    {
        _nameText.text = skillNodeData.Name;
        _captionText.text = skillNodeData.Caption;
        if(_player.SkillPoints < skillNodeData.Cost)
        {
            _costText.color = Color.red;
        }
        else
        {
            _costText.color = Color.white;
        }
        _costText.text = $"{skillNodeData.Cost} skill points";
    }

    private void OnDisable()
    {
        _image.CrossFadeAlpha(0, 0, false);
    }
}
