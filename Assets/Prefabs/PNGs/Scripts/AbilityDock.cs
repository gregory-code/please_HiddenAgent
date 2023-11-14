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
    [SerializeField] float scaleRange = 100f;

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

        UpdateScale();
    }

    private void UpdateScale()
    {
        if(touchData == null)
        {
            abilityWidgets.ForEach(widget => { widget.SetScaleAmount(0); }); // for each very powerful
        }
        else
        {
            float touchPosY = touchData.position.y;
            foreach(AbiliyWidget widet in abilityWidgets)
            {
                float widgetPosY = widet.transform.position.y;
                float distanceY = Mathf.Abs(widgetPosY - touchPosY);
                if(distanceY > scaleRange)
                {
                    widet.SetScaleAmount(0);
                }
                else
                {
                    widet.SetScaleAmount((scaleRange - distanceY) / scaleRange);
                }
            }
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
