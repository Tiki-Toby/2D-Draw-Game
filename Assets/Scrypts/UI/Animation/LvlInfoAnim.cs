using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LvlInfoAnim : MonoBehaviour
{
    void Start()
    {
        for(int i=0; i<3; i++)
        {
            transform.GetChild(i).GetComponent<Text>().DOFade(0, 5f);
        }
   
    }


    void Update()
    {
        
    }
}
