using UnityEngine;

public class MagnetCollector : MonoBehaviour
{
    public float duration = 0.0f;
    internal float timeActive = 0.0f;

    void Update()
    {
        if (this.duration < this.timeActive)
        {
            this.timeActive = 0.0f;
            this.enabled = false;
        }
        else
        {
            this.timeActive += Time.deltaTime;
        }
    }
}
