using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using LevelGenerator.Scripts.Exceptions;
using LevelGenerator.Scripts.Helpers;
using LevelGenerator.Scripts.Structure;
using EmeraldAI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//This scrpit is a custom component 

public class AIDirector : MonoBehaviour
{
    public GameObject generatorObject;
    [SerializeField] int maxlayerLimit;
    [SerializeField] ElementBar[] elements;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] bool useAI, training;
    [SerializeField, Header("Rooms")] GameObject[] rooms;
    [SerializeField] GameObject corridor;

    LevelGenerator.Scripts.LevelGenerator generator;
    Vector3 levelPosition;
    int curLayer;
    PPOAgent agent;
    string sceneName;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //generator = generatorObject.GetComponent<LevelGenerator.Scripts.LevelGenerator>();
        agent = GetComponent<PPOAgent>();

        if (!training)
        {
            //levelPosition = generatorObject.transform.position;
            BuildNextLevel();
            //generator.BuildDungeon();
            //curLayer++;
            ResetTotal();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        curLayer = 1;
        if(!training) surface.BuildNavMesh();
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (SceneManager.GetActiveScene().name != sceneName && !SceneManager.GetActiveScene().name.Contains("LoadScene"))
        {
            //Reward();
            BuildNextLevel();
            sceneName = SceneManager.GetActiveScene().name;
        }

        //monitor do not use for training
        /*if(training)
            Reward();*/
    }

    //For training purposes
    void Reward() 
    {
        // for loop through each element to see if total is inside or out side border
        //Apply -1 or 1 reward based on total being inside or outside of border to agent
        int minValue = 0;

        foreach (ElementBar element in elements)
        {
            if (element.monitoredTotal.total < element.maxValue && element.monitoredTotal.total > 0)
            {
                if (element.monitoredTotal.total < element.maxBorder && element.monitoredTotal.total > element.minBorder)
                {
                    //bell curve
                    if (element.monitoredTotal.total >= ((element.minBorder + element.maxBorder) / 2))
                        minValue = element.maxBorder;
                    else
                        minValue = element.minBorder;

                    // the closer the total is to the middle of the border, the higher the reward
                    float rewardValue = Mathf.InverseLerp(minValue, ((element.minBorder + element.maxBorder) / 2), element.monitoredTotal.total);

                    Debug.Log(element.monitoredTotal.name + " In border " + rewardValue);
                    agent.SetReward(+1);
                    agent.EndEpisode();
                }
                else
                {
                    agent.SetReward(-1);
                    Debug.Log("out or border");
                }
            }
        }
    }

    void PickRooms(int room, LevelGenerator.Scripts.LevelGenerator curGenerator)
    {
        //pick rooms within range of parameter

        curGenerator.Sections.Add(rooms[room].GetComponent<LevelGenerator.Scripts.Section>());

        if(room > 0)
            curGenerator.Sections.Add(rooms[room - 1].GetComponent<LevelGenerator.Scripts.Section>());

        if(room < (rooms.Length -1))
            curGenerator.Sections.Add(rooms[room + 1].GetComponent<LevelGenerator.Scripts.Section>());

        curGenerator.Sections.Add(corridor.GetComponent<LevelGenerator.Scripts.Section>());
    }

    //call when player moves to next layer
    void BuildNextLevel()
    {
        if (curLayer < maxlayerLimit)
        {
            GameObject inst = Instantiate(generatorObject, new Vector3(0, -28.8999996f, 0), Quaternion.identity);
            LevelGenerator.Scripts.LevelGenerator instGenerator = inst.GetComponent<LevelGenerator.Scripts.LevelGenerator>();
            instGenerator.Seed = 0;
            

            //Use director output when it enters level 2 and 3 and if build is for test with AI
            if (useAI && !SceneManager.GetActiveScene().name.Contains("Level 1") && !training)
            {
                instGenerator.Sections.Clear();
                Debug.Log("Agent output: " + agent.ActionValue());
                PickRooms(agent.ActionValue(), instGenerator);
            }

            ResetTotal();
            instGenerator.BuildDungeon();
            levelPosition = inst.transform.position;
            surface.BuildNavMesh();
            curLayer++;
        }
    }

    //reset current total to prevent data contamination for new playthrough
    public void ResetTotal()
    {
        foreach (ElementBar element in elements)
        {
            element.ResetValues();
        }
    }
}
