using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : ScriptableObject
{
    HashSet<PerceptionStimuli> currentlyPercievableStimulis = new HashSet<PerceptionStimuli>();
    public GameObject Owner
    {
        get;
        private set;
    }

    public void Init(GameObject owner)
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
                Debug.Log($"I sensed: {stimuli.gameObject.name}");
            }

            if(!IsStimuliSensable(stimuli) && IsStimuliSensed(stimuli))
            {
                currentlyPercievableStimulis.Remove(stimuli);
                Debug.Log($"I lost track of: {stimuli.gameObject.name}");
            }
        }
    }

    private bool IsStimuliSensed(PerceptionStimuli stimuli)
    {
        return currentlyPercievableStimulis.Contains(stimuli); //constant time O(1)
    }

    public abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    public virtual void DrawDebug()
    {

    }















    static HashSet<PerceptionStimuli> registeredStimulis;
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Add(stimuli);
    }
    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }
}
