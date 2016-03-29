using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    private bool isMute;

    public void Mute()
    {
        this.isMute = !this.isMute;
        AudioListener.volume = this.isMute ? 0 : 1;
    }
}