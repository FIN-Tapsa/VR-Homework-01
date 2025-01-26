using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Nyt alkaa: ");
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            print(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }
    }


}
