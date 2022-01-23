using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
	public AudioSource dieAudioSource;
	public GameObject masterObject;
	private AudioSource musicSource;
	private WinCondition winCondition;
	public Text loseText;
	public Image loseBox;
	public GameObject player;
	private MovementScript movementScript;
	
    // Start is called before the first frame update
    void Start()
    {
        musicSource = masterObject.GetComponent<AudioSource>();
		winCondition = masterObject.GetComponent<WinCondition>();
		movementScript = player.GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" && movementScript.points != 3)
		{
			Destroy(coll.gameObject);
			dieAudioSource.Play();
			musicSource.Stop();
			loseText.enabled = true;
			loseBox.enabled = true;
			winCondition.death = 1;
		}
	}
}
