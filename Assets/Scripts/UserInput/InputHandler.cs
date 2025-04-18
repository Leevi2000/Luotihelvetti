using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Keybinds keybinds;
   
    public bool IsForwardPressed() => Input.GetKeyDown(keybinds.Player_up);
    public bool IsBackwardPressed() => Input.GetKeyDown(keybinds.Player_down);
    public bool IsLeftPressed() => Input.GetKeyDown(keybinds.Player_left);
    public bool IsRightPressed() => Input.GetKeyDown(keybinds.Player_right);
    public bool IsShootPressed() => Input.GetKeyDown(keybinds.Player_shoot);
    public bool IsFocusPressed() => Input.GetKeyDown(keybinds.Player_focus);

}
