using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UIを使用可能にする

public class PleaseFade : MonoBehaviour {

    /*****************************
     * 変数の宣言
    *****************************/

    private float speed = 0.018f;  //透明化の速さ
    private float alfa;    //A値を操作するための変数
    private float red, green, blue;    //RGBを操作するための変数
    private int FadeUPDOWNFlg = 0;  //フェードインアウトの切り替え用変数
    private GameObject shutter;
    public AnimatorStateInfo animinfo;
    private bool Startflg = false;
    private GameObject Press;

    // Use this for initialization
    void Start () {
        //色を取得
        this.Press = GameObject.Find("PRESS");
        this.red = this.Press.GetComponent<Image>().color.r;
        this.green = this.Press.GetComponent<Image>().color.g;
        this.blue = this.Press.GetComponent<Image>().color.b;
        this.shutter = GameObject.Find("Shutter");
        
        //this.animation = GameObject.Find("Shutter").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        this.Press.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        animinfo = shutter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if(animinfo.normalizedTime > 1.0f){
            Startflg = true;
        }
        
        if(Startflg == true){

            if (FadeUPDOWNFlg == 0){
                alfa += speed;

                if (alfa >= 1.0f){
                    FadeUPDOWNFlg = 1;

                }

            } else if (FadeUPDOWNFlg == 1){
                alfa -= speed;

                if (alfa <= 0.0f){
                    FadeUPDOWNFlg = 0;
                }
            }

            if (Input.GetMouseButtonDown(0)){
                Press.SetActive (false);
            }

        }
    }
}
