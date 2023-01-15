using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPopupController : MonoBehaviour, IPopup
{
    PopupManager popupManager;

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }
#endregion
}
