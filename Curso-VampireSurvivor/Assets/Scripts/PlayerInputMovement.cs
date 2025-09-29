using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform bg;
    [SerializeField] private RectTransform handle;

    [Header("Tuning")]
    [Tooltip("Radio máximo en px del Canvas para el movimiento del handle")]
    [SerializeField] private float maxRadius = 120f;
    [Range(0f, 1f)]
    [SerializeField] private float deadZone = 0.1f;
    [SerializeField] private bool hideWhenReleased = true;

    private RectTransform canvasRect;
    private Camera uiCamera;
    private bool isHeld;
    private Vector2 input;
    private Vector2 first_input;

    public void OnTouchButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isHeld = true;
            input = Vector2.zero;

            first_input = Touchscreen.current.primaryTouch.position.ReadValue();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, first_input, uiCamera, out Vector2 localPoint))
            {
                bg.anchoredPosition = localPoint;
                handle.anchoredPosition = localPoint;
            }

            if (hideWhenReleased)
            {
                bg.gameObject.SetActive(true);
                handle.gameObject.SetActive(true);
            }

            Debug.Log("Screen is pressed");
        }
        else if (ctx.canceled)
        {
            ReleaseStick();
        }
    }

    public void OnDeltaTouch(InputAction.CallbackContext ctx)
    {
        if (isHeld)
        {
            input = ctx.ReadValue<Vector2>();

            UpdateHandle(input);
        }
    }

    void Awake()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        if (hideWhenReleased)
        {
            if (bg) bg.gameObject.SetActive(false);
            if (handle) handle.gameObject.SetActive(false);
        }
    }

    private void UpdateHandle(Vector2 screenPos)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out Vector2 localPoint)) return;

        Vector2 delta = localPoint - bg.anchoredPosition;

        Vector2 clamped = Vector2.ClampMagnitude(delta, maxRadius);

        handle.anchoredPosition = bg.anchoredPosition + clamped;

        Vector2 raw = clamped / maxRadius;
        input = raw.magnitude < deadZone ? Vector2.zero : raw;
    }

    private void ReleaseStick()
    {
        isHeld = false;
        input = Vector2.zero;

        handle.anchoredPosition = bg.anchoredPosition;

        if (hideWhenReleased)
        {
            bg.gameObject.SetActive(false);
            handle.gameObject.SetActive(false);
        }
    }

}
