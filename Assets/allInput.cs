using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class allInput : MonoBehaviour
{
    public SteamVR_Action_Single squeezeAction;

    void Update()
    {
        if (SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Teleport down");
        }

        if (SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch").GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Grab pinch down");
        }

        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);

        if (triggerValue > 0.0f)
        {
            print(triggerValue);

        }
    }
}


/*

*/