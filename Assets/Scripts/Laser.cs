using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // speed variable
    [SerializeField]
    private float speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        // translate laser up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
