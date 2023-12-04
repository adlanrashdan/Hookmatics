using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    //make an instance of the joistick
    public static FixedJoystick instance;

    private void Awake()
    {
        instance = this;
    }
}