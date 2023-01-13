using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopupManager : MonoBehaviour
{
    List<PopupInfo> PopupList = new List<PopupInfo>();

    public void Setup()
    {
        PopupList = transform.GetComponentsInChildren<PopupInfo>()?.ToList();
    }
}
