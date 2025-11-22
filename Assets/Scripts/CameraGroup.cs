using UnityEngine;

public class CameraGroup : MonoBehaviour
{
    [SerializeField]
    private KeyCode nextCameraKetCode = KeyCode.Space;

    private Camera[] cameras;

    private int currentCameraIndex;

    private void Awake()
    {
        cameras = GetComponentsInChildren<Camera>();

        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(nextCameraKetCode))
        {
            return;
        }

        cameras[currentCameraIndex++].gameObject.SetActive(false);

        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }

        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
