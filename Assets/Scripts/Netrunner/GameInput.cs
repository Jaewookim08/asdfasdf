using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Netrunner {
    public static class GameInput {
        public enum Key {None=0, Action1=1, Action2=2, Action3=3, Action4, Action5, Action6, Left=7, Right=8, Up=9, Down=10};

        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsJoystick(int player) {
            return new Vector2(Input.GetAxis("LHorizontal_" + player),
                       Input.GetAxis("LVertical_" + player)).SqrMagnitude() > 0.0001;
        }

        public static bool GetKey(int player, Key key) {
            bool pressed = false;
            KeyCode code;
            if ((code = GetRealKey(player, key, 0)) != 0) pressed |= Input.GetKey(code);
            if ((code = GetRealKey(player, key, 1)) != 0) pressed |= Input.GetKey(code);
            return pressed;
        }


        /////////////////////////////////////////////////////////////////////////////
        // 키보드, 컨트롤러를 같이 누르는 경우 keydown 2번, keyup 0번 나오는 문제? //
        /////////////////////////////////////////////////////////////////////////////

        public static bool GetKeyDown(int player, Key key) {
            bool pressed = false;
            KeyCode code;
            if ((code = GetRealKey(player, key, 0)) != 0) pressed |= Input.GetKeyDown(code);
            if ((code = GetRealKey(player, key, 1)) != 0) pressed |= Input.GetKeyDown(code);
            return pressed;
        }
        
        public static bool GetKeyUp(int player, Key key) {
            bool pressed = false;
            KeyCode code;
            if ((code = GetRealKey(player, key, 0)) != 0) pressed |= Input.GetKeyUp(code);
            if ((code = GetRealKey(player, key, 1)) != 0) pressed |= Input.GetKeyUp(code);
            return pressed;
        }

        public static float GetHorizontalIgnoreDirection(int player)
        {
            Vector2 joystick = getLeftJoystickVector2(player);
            float val = joystick.magnitude;
            if (val > 0.19f)
            {
                if (joystick.x < 0) val = -val;
                return val;
            }
            val = Input.GetAxis("BHorizontal_" + player);
            if (val != 0) return val;
            else return (GetKey(player, Key.Right) ? 1f : 0f) + (GetKey(player, Key.Left) ? -1f : 0f);
        }

        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static float GetHorizontalAxis_(int player) {
            return GetVector2_(player).x;
        }
        /// <summary>
        /// depreacted
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static float GetVerticalAxis_(int player) {
            return GetVector2_(player).y;
        }
    
        // 화살표로 가리키는 방향의 단위 벡터(또는 0벡터) 반환.
        public static Vector2 GetDirection(int player) {
            Vector2 vec = getButtonJoystickVector2(player);
            if (vec.sqrMagnitude > 0.1f) return vec;
            else return GetKeyboardVector2(player);
        }

        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static Vector2 GetVector2_(int player) {
            if (IsJoystick(player)) return new Vector2(Input.GetAxis("LHorizontal_" + player), Input.GetAxis("LVertical_" + player));;
            return new Vector2((GetKey(player, Key.Right) ? 1 : 0) - (GetKey(player, Key.Left) ? 1 : 0),
                (GetKey(player, Key.Up) ? 1 : 0) - (GetKey(player, Key.Down) ? 1 : 0));
        }

        public static Vector2 GetKeyboardVector2(int player)
        {
            return new Vector2((GetKey(player, Key.Right) ? 1 : 0) - (GetKey(player, Key.Left) ? 1 : 0),
                (GetKey(player, Key.Up) ? 1 : 0) - (GetKey(player, Key.Down) ? 1 : 0)).normalized;
        }
        public static Vector2 getLeftJoystickVector2(int player)
        {
            return new Vector2(Input.GetAxis("LHorizontal_" + player), Input.GetAxis("LVertical_" + player)); ;
        }
        public static Vector2 getButtonJoystickVector2(int player)
        {
            return new Vector2(Input.GetAxis("BHorizontal_" + player), Input.GetAxis("BVertical_" + player)).normalized;
        }


        public static KeyCode GetRealKey(int player, Key key, int idx) {
            return SaveManager.current.KeyBindings[player][idx][(int) key];
        }
        
        /*private static List<KeyCode>[] LoadKeyBindings(int player, bool isJoystick=false) {
            return SaveManager.current.KeyBindings[player];
        }*/
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