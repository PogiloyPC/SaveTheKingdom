using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorIdUnit
{
    private int _id;

    public int GenerateNewId()
    {
        _id++;

        return _id;
    }
}
