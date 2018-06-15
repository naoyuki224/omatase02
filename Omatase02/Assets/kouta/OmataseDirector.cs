using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmataseDirector : MonoBehaviour {

    /*****************************
     * 変数の宣言
    *****************************/
    private GameObject Omatase;     //ゲームオブジェクト Omataseを格納
    private PleaseFade AnimeInfo;   //PleaseFadeからのアニメーションの情報を格納する変数
    private Animator OmataseFlg;    //Omataseがフェードインするための切り替え用変数 アニメーショントリガーのBool値を格納

    // Use this for initialization
    void Start()
    {
        this.Omatase = GameObject.Find("Omatase");
        this.OmataseFlg = Omatase.GetComponent<Animator>();
        this.AnimeInfo = GameObject.Find("PRESS").GetComponent<PleaseFade>();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.AnimeInfo.animinfo.normalizedTime > 1.0f){
            OmataseFlg.SetBool("OmataseFadeIn", true);
        }
    }
}
