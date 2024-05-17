using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrabandManager : MonoBehaviour
{
    public GameObject contrabandText;
    public void CarryingContraband(bool isCarryingContraband)
    {
        contrabandText.SetActive(isCarryingContraband);
    }
}
