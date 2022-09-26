using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteraction : MonoBehaviour
{
    [SerializeField] FloatVariable enemtHitPlayerVar, playerHitEnemyVar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Allows the enemie AI to interacto with the float varaibles to ensure total is increased 
    public void IncreaseplayerHitEnemyVarTotal()
    {
        playerHitEnemyVar.total++;
        //Debug.LogWarning(playerHitEnemyVar.name + ": " + playerHitEnemyVar.total.ToString());
    }

    public void IncreaseenemtHitPlayerVarTotal()
    {
        enemtHitPlayerVar.total++;
        //Debug.LogWarning(enemtHitPlayerVar.name + ": " + enemtHitPlayerVar.total.ToString());
    }
}
