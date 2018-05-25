using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public GameObject[] Temporary;
    public FoodReset ResetMethod;
    private Vector2 position;
    private bool flg = false;

    // Use this for initialization
    void Start () {

        position = this.transform.position;//生成したときに自分の位置を保存
        this.ResetMethod = GameObject.Find("ResetButton").GetComponent<FoodReset>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((flg && Input.GetMouseButtonUp(0)) || ResetMethod.ResetFlg == true)//ミキサーに入ったとき、または、完成ボタンが押されたとき
        {
            GameObject CreateObject = GameObject.Find("position1");
            CreateObject.GetComponent<Create>().GenerationObject(position);//生成時に保存した位置にオブジェクトを生成
            Destroy(gameObject);
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
