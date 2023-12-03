using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float speedMultiplay = 2f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    public Vector3 laserOffset = new Vector3(0, 1.05f, 0);

    [SerializeField] private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField] private int lives = 3;
    private SpawnManager spawnManager;
    [SerializeField] private bool isTripleShotActive = false;
    private bool isShieldActive = false;
    [SerializeField] private GameObject shieldBubble;
    //[SerializeField] private bool isSpeedEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0,0,0);
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        shieldBubble.SetActive(false);
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

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

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

        if (isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            shieldBubble.SetActive(false);
            return;
        }
        lives--;

        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void SpeedPowerupActive()
    {
        //isSpeedEnabled = true;
        speed *= speedMultiplay;
        StartCoroutine(SpeedPowerUpDownRoutine());
    }

    IEnumerator SpeedPowerUpDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        //isSpeedEnabled = false;
        speed /= speedMultiplay;
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        shieldBubble.SetActive(true);
    }
}
