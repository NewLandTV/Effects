using UnityEngine;

public class TransformObject : MonoBehaviour
{
    protected new Transform transform;

    protected void InitializeTransformObject() => transform = GetComponent<Transform>();
}
