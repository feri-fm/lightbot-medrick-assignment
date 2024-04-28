using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSizeFitterHelper : ContentSizeFitter
{
    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        if (gameObject.activeInHierarchy)
            StartCoroutine(IOnRectTransformDimensionsChange());
    }

    IEnumerator IOnRectTransformDimensionsChange()
    {
        yield return new WaitForEndOfFrame();
        base.OnRectTransformDimensionsChange();
        var c = GetComponentInParent<ContentSizeFitterHelper>();
        if (c != null)
            c.ForceUpdate();
    }

    public void ForceUpdate()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
