using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityComponent owningAbility;
    [SerializeField] AbiliyWidget abilityWidgetPrefab;

    [SerializeField] RectTransform widgetRoot;

    List<AbiliyWidget> abilityWidgets = new List<AbiliyWidget>();

    PointerEventData touchData;
    AbiliyWidget highlightedAbilityWidget;

    private void Awake()
    {
        owningAbility.onNewAbilityAdded += AddNewAbilityWidget;
    }

    private void AddNewAbilityWidget(Ability ability)
    {
        AbiliyWidget newWidget = Instantiate(abilityWidgetPrefab, widgetRoot); // the widgetRoot is the parent
        newWidget.Init(ability);
        abilityWidgets.Add(newWidget);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(highlightedAbilityWidget != null)
        {
            highlightedAbilityWidget.ActivateAbility();
        }
        touchData = null;
    }

    private void Update()
    {
        if(touchData != null)
        {
            highlightedAbilityWidget = GetWidgetFromTouchData();
        }
    }

    private AbiliyWidget GetWidgetFromTouchData()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(touchData, results);
        foreach(RaycastResult result in results)
        {
            AbiliyWidget foundWidget = result.gameObject.GetComponent<AbiliyWidget>();
            if(foundWidget != null)
            {
                return foundWidget;
            }
        }
        return null;
    }
}
