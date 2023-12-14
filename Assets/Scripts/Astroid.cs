using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3.0f;
    [SerializeField] private GameObject explosionPrefab;
    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    //check for LASER collission (Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(explosion, 2.8f);
            spawnManager.StartSpawing();
            Destroy(this.gameObject, 0.3f);
        }
    }
    //instantiate explosion at the position of the astroid (us)
    //destroy the explosion after 3 seconds
}
