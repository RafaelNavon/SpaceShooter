using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int powerupID;
    //[SerializeField] private GameObject tripleShotPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                switch (powerupID)
                {
                    case 0: _player.TripleShotActive(); break;
                    case 1: _player.SpeedPowerupActive(); break;
                    case 2: _player.ShieldActive(); break;
                    default: return;
                }
            }
            Destroy(this.gameObject);
            //Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
    }
}
