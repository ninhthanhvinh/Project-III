using Control;
using RPG.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    public abstract IEnumerator FindingArea(PlayerController player, Skill skill);
}
