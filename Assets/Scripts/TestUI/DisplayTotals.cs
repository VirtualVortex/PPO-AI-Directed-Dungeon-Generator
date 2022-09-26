using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayTotals : MonoBehaviour
{
    [SerializeField] FloatVariable playerTotal, enemyTotal;
    [SerializeField] Text playerTotalText, enemyTotalText, sceneName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayData();
    }

    public void DisplayData()
    {
        playerTotalText.text = playerTotal.total.ToString();
        enemyTotalText.text = enemyTotal.total.ToString();
        sceneName.text = SceneManager.GetActiveScene().name;
    }
    
}
