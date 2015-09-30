using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


static public class UIUtils
{
    static public void SetRectAsFullScreen(ref RectTransform targetTrans)
    {
        targetTrans.anchorMin = Vector2.zero;
        targetTrans.anchorMax = Vector2.one;
        targetTrans.offsetMin = Vector2.zero;
        targetTrans.offsetMax = Vector2.zero;
    }
}
