using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    private void Start()
    {
        StartCoroutine(StopBullet());
    }
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    IEnumerator StopBullet() //i waiting durign 4 seconds before deleting myself
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) //i destroying myself when i enter in colision with everything
    {
        Destroy(gameObject);
    }
}