using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player")]
    [field: SerializeField] public EventReference dash { get; private set; }

    [field: Header ("Reward SFX")]
    [field: SerializeField] public EventReference coinCollected { get; private set; } // Field keyword help show in inspector if setter is private
    [field: SerializeField] public EventReference scrollCollected { get; private set; }

    [field: Header("Trap SFX")]
    [field: SerializeField] public EventReference shotFire { get; private set; }
    [field: SerializeField] public EventReference trapDroped { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference gameplayBMG { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public EventReference buttonClick { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }


}
