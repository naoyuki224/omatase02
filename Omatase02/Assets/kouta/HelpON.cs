using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpON : MonoBehaviour {

    public int HelpFlg = 0;

    // Use this for initialization
 //   void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void HelpONDown()
    {
        HelpFlg = 1;
        Debug.Log(HelpFlg);
    }

    public void HelpOFFDown()
    {
        HelpFlg = 0;
        Debug.Log(HelpFlg);
    }
}
