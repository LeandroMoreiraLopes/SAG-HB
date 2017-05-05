using System.Collections.Generic;

public class CtrCadastroAluno{

	static CtrCadastroAluno umCtrCadastroAluno;
    AlunoDAO alunoDAO;

    public CtrCadastroAluno()
    {
        DAOFactory daoFactory = new DAOFactory();
        alunoDAO = daoFactory.getAlunoDAO();
    }
    
    public static CtrCadastroAluno getInstance(){
		if (umCtrCadastroAluno == null){
			umCtrCadastroAluno = new CtrCadastroAluno();
		}
		return umCtrCadastroAluno;
	} 

	public List <Aluno> ListarTodos()
	{
        List<Aluno> lista = new List<Aluno>();
        try
        {
            lista = alunoDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
	}

    public List<Aluno> ListarTodosPorAvaliacao(int materia_id)
    {
        List<Aluno> lista = new List<Aluno>();
        try
        {
            lista = alunoDAO.PegarAlunosPorAvaliacao(materia_id);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Aluno aluno){
        if (aluno.GetMatricula() < 0) {
			throw new ExcecaoSAG("A Matrícula não dave ser negativa.") ;
		}
		if(aluno.GetNomeCompleto() == null || aluno.GetNomeCompleto().Trim().Equals("")){
			throw new ExcecaoSAG("Nome completo deve ser preenchido");
		}
		if(aluno.GetNascimento() <= 0){
			throw new ExcecaoSAG("Data de nascimento deve ser preenchida");
		}
		if(aluno.GetCpf() == null || aluno.GetCpf().Trim().Equals("")){
			throw new ExcecaoSAG("CPF deve ser preenchido");
		}
		if (aluno.GetTelefone() <= 0) {
			throw new ExcecaoSAG("O Telefone deve ser preenchido com um número válido.") ;
		}
		if (aluno.GetCelular() <= 0) {
			throw new ExcecaoSAG("O Celular deve ser preenchido com um número válido.") ;
		}
        if (aluno.GetTelefone().ToString().Length <= 7)
        {
            throw new ExcecaoSAG("O Telefone deve ser preenchido com no mínimo 8 dígitos.");
        }
        if (aluno.GetCelular().ToString().Length <= 8)
        {
            throw new ExcecaoSAG("O Celular deve ser preenchido com no mínimo 9 dígitos.");
        }
        if (aluno.GetUsuario() == null || aluno.GetUsuario().Trim().Equals("")){
			throw new ExcecaoSAG("Usuario deve ser preenchido");
		}
		if(aluno.GetSenha() == null || aluno.GetSenha().Trim().Equals("")){
			throw new ExcecaoSAG("Senha deve ser preenchido");
		} 
		if(aluno.GetEmail() == null || aluno.GetEmail().Trim().Equals("")){
			throw new ExcecaoSAG("Email deve ser preenchido");
		} 
		return true ;
	}

	public void Incluir (Aluno aluno){
        try
        {
            if (Validar(aluno))
            {
                alunoDAO.Incluir(aluno);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

	public void Alterar (Aluno aluno){
        try
        {
            if (Validar(aluno))
            {
                alunoDAO.Alterar(aluno);
            }

        }
        catch (ExcecaoSAG ex)
        {
			throw new ExcecaoSAG(ex.getMsg());
		}

	}

	public void Excluir (Aluno aluno){
        try
        {
            alunoDAO.Excluir(aluno);
        }
        catch (ExcecaoSAG ex)
        {
			throw new ExcecaoSAG(ex.getMsg());
		}

	}

	public void Carregar(Aluno aluno){
		try
        {
			alunoDAO.Carregar(aluno);
		}
        catch (ExcecaoSAG ex)
        {
			throw new ExcecaoSAG(ex.getMsg());
		}
	}
}
