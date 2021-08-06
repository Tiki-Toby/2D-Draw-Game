using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class Pause : MonoBehaviour
    {
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (GameObject.FindGameObjectsWithTag("Panel").Length < 2)
                {
                    if (Time.timeScale == 0)
                    {
                        Time.timeScale = 1;
                    }
                    else
                    {
                        Time.timeScale = 0;
                    }
                }
            });
        }
    }
}