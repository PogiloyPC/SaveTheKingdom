using InterfaceTask;
using UnityEngine;

public class PostPositionGenerator : IGeneratorPosPost
{
    private float _maxX = 0.3f;
    private float _minX = -0.3f;
    private float _Y = 0f;
    private float _Z = 0f;

    public Vector3 GeneratePosPost()
    {
        float X = Random.Range(_minX, _maxX);

        return new Vector3(X, _Y, _Z);
    }
}
