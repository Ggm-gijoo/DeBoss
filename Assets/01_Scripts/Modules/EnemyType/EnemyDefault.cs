using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDefault : MonoBehaviour
{
    public abstract void Attack();
    public virtual void Skill1() { }
    public virtual void Skill2() { }
    public virtual void Skill3() { }
}
