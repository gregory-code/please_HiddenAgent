using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] Sense[] startSenses;

    List<Sense> instantiatedSenses = new List<Sense>();

    private void Awake()
    {
        foreach(var sense in startSenses)
        {
            Sense newSense = ScriptableObject.Instantiate(sense);
            instantiatedSenses.Add(newSense);
            newSense.Init(this);
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
    }
}
