using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShutter : MonoBehaviour {

    /*****************************
     * 変数の宣言
    *****************************/

    private Animator ShutterAnim;
    private GameObject Shutter;

    // Use this for initialization
    void Start () {
        this.Shutter = GameObject.Find("Shutter");
        ShutterAnim = Shutter.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
