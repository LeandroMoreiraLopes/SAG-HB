using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    static int segundos, minutos, horas;
    bool gameOver;

    public void IniciarContagem()
    {
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
            if (gameOver)
                break;
        }
    }

    public void SetGameOver(bool b)
    {
        gameOver = b;
    }
}
