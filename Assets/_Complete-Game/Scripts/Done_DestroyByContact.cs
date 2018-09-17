using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		    FindObjectOfType<InputField>().readOnly = true;
			gameController.GameOver();
		}
		else
		{
		    TextMeshPro textMeshPro = gameObject.GetComponentInChildren<TextMeshPro>();
            if (textMeshPro == null)
                Debug.Log("????");
		    gameController.currentWords.Remove(textMeshPro.text);
		    gameController.currentHazards.Remove(gameObject);
        }
		
		gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}