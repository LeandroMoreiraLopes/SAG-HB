using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tema {

    int id, matId;
    string nome, descricao, serie;

    public int GetId()
    {
        return id;
    }

    public void SetId(int i)
    {
        id = i;
    }

    public string GetNome()
    {
        return nome;
    }

    public void SetNome(string s)
    {
        nome = s;
    }

    public string GetDescricao()
    {
        return descricao;
    }

    public void SetDescricao(string s)
    {
        descricao = s;
    }

    public string GetSerie()
    {
        return serie;
    }

    public void SetSerie(string s)
    {
        serie = s;
    }

    public int GetMatId()
    {
        return matId;
    }

    public void SetMatId(int i)
    {
        matId = i;
    }
}
