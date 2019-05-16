using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanciaCarta : MonoBehaviour, IClicavel
{
    public SeguradorDeJogador jogadorDono;
    public Efeito efeito;
    public bool efeitoUsado = false;
    public bool podeSofrerEfeito = true;
    public LogicaInstanciaCarta logicaAtual;
    public ExibirInfoCarta infoCarta;
    public bool podeAtacarNesteTurno;
    public bool podeSerAtacada;

    public bool PodeAtacar()
    {
        bool resultado = true;
        if (podeAtacarNesteTurno == false)
        {
            resultado = false;
        }
        if (infoCarta.carta.tipoCarta.DiferenteTipoDeAtacar(this))
        {
            resultado = true;
        }
        return resultado;
    }
    void Start()
    {
        infoCarta = GetComponent<ExibirInfoCarta>();
    }

    public IEnumerator AnimacaoDano(int dano)
    {
        if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(this))
        {
            transform.Find("Coração Dano").gameObject.transform.Rotate(0, 0, 180f);
        }
        else
        {
            transform.Find("Coração Dano").gameObject.transform.Rotate(0, 0, 0);
        }
        transform.Find("Coração Dano").gameObject.SetActive(true);
        transform.Find("Coração Dano").Find("Texto").GetComponent<Text>().text = dano.ToString();
        yield return new WaitForSeconds(0.8f);
        transform.Find("Coração Dano").gameObject.SetActive(false);
    }
    public IEnumerator AnimacaoCura(int cura)
    {
        if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(this))
        {
            transform.Find("Coração Dano").gameObject.transform.Rotate(0, 0, 180f);
        }
        else
        {
            transform.Find("Coração Dano").gameObject.transform.Rotate(0, 0, 0);
        }
        transform.Find("Coração Cura").gameObject.SetActive(true);
        transform.Find("Coração Cura").Find("Texto").GetComponent<Text>().text = "+" + cura.ToString();
        yield return new WaitForSeconds(0.8f);
        transform.Find("Coração Cura").gameObject.SetActive(false);
    }
    void IClicavel.AoClicar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoClicar(this);
        }
    }

    void IClicavel.AoOlhar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoOlhar(this);
        }
    }
}
