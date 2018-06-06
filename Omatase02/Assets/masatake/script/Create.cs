using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour {

    //生成するための配列
    public GameObject [] Temporary;

    // Use this for initialization
	void Start () {
        //オブジェクトの生成
        GenerationObject(this.transform.position);
    }

    //// Update is called once per frame
    //void Update () {

    //}

    public void GenerationObject(Vector2 position)//オブジェクトの生成
    {
        Instantiate(Temporary[Random.Range(0, Temporary.Length)], position, Quaternion.identity);
    }
}
