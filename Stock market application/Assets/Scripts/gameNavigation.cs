using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

public class gameNavigation : MonoBehaviour
{
   [Header("Objects")]
   public GameObject Pregame;

   public GameObject Game;

   public void Start()
   {
      Pregame.SetActive(true);
      Game.SetActive(false);
    }

   public void StartGame()
   {
       Pregame.SetActive(false);
       Game.SetActive(true);
       GameObject.Find("EventSystem").GetComponent<GameApplication>().gameStarted = true;
   }
}