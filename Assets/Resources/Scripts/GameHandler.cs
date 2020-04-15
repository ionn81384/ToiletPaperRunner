using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance = null;

    public GameObject[] canvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // If sucess then keep it
        DontDestroyOnLoad(this.gameObject);
    }
}
