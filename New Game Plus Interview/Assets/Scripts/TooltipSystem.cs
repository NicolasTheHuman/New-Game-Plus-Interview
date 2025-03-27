using System;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _instance;

    public static TooltipSystem Instance => _instance;
    
    public Tooltip tooltip;
    
    private void Awake()
    {
        _instance = this;
    }

    public static void Show(string content)
    {
        if(content.Equals(""))
            return;
        
        _instance.tooltip.TooltipText(content);
        _instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.tooltip.gameObject.SetActive(false);
    }
}
