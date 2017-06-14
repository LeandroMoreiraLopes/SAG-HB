using UnityEngine;
using System.Collections;

public class PerguntaDaAvaliacaoDoAluno : MonoBehaviour
{
    int id, avaliacao_id, pergunta_id;
    bool correta;

    public int getId()
    {
        return id;
    }
    
    public void setId(int i)
    {
        id = i;
    }

    public int getAvaliacaoId()
    {
        return avaliacao_id;
    }

    public void setAvaliacaoId(int i)
    {
        avaliacao_id = i;
    }
    public int getPerguntaId()
    {
        return pergunta_id;
    }

    public void setPerguntaId(int i)
    {
        pergunta_id = i;
    }

    public bool getCorreta()
    {
        return correta;
    }

    public void setCorreta(bool b)
    {
        correta = b;
    }
}
