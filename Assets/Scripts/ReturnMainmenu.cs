using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMainmenu : MonoBehaviour
{

    public void returnMainmenu()
    {
        GameController.gameController.backMainmenu();
    }
}
