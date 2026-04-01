using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum WaveType { Bell }

    [SerializeField] private bool on = true;
    public WaveType type = WaveType.Bell;

    public float maxHeight = 1f;
    public float speed = 1f;
    public Vector2 offset = Vector2.zero;
    public Vector2 scale = Vector2.one;

    public float GetHeight(float x, float y)
    {
        if(!on) return 0f;

        switch(type)
        {
            case WaveType.Bell:
                return GetBellHeight(x, y);
        }

        return 0;
    } 

    public float GetBellHeight(float x, float y)
    {
        Vector2 sinWave = new Vector2(
            Mathf.Sin(x / scale.x + X + Time.time * speed),
            Mathf.Sin(y / scale.y + Y + Time.time * speed)
        );

        return maxHeight * 0.5f * (sinWave.x + sinWave.y);
    }

    private float X => -transform.position.x / scale.x + offset.x;
    private float Y => -transform.position.y / scale.y + offset.y;
}
