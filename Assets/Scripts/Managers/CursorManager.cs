using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private bool hotSpotIsCenter;
    [SerializeField]
    private Vector2 adjustHotSpot;
    private Vector2 hotSpot;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        hotSpot = hotSpotIsCenter ? new Vector2(cursorTexture.width >> 1, cursorTexture.height >> 1) : adjustHotSpot;

        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
