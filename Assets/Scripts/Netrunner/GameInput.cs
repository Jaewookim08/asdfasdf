using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Netrunner {
    public static class GameInput {
        public enum Key {None=0, Action1=1, Action2=2, Action3=3, Action4=4, Action5=5, Action6=6, Left=7, Right=8, Up=9, Down=10};
        

        public static bool GetKey(int player, Key key) 
        {
            return GetRealKeys(player, key).Aggregate(false, (curr, keyCode) => curr | Input.GetKey(keyCode));
        }


        /////////////////////////////////////////////////////////////////////////////
        // 키보드, 컨트롤러를 같이 누르는 경우 keydown 2번, keyup 0번 나오는 문제? //
        /////////////////////////////////////////////////////////////////////////////

        public static bool GetKeyDown(int player, Key key) 
        {
            return GetRealKeys(player, key).Aggregate(false, (curr, keyCode) => curr | Input.GetKeyDown(keyCode));
        }
        
        public static bool GetKeyUp(int player, Key key) 
        {
            return GetRealKeys(player, key).Aggregate(false, (curr, keyCode) => curr | Input.GetKeyUp(keyCode));
        }

        public static float GetHorizontal(int player)
        {
            var joystick = GetLeftJoystickVector(player);
            var mag = joystick.magnitude;
            if (mag > 0.19f)
            {
                if (joystick.x < 0) mag = -mag;
                return mag;
            }
            var hor = Input.GetAxis("BHorizontal_" + player);
            if (Math.Abs(hor) > 0.001) return hor;
            return (GetKey(player, Key.Right) ? 1f : 0f) + (GetKey(player, Key.Left) ? -1f : 0f);
        }
        
    
        // 화살표로 가리키는 방향의 단위 벡터(또는 0벡터) 반환.
        public static Vector2 GetDirection(int player)
        {
            var vec = GetButtonJoystickDirection(player);
            if (vec.sqrMagnitude > 0.1f) return vec;
            return GetKeyboardDirection(player);
        }


        private static Vector2 GetKeyboardDirection(int player)
        {
            return new Vector2((GetKey(player, Key.Right) ? 1 : 0) - (GetKey(player, Key.Left) ? 1 : 0),
                (GetKey(player, Key.Up) ? 1 : 0) - (GetKey(player, Key.Down) ? 1 : 0)).normalized;
        }
        public static Vector2 GetLeftJoystickVector(int player)
        {
            return new Vector2(Input.GetAxis("LHorizontal_" + player), Input.GetAxis("LVertical_" + player)); ;
        }
        private static Vector2 GetButtonJoystickDirection(int player)
        {
            return new Vector2(Input.GetAxis("BHorizontal_" + player), Input.GetAxis("BVertical_" + player)).normalized;
        }


        public static List<KeyCode> GetRealKeys(int player, Key key) {
            return SaveManager.Current.KeyBindings[player]
                .Select(keyList => keyList[(int) key])
                .Where(k => k != KeyCode.None).ToList();
        }
    }
}


/*
Buttons
    Square  = joystick button 0
    X       = joystick button 1
    Circle  = joystick button 2
    Triangle= joystick button 3
    L1      = joystick button 4
    R1      = joystick button 5
    L2      = joystick button 6
    R2      = joystick button 7
    Share	= joystick button 8
    Options = joystick button 9
    L3      = joystick button 10
    R3      = joystick button 11
    PS      = joystick button 12
    PadPress= joystick button 13

Axes:
    LeftStickX      = X-Axis
    LeftStickY      = Y-Axis (Inverted?)
    RightStickX     = 3rd Axis
    RightStickY     = 4th Axis (Inverted?)
    L2              = 5th Axis (-1.0f to 1.0f range, unpressed is -1.0f)
    R2              = 6th Axis (-1.0f to 1.0f range, unpressed is -1.0f)
    DPadX           = 7th Axis
    DPadY           = 8th Axis (Inverted?)

*/