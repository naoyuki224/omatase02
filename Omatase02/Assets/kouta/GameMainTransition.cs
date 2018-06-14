using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;     //UIを使用可能にする

public class GameMainTransition : MonoBehaviour {

    /*********************************************
     * 変数の宣言
    ***********************************************/

    private Animator ShutterAnim;
    private GameObject Shutter;
    private GameObject getHelpOn;
    private HelpON HelpON;

    // Use this for initialization
    void Start () {
        this.Shutter = GameObject.Find("Shutter");
        ShutterAnim = Shutter.GetComponent<Animator>();
        this.getHelpOn = GameObject.FindGameObjectWithTag("HelpOn");
        this.HelpON = getHelpOn.GetComponent<HelpON>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StartDown ()
    {
        Invoke("SceneTransition", 1.2f);
        ShutterAnim.SetBool("NextShutter", true);
    }

    public void SceneTransition()
    {
        //this.HelpON = getHelpOn.GetComponent<HelpON>();
        Debug.Log(this.HelpON.HelpFlg);

        if (this.HelpON.HelpFlg == 1)
        {
            SceneManager.LoadScene("Help_Scene");
        }
        if (this.HelpON.HelpFlg == 0){
            Debug.Log("aaaaaa");
            SceneManager.LoadScene("GameMain_Scene");
        }
    }
}
