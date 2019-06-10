using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarSons : MonoBehaviour
{
    public AudioClip somDeFundo;
    public AudioSource fonteAudio;
    void Start()
    {
        fonteAudio.loop = true;
        fonteAudio.clip = somDeFundo;
        fonteAudio.volume = Configuracoes.volumeMusicaFundo;
        fonteAudio.Play();
    }
}
