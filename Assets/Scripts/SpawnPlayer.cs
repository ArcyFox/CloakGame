using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject player;
    GameObject gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnPlayer() {

        gamePlayer.transform.position = transform.position;

    }

    public void spawnPlayer() {

        gamePlayer = GameObject.Instantiate(player);
        gamePlayer.transform.position = transform.position;

    }
}
