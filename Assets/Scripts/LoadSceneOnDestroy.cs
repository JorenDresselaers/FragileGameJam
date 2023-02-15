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
    [SerializeField] private bool _triggeredAutomatically = false;

    private bool _objectAssigned = false;
    private bool _hasTriggered = false;

    void Awake()
    {
        if (_triggerWhenDestroyed) _objectAssigned = true;
    }

    void Update()
    {
        if (_objectAssigned && !_triggerWhenDestroyed && !_hasTriggered && _triggeredAutomatically)
        {
            _hasTriggered = true;
            Invoke(LoadLevelMethod, _timeToLoad);
        }
    }

    void OnDestroy()
    {
        if ((!_triggerWhenDestroyed || !_objectAssigned) && _triggeredAutomatically)
        {
            SceneManager.LoadScene(_levelName);
        }
    }

    private const string LoadLevelMethod = "LoadLevel";
    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
}
