using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoreScenePopupManager : MonoBehaviour
{
    [SerializeField]
    private List<PopupInfo> popupList;

    void Setup()
    {
        popupList = transform.GetComponentsInChildren<PopupInfo>().ToList();
    }

    public void OpenPopup(PopupType type)
    {
        PopupInfo selectedPopup = popupList.Where( p => p.type == type).ToList()[0];
        if (selectedPopup)
        {
            selectedPopup.gameObject.SetActive(true);
        }
    }

    public void ClosePopup(PopupType type)
    {
        PopupInfo selectedPopup = popupList.Where( p => p.type == type).ToList()[0];
        if (selectedPopup)
        {
            selectedPopup.gameObject.SetActive(false);
        }
    }
}
