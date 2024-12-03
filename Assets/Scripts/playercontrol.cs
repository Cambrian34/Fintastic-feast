using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5.0f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audio2;

    [Header("State Management")]
    [SerializeField] private bool facingRight = false;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    //score
    [Header("Score")]
    public int score = 0;

    private void Update()
    {
        HandleMovement();
    }

    // Handle fish movement
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Apply movement
        Vector3 movement = new Vector3(moveX, moveY, 0);
        transform.position += movement;

        // Update facing direction
        if (moveX > 0 && facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && !facingRight)
        {
            Flip();
        }
    }

    // Flip the fish sprite when changing direction
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Reverse X scale to flip the sprite
        transform.localScale = scale;
    }



    //collision with enemy
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.collider.CompareTag("enemy")||collision.gameObject.tag =="Enemy(Clone)")

        {
            PlaySound();
            //go to main menu
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

        }
        if (collision.gameObject.tag == "food"||collision.gameObject.tag =="food(Clone)")
        {
            score+=10;
            Destroy(collision.gameObject);
            PlaySound();

        }
        
    }

    //play sound
    public void PlaySound()
    {
        audio2.Play();
    }
    
}
