using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject("@Sound");
            Object.DontDestroyOnLoad(root);
            
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource source in _audioSources)
        {
            source.clip = null;
            source.Stop();
        }
        
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioSource = GetOrAddAudioClip(path, type);
        Play(audioSource, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;
        
        switch (type)
        {
            case Define.Sound.Bgm:
            {
                AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
                
                if (audioSource.isPlaying)
                    audioSource.Stop();
                
                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.loop = true;
                audioSource.Play();

                break;
            }
            default:
            {

                AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
                break;
            }
        }
    }
    
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;
        switch (type)
        {
            case Define.Sound.Bgm:
                audioClip = Managers.Resource.Load<AudioClip>(path);
                break;
            default:
                if (_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = GetOrAddAudioClip(path);
                    _audioClips.Add(path, audioClip);
                }
                break;
        }
        
        if (audioClip == null)
            Debug.Log($"AudioClip Missing! {path}");
        
        return audioClip;
    }
}