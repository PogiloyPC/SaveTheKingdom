using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _id;

    public int Id { get { return _id; } private set { } }

    [SerializeField] private TypeItem _typeItem;

    public TypeItem ItemType => _typeItem;


}

public enum TypeItem
{
    pickaxe,
    hoe,
    axe,
    sword,
    bow,
    fishingRod,
    spear,
    staff,
    hamer
}
