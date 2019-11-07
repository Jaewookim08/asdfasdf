using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keytest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) Debug.Log("1 0"); //오른쪽 버튼 왼
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)) Debug.Log("1 1"); //아래
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) Debug.Log("1 2"); //오른
        if (Input.GetKeyDown(KeyCode.Joystick1Button3)) Debug.Log("1 3");//위
        if (Input.GetKeyDown(KeyCode.Joystick1Button4)) Debug.Log("1 4");//L1 왼쪽 위
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) Debug.Log("1 5");//L2
        if (Input.GetKeyDown(KeyCode.Joystick1Button6)) Debug.Log("1 6");//R1
        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) Debug.Log("1 7");//R2
        if (Input.GetKeyDown(KeyCode.Joystick1Button8)) Debug.Log("1 8");//share
        if (Input.GetKeyDown(KeyCode.Joystick1Button9)) Debug.Log("1 9");//options
        if (Input.GetKeyDown(KeyCode.Joystick1Button10)) Debug.Log("1 10");
        if (Input.GetKeyDown(KeyCode.Joystick1Button11)) Debug.Log("1 11");
        if (Input.GetKeyDown(KeyCode.Joystick1Button12)) Debug.Log("1 12");//ps
        if (Input.GetKeyDown(KeyCode.Joystick1Button13)) Debug.Log("1 13");
        if (Input.GetKeyDown(KeyCode.Joystick1Button14)) Debug.Log("1 14");
        if (Input.GetKeyDown(KeyCode.Joystick1Button15)) Debug.Log("1 15");
        if (Input.GetKeyDown(KeyCode.Joystick1Button16)) Debug.Log("1 16");
        if (Input.GetKeyDown(KeyCode.Joystick1Button17)) Debug.Log("1 17");
        if (Input.GetKeyDown(KeyCode.Joystick1Button18)) Debug.Log("1 18");
        if (Input.GetKeyDown(KeyCode.Joystick1Button19)) Debug.Log("1 19");
    }
}
