using System.Collections;
using UnityEngine;

public class Follow : TransformObject
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private void Awake() => InitializeTransformObject();

    private IEnumerator Start()
    {
        while (true)
        {
            transform.position = target.position + offset;

            transform.LookAt(target);

            yield return null;
        }
    }
}
