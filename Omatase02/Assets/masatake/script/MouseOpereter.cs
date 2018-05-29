using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOpereter : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            //Cursor.lockState = CursorLockMode.None;
            return;  //lockStateがLockedだったら以後の処理をしない
        }

        //カメラから見たオブジェクトの現在位置を画面位置座標に変換
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //取得したscreenpointの値を変数に格納
        float posx = Input.mousePosition.x;
        float posy = Input.mousePosition.y;

        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(posx, posy, 0));
    }

    void OnMouseDrag()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
           // Cursor.lockState = CursorLockMode.None;
            return;  //lockStateがLockedだったら以後の処理をしない
        }

        //ドラッグ時のマウス位置をシーン上の座標に変換
        float posx = Input.mousePosition.x;
        float posy = Input.mousePosition.y;

        //Debug.Log(posx.ToString() + " - " + posy.ToString());

        Vector3 currentScreenPoint = new Vector3(posx, posy, 0);

        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;

        transform.position = currentPosition;
    }

    //Use this for initialization
   //void Start()
   // {

   // }

    //// Update is called once per frame
    //void Update () {

    //}
}
