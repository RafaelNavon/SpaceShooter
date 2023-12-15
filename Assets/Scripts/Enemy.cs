using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;

    private GameObject enemyPrefab;
    private Player player;
    private Animator anim;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is NULL");
        }
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is null");
        }
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Enemy's audioSource is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7f, 0);
            //Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AddScore(10);
            }
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 2.0f);
        }
    }
}
