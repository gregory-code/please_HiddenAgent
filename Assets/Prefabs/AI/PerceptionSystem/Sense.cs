using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : ScriptableObject
{
    [SerializeField] float forgetTime = 2f;

    HashSet<PerceptionStimuli> currentlyPercievableStimulis = new HashSet<PerceptionStimuli>();
    Dictionary<PerceptionStimuli, Coroutine> currentForgettingCoroutines = new Dictionary<PerceptionStimuli, Coroutine>();
    public MonoBehaviour Owner
    {
        get;
        private set;
    }

    public void Init(MonoBehaviour owner)
    {
        Owner = owner; 
    }


    public void Update()
    {
        foreach(PerceptionStimuli stimuli in registeredStimulis)
        {
            if (IsStimuliSensable(stimuli) && !IsStimuliSensed(stimuli))
            { 
                currentlyPercievableStimulis.Add(stimuli);
                if(currentForgettingCoroutines.ContainsKey(stimuli))
                {
                    StopForgettingStimuli(stimuli);
                }
                else
                {
                    Debug.Log($"I sensed: {stimuli.gameObject.name}");
                }
            }

            if(!IsStimuliSensable(stimuli) && IsStimuliSensed(stimuli))
            {
                currentlyPercievableStimulis.Remove(stimuli);
                StartForgettingStimuli(stimuli);
            }
        }
    }

    private void StopForgettingStimuli(PerceptionStimuli stimuli)
    {
        Owner.StopCoroutine(currentForgettingCoroutines[stimuli]);
        currentForgettingCoroutines.Remove(stimuli);
    }

    private void StartForgettingStimuli(PerceptionStimuli stimuli)
    {
        Coroutine forgettingCoroutine = Owner.StartCoroutine(ForgettingCoroutine(stimuli));
        currentForgettingCoroutines.Add(stimuli, forgettingCoroutine); 
    }

    private IEnumerator ForgettingCoroutine(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        currentForgettingCoroutines.Remove(stimuli); //we have forgot it already, coroutine is done.
        Debug.Log($"I lost track of: {stimuli.gameObject.name}");
    }

    private bool IsStimuliSensed(PerceptionStimuli stimuli)
    {
        return currentlyPercievableStimulis.Contains(stimuli); //constant time O(1)
    }

    public abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    public virtual void DrawDebug()
    {

    }















    static HashSet<PerceptionStimuli> registeredStimulis = new HashSet<PerceptionStimuli>();
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Add(stimuli);
    }
    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }
}
