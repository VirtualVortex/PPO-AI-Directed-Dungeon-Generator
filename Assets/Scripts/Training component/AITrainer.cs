using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using UnityEngine.UI;

public class AITrainer : MonoBehaviour
{
    public AIDirector director;
    public PPOAgent ppoAgent;
    [SerializeField, Range(0,9)] int testOuput;
    [SerializeField] bool increaseEnemyHitTotal, increasePlayerHitTotal;
    [SerializeField] Image currentEnemyTotalPanel, currentPlayerTotalPanel;
    [SerializeField] Text currentEnemyTotal, currentPlayerTotal, outputText, playerMinBorder, playerMaxBorder, playerMaxValue, enemyMinBorder, enemyMaxBorder, enemyMaxValue;

    [SerializeField] ElementBar[] elements;

    float currentOutput;
    

    private void Awake()
    {
        director.ResetTotal();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMinBorder.text = elements[0].minBorder.ToString();
        enemyMaxBorder.text = elements[0].maxBorder.ToString();
        enemyMaxValue.text = elements[0].maxValue.ToString();

        playerMinBorder.text = elements[1].minBorder.ToString();
        playerMaxBorder.text = elements[1].maxBorder.ToString();
        playerMaxValue.text = elements[1].maxValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Apply reward each frame based on current total

        UI();
        MeasureOuput();
        

    }

    void MeasureOuput() 
    {
        //increase or decrease current total (float variable) based on agent output and boolean settings
        //check total isn't smaller than zero or creater than element bar maximum

        int output = ppoAgent.ActionValue();
        //int output = testOuput;

        outputText.text = output.ToString();

        

        foreach (ElementBar element in elements)
        {
            //for current testing simply increase both float variable totals, in laster version give option to increase or decease
            if (element.monitoredTotal.total <= element.maxValue && element.monitoredTotal.total >= 0)
            {

                // interger values need to be encased into floats for division to work
                float totalAddative = ((float)output / (float)element.maxValue);

                if (output > currentOutput)
                {
                    element.monitoredTotal.total = Mathf.Clamp((element.monitoredTotal.total + 5.3f), 0, element.maxValue);
                    
                }
                else if (output < currentOutput)
                {
                    element.monitoredTotal.total = Mathf.Clamp((element.monitoredTotal.total - 5.3f), 0, element.maxValue);
                    
                }

                //element.monitoredTotal.total += totalAddative;
            }

            //safety measure in case total goes beyond boundaries
            if (element.monitoredTotal.total >= element.maxValue)
            {
                ppoAgent.EndEpisode();
            }
            else if (element.monitoredTotal.total <= 0)
                ppoAgent.EndEpisode();
        }
        currentOutput = output;

        Reward(elements[0], currentEnemyTotalPanel);
        Reward(elements[1], currentPlayerTotalPanel);
    }

    

    private void UI()
    {
        currentEnemyTotal.text = elements[0].monitoredTotal.total.ToString();
        currentPlayerTotal.text = elements[1].monitoredTotal.total.ToString();
    }

    void Reward(ElementBar element, Image panel)
    {
        // for loop through each element to see if total is inside or out side border
        //Apply -1 or 1 reward based on total being inside or outside of border to agent
        int minValue = 0;

        //Keep current total in bar 
        if (element.monitoredTotal.total < element.maxValue && element.monitoredTotal.total >= 0)
        {
            //Check current total is in border
            if (element.monitoredTotal.total <= element.maxBorder && element.monitoredTotal.total >= element.minBorder)
            {
                panel.color = Color.green;

                //bell curve
                if (element.monitoredTotal.total >= ((element.minBorder + element.maxBorder) / 2))
                    minValue = element.maxBorder;
                else
                    minValue = element.minBorder;

                // the closer the total is to the middle of the border, the higher the reward
                float rewardValue = Mathf.InverseLerp(minValue, ((element.minBorder + element.maxBorder) / 2), element.monitoredTotal.total);

                //Calculate and set reward
                Debug.Log(element.monitoredTotal.name + " In border " + rewardValue);
                ppoAgent.SetReward(rewardValue);
                ppoAgent.EndEpisode();
            }
            else
            {
                if (element.monitoredTotal.total < element.minBorder)
                {
                    ppoAgent.SetReward(-Mathf.InverseLerp(element.minBorder, 0, element.monitoredTotal.total));
                    //Debug.Log(-Mathf.InverseLerp(element.minBorder, 0, element.monitoredTotal.total));
                }
                else if (element.monitoredTotal.total > element.maxBorder)
                {
                    ppoAgent.SetReward(-Mathf.InverseLerp(element.maxBorder, element.maxValue, element.monitoredTotal.total));
                    //Debug.Log(-Mathf.InverseLerp(element.maxBorder, element.maxValue, element.monitoredTotal.total));
                }


                panel.color = Color.red;
                Debug.Log("out or border");
            }
        }

        /*foreach (ElementBar element in elements)
        {
            
        }*/

    }
}
