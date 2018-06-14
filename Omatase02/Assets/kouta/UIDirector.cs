using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UIを使用可能にする

public class UIDirector : MonoBehaviour {

    /*****************************
     * 変数の宣言
    *****************************/

    private float speed = 0.05f;  //透明化の速さ
    private float red, green, blue;    //RGBを操作するための変数

    private GameObject Press;

    //ボタンそれぞれの入れ物
    private GameObject StartButton;
    private GameObject RankingButton;
    private GameObject EndButton;
    private GameObject HelpON;
    private GameObject HelpOFF;

    private float ButtonAlpha;      //ボタンのアルファ値

    

    // Use this for initialization
    void Start () {
        //色を取得
        this.Press = GameObject.Find("PRESS");
        this.red = this.Press.GetComponent<Image>().color.r;
        this.green = this.Press.GetComponent<Image>().color.g;
        this.blue = this.Press.GetComponent<Image>().color.b;

        //スタート、ランキング、エンドボタンの初期設定
        this.ButtonAlpha = 0.0f;        //アルファ値設定

        this.StartButton = GameObject.Find("Start");
        this.RankingButton = GameObject.Find("Ranking");
        this.EndButton = GameObject.Find("End");
        this.HelpON = GameObject.Find("HelpON");
        this.HelpOFF = GameObject.Find("HelpOFF");

        this.StartButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
        this.RankingButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
        this.EndButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
        this.HelpON.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
        this.HelpOFF.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);

        this.StartButton.SetActive(false);
        this.RankingButton.SetActive(false);
        this.EndButton.SetActive(false);
        this.HelpON.SetActive(false);
        this.HelpOFF.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Press.activeInHierarchy == false)
        {
            this.StartButton.SetActive(true);
            this.RankingButton.SetActive(true);
            this.EndButton.SetActive(true);
            this.HelpON.SetActive(true);
            this.HelpOFF.SetActive(true);
        }
        if (StartButton.activeInHierarchy == true)
        {
            this.StartButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
            this.RankingButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
            this.EndButton.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
            this.HelpON.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
            this.HelpOFF.GetComponent<Image>().color = new Color(this.red, this.green, this.blue, this.ButtonAlpha);
            ButtonAlpha += speed;
            //Debug.Log(ButtonAlpha);
        }
    }
}
