using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAI : MonoBehaviour
{
    AIMotion [] aiMotion;

    void Start()
    {
        aiMotion = FindObjectsOfType<AIMotion>();
    }

    public void UpdateMassiveAnts()
    {
        aiMotion = FindObjectsOfType<AIMotion>();
    }

    public void SetDestination()
    {
        foreach(AIMotion ai in aiMotion)
        {
            ai.agent.SetDestination(transform.position);
        }
        
    }

    void Update()
    {
        
    }
}
