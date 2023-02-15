using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnDestroy : MonoBehaviour
{
    [SerializeField] private string _levelName = "Level01";
    [SerializeField] private float _timeToLoad = 0f;
    [SerializeField] private GameObject _triggerWhenDestroyed;

    private bool _objectAssigned = false;
    private bool _hasTriggered = false;

    void Awake()
    {
        if (_triggerWhenDestroyed) _objectAssigned = true;
    }

    void Update()
    {
        if (_objectAssigned && !_triggerWhenDestroyed && !_hasTriggered)
        {
            _hasTriggered = true;
            print("Destroyed");
            Invoke(LoadLevelMethod, _timeToLoad);
        }
    }

    void OnDestroy()
    {
        if (!_triggerWhenDestroyed || !_objectAssigned)
        {
            SceneManager.LoadScene(_levelName);
        }
    }

    private const string LoadLevelMethod = "LoadLevel";
    void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
}
