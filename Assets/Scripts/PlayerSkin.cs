using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    SkinnedMeshRenderer SMR;

     void Start()
    {
        SMR = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }




}
