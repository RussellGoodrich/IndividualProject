using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
	public AudioSource musicAudioSource;
	public AudioSource rulesAudioSource;
	public GameObject player;
	private MovementScript move_script;
	public Text winText;
	public Image winBox;
	public Text rulesText;
	public Image rulesBox;
	Rigidbody2D playerBody;
	public Text timeCheck;
	public float time;
	public float death;
	
    // Start is called before the first frame update
    void Start()
    {
		move_script = player.GetComponent<MovementScript>();
		StartCoroutine(Waiting());
		rulesAudioSource.Play();
		playerBody = player.GetComponent<Rigidbody2D>();
		time = 0;
		death = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_script.points >= 3)
		{
			musicAudioSource.Stop();
			winText.enabled = true;
			winBox.enabled = true;
		}
		
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		
    }
	
	IEnumerator Waiting()
		{
			yield return new WaitForSeconds(2);
			rulesText.enabled = false;
			rulesBox.enabled = false;
			musicAudioSource.Play();
			playerBody.gravityScale = 5f;
			move_script.speed = 0;
			move_script.jump = 20;
			move_script.start = 1;
			for(;;) 
			{
				yield return new WaitForSeconds(1);
				time += 1;
				if (death == 0 && move_script.points < 3)
				{
					timeCheck.text = "Time: " + time;
				}
			}
		}
}
