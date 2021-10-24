using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rd2d;

	public int winScore;
	public float speed;
	public float jumpForce = 3;
	public Text scoreText;

	private int score = 0;

	void Start()
	{
		rd2d = GetComponent<Rigidbody2D>();
		score = 0;
		scoreText.text = "Score: " + score.ToString();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Log("Escape pressed. Application Ended.");
		}
	}

	void FixedUpdate()
	{
		float hozMovement = Input.GetAxis("Horizontal");
		rd2d.AddForce(new Vector2(hozMovement * speed, 0));
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Coin") {
			score += 1;
			scoreText.text = "Score: " + score.ToString();
			Destroy(collision.collider.gameObject);

			if (score >= winScore) {
				scoreText.text = "You Won!\nGame by: Toby Martin\nPress Esc to Exit";
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground") {
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) {
				rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			}
		}
	}
}
