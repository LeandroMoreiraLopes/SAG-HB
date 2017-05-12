using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    AllBattlesController battlesControl;

    [SerializeField]
    Text tempoTxt;

    static int segundos, minutos, horas, tempoParaPergunta = 15;
    bool respondendo;

    bool gameOver;

    void Start()
    {
        battlesControl = GetComponent<AllBattlesController>();       
    }

    public void IniciarContagem()
    {
        tempoTxt.gameObject.SetActive(true);
        StartCoroutine(contadorDeTempo());
    }

    IEnumerator contadorDeTempo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            segundos++;
            if (segundos == 60)
            {
                minutos++;
                segundos = 0;
                if (minutos == 60)
                {
                    horas++;
                    minutos = 0;
                }
            }


            if (!respondendo)
            {
                tempoParaPergunta--;
                if (tempoParaPergunta == 0)
                {
                    respondendo = true;
                    battlesControl.ChamandoPerguntaDasBatalhasAtivas();
                    tempoParaPergunta = 20;
                }
            }

            if (gameOver)
                break;

            string hh = horas.ToString(), mm = minutos.ToString(), ss = segundos.ToString();
            if (hh.Length == 1) hh = "0" + hh; if (mm.Length == 1) mm = "0" + mm; if (ss.Length == 1) ss = "0" + ss;
            tempoTxt.text = "Tempo: " + hh + ":" + mm + ":" + ss;
        }
    }

    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public void Respondido()
    {
        respondendo = false;
    }
}
