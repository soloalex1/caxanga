using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarSons : MonoBehaviour
{
    public AudioClip somDeFundo;
    public AudioSource fonteAudio;

    public GameObject x;
    void Start()
    {
        fonteAudio.loop = true;
        fonteAudio.clip = somDeFundo;
        fonteAudio.volume = Configuracoes.volumeMusicaFundo;
        fonteAudio.Play();
    }
    public void DesligarSons()
    {
        if (!Configuracoes.semSom)
        {
            Configuracoes.semSom = true;
            fonteAudio.volume = 0;
            Configuracoes.volumeSFX = 0;
            x.SetActive(true);
        }
        else
        {
            Configuracoes.semSom = false;
            fonteAudio.volume = Configuracoes.volumeMusicaFundo;
            Configuracoes.volumeSFX = 0;
            x.SetActive(false);
        }
    }
    public void MudarSom(float novoVolume)
    {
        Configuracoes.volumeMusicaFundo = novoVolume;
        Configuracoes.volumeSFX = novoVolume / 2;
        fonteAudio.volume = Configuracoes.volumeMusicaFundo;
        if (novoVolume == 0)
        {
            x.SetActive(true);
        }
        else
        {
            x.SetActive(false);
        }
    }
}
