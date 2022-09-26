using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PPOAgent : Agent
{
    [SerializeField] ElementBar playerElement, enemyElement;

    int value;

    //Set AI to observe element varaibles to ensure the AI knows what to look for
    public override void CollectObservations(VectorSensor sensor)
    {
        //Observe player hit enemies element
        sensor.AddObservation(playerElement.maxValue);
        sensor.AddObservation(playerElement.minBorder);
        sensor.AddObservation(playerElement.maxBorder);
        sensor.AddObservation(playerElement.monitoredTotal.total);

        //Observe enemies hit player element
        sensor.AddObservation(enemyElement.maxValue);
        sensor.AddObservation(enemyElement.minBorder);
        sensor.AddObservation(enemyElement.maxBorder);
        sensor.AddObservation(enemyElement.monitoredTotal.total);
    }

    //AI will ouput action here and send suggested room to director
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log(vectorAction);
        foreach (float action in vectorAction)
        {
            
            if (value != action)
                value = (int)action;
        }
    }
    
    //Director will call this function to assigned reward to PPO AI
    public void RewardAgent(float reward)
    {
        SetReward(reward);
    }

    public int ActionValue()
    {
        return value;
    }
}
