using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageHandler : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] private int m_TrackedImageIndex;
    [SerializeField] private GameObject m_TrackedImagePrefab;

    
    private GameObject _prefabInstance;
    private bool _initialized;
    private Guid _guid;
    private string _name;
    
    
    void OnEnable()
    {
        _guid = m_TrackedImageManager.referenceLibrary[m_TrackedImageIndex].guid;
        _name = m_TrackedImageManager.referenceLibrary[m_TrackedImageIndex].name;
        
        m_TrackedImageManager.trackedImagesChanged += OnChanged;
    }

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        for (var i = 0; i < eventArgs.added.Count; i++)
        {
            var newImage = eventArgs.added[i];
            if (!_initialized && newImage.referenceImage.name == _name)
            {
                _prefabInstance = Instantiate(m_TrackedImagePrefab, newImage.transform);
                _initialized = true;
            }
        }

        for (var i = 0; i < eventArgs.updated.Count; i++)
        {
            var updatedImage = eventArgs.updated[i];
            if (_initialized && updatedImage.referenceImage.name == _name)
                _prefabInstance.SetActive(updatedImage.trackingState == TrackingState.Tracking);
        }
    }

}
