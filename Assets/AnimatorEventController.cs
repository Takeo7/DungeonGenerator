using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventController : MonoBehaviour
{
    [SerializeField]
    PlayerMovement pm;

    public void SetJumpFalse()
    {
        pm.SetJumpFalse();
    }
}
