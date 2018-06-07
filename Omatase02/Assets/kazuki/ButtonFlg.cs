using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlg : MonoBehaviour {

    GameObject LeftButtonFlg;
    GameObject RightButtonFlg;
    public void RCloseButton()
    {
        RightButtonFlg.SetActive(false);
        LeftButtonFlg.SetActive(true);
    }
    public void LCloseButton()
    {
        LeftButtonFlg.SetActive(false);
        RightButtonFlg.SetActive(true);
    }

    // Use this for initialization
    void Start ()
    {
        LeftButtonFlg = GameObject.Find("LeftButton").gameObject;
        RightButtonFlg = GameObject.Find("RightButton").gameObject;
        LeftButtonFlg.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
