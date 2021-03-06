﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAOFactory {

    // Aqui entram as implementa��es dos m�todos abstratos 
    // especificados em DAOFactory.

    public TemaDAO getTemaDAO()
    {
        return new TemaDAO();
    }

    public MateriaDAO getMateriaDAO()
    {
        return new MateriaDAO();
    }

    public AlunoDAO getAlunoDAO()
    {
        return new AlunoDAO();
    }

    public LoginDAO getLoginDAO()
    {
        return new LoginDAO();
    }

    public FuncionarioDAO getFuncionarioDAO()
    {
        return new FuncionarioDAO();
    }

    public PerguntaDAO getPerguntaDAO()
    {
        return new PerguntaDAO();
    }

    public AvaliacaoDAO getAvaliacaoDAO()
    {
        return new AvaliacaoDAO();
    }
    /*
    public SalaDAO getSalaDAO()
    {
        return new SalaDAO();
    }*/
}
