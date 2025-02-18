using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public Boss boss;

    private void Awake()
    {
        instance = this;
    }

    public void StartBoss()
    {
        boss.Init();
    }
}
