using System.Collections.Generic;

public class CtrCadastroFuncionario
{

    static CtrCadastroFuncionario umCtrCadastroFuncionario;
    FuncionarioDAO funcionarioDAO;

    public CtrCadastroFuncionario()
    {
        DAOFactory daoFactory = new DAOFactory();
        funcionarioDAO = daoFactory.getFuncionarioDAO();
    }

    public static CtrCadastroFuncionario getInstance()
    {
        if (umCtrCadastroFuncionario == null)
        {
            umCtrCadastroFuncionario = new CtrCadastroFuncionario();
        }
        return umCtrCadastroFuncionario;
    }

    public List<Funcionario> ListarTodos()
    {
        List<Funcionario> lista = new List<Funcionario>();
        try
        {
            lista = funcionarioDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Funcionario funcionario)
    {
        if (funcionario.GetMatricula() < 0)
        {
            throw new ExcecaoSAG("A Matrícula não dave ser negativa.");
        }
        if (funcionario.GetNomeCompleto() == null || funcionario.GetNomeCompleto().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Nome completo deve ser preenchido");
        }
        if (funcionario.GetNascimento() <= 0)
        {
            throw new ExcecaoSAG("Data de nascimento deve ser preenchida");
        }
        if (funcionario.GetCpf() == null || funcionario.GetCpf().Trim().Equals(""))
        {
            throw new ExcecaoSAG("CPF deve ser preenchido");
        }
        if (funcionario.GetTelefone() <= 0)
        {
            throw new ExcecaoSAG("O Telefone deve ser preenchido com um número válido.");
        }
        if (funcionario.GetCelular() <= 0)
        {
            throw new ExcecaoSAG("O Celular deve ser preenchido com um número válido.");
        }
        if (funcionario.GetTelefone().ToString().Length <= 7)
        {
            throw new ExcecaoSAG("O Telefone deve ser preenchido com no mínimo 8 dígitos.");
        }
        if (funcionario.GetCelular().ToString().Length <= 8)
        {
            throw new ExcecaoSAG("O Celular deve ser preenchido com no mínimo 9 dígitos.");
        }
        if (funcionario.GetUsuario() == null || funcionario.GetUsuario().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Usuario deve ser preenchido");
        }
        if (funcionario.GetSenha() == null || funcionario.GetSenha().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Senha deve ser preenchido");
        }
        if (funcionario.GetEmail() == null || funcionario.GetEmail().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Email deve ser preenchido");
        }
        return true;
    }

    public void Incluir(Funcionario funcionario)
    {
        try
        {
            if (Validar(funcionario))
            {
                funcionarioDAO.Incluir(funcionario);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Alterar(Funcionario funcionario)
    {
        try
        {
            if (Validar(funcionario))
            {
                funcionarioDAO.Alterar(funcionario);
            }

        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Excluir(Funcionario funcionario)
    {
        try
        {
            funcionarioDAO.Excluir(funcionario);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Carregar(Funcionario funcionario)
    {
        try
        {
            funcionarioDAO.Carregar(funcionario);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
    }
}

