using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _force = 230f;
    private Rigidbody2D rb;
        
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
            rb.AddForce(_force * Vector2.up);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Obstacle"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}
}
