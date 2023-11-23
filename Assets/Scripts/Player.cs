using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private GameObject laserPrefab;
    public Vector3 laserOffset = new Vector3(0, 0.8f, 0);

    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    private int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0,0,0);
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            ShootLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.6f), 0);

        if (transform.position.x > 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        lives--;
        
        if(lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
