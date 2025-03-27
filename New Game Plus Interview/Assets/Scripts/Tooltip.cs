using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _contentField;

    [SerializeField] 
    private LayoutElement _layoutElement;

    [SerializeField] 
    private int _characterWrapLimit;

    [SerializeField]
    private Vector3 _tooltipOffset;
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!gameObject.activeSelf)
            return;

        transform.position = Input.mousePosition + _tooltipOffset;
    }

    public void TooltipText(string content)
    {
        _contentField.text = content;
        if(!gameObject.activeSelf)
            return;
        _layoutElement.enabled = _contentField.text.Length > _characterWrapLimit;
    }

    private void OnEnable()
    {
        transform.position = Input.mousePosition + _tooltipOffset;
    }
}
