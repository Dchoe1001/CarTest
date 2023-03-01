using UnityEngine;

public class test : MonoBehaviour
{
    private const string A_BUTTON = "A Button"; // G920 A BUTTON

    private void Update()
    {
        float aButtonValue = Input.GetAxisRaw(A_BUTTON);
        Debug.Log("A button value: " + aButtonValue);
    }
}