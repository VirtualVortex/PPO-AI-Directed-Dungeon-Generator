using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCol : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            sceneChanger.MovePlayer();
        }
    }
}
