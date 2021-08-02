using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using DG.Tweening;

public class InitLosePanel : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 1);
        transform.DOScale(new Vector3(1, 1, 1), 1f).SetUpdate(true);
    }

    void Update()
    {
        
    }
}
