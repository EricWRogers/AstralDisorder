using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Audio;
using OmnicatLabs.Timers;


public class MonsterAudio : MonoBehaviour
{
    public float growlIntervalMin = 20f;
    public float growlIntervalMax = 45f;

    public float stepInterval = .3f;

    [HideInInspector]
    public Timer timer;
    [HideInInspector]
    public Timer footstepTimer;
    //private void PostPlay()
    //{
    //    var rand = RandTime();
    //    TimerManager.Instance.CreateTimer(rand, () => AudioManager.Instance.Play("MonsterGrowl"), true);
    //    //TimerManager.Instance.CreateTimer(stepInterval, PlayFootstep, true);
    //}

    public void StartGrowls()
    {
        var rand = RandTime();
        TimerManager.Instance.CreateTimer(rand, () => AudioManager.Instance.Play("MonsterGrowl"), true);
    }

    private float RandTime()
    {
        return Random.Range(growlIntervalMin, growlIntervalMax);
    }

    public void PlayFootStep()
    {
        AudioManager.Instance.Play("MonsterFootstep", gameObject);
    }

    //private void PlayFootstep()
    //{
    //    Debug.Log("Played");
    //    AudioManager.Instance.Play("MonsterFootstep", gameObject);
    //    //TimerManager.Instance.CreateTimer(stepInterval, PlayFootstep);
    //}

    //public void PlaySound()
    //{
    //    var rand = RandTime();
    //    AudioManager.Instance.Play("MonsterGrowl", gameObject);
    //    //TimerManager.Instance.CreateTimer(rand, PlaySound);
    //}
}
