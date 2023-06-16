using UnityEngine;

public class GeneratorIdUnit : MonoBehaviour
{
    private int[] _busyIds = new int[0];
    
    public int GenerateNewId()
    {
        int id = _busyIds.Length + 1;

        int[] busyIds = new int[_busyIds.Length + 1];

        for (int i = 0; i < _busyIds.Length; i++)        
            busyIds[i] = _busyIds[i];

        busyIds[busyIds.Length - 1] = id;

        _busyIds = busyIds;

        return id;
    }

}
