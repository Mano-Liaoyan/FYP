using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Skill
{
    public string name;
    public string type;
    public int maxUsingTimes;
}

public struct CharactorData
{
    public string name;
    public Vector3 currentPostion;
    public int level;
    public float health;
    public float mana;
    public Skill[] skills;
}
