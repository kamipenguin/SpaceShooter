using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public InputField inputField;
    public Done_PlayerController playerController;

    void OnGUI()
    {
        if (inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            CheckWord();
            inputField.text = "";
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }

    private void CheckWord()
    {
        //playerController.Fire();
    }
}
