using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DelegateManager.OnUserDataLoad += OnUserDataLoad;
    }

    // Update is called once per frame
    private void OnUserDataLoad()
    {
        SceneManager.LoadScene("View");
    }
}
