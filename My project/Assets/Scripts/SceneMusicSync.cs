using UnityEngine;

public class SceneMusicSync : MonoBehaviour
{
    void Start()
    {
        DynamicPlayer player = FindObjectOfType<DynamicPlayer>();
        if (player == null) return;

        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        if (musicOn)
            player.PlayMusic();
        else
            player.StopMusic();
    }
}