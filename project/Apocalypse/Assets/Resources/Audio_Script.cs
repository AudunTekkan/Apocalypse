using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Audio_Script : MonoBehaviour {
    public AudioClip sound_button;
    public AudioClip sound_death;
    public AudioClip sound_pain1;
    public AudioClip sound_pain2;
    public AudioClip sound_pain3;
    public AudioClip sound_victory;
    public AudioClip sound_hitTheFloor;
    public AudioClip sound_powerOfMathematics;
    //звуки для уровня 12491
    public AudioClip sound_cryBird;
    public AudioClip sound_birdFlies;
    public AudioClip sound_bananaHit;
    public AudioClip sound_bananaFlies;
    public AudioClip sound_explosion;
    public AudioClip sound_turn;
    public AudioClip sound_slip;
    public AudioClip sound_toHurtYourselfOnTheShardsOfGlass;
    public AudioClip sound_hitTheWall;
    public AudioClip sound_bombHissings;
    public AudioClip sound_spikes;

    public AudioClip music_museum1;
    public AudioClip music_theftOfPainting;
    public AudioClip music_danger;
    public AudioClip music_museum2;
    public AudioClip music_bonfire;
    public AudioClip music_pursuit;


    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSounds;

    public Text lvl;
    public Text textForButtons;
    public Text lvl1212;
    public Text lvl12491;

    public Slider slider_health;
    public Slider slider_audio;

    public Image Sounds_image;
    public Image Music_image;

    private string s_lvl_music;
    private string s_lvl_sounds;
    private string s_health;
    private string s_musicLvl;

    private bool sounds_bool;
    private bool music_bool;

    private float f_audio;

    void Start()
    {
        audioSourceSounds = GetComponent<AudioSource>();
        audioSourceMusic = GetComponent<AudioSource>();
        audioSourceMusic.loop = true;

        audioSourceMusic.volume = slider_audio.value;
        s_lvl_music = "0";
        s_lvl_sounds = "0";
        s_musicLvl = "0";
        f_audio = 0.4f;
        s_health = slider_health.value.ToString();

        sounds_bool = true;
        music_bool = true;
    }

    void Update() {
        audioSourceMusic.volume = slider_audio.value * f_audio;

        if (s_musicLvl != "Current Level: 124915") 
        {
            audioSourceMusic.loop = true;
        } else
        {
            audioSourceMusic.loop = false;
        }
        switch (lvl.text)
        {
            case "Current Level: 1":
            case "Current Level: 11":
            case "Current Level: 113":
            case "Current Level: 1133":
            case "Current Level: 11333":
                s_musicLvl = "Current Level: 1";
                break;
            case "Current Level: 13":
                s_musicLvl = "Current Level: 13";
                break;
            case "Current Level: 131":
            case "Current Level: 1311":
            case "Current Level: 1312":
            case "Current Level: 1313":
            case "Current Level: 132":
            case "Current Level: 1321":
            case "Current Level: 1322":
            case "Current Level: 1323":
                s_musicLvl = "Current Level: 131";
                break;
            case "Current Level: 12":
            case "Current Level: 122":
            case "Current Level: 123":
            case "Current Level: 1234":
            case "Current Level: 121":
            case "Current Level: 1212":
                s_musicLvl = "Current Level: 12";
                break;
            case "Current Level: 124":
            case "Current Level: 1249":
                s_musicLvl = "Current Level: 124";
                break;
            case "Current Level: 12491":
                s_musicLvl = "Current Level: 12491";
                break;
            case "Current Level: 124915":
                s_musicLvl = "Current Level: 124915";
                break;
        }

        if (sounds_bool == true)
        {
            if (textForButtons.text == "play button")
            {
                textForButtons.text = "";                
                audioSourceSounds.PlayOneShot(sound_button, 0.7F);               
            }

            if (slider_health.value.ToString() != s_health)
            {
                if (slider_health.value == 0)
                {
                    audioSourceSounds.PlayOneShot(sound_death, 0.3F);
                }
                else if ((System.Convert.ToInt32(s_health) - slider_health.value < 14) && (System.Convert.ToInt32(s_health) - slider_health.value > 0))
                {
                    audioSourceSounds.PlayOneShot(sound_pain1, 0.3F);
                }
                else if ((System.Convert.ToInt32(s_health) - slider_health.value < 40) && (System.Convert.ToInt32(s_health) - slider_health.value > 0))
                {
                    audioSourceSounds.PlayOneShot(sound_pain2, 0.3F);
                }
                else if (System.Convert.ToInt32(s_health) - slider_health.value > 0)
                {
                    audioSourceSounds.PlayOneShot(sound_pain3, 0.3F);
                }
                s_health = slider_health.value.ToString();
            }


            if (lvl.text == "Current Level: 12491")
            {
                if (lvl12491.text == "hitTheWall")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_hitTheWall, 1F);
                }
                else if (lvl12491.text == "birdFlies")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_birdFlies, 0.8F);
                }
                else if (lvl12491.text == "bananaHit")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_bananaHit, 0.8F);
                }
                else if (lvl12491.text == "bananaFlies")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_bananaFlies, 0.8F);
                }
                else if (lvl12491.text == "explosion")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_explosion, 0.8F);
                }
                else if (lvl12491.text == "turn")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_turn, 1F);
                }
                else if (lvl12491.text == "slip")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_slip, 0.8F);
                }
                else if (lvl12491.text == "toHurtYourselfOnTheShardsOfGlass")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_toHurtYourselfOnTheShardsOfGlass, 1F);
                }
                else if (lvl12491.text == "cryBird")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_cryBird, 0.8F);
                }
                else if (lvl12491.text == "bombHissings")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_bombHissings, 0.8F);
                }
                else if (lvl12491.text == "spikes")
                {
                    lvl12491.text = "";
                    audioSourceSounds.PlayOneShot(sound_spikes, 1F);
                }
            }

            if (lvl.text != s_lvl_sounds)
            {
                s_lvl_sounds = lvl.text;
                switch (lvl.text)
                {
                    case "Current Level: 1212":
                        if (lvl1212.text == "hit the floor")
                        {
                            audioSourceSounds.PlayOneShot(sound_bananaHit, 0.9F);
                            lvl1212.text = "";
                        }
                        else if (lvl1212.text == "power of mathematics")
                        {
                            audioSourceSounds.PlayOneShot(sound_powerOfMathematics, 0.8F);
                            lvl1212.text = "";
                        }
                        break;
                }
            }
        }

        if (music_bool == true)
        {
            if (lvl.text != s_lvl_music)
            {
                s_lvl_music = lvl.text;
                switch (s_musicLvl)
                {
                    case "Current Level: 1":
                        if (audioSourceMusic.clip != music_museum1)
                        {
                            audioSourceMusic.clip = music_museum1;
                            f_audio = 0.5f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 13":
                        if (audioSourceMusic.clip != music_theftOfPainting)
                        {
                            audioSourceMusic.clip = music_theftOfPainting;
                            f_audio = 1f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 131":
                        if (audioSourceMusic.clip != music_danger)
                        {
                            audioSourceMusic.clip = music_danger;
                            f_audio = 0.5f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 12":
                        if (audioSourceMusic.clip != music_museum2)
                        {
                            audioSourceMusic.clip = music_museum2;
                            f_audio = 0.4f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 124":
                        if (audioSourceMusic.clip != music_bonfire)
                        {
                            audioSourceMusic.clip = music_bonfire;
                            f_audio = 1f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 12491":
                        if (audioSourceMusic.clip != music_pursuit)
                        {
                            audioSourceMusic.clip = music_pursuit;
                            f_audio = 0.8f;
                            audioSourceMusic.Play();
                        }
                        break;
                    case "Current Level: 124915":
                        if (audioSourceMusic.clip != sound_victory)
                        {
                            audioSourceMusic.clip = sound_victory;
                            f_audio = 0.7f;
                            audioSourceMusic.Play();
                        }
                        break;
                }
            }
        }

        
    }

    public void AudioBoolSounds(bool soundsBool)
    {
        if (soundsBool == true)
        {
            sounds_bool = true;
        }
        else
        {
            sounds_bool = false;
        }        
    }
    public void AudioBoolMusic(bool musicBool)
    {
        if (musicBool == true)
        {
            music_bool = true;
        }
        else
        {
            music_bool = false;
            s_lvl_music = "0";
            audioSourceMusic.Stop();
            audioSourceMusic.clip = sound_hitTheFloor;
        }
    }
}



