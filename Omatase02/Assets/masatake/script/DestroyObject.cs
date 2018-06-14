using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public GameObject[] Temporary;
    public FoodReset ResetMethod;
    public int count = 0;
    private Vector2 position;
    private bool flg = false;

    float ScaleX,ScaleY;//オブジェクトのスケールを保存する変数


    // Use this for initialization
    void Start () {
        position = this.transform.position;//生成したときに自分の位置を保存
        this.ResetMethod = GameObject.Find("ResetButton").GetComponent<FoodReset>();
        ScaleX = this.transform.localScale.x;
        ScaleY = this.transform.localScale.y;
        this.transform.localScale = new Vector3(0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if ((flg && Input.GetMouseButtonUp(0)) || ResetMethod.ResetFlg == true)//ミキサーに入ったとき、または、リセットボタンが押されたとき
        {
            GameObject CreateObject = GameObject.Find("position1");
            CreateObject.GetComponent<Create>().GenerationObject(position);//生成時に保存した位置にオブジェクトを生成
            Destroy(gameObject);
        }

        if(ScaleX > this.transform.localScale.x)
        {
            this.transform.localScale += new Vector3(0.05f, 0.05f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "sircle")
        //{
            flg = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "sircle")
        //{
            flg = true;
        //}
    }
}
