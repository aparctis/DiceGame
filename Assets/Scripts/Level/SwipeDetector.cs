using UnityEngine;
using UnityEngine.Events;
using Zenject;

public interface ISwipeDetector
{
    public event UnityAction onSwipeLeft;
    public event UnityAction onSwipeRight;
    public  event UnityAction onSwipeUp;
    public event UnityAction onSwipeDown;
    public event UnityAction<Vector2> onAnySwipe;
}

public class SwipeDetector : MonoBehaviour, ISwipeDetector, IInitializable
{
    [Header("Swipe Settings")]
    [SerializeField] private float minimumSwipeDistance = 50f;
    [SerializeField] private float maximumSwipeTime = 0.5f;

    public event UnityAction onSwipeLeft;
    public event UnityAction onSwipeRight;
    public event UnityAction onSwipeUp;
    public event UnityAction onSwipeDown;
    public event UnityAction<Vector2> onAnySwipe;

    private Vector2 touchStartPosition;
    private float touchStartTime;
    private bool isSwiping = false;

    public void Initialize()
    {
        // Zenject initialization if needed
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartSwipe(touch.position);
                    break;

                case TouchPhase.Ended:
                    EndSwipe(touch.position);
                    break;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            StartSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwipe(Input.mousePosition);
        }
#endif
    }

    private void StartSwipe(Vector2 position)
    {
        touchStartPosition = position;
        touchStartTime = Time.time;
        isSwiping = true;
    }

    private void EndSwipe(Vector2 position)
    {
        if (!isSwiping) return;

        isSwiping = false;
        float swipeTime = Time.time - touchStartTime;

        if (swipeTime > maximumSwipeTime) return;

        Vector2 swipeDistance = position - touchStartPosition;

        if (swipeDistance.magnitude < minimumSwipeDistance) return;

        onAnySwipe?.Invoke(swipeDistance.normalized);
        Debug.Log("SWIPE");

        if (Mathf.Abs(swipeDistance.x) > Mathf.Abs(swipeDistance.y))
        {
            if (swipeDistance.x > 0)
            {
                onSwipeRight?.Invoke();
            }
            else
            {
                onSwipeLeft?.Invoke();
            }
        }
        else
        {
            if (swipeDistance.y > 0)
            {
                onSwipeUp?.Invoke();
            }
            else
            {
                onSwipeDown?.Invoke();
            }
        }
    }
}