using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatics
{
    static GameObject playerGameObject;

    public static void SetPlayerGameObject(GameObject player)
    {
        playerGameObject = player;
    }

    public static GameObject GetPlayerGameObject()
    {
        if(playerGameObject == null)
        {
            playerGameObject = GameObject.FindObjectOfType<PlayerCharacter>().gameObject;
        }
        return playerGameObject;
    }
}
