using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class SoundsScript : MonoBehaviour
{
    bool isMute;

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }
}
