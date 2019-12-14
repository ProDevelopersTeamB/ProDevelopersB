﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public List<AudioClip> BGMList;
    public List<AudioClip> SEList;
    public List<AudioClip> exVoiceList;

    // 同時に再生可能なSE数の上限
    private readonly int SE_COUNT_LIMIT = 100;

    private AudioSource bgmSource = null;
    private List<AudioSource> seSources = null;
    private AudioSource exVoiceSource = null;
    private Dictionary<string, AudioClip> bgmDict = null;
    private Dictionary<string, AudioClip> seDict = null;
    private Dictionary<string, AudioClip> exVoiceDict = null;

    private bool isFadeOut = false;
    private bool isFadeIn = false;
    private float fadeToVolume = 0.0f;

    public void Awake()
    {
        // Audio Listener がなければ作る
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>(); //DEBUG
        }
        // Audio Source を作る
        this.bgmSource = this.gameObject.AddComponent<AudioSource>();
        this.seSources = new List<AudioSource>();
        this.exVoiceSource = this.gameObject.AddComponent<AudioSource>();

        // BGM/SEリストの初期化
        this.bgmDict = new Dictionary<string, AudioClip>();
        this.seDict = new Dictionary<string, AudioClip>();
        this.exVoiceDict = new Dictionary<string, AudioClip>();

        // Inspectorから設定したAudioClipをリストに格納する
        Action<Dictionary<string, AudioClip>, AudioClip> addClipDict = (dict, c) =>
        {
            if (!dict.ContainsKey(c.name))
            {
                dict.Add(c.name, c);
            }
        };

        AudioClip[] seFiles = Resources.LoadAll<AudioClip>("Sounds/SE");
        foreach (AudioClip var in seFiles)
        {
            this.SEList.Add(var);
        }

        AudioClip[] bgmFiles = Resources.LoadAll<AudioClip>("Sounds/BGM");
        foreach (AudioClip var in bgmFiles)
        {
            this.BGMList.Add(var);
        }

        AudioClip[] exVoiceFiles = Resources.LoadAll<AudioClip>("Sounds/ExVoice");
        foreach (AudioClip var in exVoiceFiles)
        {
            this.exVoiceList.Add(var);
        }

        this.BGMList.ForEach(bgm => addClipDict(this.bgmDict, bgm));
        this.SEList.ForEach(se => addClipDict(this.seDict, se));
        this.exVoiceList.ForEach(exVoice => addClipDict(this.exVoiceDict, exVoice));
    }

    public void Update()
    {
        if (isFadeOut)
        {
            this.bgmSource.volume = this.bgmSource.volume * 0.98f;
            if (this.bgmSource.volume < 0.01f)
            {
                this.StopBGM();
            }
        }
        if (isFadeIn)
        {
            if (fadeToVolume >= this.bgmSource.volume)
            {
                this.bgmSource.volume += 0.002f;
            }
            else
            {
                isFadeIn = false;
            }
        }
    }

    public void PlaySE(string seName, float volume = 1.0f, bool loop = false)
    {
        if (!this.seDict.ContainsKey(seName))
            throw new ArgumentException(seName + " not found", "seName");

        AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
        if (source == null)
        {
            if (this.seSources.Count >= this.SE_COUNT_LIMIT)
            {
                Debug.Log("SE AudioSource is full");
                return;
            }

            source = this.gameObject.AddComponent<AudioSource>();
            this.seSources.Add(source);
        }

        source.clip = this.seDict[seName];
        source.volume = volume;
        source.pitch = 1.0f;
        source.loop = loop;
        source.Play();
    }

    public void StopSE()
    {
        this.seSources.ForEach(s => s.Stop());
    }

    public void StopSE(string seName)
    {
        foreach (var source in this.seSources)
        {
            if (source.clip.name == seName)
            {
                source.Stop();
            }
        }
    }

    public void PlaySE(string seName)
    {
        PlaySE(seName, 0.3f, false);
    }

    public void PlayExVoice(string exVoiceName, float volume = 1.0f, bool loop = false)
    {
        if (!this.exVoiceDict.ContainsKey(exVoiceName))
            throw new ArgumentException(exVoiceName + " not found", "exVoiceName");
        if (this.exVoiceSource.clip == this.exVoiceDict[exVoiceName])
            return;
        this.exVoiceSource.Stop();
        this.exVoiceSource.clip = this.exVoiceDict[exVoiceName];
        this.exVoiceSource.loop = loop;
        this.exVoiceSource.volume = volume;
        this.exVoiceSource.pitch = 1;
        this.exVoiceSource.Play();
        isFadeOut = false;
    }

    public bool IsPlayingExVoice()
    {
        return this.exVoiceSource.isPlaying;
    }

    public void PlayBGM(string bgmName, float volume = 1.0f, bool loop = true)
    {
        if (!this.bgmDict.ContainsKey(bgmName))
            throw new ArgumentException(bgmName + " not found", "bgmName");
        if (this.bgmSource.clip == this.bgmDict[bgmName])
            return;
        this.bgmSource.Stop();
        this.bgmSource.clip = this.bgmDict[bgmName];
        this.bgmSource.loop = loop;
        this.bgmSource.volume = volume;
        this.bgmSource.pitch = 1;
        this.bgmSource.Play();
        isFadeOut = false;
    }

    public void FadeInBGM(string bgmName, float volume = 1.0f, bool loop = true)
    {
        PlayBGM(bgmName, 0.0f, loop);
        fadeToVolume = volume;
        isFadeIn = true;
    }

    public void StopBGM()
    {
        this.bgmSource.Stop();
        this.bgmSource.clip = null;
        isFadeOut = false;
    }

    public void FadeOutBGM()
    {
        isFadeOut = true;
    }

    public void SetBGMPitch(float pitch)
    {
        this.bgmSource.pitch = pitch;
    }

    public void SetSEPitch(string seName, float pitch)
    {
        foreach (var source in this.seSources)
        {
            if (source.clip.name == seName)
            {
                source.pitch = pitch;
            }
        }
    }

    public bool IsPlayingSE()
    {
        foreach (var source in this.seSources)
        {
            if (source.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}