using UnityEngine;
using static LogitechGSDK;

public class test : MonoBehaviour
{
    private const string gas = "GasPedal"; // G920 A BUTTON
    private const string pedal = "BrakePedal"; // G920 A BUTTON
    private string actualState;

    // Controller index (0 = first controller)
    public int controllerIndex = 0;
    private void Update()
    {
        float gasInput = Input.GetAxisRaw("GasPedal"); // Get raw input from the G920's gas pedal
        float brakeInput = Input.GetAxisRaw("BrakePedal"); // Get raw input from the G920's brake pedal

        Debug.Log("gas button value: " + gasInput);
        //Debug.Log("brake button value: " + brakeInput);
        
        
        
        //LogitechGSDK.DIJOYSTATE2ENGINES previousState = new LogitechGSDK.DIJOYSTATE2ENGINES();
        //LogitechGSDK.DIJOYSTATE2ENGINES currentState = LogitechGSDK.LogiGetStateUnity(0);

        /*if (previousState.lX != currentState.lX || previousState.lY != currentState.lY || previousState.lZ != currentState.lZ)
        {
            actualState += "x-axis position :" + currentState.lX + "\n";
        }

        if (currentState != null)
        {
            // Access members of currentState here
        }

        previousState = currentState;


        /*if (LogiUpdate() && LogiIsConnected(controllerIndex))
        {
            DIJOYSTATE2ENGINES state = LogiGetStateUnity(controllerIndex);

            // Get X-axis rotation from the steering wheel
            float xRotation = state.lRx;
            Debug.Log("xRotation " + xRotation);

        }*/
    }
}
