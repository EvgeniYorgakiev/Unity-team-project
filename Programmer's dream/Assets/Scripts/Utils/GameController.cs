using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float IncreasedSpeedPerTime = 0.001f;
    private const float MaxGlobalSpeed = 2.5f;
    private const float TimeForSpeedIncrease = 6f;

    public AudioSource backgroundMusic;
    internal int score = 0;
    internal bool gameIsRunning = true;
    internal int currencyValue = 100;
    internal float globalSpeedModifier = 1.0f;
    private float currentSecond = 0.0f;

    void Start()
    {
        this.backgroundMusic.Play();
        this.backgroundMusic.loop = true;
    }

    void Update()
    {
        if (this.globalSpeedModifier < MaxGlobalSpeed)
        {
            this.currentSecond += Time.deltaTime;
            if (this.currentSecond / TimeForSpeedIncrease > 1)
            {
                this.currentSecond = 0;
                this.globalSpeedModifier += IncreasedSpeedPerTime;
            }
        }
    }
}