using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodReset : MonoBehaviour {
    public bool ResetFlg = false;
    Button CompletionButton;

	// Use this for initialization
	void Start () {
        CompletionButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    //リセットボタンが押されたときに食材をリセットする
    public void ChengeResetFlgTrue()
    {
        ResetFlg = true ;
        Invoke("ChengeResetFlgFalse",0.1f);
    }

    public void ChengeResetFlgFalse()
    {
        ResetFlg = false;
    }

    //リセットボタンのON/OFFの切り替え
    public void InteractableChangeTrue()
    {
        CompletionButton.interactable = true;
    }

    public void InteractableChangeFalse()
    {
        CompletionButton.interactable = false;
    }

}
