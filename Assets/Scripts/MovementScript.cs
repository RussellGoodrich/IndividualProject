using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D rb2d;
	BoxCollider2D bc2d;
	public float speed;
	public float jump;
	bool onGround = false;
	public Transform onGroundCheck;
	public float checkGround;
	public LayerMask groundLayer;
	public float groundLast;
	float lastTime;
	public AudioSource hopAudioSource;
	public AudioSource landAudioSource;
	public AudioSource absorbAudioSource;
	public AudioSource winAudioSource;
	private Animator animator;
	private SpriteRenderer renderer;
	public int points = 0;
	public ParticleSystem particleSystem;
	public Text scoreMe;
	public float start;
	
	// Start is called before the first frame update
    void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
		bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		scoreMe.text = "Score: " + points;
		
		float x = Input.GetAxis("Horizontal");
		float moveX = x*speed;
		rb2d.velocity = new Vector2 (moveX, rb2d.velocity.y);
		
		if (Input.GetKeyDown(KeyCode.Space) && points < 3 && onGround || Time.time - lastTime <= groundLast) 
		{
			hopAudioSource.Play();
			rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
		}
		
		Collider2D collider = Physics2D.OverlapCircle(onGroundCheck.position, checkGround, groundLayer);
		if (collider != null) {
			onGround = true;
			speed = 0;
		} else {
			if (onGround) {
				lastTime = Time.time;
			}
			onGround = false;
			if (start >= 1)
			{
				speed = 8;
			}
		}
		
		if (Input.GetAxisRaw("Horizontal") > 0)
		{
			renderer.flipX = false;
		}
		else if (Input.GetAxisRaw("Horizontal") < 0)
		{
			renderer.flipX = true;
		}   
		
		if (points >= 3) {
			speed = 0;
			jump = 0;
		}
		
    }
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Coin")
		{
			Destroy(coll.gameObject);
			points += 1;
			absorbAudioSource.Play();
			particleSystem.Play();
			if (points >= 3)
			{
				winAudioSource.Play();
			}
		}
		
		if (coll.gameObject.tag == "Ground")
		{
			landAudioSource.Play();
		}
	}
	
}
