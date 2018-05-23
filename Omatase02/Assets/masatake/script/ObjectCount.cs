using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCount : MonoBehaviour {

    Text Timer;
    Text Remaining;
    Text FoodObject;
    GameObject CompletionButton;
    private int foodcnt = 0;//食材の残りの数
    const int REMAINING = 2;
    [SerializeField]private float timelimit = 9.9f;
    [SerializeField] private string[] Order = new string[4];
    [SerializeField] private int foodflg = 99;//0でフルーツ、1で機械、2で肉、3で野菜、99で再取得(指定する種類を取得)
    [SerializeField] private int playerflg = 99;//同上(プレイヤーが持っている種類)
    [SerializeField] private bool flg = false;

    // Use this for initialization
    void Start () {
        this.Timer = GameObject.Find("Timer").GetComponent<Text>();
        this.Remaining = GameObject.Find("remaining").GetComponent<Text>();
        this.FoodObject = GameObject.Find("FoodOrder").GetComponent<Text>();
        this.Order[0] = "フルーツジュース";
        this.Order[1] = "機械ジュース";
        this.Order[2] = "肉ジュース";
        this.Order[3] = "野菜ジュース";
        this.CompletionButton = GameObject.Find("CompletionButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            MainProcess();
        }
        else if(flg)
        {
            PerformanceTime();
        }

    }

    private void MainProcess()
    {
        if (foodflg == playerflg && Input.GetMouseButtonUp(0))
        {
            foodcnt++;
            playerflg = 99;
        }
        else if (foodflg != playerflg && playerflg != 99 && Input.GetMouseButtonUp(0))
        {
            foodflg = 99;
            playerflg = 99;
        }

        TimeCount();

        if (foodflg > 4 || this.timelimit <= 0)
        {
            OrderReset();
        }

        if ((REMAINING - foodcnt) > 0)
        {
            Remaining.text = (REMAINING - this.foodcnt).ToString("F0");
        }
        else
        {
            Remaining.text = "OK";
            CompletionButton.GetComponent<Completion>().InteractableChange();
        }
    }

    private void TimeCount()
    {
        //if (this.timelimit <= 0)
        //{
        //    this.timelimit = 9.9f;
        //}

        this.timelimit -= Time.deltaTime;

        Timer.text = this.timelimit.ToString("F1");
    }

    private void OrderCount()//新たな注文を取得
    {
        foodcnt = 0;
        foodflg = Random.Range(0, 4);
        FoodObject.text = this.Order[foodflg];
        Debug.Log(Order[foodflg]);
        Debug.Log(foodcnt);
    }

    private void PerformanceTime()
    {
        OrderCount();
        Invoke("CursleFlagChange",2);
        flg = false;
        Debug.Log("PerformanceTime");
    }

    private void CursleFlagChange()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OrderReset()
    {
        this.timelimit = 9.9f;
        Debug.Log("TimeReset");
        Debug.Log(timelimit);
        Cursor.lockState = CursorLockMode.Locked;
        flg = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //円の中に入った時にどのフルーツが入ったか確認する
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
