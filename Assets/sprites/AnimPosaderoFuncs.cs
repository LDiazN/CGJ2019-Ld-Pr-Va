using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPosaderoFuncs : MonoBehaviour
{
    PosaderoController container;

    private void RecoverWalk()
    {
        FindObjectOfType<PosaderoController>().RecoverWalk();
    }
}
