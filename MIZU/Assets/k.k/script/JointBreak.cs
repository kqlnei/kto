using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    public float b_force = 1000f;
    public float b_torque = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        CharacterJoint joint = GetComponent<CharacterJoint>();
        joint.breakForce = b_force;   // �ő�1000�̗͂ŉ���
        joint.breakTorque = b_torque;   // �ő�500�̃g���N�ŉ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnJointBreak(float breakForce)
    {
        Debug.Log("Joint broke with force: " + breakForce);
    }
}
