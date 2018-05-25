using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCount : MonoBehaviour {

    Text Timer;
    Text Remaining;
    Text FoodObject;
    Text AmountText;
    Text TrustText;

    GameObject CompletionButton;
    GameObject ResetButton;

    private int foodcnt = 0;//食材の残りの数
    const int REMAINING = 5;
    const int price = 100;//スムージーの単価
    [SerializeField] private float timelimit = 9.9f;
    [SerializeField] private string[] Order = new string[4];
    [SerializeField] private int foodflg = 99;//0でフルーツ、1で機械、2で肉、3で野菜、99で再取得(指定する種類を取得)
    [SerializeField] private int playerflg = 99;//同上(プレイヤーが持っている種類)
    [SerializeField] private bool flg = false;
    public int SmoothieCount = 0;//完成させたスムージーの数
    public int TotalAmount = 0;//売上金額(入れた食材の数*100円)
    string AmountString;
    int TrustPer = 125;//信頼度
    int[] Trustarray = { 25, 50,75 };
    int TrustLevel = 0;

    // Use this for initialization
    void Start () {
        this.Timer = GameObject.Find("Timer").GetComponent<Text>();
        this.Remaining = GameObject.Find("remaining").GetComponent<Text>();
        this.FoodObject = GameObject.Find("FoodOrder").GetComponent<Text>();
        this.AmountText = GameObject.Find("TotalAmount").GetComponent<Text>();
        this.TrustText = GameObject.Find("Trust").GetComponent<Text>();

        this.Order[0] = "フルーツスムージー";
        this.Order[1] = "機械スムージー";
        this.Order[2] = "ミートスムージー";
        this.Order[3] = "野菜スムージー";

        this.CompletionButton = GameObject.Find("CompletionButton");
        this.ResetButton = GameObject.Find("ResetButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)//スムージーを作る時間
        {
            MainProcess();
        }
        else if(flg)//注文の切り替え
        {
            PerformanceTime();
        }

    }

    private void MainProcess()
    {
        if (foodflg == playerflg && Input.GetMouseButtonUp(0))//注文とミキサーに入れたものが同じ時
        {
            foodcnt++;
            playerflg = 99;
        }
        else if (foodflg != playerflg && playerflg != 99 && Input.GetMouseButtonUp(0))//注文とミキサーに入れたものが違うとき
        {
            foodflg = 99;
            playerflg = 99;
        }

        TimeCount();//時間の取得

        if (foodflg > 4 || this.timelimit <= 0)
        {
            OrderReset();//注文の切り替え
        }

        if ((REMAINING - foodcnt) > 0)//食材の残りの数の表示
        {
            Remaining.text = (REMAINING - this.foodcnt).ToString("F0");
        }
        else if(foodflg > 4)
        {
            Remaining.text = "Miss!";
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
        //Debug.Log(Order[foodflg]);
        //Debug.Log(foodcnt);
    }

    private void PerformanceTime()
    {
        OrderCount();
        Invoke("CursleFlagChange",2);//2秒後に操作可能にする
        flg = false;

        AmountString = this.TotalAmount.ToString("F0") + "円";
        AmountText.text = string.Format("{0,8}", AmountString);//売上金額の表示

        TrustText.text = this.TrustPer.ToString("F0") + "％";//

        //Debug.Log("PerformanceTime");
    }

    private void CursleFlagChange()//カーソル無効の解除
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OrderReset()//注文の切り替え
    {
        this.timelimit = 9.9f;//制限時間のリセット

        if(foodflg < 99 && REMAINING <= foodcnt)//注文のスムージーが完成したとき
        {
            SmoothieCount++;
            TrustPer += 10;
            if(TrustPer >= 100)
            {
                TrustPer = 100;
            }
            TotalAmount += price * foodcnt;//合計金額
            if(SmoothieCount != 0 && SmoothieCount % 5 == 0)//完成させたスムージーの数が5の倍数の時、リセットボタンを使用可能にする
            {
                ResetButton.GetComponent<FoodReset>().InteractableChangeTrue();
            }

            if(SmoothieCount % 10 == 0 && TrustLevel < 2)//完成させたスムージーの数が10の倍数の時、信頼度のレベルを上げる
            {
                TrustLevel++;
            }
        }
        else//注文と違う食材を入れたとき
        {
            TrustPer -= Trustarray[TrustLevel];
            if(TrustPer <= 0)
            {
                TrustPer = 0;
                Debug.Log("GameOver");
            }
        }

        Debug.Log(SmoothieCount);
        //Debug.Log("TimeReset");
        //Debug.Log(timelimit);
        
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
        //食材を持ったままミキサーから出たら、消さないようにする
        if (!Input.GetMouseButtonUp(0))
        {
            playerflg = 99;
        }
    }
}
