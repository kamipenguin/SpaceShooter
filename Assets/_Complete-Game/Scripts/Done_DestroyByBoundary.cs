using UnityEngine;
using System.Collections;
using TMPro;

public class Done_DestroyByBoundary : MonoBehaviour
{
    private Done_GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<Done_GameController>();
    }

	void OnTriggerExit (Collider other)
	{
	    Transform parent= other.gameObject.transform.parent;
	    if (parent == null)
	        Destroy(other.gameObject);
	    else
	    {
	        TextMeshPro textMeshPro = parent.GetComponentInChildren<TextMeshPro>();
            gameController.currentWords.Remove(textMeshPro.text);
	        gameController.currentHazards.Remove(parent.gameObject);
	        Destroy(parent.gameObject);
        }
    }
}