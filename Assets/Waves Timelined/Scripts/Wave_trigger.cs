using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wave_trigger : MonoBehaviour
{
    public Action trigged;
    private bool beenTrigged = false;

    private void OnTriggerStay(Collider other)
    {
        if (beenTrigged) return;

        beenTrigged = true;
        trigged.Invoke();
    }
}
