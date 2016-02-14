using UnityEngine;

public interface IScene
{
    Vector2 originalPos
    {
        get;
    }
    GameObject sceneObject
    {
        get;
    }
    void Enter();
    void Leave();

    void EscapeProcess();
}