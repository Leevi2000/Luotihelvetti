using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Keybinds keybinds = new Keybinds();
   
    public bool IsForwardPressed() => Input.GetKey(keybinds.Player_up);
    public bool IsBackwardPressed() => Input.GetKey(keybinds.Player_down);
    public bool IsLeftPressed() => Input.GetKey(keybinds.Player_left);
    public bool IsRightPressed() => Input.GetKey(keybinds.Player_right);
    public bool IsShootPressed() => Input.GetKey(keybinds.Player_shoot);
    public bool IsFocusPressed() => Input.GetKey(keybinds.Player_focus);

}
