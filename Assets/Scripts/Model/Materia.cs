using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materia {

    int id;
    string nome, descricao;

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
}
