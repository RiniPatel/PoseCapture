using UnityEngine;

public class Upper_Leg_R : MonoBehaviour
{
    static int UPPER_LEG_R_ID = 5;
    float timeToGo;
    Quaternion offset, orig;
    bool isCalibrated;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Upper Leg R is up" + transform.rotation.eulerAngles);
        offset = Quaternion.Euler(transform.rotation.eulerAngles);
        isCalibrated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (UDPScript.IsDataReady(UPPER_LEG_R_ID))
            {
                orig = Quaternion.Euler(-UDPScript.GetPitch(UPPER_LEG_R_ID), UDPScript.GetYaw(UPPER_LEG_R_ID), -UDPScript.GetRoll(UPPER_LEG_R_ID));
                isCalibrated = true;
            }
            return;
        }

        if (Time.fixedTime >= timeToGo)
        {
            Quaternion q = Quaternion.Euler(-UDPScript.GetPitch(UPPER_LEG_R_ID), UDPScript.GetYaw(UPPER_LEG_R_ID), -UDPScript.GetRoll(UPPER_LEG_R_ID));
            transform.rotation = Quaternion.Inverse(orig) * q * offset;

            timeToGo = Time.fixedTime + 0.1f;
        }
    }
}