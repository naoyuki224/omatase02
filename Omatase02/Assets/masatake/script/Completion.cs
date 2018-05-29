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
        OB = GameObject.Find("Mixer").GetComponent<ObjectCount>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    //完成ボタンのON/OFFの切り替え
    public void InteractableChangeTrue()
    {
        CompletionButton.interactable = true;
    }

    public void InteractableChangeFalse()
    {
        CompletionButton.interactable = false;
    }

    //完成ボタンが押されたときにOrderResetを呼び出す関数
    public void ResetButton()
    {
        OB.OrderReset();
    }
}
