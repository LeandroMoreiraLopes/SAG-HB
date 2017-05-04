using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrLogin {

    static CtrLogin umCtrLogin;
    LoginDAO loginDAO;

    public CtrLogin()
    {
        DAOFactory daoFactory = new DAOFactory();
        loginDAO = daoFactory.getLoginDAO();
    }

    public static CtrLogin getInstance()
    {
        if (umCtrLogin == null)
        {
            umCtrLogin = new CtrLogin();
        }
        return umCtrLogin;
    }

    private bool Validar(Pessoa pessoa)
    {
        
        if (pessoa.GetUsuario() == null || pessoa.GetUsuario().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Usuario deve ser preenchido");
        }
        if (pessoa.GetSenha() == null || pessoa.GetSenha().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Senha deve ser preenchido");
        }
        return true;
    }

    public int Logar(Aluno pessoa)
    {
        int statusLogin;
        try
        {
            statusLogin = loginDAO.Login(pessoa);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return statusLogin;
    }

    public int GetFuncionarioId(Funcionario funcionario)
    {
        int idFuncionarioLogado;
        try
        {
            idFuncionarioLogado = loginDAO.GetFuncionarioId(funcionario);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return idFuncionarioLogado;
    }
}