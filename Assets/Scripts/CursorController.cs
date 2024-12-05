using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    private Pause pauseScript;

    void Update()
    {
        if (pauseScript == true)
        {
          
            Cursor.visible = true;
           
        }

        if (pauseScript == false)
        {
            //Hides the cursor in game and positions the cursor to the centre of the screen
            Cursor.visible = false;
            
        }
    }
}
