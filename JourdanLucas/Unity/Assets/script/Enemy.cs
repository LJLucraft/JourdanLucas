using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public
    public Waypoints waypoints;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float timeToReaload = 1;
    public GameObject partToRotate;
    public float speed = 5f;
    public float speedRot = 0.5f;
    public float turretSpeed = 1f;
    public Transform targetPlayer;

    //private
    Vector3 dir;
    Vector3 player;
    Quaternion rotDir;
    Quaternion rot;
    private int wavePointIndex = 0;
    private Transform target;


    void Update()
    {
        FollowThePlayer();
    }

    private void Start()
    {
        target = waypoints.points[0];

        StartCoroutine(BrainIA());
        StartCoroutine(FireBrain());
    }

    IEnumerator BrainIA() //this is the brain of the enemy, it's only a loop. I wanted to use the Coroutine to make a tests and waiting for a few time
    {
        do
        {
            rotDir = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotDir, speedRot / 100f);

            dir = (target.position - transform.position).normalized;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= 0.4)
            {
                GetNextPoint();
            }
            yield return null;
        } while (true);
    }

    private void GetNextPoint() //if the enemy reach the point, they going on the next point
    {
        if (wavePointIndex >= waypoints.points.Length - 1)
        {
            target = waypoints.points[0];
            wavePointIndex = 0;
        }
        else
        {
            wavePointIndex++;
            target = waypoints.points[wavePointIndex];
        }
    }

    private void FollowThePlayer() //the enemy follows the player by sight
    {
        player = (targetPlayer.position - transform.position).normalized;
        rot = Quaternion.LookRotation(player);
        partToRotate.transform.rotation = Quaternion.Slerp(transform.rotation, rot, turretSpeed);
    }

    IEnumerator FireBrain() //if the player press fire (right ou left clic)
    {
        do
        {
                Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

                yield return new WaitForSeconds(timeToReaload);

            yield return new WaitForSeconds(0);

        } while (true);
    }
} 