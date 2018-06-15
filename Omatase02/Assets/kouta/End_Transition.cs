using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Transition : MonoBehaviour {

    /*********************************************
     * 変数の宣言
    ***********************************************/

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

    public void EndDown () {
        Invoke("SceneTransition", 1.2f);
        ShutterAnim.SetBool("NextShutter", true);
        
    }
    public void SceneTransition()
    {
        SceneManager.LoadScene("End_Scene");
    }
}
