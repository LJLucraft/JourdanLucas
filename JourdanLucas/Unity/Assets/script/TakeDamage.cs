using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float damageTaken;
    public float health;
    public GameObject enemyManager;
    public GameObject playerManager;
    public bool enemy = false;

    public PlayerController playerController;

    public GameObject win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (health > 0)
            {
                health = health - damageTaken;
            }
        }
    }
    void Update()
    {
        if (enemy == false)
        {
            if (health <= 0)
            {
                win.SetActive(true);
                Destroy(playerManager);
                Destroy(enemyManager);
            }
        }
        else if (enemy == true)
        {
            if (health <= 0)
            {
                playerController.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
