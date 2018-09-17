using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _textPrefab;
    private Done_GameController _gameController;
    private GameObject _textObj;
    private string _chosenword;
    private readonly List<string> _words = new List<string>()
        {
            "star", "space", "ship", "ceres", "asteroid", "comet",
            "planet", "sun", "moon", "earth", "galaxy", "jupiter",
            "mars", "venus", "meteor", "mercury", "neptune", "orb",
            "pluto", "satellite", "supernova", "world", "antimatter"
        };

    // Use this for initialization
    void Start ()
	{
	    _textObj = Instantiate(_textPrefab, transform);
	    TextMeshPro textMeshPro = _textObj.GetComponent<TextMeshPro>();
	    _chosenword = _words[Random.Range(0, _words.Count)];
	    textMeshPro.text = _chosenword;
	    _gameController = FindObjectOfType<Done_GameController>();
        _gameController.currentWords.Add(_chosenword);
	}
}
