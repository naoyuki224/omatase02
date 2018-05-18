using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public GameObject[] Temporary;
    private Vector2 position;
    private bool flg = false;

    // Use this for initialization
    void Start () {
        position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (flg && Input.GetMouseButtonUp(0))
        {
            GameObject CreateObject = GameObject.Find("position1");
            CreateObject.GetComponent<Create>().GenerationObject(position);
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
