using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public List<string> currentWords = new List<string>();
    public List<GameObject> currentHazards = new List<GameObject>();
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public InputField inputField;
    public Done_PlayerController playerController;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void OnGUI()
    {
        if (inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            CheckWord(inputField.text);
            inputField.text = "";
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }

    private void CheckWord(string inputWord)
    {
        if (currentWords.Count == 0)
            return;

        if (currentWords.Contains(inputWord))
        {
            foreach (GameObject hazard in currentHazards)
            {
                TextMeshPro textMeshPro = hazard.gameObject.GetComponentInChildren<TextMeshPro>();
                if (textMeshPro.text == inputWord)
                {
                    playerController.Fire(hazard.transform.position);
                    break;
                }
            }
            currentWords.Remove(inputWord);
            CheckWord(inputWord);
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject currentHazard = Instantiate(hazard, spawnPosition, spawnRotation);
                currentHazards.Add(currentHazard);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}