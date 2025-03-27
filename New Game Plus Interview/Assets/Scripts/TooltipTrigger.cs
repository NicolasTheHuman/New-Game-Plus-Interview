using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string _content = "";

    [SerializeField]
    private float _showDelay = 0.5f;
    private WaitForSeconds _waitForSeconds;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _waitForSeconds = new WaitForSeconds(_showDelay);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_content.Equals(""))
            return;
        
        StopAllCoroutines();
        StartCoroutine(TooltipDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TooltipSystem.Hide();
    }

    private IEnumerator TooltipDelay()
    {
        if (!TooltipSystem.Instance)
            yield break;
        
        yield return _waitForSeconds;
        
        TooltipSystem.Show(_content);
    }

    public void SetData(string content = "")
    {
        _content = content;
    }
}
