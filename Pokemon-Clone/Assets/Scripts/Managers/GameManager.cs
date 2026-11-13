using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SO_EventSender eventSender;
    
    //Variables to keep track of
    [SerializeField] private bool isBattling;

    //Properties
    public SO_EventSender EventSender { get => eventSender; }


    private void OnEnable()
    {
        eventSender.enterBattle.AddListener(EnterBattle);
        eventSender.exitBattle.AddListener(ExitBattle);
    }

  

    private void OnDisable()
    {
        eventSender.enterBattle.RemoveListener(EnterBattle);
        eventSender.exitBattle.RemoveListener(ExitBattle);
    }


    private void Awake()
    {
        //Singleton pattern - make sure only 1 Game Manager is present at a time
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void EnterBattle()
    {
        isBattling = true;
        Debug.Log("GameManager - Loading Battle Scene");
        eventSender.EnableInputEventSend(false);
        SceneManager.LoadScene("Battle");
    }


    private void ExitBattle()
    {
        isBattling = false;
        eventSender.EnableInputEventSend(true);
    }

    //Getters and Setters
    public bool GetIsBattling()
    {
        return isBattling;
    }


}
