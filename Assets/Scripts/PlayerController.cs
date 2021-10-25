using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rd2d;
	private SpriteRenderer sprite;

	public int tpScore = 4;
	public int winScore = 8;
	public int startLives = 3;
	public float speed;
	public float jumpForce = 3;
	public Text scoreText;
	public Text winText;
	public Text livesText;
	public AudioSource sfxPlayer;
	public GameObject levelTwoTp;

	private int score = 0;
	private int lives = 3;
	private bool playing = true;
	private bool won = false;
	private bool onLevelTwo = false;

	void Start()
	{
		rd2d = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(1, 1, 1, 1);
		score = 0;
		lives = startLives;
		scoreText.text = "Score: " + score.ToString();
		livesText.text = "Lives: " + lives.ToString();
		winText.gameObject.SetActive(false);
		playing = true;
		won = false;
		onLevelTwo = false;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Log("Escape pressed. Application Ended.");
		}
		else if (Input.GetKeyDown(KeyCode.H)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void FixedUpdate()
	{
		if (playing)
		{
			float hozMovement = Input.GetAxis("Horizontal");
			rd2d.AddForce(new Vector2(hozMovement * speed, 0));
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (playing)
		{
			if (collision.collider.tag == "Coin")
			{
				score += 1;
				scoreText.text = "Score: " + score.ToString();
				Destroy(collision.collider.gameObject);

				if (!onLevelTwo && score >= tpScore) {
					lives = startLives;
					livesText.text = "Lives: " + lives.ToString();
					transform.position = levelTwoTp.transform.position;
					onLevelTwo = true;
				}

				if (score >= winScore)
				{
					won = true;
					winText.text = "You Won!\nGame by: Toby Martin";
					winText.gameObject.SetActive(true);
					sfxPlayer.Play();
				}
			}
			if (!won && collision.collider.tag == "Enemy")
			{
				lives -= 1;
				livesText.text = "Lives: " + lives.ToString();
				Destroy(collision.collider.gameObject);

				if (lives <= 0)
				{
					playing = false;
					sprite.color = new Color(1, 1, 1, 0);
					winText.text = "You Lost!\nGame by: Toby Martin";
					winText.gameObject.SetActive(true);
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (playing && collision.collider.tag == "Ground") {
			if (Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)) {
				rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			}
		}
	}
}
