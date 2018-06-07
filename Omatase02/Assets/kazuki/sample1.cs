using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sample1 : MonoBehaviour
{
    public Sprite HelpImage1;
    public Sprite HelpImage2;
    private bool flg = true;
    public void ChangeImage()
    {
        var img = GetComponent<Image>();
        img.sprite = (flg) ? HelpImage2 : HelpImage1;
        flg = !flg;
    }

    //void Start()
    //{
    //}

    void Update ()
    {
    }
}
