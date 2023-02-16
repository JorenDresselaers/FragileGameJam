using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnPress : MonoBehaviour
{
    [SerializeField] private String _levelToLoad;
    [SerializeField] private bool _loadDifferentLevel = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (_loadDifferentLevel)
            {
                SceneManager.LoadScene(_levelToLoad);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
