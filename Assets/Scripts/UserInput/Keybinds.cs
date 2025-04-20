using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds
{
    private KeyCode player_up = KeyCode.UpArrow;
    private KeyCode player_down = KeyCode.DownArrow;
    private KeyCode player_left = KeyCode.LeftArrow;
    private KeyCode player_right = KeyCode.RightArrow;
    private KeyCode player_shoot = KeyCode.Z;
    private KeyCode player_focus = KeyCode.LeftShift;
    private KeyCode player_dash = KeyCode.X;

    public KeyCode Player_up { get => player_up; set => player_up = value; }
    public KeyCode Player_down { get => player_down; set => player_down = value; }
    public KeyCode Player_left { get => player_left; set => player_left = value; }
    public KeyCode Player_right { get => player_right; set => player_right = value; }
    public KeyCode Player_shoot { get => player_shoot; set => player_shoot = value; }
    public KeyCode Player_focus { get => player_focus; set => player_focus = value; }
    public KeyCode Player_dash { get => player_dash; set => player_dash = value; }
}
