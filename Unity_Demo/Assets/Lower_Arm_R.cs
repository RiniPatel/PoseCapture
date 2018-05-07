using UnityEngine;

public class Lower_Arm_R : MonoBehaviour
{
    static int LOWER_ARM_R_ID = 3;
    float timeToGo;
    Quaternion offset, orig;
    bool isCalibrated;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Lower Arm R is up" + transform.rotation.eulerAngles);
        //offset = new Vector3(0, 180, -90);
        offset = Quaternion.Euler(transform.rotation.eulerAngles);
        isCalibrated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (UDPScript.IsDataReady(LOWER_ARM_R_ID))
            {
                orig = Quaternion.Euler(UDPScript.GetRoll(LOWER_ARM_R_ID), UDPScript.GetYaw(LOWER_ARM_R_ID), UDPScript.GetPitch(LOWER_ARM_R_ID));
                isCalibrated = true;
            }
            return;
        }


        if (Time.fixedTime >= timeToGo)
        {
            Quaternion q = Quaternion.Euler(-UDPScript.GetRoll(LOWER_ARM_R_ID), UDPScript.GetYaw(LOWER_ARM_R_ID), UDPScript.GetPitch(LOWER_ARM_R_ID));
            transform.rotation = Quaternion.Inverse(orig) * q * offset;

            timeToGo = Time.fixedTime + 0.1f;
        }
    }
}