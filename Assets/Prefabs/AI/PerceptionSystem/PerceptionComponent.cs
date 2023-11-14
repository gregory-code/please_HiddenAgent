using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] Sense[] startSenses;

    List<Sense> instantiatedSenses = new List<Sense>();
    LinkedList<PerceptionStimuli> perceivedStimulis = new LinkedList<PerceptionStimuli>();

    public Action<GameObject> onTargetUpdated;

    void SetTarget(GameObject newTarget)
    {
        if(target != newTarget)
        {
            target = newTarget;
            onTargetUpdated?.Invoke(target);
        }
    }

    GameObject target;

    private void Awake()
    {
        foreach(var sense in startSenses)
        {
            Sense newSense = ScriptableObject.Instantiate(sense);
            instantiatedSenses.Add(newSense);
            newSense.Init(this);
            newSense.onPerceptionUpdated += PerceptionUpdated;
        }
    }

    private void PerceptionUpdated(PerceptionStimuli stimuli, bool successfullySensed)
    {
        //update the linked linked list
        var node = perceivedStimulis.Find(stimuli);
        if(successfullySensed)
        {
            if(node != null)
            {
                perceivedStimulis.AddAfter(node, stimuli);
            }
            else
            {
                perceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            perceivedStimulis.Remove(node);  
        }






        //determining if target need to be updated
        if (perceivedStimulis.Count != 0)
        {
            if (target == null || target != perceivedStimulis.First.Value)
            {
                SetTarget(perceivedStimulis.First.Value.gameObject);
            }
        }
        else
        {
            SetTarget(null);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var sense in instantiatedSenses)
        {
            sense.Update();
        }
    }

    private void OnDrawGizmos()
    {
        foreach(Sense sense in instantiatedSenses)
        {
            sense.DrawDebug();
        }

        if(target!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.transform.position, 1f);
            Gizmos.DrawLine(transform.position+Vector3.up, target.transform.position+Vector3.up);
        }
    }
}
