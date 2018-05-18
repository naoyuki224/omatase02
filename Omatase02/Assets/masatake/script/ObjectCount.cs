using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCount : MonoBehaviour {

    GameObject Timer;
    GameObject Remaining;
    public int foodcnt = 5;
    float timelimit = 10.0f;
    private string[] Order = new string[4];
    private int foodflg = 99;//0でフルーツ、1で機械、2で肉、3で野菜、99で再取得(指定する種類を取得)
    private int playerflg = 99;//同上(プレイヤーが持っている種類)


    // Use this for initialization
    void Start () {
        this.Timer = GameObject.Find("Timer");
        this.Remaining = GameObject.Find("remaining");
        this.Order[0] = "フルーツジュース";
        this.Order[1] = "機械ジュース";
        this.Order[2] = "肉ジュース";
        this.Order[3] = "野菜ジュース";
    }

    // Update is called once per frame
    void Update()
    {
        if(foodflg == playerflg && Input.GetMouseButtonUp(0))
        {
            foodcnt--;
            playerflg = 99;
        }
        else if(foodflg != playerflg && playerflg != 99 && Input.GetMouseButtonUp(0))
        {
            foodcnt = 5;
            foodflg = 99;
            playerflg = 99;
        }

        TimeCount();

        if(foodflg > 4 || foodcnt == 0 || this.timelimit <= 0)
        {
            OrderCount();
            foodcnt = 5;
            timelimit = 10.0f;
        }
        this.Remaining.GetComponent<Text>().text = this.foodcnt.ToString("F0");
    }

    private void TimeCount()
    {
        if (this.timelimit <= 0)
        {
            this.timelimit = 9.9f;
        }

        this.timelimit -= Time.deltaTime;

        this.Timer.GetComponent<Text>().text = this.timelimit.ToString("F1");
    }

    public void OrderCount()
    {
        foodflg = Random.Range(0, 4);
        Debug.Log(Order[foodflg]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fruit")
        {
            playerflg = 0;
        }
        else if (collision.gameObject.tag == "machine")
        {
            playerflg = 1;
        }
        else if (collision.gameObject.tag == "meat")
        {
            playerflg = 2;
        }
        else if (collision.gameObject.tag == "vegetable")
        {
            playerflg = 3;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Input.GetMouseButtonUp(0))
        {
            playerflg = 99;
        }
    }
}
