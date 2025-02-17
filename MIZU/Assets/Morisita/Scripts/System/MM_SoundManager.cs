using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MM_SoundManager : MM_SingletonMonoBehaviour<MM_SoundManager>
{
    public float masterVolume = 1f;
    public float seVolume = 1f;
    public float bgmVolume = 1f;

    private AudioSource bgmSource;
    private Dictionary<SoundType, AudioClip> audioClips = new();

    public enum SoundType
    {
        None,
        BGM,
        GameOver,
        Death,
        TitleBGM,
        StageBGM,
        Transform,
        ButtonPush,
        ClearBGM,
        WaterUpDown,
        // �����ɍĐ�����SE,BGM�̎�ނ�ǉ�����

    }

    [System.Serializable]
    public class SoundItem
    {
        public SoundType type;
        public AudioClip clip;
    }

    public SoundItem[] preloadedSounds;

    private void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.loop = true;

        LoadPreloadedSounds();
    }

    private void LoadPreloadedSounds()
    {
        foreach (var soundItem in preloadedSounds)
        {
            if (soundItem.clip != null)
            {
                audioClips[soundItem.type] = soundItem.clip;
            }
        }
    }

    public void LoadSound(SoundType type, AudioClip clip)
    {
        audioClips[type] = clip;
    }

    /// <summary>
    /// SE���Đ����܂�
    /// </summary>
    /// <param name="type"></param>
    /// <param name="volume"></param>
    public void PlaySE(SoundType type, float volume = 1f)
    {
        if (audioClips.TryGetValue(type, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume * seVolume * masterVolume);
        }
        else
        {
            Debug.LogWarning($"�T�E���h {type} ��������܂���B");
        }
    }
    /// <summary>
    /// BGM���Đ����܂�
    /// BGM���t�F�[�h�C���E�t�F�[�h�A�E�g�����邱�Ƃ��ł��܂�
    /// fadeDuration=1f,�t�F�[�h�C���E�t�F�[�h�A�E�g��1�b
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fade"></param>
    /// <param name="fadeDuration"></param>
    public void PlayBGM(SoundType type, bool fade = false, float fadeDuration = 1f)
    {
        if (audioClips.TryGetValue(type, out AudioClip clip))
        {
            if (fade)
            {
                StartCoroutine(FadeBGM(clip, fadeDuration));
            }
            else
            {
                bgmSource.clip = clip;
                bgmSource.volume = bgmVolume * masterVolume;
                bgmSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"BGM {type} ��������܂���B");
        }
    }

    private System.Collections.IEnumerator FadeBGM(AudioClip newClip, float fadeDuration)
    {
        float startVolume = bgmSource.volume;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();

        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0, bgmVolume * masterVolume, timer / fadeDuration);
            yield return null;
        }
    }
    /// <summary>
    /// �S�Ẳ��̉��ʂ�ݒ肵�܂��B
    /// �l��0~1
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume * masterVolume;
    }
    /// <summary>
    /// SE�̉��ʂ�ݒ肵�܂��B
    /// �l��0~1
    /// </summary>
    /// <param name="volume"></param>
    public void SetSEVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
    }
    /// <summary>
    /// BGM�̉��ʂ�ݒ肵�܂��B
    /// �l��0~1
    /// </summary>
    /// <param name="volume"></param>
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume * masterVolume;
    }
}