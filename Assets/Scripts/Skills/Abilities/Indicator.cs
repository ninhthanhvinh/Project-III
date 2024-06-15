using Control;
using RPG.Skills;
using System.Collections;
using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    public abstract IEnumerator FindingArea(PlayerController player, Skill skill);
}
