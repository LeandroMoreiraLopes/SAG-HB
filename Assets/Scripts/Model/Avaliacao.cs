using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avaliacao {

    int id;

    List<Tema> temas;
    Materia materia;
    Funcionario funcionarioAutor;
    List<Aluno> alunos;

    string descricao;
    int dataInicio, dataFim;
    bool simulado;

    public int GetId()
    {
        return id;
    }

    public void SetId(int i)
    {
        id = i;
    }

    public List<Tema> GetTemas()
    {
        return temas;
    }

    public void SetTemas(List<Tema> l)
    {
        temas = l;
    }

    public Materia GetMateria()
    {
        return materia;
    }

    public void SetMateria(Materia m)
    {
        materia = m;
    }

    public Funcionario GetFuncionarioAutor()
    {
        return funcionarioAutor;
    }

    public void SetFuncionarioAutor(Funcionario f)
    {
        funcionarioAutor = f;
    }

    public List<Aluno> GetAlunos()
    {
        return alunos;
    }

    public void SetAlunos(List<Aluno> l)
    {
        alunos = l;
    }

    public string GetDescricao()
    {
        return descricao;
    }

    public void SetDescricao(string s)
    {
        descricao = s;
    }

    public int GetDataInicio()
    {
        return dataInicio;
    }

    public void SetDataInicio(int i)
    {
        dataInicio = i;
    }

    public int GetDataFim()
    {
        return dataFim;
    }

    public void SetDataFim(int i)
    {
        dataFim = i;
    }

    public bool GetSimulado()
    {
        return simulado;
    }

    public void SetSimulado(bool b)
    {
        simulado = b;
    }
}
