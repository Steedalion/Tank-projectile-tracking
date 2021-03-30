using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;

    public GameObject parent;
    public GameObject target;
       [Min(0.5f)]
    public float turnSpeed;

    public float speed= 15;
    
    void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed*transform.forward;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = FaceTarget();
        float? angle = RotateTurrent();
        if(angle == null) return;
        if(Vector3.Angle(toTarget, parent.transform.forward) < 10)
            Fire();

    }

    Vector3 FaceTarget()
    {
        Vector3 toTarget = target.transform.position - parent.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(new Vector3(toTarget.x,0,toTarget.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, desiredRotation, Time.deltaTime*turnSpeed);
        return toTarget; 
    }

    float? RotateTurrent()
    {
        float? angle = CalculateProjectileAngle(true);
        if (angle == null) return null;

        transform.localEulerAngles = new Vector3((float) (360f - angle*Mathf.Rad2Deg), 0f, 0f);
        return angle;
    }

    float? CalculateProjectileAngle(bool low)
    {
        Vector3 toTarget = target.transform.position - transform.position;
        float x, y, s=speed, g = 9.81f;
        y = toTarget.y;
        toTarget.y = 0;
        x = toTarget.magnitude;
        float alpha = Mathf.Pow(s, 4) - g * (g * x * x + 2 * y * s * s);
        
        if (alpha < 0) return null;
        if (low) return Mathf.Atan2((s * s - Mathf.Sqrt(alpha)) , (g * x));
        return Mathf.Atan2((s * s + Mathf.Sqrt(alpha)) , (g * x));
        
    }
 
}
