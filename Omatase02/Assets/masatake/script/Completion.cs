using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Completion : MonoBehaviour {

    Button CompletionButton;
    ObjectCount OB;

    // Use this for initialization
    void Start ()
    {
        CompletionButton = GetComponent<Button>();
        OB = GameObject.Find("mixer").GetComponent<ObjectCount>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    //カーソルロックの切り替え
    public void InteractableChange()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            CompletionButton.interactable = true;
        }
        else if(Cursor.lockState == CursorLockMode.Locked)
        {
            CompletionButton.interactable = false;
        }
    }

    public void ResetButton()
    {
        OB.OrderReset();
    }
}
