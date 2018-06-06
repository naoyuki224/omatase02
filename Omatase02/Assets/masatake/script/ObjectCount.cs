using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCount : MonoBehaviour {

    enum Process
    {
        MAINPROCESS,
        BONUSTIME,
        FADEINPROCESS,
        FADEOUTPROCESS,
        CUSTOMERIN,
        CUSTOMEROUT,
        COMMENT,
        PERFORMANCETIME
    }

    //お客さんの画像
    public GameObject [] CustomerImage;

    //ミキサーのレンダラーと色の変数
    Renderer MixRenderer;
    Color MixColor;

    //ブラックホールのレンダラーと色の変数
    Renderer BlaRenderer;
    Color BlaColor;

    //宇宙人のレンダラーと色の変数
    Renderer AlienRenderer;
    Color AlienColor;

    //画像を切り替えるための変数
    GameObject MixerImage;
    GameObject BlackHoleImage;

    Text Timer;
    Text Remaining;
    Text FoodObject;
    Text AmountText;
    Text TrustText;

    GameObject CompletionButton;
    GameObject ResetButton;

    Rigidbody2D[] CustomerRigid;//お客さんのRigidbody

    const int TestValue = 100;

    const int REMAINING = 1;
    const int price = 100;//スムージーの単価
    const int ALLFOOD = 10;//全混ぜスムージーが発動するまでに完成させなければいけないスムージーの個数
    const float InvokeTime = 1.0f;//プレイヤーの待ち時間

    private int foodcnt = 0;//食材の残りの数
    private float timelimit = 9.9f;
    private string[] Order;
    private int foodflg = 99;//0でフルーツ、1で機械、2で肉、3で野菜、99で再取得(指定する種類を取得)
    private int playerflg = 99;//同上(プレイヤーが持っている種類)
    private bool Performanceflg = true;//PerformanceTime()に1回だけ入るための関数
    public int SmoothieCount = 0;//完成させたスムージーの数
    public int TotalAmount = 0;//売上金額(入れた食材の数*100円)
    int TrustPer = 100;//信頼度
    int[] Trustarray = { 25, 50,75 };//信頼度のレベルに応じた信頼度の減り方
    int TrustLevel = 0;//信頼度のレベル(隠しステータス)
    int ProcessFlg = (int)Process.PERFORMANCETIME;//場面の切り替え
    int W=4;//退避用変数
    float TimeMemory = 0;//一時的に時間を保存するための変数
    bool CustomerInflg = false;//お客さんの入店のフラグ
    bool CommentFlg = false;//コメントを一度だけ入るフラグ
    //お客さんのコメント(後から配列にしてコメントをいくつか用意する)
    string [] Goodcomment;
    string [] Badcomment;

    //お客さんがフェードイン、フェードアウトするときにどこまで移動するかの値
    float [] CustomerOutPosition;
    float[] CustomerInPosition;


    //ミキサーがフェードイン・フェードアウトするスピード
    const float speed = 0.01f;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;//カーソルをロック

        this.Timer = GameObject.Find("Timer").GetComponent<Text>();
        this.Remaining = GameObject.Find("remaining").GetComponent<Text>();
        this.FoodObject = GameObject.Find("FoodOrder").GetComponent<Text>();
        this.AmountText = GameObject.Find("TotalAmount").GetComponent<Text>();
        this.TrustText = GameObject.Find("Trust").GetComponent<Text>();

        this.CustomerImage = new GameObject[5];

        this.CustomerImage[0] = GameObject.Find("jk");
        this.CustomerImage[1] = GameObject.Find("robot");
        this.CustomerImage[2] = GameObject.Find("animal");
        this.CustomerImage[3] = GameObject.Find("man");
        this.CustomerImage[4] = GameObject.Find("utyuzinn");

        this.CustomerRigid = new Rigidbody2D[4];

        for(int i=0; i<4; i++)
        {
            CustomerRigid[i] = CustomerImage[i].GetComponent<Rigidbody2D>();
        }

        //お客さんが移動する範囲の値
        CustomerInPosition = new float[4];
        CustomerOutPosition = new float[4];

        CustomerInPosition[0] = -21.8f;
        CustomerInPosition[1] = -22.6f;
        CustomerInPosition[2] = -24.3f;
        CustomerInPosition[3] = -21.4f;

        CustomerOutPosition[0] = -28.7f;
        CustomerOutPosition[1] = -32.5f;
        CustomerOutPosition[2] = -31.5f;
        CustomerOutPosition[3] = -29.1f;

        this.Order = new string[5];

        this.Order[0] = "フルーツスムージーください！";
        this.Order[1] = "機械スムージーください！";
        this.Order[2] = "ミートスムージーください！";
        this.Order[3] = "野菜スムージーください！";
        this.Order[4] = "ゼンブマゼテ";

        this.Goodcomment = new string[5];
        this.Badcomment = new string[5];

        this.Goodcomment[0] = "うん、おいしい！";
        this.Goodcomment[1] = "ええやん、気に入った";
        this.Goodcomment[2] = "ああ～いいっすねぇ";
        this.Goodcomment[3] = "ありがとうございます";
        this.Goodcomment[4] = "最高やな！";

        this.Badcomment[0] = "(この店)やめたら？";
        this.Badcomment[1] = "あ ほ く さ";
        this.Badcomment[2] = "こんなん商品なんないから";
        this.Badcomment[3] = "あのさぁ...";
        this.Badcomment[4] = "あっ...";

        //this.CustomerImage[4].SetActive(false);

        this.CompletionButton = GameObject.Find("CompletionButton");
        this.ResetButton = GameObject.Find("ResetButton");

        Remaining.text = (REMAINING - this.foodcnt).ToString("F0");
        Timer.text = this.timelimit.ToString("F1");

        this.MixerImage = GameObject.Find("mixerPrefab");
        this.BlackHoleImage = GameObject.Find("BlackHolePrefab");

        //ミキサーのRGB値を取得
        MixRenderer = MixerImage.GetComponent<Renderer>();
        MixColor = MixRenderer.material.color;
        MixColor.a = 1.0f;

        //ブラックホールのRGB値を取得
        BlaRenderer = BlackHoleImage.GetComponent<Renderer>();
        BlaColor = BlaRenderer.material.color;
        BlaColor.a = 0;

        BlaRenderer.material.color = BlaColor;//変更したアルファ値を反映させる

        //宇宙人のRGB値を取得
        AlienRenderer = CustomerImage[4].GetComponent<Renderer>();
        AlienColor = AlienRenderer.material.color;
        AlienColor.a = 0;

        AlienRenderer.material.color = AlienColor;//変更したアルファ値を反映させる

        this.BlackHoleImage.SetActive(false);

        OrderCount();

        AmountText.text = string.Format("{0,8}", (this.TotalAmount.ToString("F0") + "円"));//合計金額

        TrustText.text = this.TrustPer.ToString("F0") + "％";//信頼度
    }

    // Update is called once per frame
    void Update()
    {
        //if (ProcessFlg == (int)Process.MAINPROCESS)//スムージーを作る時間
        //{
        //    MainProcess();
        //}
        //else if(ProcessFlg == (int)Process.BONUSTIME)//ボーナスタイム
        //{
        //    BonusTime();
        //}
        //else if(ProcessFlg == (int)Process.FADEINPROCESS)
        //{
        //    MixerFadeInProcess();
        //}
        //else if(ProcessFlg == (int)Process.FADEOUTPROCESS)
        //{
        //    MixerFadeOutProcess();
        //}
        //else if(flg && ProcessFlg == (int)Process.PERFORMANCETIME)//注文の切り替え
        //{
        //    PerformanceTime();
        //}

        switch (ProcessFlg)
        {
            case (int)Process.MAINPROCESS://スムージーを作る時間
                MainProcess();
                break;

            case (int)Process.BONUSTIME://ボーナスタイム
                BonusTime();
                break;

            case (int)Process.FADEINPROCESS://ミキサーのフェードイン処理
                MixerFadeInProcess();
                break;

            case (int)Process.FADEOUTPROCESS://ミキサーのフェードアウト処理
                //BlaColor.a = 0;
                MixerFadeOutProcess();
                break;

            case (int)Process.CUSTOMERIN://お客さん入店
                if (CustomerInflg)
                {
                    CustomerIN();
                }
                break;

            case (int)Process.CUSTOMEROUT://お客さん退店
                CustomerOUT();
                break;

            case (int)Process.COMMENT://お客さんのコメント
                if (CommentFlg)
                {
                    CommentTime();
                }
                break;

            case (int)Process.PERFORMANCETIME://注文の切り替え
                if (Performanceflg)
                {
                    PerformanceTime();
                }
                break;
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
        else
        {
            Remaining.text = "OK";
            CompletionButton.GetComponent<Completion>().InteractableChangeTrue();
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
        playerflg = 99;
        foodcnt = 0;

        if(SmoothieCount % ALLFOOD == 0 && SmoothieCount != 0)//完成したスムージーの数が10の倍数の時、全混ぜを発動させる
        {
            FoodObject.text = this.Order[4];//全混ぜスムージー 
            //CustomerImage[4].SetActive(true);

            //ミキサーの画像を非表示にする
            MixerImage.SetActive(false);
            if (TrustLevel < 2)
            {
                TrustLevel++;//信頼度のレベルを上げる
            }

            Invoke("CursleFlagChange", InvokeTime);
        }
        else
        {
            if(SmoothieCount % ALLFOOD == 1 && SmoothieCount != 1)
            {
                BlackHoleImage.SetActive(false);
               // CustomerImage[4].SetActive(false);
            }

            foodflg = Random.Range(0, 4);//注文を再取得
            W = foodflg;

            CustomerInflg = true;//お客さんの入店を許可する
            ProcessFlg = (int)Process.CUSTOMERIN;//お客さんの入店
        }

        CompletionButton.GetComponent<Completion>().InteractableChangeFalse();//完成ボタンを押せないように変更
        //Debug.Log(Order[foodflg]);
        //Debug.Log(foodcnt);
    }

    private void PerformanceTime()
    {
        OrderCount();
        //Invoke("CursleFlagChange", 2);//2秒後に操作可能にする

        //Debug.Log("PerformanceTime");

        Performanceflg = false;
    }

    private void CursleFlagChange()//カーソル無効の解除
    {
        Cursor.lockState = CursorLockMode.None;

        if (SmoothieCount % ALLFOOD == 0 && SmoothieCount != 0)
        {
            ProcessFlg = (int)Process.BONUSTIME;//BonusTimeに移行
        }
        else
        {
            ProcessFlg = (int)Process.MAINPROCESS;//MainProcessに移行
        }
    }

    public void OrderReset()//注文の切り替え
    {
        this.timelimit = 9.9f;//制限時間のリセット

        if((foodflg < 99 && REMAINING <= foodcnt) || ProcessFlg == (int)Process.BONUSTIME)//注文のスムージーが完成したとき
        {
            SmoothieCount++;
            TrustPer += 10;//信頼度の回復

            if (TrustPer >= 100)
            {
                TrustPer = 100;
            }

            TotalAmount += price * foodcnt;//合計金額

            if (ProcessFlg == (int)Process.BONUSTIME)
            {
                FoodObject.text = "マタクルヨ";
            }
            else
            {
                FoodObject.text = Goodcomment[Random.Range(0, 5)];//完成させたときのコメント
            }
        }
        else//注文と違う食材を入れたとき
        {
            TrustPer -= Trustarray[TrustLevel];

            if (TrustPer <= 0)
            {
                TrustPer = 0;
                Debug.Log("GameOver");
            }

            FoodObject.text = Badcomment[Random.Range(0,5)];
        }

        AmountText.text = string.Format("{0,8}", (this.TotalAmount.ToString("F0") + "円"));//合計金額

        TrustText.text = this.TrustPer.ToString("F0") + "％";//信頼度

        Debug.Log(SmoothieCount);
        //Debug.Log("TimeReset");
        //Debug.Log(timelimit);

        ResetButton.GetComponent<FoodReset>().InteractableChangeTrue();
        Cursor.lockState = CursorLockMode.Locked;//カーソルをロック

        Performanceflg = true;
        ProcessFlg = (int)Process.COMMENT;//お客さんのコメント
        CommentFlg = true;
    }

    void CommentTime()
    {
        Invoke("NextProcess", 0.5f);
        CommentFlg = false;
    }

    void NextProcess()
    {
        ProcessFlg = (int)Process.CUSTOMEROUT;//お客さんの退店
    }

    void BonusTime()//全混ぜスムージー
    {
        if (playerflg != 99 && Input.GetMouseButtonUp(0))//ミキサーに食材を入れたとき
        {
            foodcnt++;
            playerflg = 99;
        }

        TimeCount();//時間の取得

        if (this.timelimit <= 0)
        {
            OrderReset();//注文の切り替え
        }

        //if ((REMAINING - foodcnt) > 0)//食材の残りの数の表示
        //{
        //    Remaining.text = (REMAINING - this.foodcnt).ToString("F0");
        //}
        //else
        //{
        //    Remaining.text = "OK";
        //    CompletionButton.GetComponent<Completion>().InteractableChangeTrue();
        //    Debug.Log("OK");
        //}


        //今はボーナスタイム中は食材の残りの数を∞にして、完成ボタンは押せないようにする
        Remaining.text = "∞";
    }

    private void MixerFadeInProcess()//ミキサーのフェードイン処理、ブラックホールと宇宙人のフェードアウト処理
    {
        //ミキサーとブラックホールと宇宙人の同時フェード処理
        if (MixColor.a < 1.0f)
        {
            MixColor.a += speed;
            BlaColor.a -= speed;
            AlienColor.a -= speed;
            MixRenderer.material.color = MixColor;
            BlaRenderer.material.color = BlaColor;
            AlienRenderer.material.color = AlienColor;
        }
        else
        {
            ProcessFlg = (int)Process.PERFORMANCETIME;
        }

        ////ブラックホールが先にフェードアウト処理、その後ミキサーがフェードイン処理
        //if (BlaColor.a > 0)
        //{
        //    BlaColor.a -= speed;
        //    BlaRenderer.material.color = BlaColor;
        //}
        //else if (MixColor.a < 1.0f)
        //{
        //    MixColor.a += speed;
        //    MixRenderer.material.color = MixColor;
        //}
        //else
        //{
        //    ProcessFlg = (int)Process.PERFORMANCETIME;
        //}

    }

    private void MixerFadeOutProcess()//ミキサーのフェードアウト処理、ブラックホールと宇宙人ののフェードイン処理
    {
        ////ミキサーとブラックホールの同時フェード処理
        //if (MixColor.a > 0)
        //{
        //    MixColor.a -= speed;
        //    BlaColor.a += speed;
        //    MixRenderer.material.color = MixColor;
        //    BlaRenderer.material.color = BlaColor;
        //}
        //else
        //{
        //    ProcessFlg = (int)Process.PERFORMANCETIME;
        //}

        //先にミキサーがフェードアウト処理、その後ブラックホールと宇宙人のフェードイン処理
        if (MixColor.a > 0)
        {
            MixColor.a -= speed;
            MixRenderer.material.color = MixColor;
        }
        else if (BlaColor.a < 1.0f)
        {
            BlaColor.a += speed;
            AlienColor.a += speed;
            BlaRenderer.material.color = BlaColor;
            AlienRenderer.material.color = AlienColor;
        }
        else
        {
            ProcessFlg = (int)Process.PERFORMANCETIME;
        }

    }

    private void CustomerIN()
    {
        if (CustomerImage[W].transform.localPosition.x < CustomerInPosition[W])
        {
            CustomerRigid[W].velocity = new Vector2(3, 0);
        }
        else
        {
            //ProcessFlg = (int)Process.PERFORMANCETIME;//待機時間に移行
            CustomerRigid[W].velocity = Vector2.zero;
            Invoke("CursleFlagChange", InvokeTime);//0.5秒後に操作可能にする
            FoodObject.text = this.Order[foodflg];
            CustomerInflg = false;
        }
    }

    private void CustomerOUT()
    {
        if (CustomerImage[W].transform.localPosition.x > CustomerOutPosition[W])
        {
            CustomerRigid[W].velocity = new Vector2(-3, 0);
        }
        else
        {
            CustomerRigid[W].velocity = Vector2.zero;

            if (SmoothieCount % ALLFOOD == 0 && SmoothieCount != 0)
            {
                ProcessFlg = (int)Process.FADEOUTPROCESS;//ミキサーのフェードアウト処理に移行
                BlackHoleImage.SetActive(true);
            }
            else if (SmoothieCount % ALLFOOD == 1 && SmoothieCount != 1)
            {
                ProcessFlg = (int)Process.FADEINPROCESS;//ミキサーのフェードイン処理に移行
                MixerImage.SetActive(true);
            }
            else
            {
                ProcessFlg = (int)Process.PERFORMANCETIME;//待機時間の処理に移行
            }
        }
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
