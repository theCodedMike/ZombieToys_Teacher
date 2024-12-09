using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once HollowTypeName
public class GameManager
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance ??= new GameManager();
        }
    }

    private GameManager() { }

    public bool GameOver = false;
}
