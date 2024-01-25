using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public interface ISaveable
{
    public void OnTrack();

    public void OnReset();
}

public class SaveManager : MonoBehaviour
{
    public UnityEvent onSave = new UnityEvent();
    public UnityEvent onReset = new UnityEvent();

    public static SaveManager Instance;

    public List<ISaveable> trackList = new List<ISaveable>();
    private List<ISaveable> savedList = new List<ISaveable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Begins tracking a GameObject whose state should be saved.
    /// </summary>
    /// <param name="objectToTrack"></param>
    public void Track<T>(T objectToTrack) where T : ISaveable
    {
        trackList.Add(objectToTrack);
    }

    /// <summary>
    /// Saves the states of the currently tracked objects to the permanent state of the game
    /// </summary>
    public void Save()
    {
        savedList.AddRange(trackList);
        trackList.Clear();
        onSave.Invoke();
    }

    /// <summary>
    /// Clears the currently tracked objects and resets them to their original state
    /// </summary>
    public void ResetTracked()
    {
        trackList.ForEach(obj => obj.OnReset());
        onReset.Invoke();
    }
}
