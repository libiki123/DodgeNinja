using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header ("Coin SFX")]
    [field: SerializeField] public EventReference coinCollected { get; private set; } // Field keyword help show in inspector if setter is private
    [field: Header("Scroll SFX")]
    [field: SerializeField] public EventReference scrollCollected { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }


}
