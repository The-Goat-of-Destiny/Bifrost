using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Background", menuName = "Content/Character Options/Background", order = 0)]
public class Background : CharacterOption
{
    public List<Data.Attribute> AttributeBoosts;
    public int FreeBoosts = 0;
    public List<Data.Attribute> AttributeFlaws;
}
