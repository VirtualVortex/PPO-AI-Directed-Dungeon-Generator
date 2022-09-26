using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToTeleporter : MonoBehaviour
{
    [SerializeField] Transform player, teleporter;
    [SerializeField] Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = Mathf.RoundToInt(Vector3.Distance(player.position, teleporter.position)).ToString();
    }
}
