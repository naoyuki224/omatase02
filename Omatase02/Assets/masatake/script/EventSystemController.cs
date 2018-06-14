using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class EventSystemController : MonoBehaviour {
    // 入力状態
    static private bool inputEnable = true;

    // イベントシステム
    private EventSystem eventSystem = null;

    public bool isInputEnable
    {
        set
        {
            // 入力状態を反映して、保持しておく
            EventSystemController.inputEnable = value;
            this.eventSystem.enabled = EventSystemController.inputEnable;
        }
        get
        {
            // 現在の入力状態
            return EventSystemController.inputEnable;
        }
    }
    
    // Use this for initialization
    void Start () {
        // アタッチしているイベントシステムを見つけておく
        this.eventSystem = this.GetComponent<EventSystem>();
        // 入力状態をイベントシステムに反映させる
        this.isInputEnable = this.isInputEnable;
    }

    public void isInputEnableChangeTrue()
    {
        isInputEnable = true;
    }

    public void isInputEnableChangeFalse()
    {
        isInputEnable = false;
    }

    // Update is called once per frame
 //   void Update () {
		
	//}
}
