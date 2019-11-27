using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerData
{
    public string FileName = "savefile";
    public string Version = "NotSet";
    public int CompletedLevel = 0;

    public List<KeyCode>[][] KeyBindings = new List<KeyCode>[3][]
    {
        null,
        new List<KeyCode>[2]{
            new List<KeyCode>(new KeyCode[] {KeyCode.None, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.F, KeyCode.G, KeyCode.H,
                KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S }),
            new List<KeyCode>(new KeyCode[] { KeyCode.None, KeyCode.Joystick1Button7, KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button6,
                KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None})
        },
        new List<KeyCode>[2]{
            new List<KeyCode>(new KeyCode[] {KeyCode.None, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Comma, KeyCode.Period, KeyCode.Slash,
                KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow}),
            new List<KeyCode>(new KeyCode[] { KeyCode.None, KeyCode.Joystick2Button7, KeyCode.Joystick2Button0, KeyCode.Joystick2Button1, KeyCode.Joystick2Button2, KeyCode.Joystick2Button3, KeyCode.Joystick2Button6,
                KeyCode.None , KeyCode.None , KeyCode.None , KeyCode.None})
        }
    };
    

    public static PlayerData CreateNew()
    {
        PlayerData d = new PlayerData();
        d.Version = SaveManager.SavefileVersion;
        return d;
    }
}
