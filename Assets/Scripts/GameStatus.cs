using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameStatus gameStatus;
    public APPClass aPPClass =new APPClass();

    public int version;
    private void Awake()
    {
        gameStatus = this;

    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
