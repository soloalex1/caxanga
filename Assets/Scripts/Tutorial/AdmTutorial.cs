using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmTutorial : MonoBehaviour
{
    public Carta cartaExemploInicial;
    public GameObject popUp, popUpPequenoCima, popUpPequenoBaixo, popUpMedio;
    public int tempoEsperaAnimacao;
    public List<Transform> posicoesPopUps;
    public List<string> textos;
    public Text textoPopUp;
    public int indiceTexto;
    public Sprite barraDeVidaPerdida, barraDeVida;
    bool jogadorPassouTexto;
    public Sprite mascaraPoder;

    public List<GameObject> objetosDestacados;
    private void Start()
    {
        StartCoroutine(Tutorial());
    }
    IEnumerator Tutorial()
    {
        //Passo 1
        /*
            Olá, esta é uma peleja de Caxangá. Nela, você usará cartas detentoras de fragmentos de magia
             para lhe ajudar a derrotar seu inimigo.
        */
        popUp.SetActive(true);
        EscreverTextoTurorial();
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 2
        /*
            Para vencer uma partida, você terá de ganhar três rodadas de cinco.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 3
        /*
            Você e seu oponente possuem 3 cargas de poder.
        */
        jogadorPassouTexto = false;
        objetosDestacados[0].SetActive(true);
        objetosDestacados[1].SetActive(true);
        // Debug.Log("Agora eu destaco as barras de poder");
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 4
        /*
            Cada vez que você perde uma rodada, você perde uma barra de poder.
        */
        jogadorPassouTexto = false;
        objetosDestacados[2].GetComponent<Image>().sprite = barraDeVidaPerdida;
        // Debug.Log("Agora eu destaco a barra de poder perdida");
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 5
        /*
            Uma rodada acaba quando os dois jogadores passam seu turno.
        */
        jogadorPassouTexto = false;
        objetosDestacados[2].GetComponent<Image>().sprite = barraDeVida;
        objetosDestacados[0].SetActive(false);
        objetosDestacados[1].SetActive(false);
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 6
        /*
            Quando os dois jogadores passarem seus turnos, 
            quem tiver maior quantidade de poder em seu herói, vence a rodada.
        */
        jogadorPassouTexto = false;
        objetosDestacados[3].SetActive(true);
        objetosDestacados[4].SetActive(true);
        objetosDestacados[5].SetActive(true);
        objetosDestacados[6].SetActive(true);
        popUpPequenoBaixo.SetActive(true);
        popUpPequenoCima.SetActive(true);
        EscreverTextoPopUp(popUpPequenoBaixo, "Suas cargas de vida");
        EscreverTextoPopUp(popUpPequenoCima, "Cargas de vida do seu oponente");
        // Debug.Log("Agora eu destaco a vida dos jogadores");
        yield return new WaitUntil(() => jogadorPassouTexto);

        ///Passo 7
        /*
            O tabuleiro é dividido em 3 partes
        */
        jogadorPassouTexto = false;
        objetosDestacados[3].SetActive(false);
        objetosDestacados[4].SetActive(false);
        objetosDestacados[5].SetActive(false);
        objetosDestacados[6].SetActive(false);
        popUpPequenoBaixo.SetActive(false);
        popUpPequenoCima.SetActive(false);
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 8
        /*
            Os cemitérios
         */
        popUp.SetActive(false);
        popUpPequenoBaixo.SetActive(true);
        popUpPequenoCima.SetActive(true);
        posicoesPopUps[0].gameObject.SetActive(true);
        posicoesPopUps[1].gameObject.SetActive(true);
        popUpPequenoCima.transform.SetParent(posicoesPopUps[0]);
        popUpPequenoBaixo.transform.SetParent(posicoesPopUps[1]);
        popUpPequenoBaixo.transform.localPosition = new Vector3(0, 0, 0);
        popUpPequenoCima.transform.localPosition = new Vector3(0, 0, 0);
        EscreverTextoPopUp(popUpPequenoBaixo, "Seu cemitério");
        EscreverTextoPopUp(popUpPequenoCima, "Cemitério do oponente");
        // Debug.Log("Agora vou destacar os cemitérios");
        yield return new WaitForSeconds(tempoEsperaAnimacao);

        //Passo 9
        /*
            Os baralhos
         */
        posicoesPopUps[0].gameObject.SetActive(false);
        posicoesPopUps[1].gameObject.SetActive(false);
        posicoesPopUps[2].gameObject.SetActive(true);
        posicoesPopUps[3].gameObject.SetActive(true);
        popUpPequenoCima.transform.SetParent(posicoesPopUps[3]);
        popUpPequenoBaixo.transform.SetParent(posicoesPopUps[2]);
        popUpPequenoBaixo.transform.localPosition = new Vector3(0, 0, 0);
        popUpPequenoCima.transform.localPosition = new Vector3(0, 0, 0);
        EscreverTextoPopUp(popUpPequenoBaixo, "Baralho do oponente");
        EscreverTextoPopUp(popUpPequenoCima, "Seu Baralho");
        popUpPequenoCima.SetActive(true);
        popUpPequenoBaixo.SetActive(true);
        //Debug.Log("Agora vou destacar os baralhos");
        yield return new WaitForSeconds(tempoEsperaAnimacao);

        //Passo 10
        /*
            E os campos
         */
        posicoesPopUps[2].gameObject.SetActive(false);
        posicoesPopUps[3].gameObject.SetActive(false);
        popUpMedio.SetActive(true);
        EscreverTextoPopUp(popUpMedio, "E os campos, onde as cartas são baixadas");
        //Debug.Log("Agora vou destacar os campos");
        yield return new WaitForSeconds(tempoEsperaAnimacao);
        //Passo 11
        /*
            Atenção! As cartas só são puxadas no começo da rodada
         */
        popUpMedio.SetActive(false);
        popUp.SetActive(true);
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        //Passo 12
        /*
            Para jogar uma carta em campo, seja ela uma Lenda ou um Feitiço, 
            você precisa ter a quantidade de magia que ela demanda.
         */
        popUp.transform.SetParent(posicoesPopUps[4]);
        objetosDestacados[7].SetActive(true);
        popUp.transform.localPosition = new Vector3(0, 0, 0);
        objetosDestacados[7].GetComponentInChildren<ExibirInfoCarta>().carta = cartaExemploInicial;
        objetosDestacados[7].GetComponentInChildren<ExibirInfoCarta>().CarregarCarta(cartaExemploInicial);
        objetosDestacados[8].SetActive(true);
        objetosDestacados[9].SetActive(true);
        jogadorPassouTexto = false;
        //Debug.Log("Agora vou destacar a magia do jogador e da carta");
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 13*/
        /*
        Logo, é importante tomar cuidado com esse recurso, uma vez que ele só é restaurado ao fim da rodada.
         */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 14*/
        /*
        Tenha isso em mente ao formular sua estratégia na peleja.
         */
        popUp.transform.SetParent(posicoesPopUps[5]);
        popUp.transform.localPosition = new Vector3(0, 0, 0);
        objetosDestacados[7].SetActive(false);
        objetosDestacados[8].SetActive(false);
        objetosDestacados[9].SetActive(false);
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 15*/
        /*
        Também ao fim da rodada, os jogadores recebem duas cartas de seus respectivos baralhos.
         */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 16*/
        /*
        Lembre-se de que é a única ocasião que permite isso aos jogadores (com exceção de efeitos de carta). Use com sabedoria e astúcia!
         */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 17*/
        /*
        N HÁ TEXTO, APENAS ANIMAÇÕES
         */
        popUp.SetActive(false);
        transform.Find("Fundo").gameObject.SetActive(false);
        StartCoroutine(Configuracoes.admJogo.FadeTextoTurno(Configuracoes.admJogo.jogadorAtual));
        yield return new WaitForSeconds(1);
        /*Passo 18*/
        /*
        Seu turno começou, hora de usar cartas!
        */
        transform.Find("Fundo").gameObject.SetActive(true);
        popUp.SetActive(true);
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 19*/
        /*
        Seu turno começou, hora de usar cartas!
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 20*/
        /*
        Quando você não tem mais ações para serem feitas, seu turno é encerrado automaticamente.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 21*/
        /*
        Contudo, você também pode passar seu turno quando for sua vez, 
        desde que você não tenha nem atacado, nem jogando nenhuma feito nenhuma ação antes.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 22*/
        /*
        Às vezes, é melhor deixar seu oponente vencer,
         para que você possa poupar cartas para as próximas rodadas e vencer a peleja.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 23*/
        /*
         Existem dois tipo de carta: Lenda e Feitiço. Você só pode invocar UMA Lenda e UM Feitiço por turno. 
        */

        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 24*/
        /*
           As lendas são as criaturas que você pode pôr em campo
        */
        popUp.transform.SetParent(posicoesPopUps[4]);
        objetosDestacados[7].SetActive(true);
        popUp.transform.localPosition = new Vector3(0, 0, 0);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Segurador Flechinha/Flechinha").gameObject.SetActive(false);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Arte da Carta").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Frente da Carta/mascara").gameObject.SetActive(false);
        objetosDestacados[7].GetComponentInChildren<ExibirInfoCarta>().carta = cartaExemploInicial;
        objetosDestacados[7].GetComponentInChildren<ExibirInfoCarta>().CarregarCarta(cartaExemploInicial);
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 25*/
        /*
         E são a sua fonte primária de dano ao seu inimigo, pois você só pode atacar seu oponente por meio delas
        */

        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 26*/
        /*
        Somente as Cartas do tipo Lenda possuem poder
        */
        objetosDestacados[7].transform.Find("Carta sendo olhada/Frente da Carta/mascara").gameObject.SetActive(true);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Segurador Flechinha/Flechinha").gameObject.SetActive(true);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Segurador Flechinha").transform.localPosition = new Vector3(-543, 0, 0);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Arte da Carta").GetComponent<Image>().color = new Color(123, 123, 123);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Frente da Carta/mascara/Mascara").transform.localPosition = new Vector3(25, 93, 0);
        objetosDestacados[7].transform.Find("Carta sendo olhada/Frente da Carta/mascara/Mascara").GetComponent<Image>().sprite = mascaraPoder;
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 27*/
        /*
        O poder representa tanto a vida quanto a força de ataque de sua Lenda.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 28*/
        /*
            Você possui a carta Boiuna na sua mão
        */
        objetosDestacados[7].gameObject.SetActive(false);
        popUp.transform.SetParent(posicoesPopUps[5]);
        popUp.transform.localPosition = new Vector3(0, 0, 0);
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 29*/
        /*
        Note que ela possui 4 de poder e 3 de custo. Coloque ela em campo.
        */

        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);
        /*Passo 30*/
        /*
        Assim que a carta é colocada em campo, ela deve carregar seu poder e não pode atacar imediatamente.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 31*/
        /*
        Às vezes, é melhor deixar seu oponente vencer,
         para que você possa poupar cartas para as próximas rodadas e vencer a peleja.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

        /*Passo 32*/
        /*
        Às vezes, é melhor deixar seu oponente vencer,
         para que você possa poupar cartas para as próximas rodadas e vencer a peleja.
        */
        jogadorPassouTexto = false;
        yield return new WaitUntil(() => jogadorPassouTexto);

    }
    void EscreverTextoTurorial()
    {
        if (textoPopUp != null && textos.Count > indiceTexto)
        {
            popUp.SetActive(true);
            textoPopUp.text = textos[indiceTexto];
            indiceTexto++;
        }
        else
        {
            Configuracoes.admCena.CarregarCena("Tela Inicial");
        }
    }
    void EscreverTextoPopUp(GameObject popUp, string texto)
    {
        if (popUp != null)
            popUp.GetComponentInChildren<Text>().text = texto;
    }
    public void PassarTexto()
    {
        EscreverTextoTurorial();
        jogadorPassouTexto = true;
    }
}
