using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopup
{
    void Setup(PopupManager manager);

    void Enable(int option);

    void Disable();
}
