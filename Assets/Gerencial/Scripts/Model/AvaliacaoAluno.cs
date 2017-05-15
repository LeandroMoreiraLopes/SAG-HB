using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaliacaoAluno {

    Aluno aluno;
    Avaliacao avaliacao;

    int dataRealizacao, tema1TotalDePerguntas, tema1TotalDeAcertos, tema2TotalDePerguntas, tema2TotalDeAcertos,
        tema3TotalDePerguntas, tema3TotalDeAcertos, tema4TotalDePerguntas, tema4TotalDeAcertos;

    public Aluno getAluno()
    {
        return aluno;
    }

    public void SetAluno(Aluno a)
    {
        aluno = a;
    }

    public Avaliacao GetAvaliacao()
    {
        return avaliacao;
    }

    public void SetAvaliacao(Avaliacao a)
    {
        avaliacao = a;
    }

    public int GetDataRealizacao()
    {
        return dataRealizacao;
    }

    public void SetDataRealizacao(int i)
    {
        dataRealizacao = i;
    }

    public int GetTema1TotalDePerguntas()
    {
        return tema1TotalDePerguntas;
    }

    public void SetTema1TotalDePerguntas(int i)
    {
        tema1TotalDePerguntas = i;
    }

    public int GetTema2TotalDePerguntas()
    {
        return tema2TotalDePerguntas;
    }

    public void SetTema2TotalDePerguntas(int i)
    {
        tema2TotalDePerguntas = i;
    }

    public int GetTema3TotalDePerguntas()
    {
        return tema3TotalDePerguntas;
    }

    public void SetTema3TotalDePerguntas(int i)
    {
        tema3TotalDePerguntas = i;
    }

    public int GetTema4TotalDePerguntas()
    {
        return tema4TotalDePerguntas;
    }

    public void SetTema4TotalDePerguntas(int i)
    {
        tema4TotalDePerguntas = i;
    }

    public int GetTema1TotalDeAcertos()
    {
        return tema1TotalDeAcertos;
    }

    public void SetTema1TotalDeAcertos(int i)
    {
        tema1TotalDeAcertos = i;
    }

    public int GetTema2TotalDeAcertos()
    {
        return tema2TotalDeAcertos;
    }

    public void SetTema2TotalDeAcertos(int i)
    {
        tema2TotalDeAcertos = i;
    }

    public int GetTema3TotalDeAcertos()
    {
        return tema3TotalDeAcertos;
    }

    public void SetTema3TotalDeAcertos(int i)
    {
        tema3TotalDeAcertos = i;
    }

    public int GetTema4TotalDeAcertos()
    {
        return tema4TotalDeAcertos;
    }

    public void SetTema4TotalDeAcertos(int i)
    {
        tema4TotalDeAcertos = i;
    }
}
