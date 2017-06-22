using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class AvaliacaoDAO {

    public List<Avaliacao> PegarTodos()
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        Avaliacao umaAvaliacao;

        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_PegarTodos";

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacao = new Avaliacao();
                    umaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));
                                 
                    avaliacoes.Add(umaAvaliacao);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listar as avaliacoes. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return avaliacoes;
    }

    public List<Avaliacao> PegarTodosCompleto()
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        Avaliacao umaAvaliacao;
        Materia umaMateria;
        Funcionario umFuncionario;
        List<Aluno> alunos = new List<Aluno>();
        List<Tema> temas = new List<Tema>();

        DAOFactory daoFactory = new DAOFactory();
        MateriaDAO matDAO = daoFactory.getMateriaDAO();
        FuncionarioDAO funcDAO = daoFactory.getFuncionarioDAO();
        AlunoDAO alunoDAO = daoFactory.getAlunoDAO();
        TemaDAO temaDAO = daoFactory.getTemaDAO();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_PegarTodos";

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacao = new Avaliacao();
                    umaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));

                   /* umaMateria = new Materia();
                    umaMateria.SetId(rsAvaliacao.GetInt32("materia_id"));
                    matDAO.Carregar(umaMateria);
                    umaAvaliacao.SetMateria(umaMateria);

                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(rsAvaliacao.GetInt32("funcionario_id"));
                    funcDAO.Carregar(umFuncionario);
                    umaAvaliacao.SetFuncionarioAutor(umFuncionario);

                    alunos = alunoDAO.PegarAlunosPorAvaliacao(umaAvaliacao.GetId());
                    umaAvaliacao.SetAlunos(alunos);

                    temas = temaDAO.PegarTemasPorMateria(umaMateria.GetId());
                    umaAvaliacao.SetTemas(temas);*/

                    avaliacoes.Add(umaAvaliacao);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listar as avaliacoes. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }

        for (int i = 0; i < avaliacoes.Count; i++)
        {
            Carregar(avaliacoes[i]);
        }

        //retorna a lista de alunos
        return avaliacoes;
    }

    public List<Avaliacao> PegarTodosPorAluno(int alunoId)
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        Avaliacao umaAvaliacao;
        Materia umaMateria;
        Funcionario umFuncionario;
        List<Aluno> alunos = new List<Aluno>();
        List<Tema> temas = new List<Tema>();

        DAOFactory daoFactory = new DAOFactory();
        MateriaDAO matDAO = daoFactory.getMateriaDAO();
        FuncionarioDAO funcDAO = daoFactory.getFuncionarioDAO();
        AlunoDAO alunoDAO = daoFactory.getAlunoDAO();
        TemaDAO temaDAO = daoFactory.getTemaDAO();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_PegarTodos_Avaliacao";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", alunoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacao = new Avaliacao();
                    umaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));

                    umaMateria = new Materia();
                    umaMateria.SetId(rsAvaliacao.GetInt32("materia_id"));
                    umaAvaliacao.SetMateria(umaMateria);

                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(rsAvaliacao.GetInt32("funcionario_id"));
                    umaAvaliacao.SetFuncionarioAutor(umFuncionario);

                    avaliacoes.Add(umaAvaliacao);
                }
                db.Close();
                for (int i = 0; i < avaliacoes.Count; i++)
                {
                    matDAO.Carregar(avaliacoes[i].GetMateria());
                    funcDAO.Carregar(avaliacoes[i].GetFuncionarioAutor());
                    alunos = alunoDAO.PegarAlunosPorAvaliacao(avaliacoes[i].GetId());
                    avaliacoes[i].SetAlunos(alunos);
                    temas = temaDAO.PegarTemasPorAvaliacao(avaliacoes[i].GetId());
                    avaliacoes[i].SetTemas(temas);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listarAAAA as avaliacoes por aluno. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return avaliacoes;
    }

    public List<Avaliacao> PegarSimuladosPorAluno(int alunoId)
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        Avaliacao umaAvaliacao;
        Materia umaMateria;
        Funcionario umFuncionario;
        List<Aluno> alunos = new List<Aluno>();
        List<Tema> temas = new List<Tema>();

        DAOFactory daoFactory = new DAOFactory();
        MateriaDAO matDAO = daoFactory.getMateriaDAO();
        FuncionarioDAO funcDAO = daoFactory.getFuncionarioDAO();
        AlunoDAO alunoDAO = daoFactory.getAlunoDAO();
        TemaDAO temaDAO = daoFactory.getTemaDAO();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_PegarTodos_Simulado";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", alunoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacao = new Avaliacao();
                    umaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));

                    umaMateria = new Materia();
                    umaMateria.SetId(rsAvaliacao.GetInt32("materia_id"));
                    umaAvaliacao.SetMateria(umaMateria);

                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(rsAvaliacao.GetInt32("funcionario_id"));
                    umaAvaliacao.SetFuncionarioAutor(umFuncionario);

                    avaliacoes.Add(umaAvaliacao);
                }
                db.Close();
                for (int i = 0; i < avaliacoes.Count; i++)
                {
                    matDAO.Carregar(avaliacoes[i].GetMateria());
                    funcDAO.Carregar(avaliacoes[i].GetFuncionarioAutor());
                    alunos = alunoDAO.PegarAlunosPorAvaliacao(avaliacoes[i].GetId());
                    avaliacoes[i].SetAlunos(alunos);
                    temas = temaDAO.PegarTemasPorAvaliacao(avaliacoes[i].GetId());
                    avaliacoes[i].SetTemas(temas);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listarBBBB as avaliacoes por aluno. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return avaliacoes;
    }

    public List<AvaliacaoAluno> PegarDadosDaAvaliacaoAlunoPorAvaliacao(int avaliacaoId)
    {
        List<AvaliacaoAluno> avaliacoesDeAlunos = new List<AvaliacaoAluno>();
        AvaliacaoAluno umaAvaliacaoDeAluno;
        
        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Carregar_AvaliacaoDoAluno_Por_Avaliacao";

            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacaoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacaoDeAluno = new AvaliacaoAluno();
                    umaAvaliacaoDeAluno.setId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacaoDeAluno.SetDataRealizacao(rsAvaliacao.GetInt32("data_realizacao"));
                    
                    avaliacoesDeAlunos.Add(umaAvaliacaoDeAluno);
                }
                db.Close();
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listarCCC as avaliacoes por aluno. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return avaliacoesDeAlunos;
    }

    public List<PerguntaDaAvaliacaoDoAluno> PegarPerguntasDeUmaAvaliacao(int avaliacaoDoAlunoId)
    {
        List<PerguntaDaAvaliacaoDoAluno> perguntasDaAvaliacaoDoAluno = new List<PerguntaDaAvaliacaoDoAluno>();
        PerguntaDaAvaliacaoDoAluno umaPerguntaDaAvaliacaoDeAluno;

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Carregar_Perguntas_Da_Avaliacao_Do_Aluno";

            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_DO_ALUNO_ID", avaliacaoDoAlunoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaPerguntaDaAvaliacaoDeAluno = new PerguntaDaAvaliacaoDoAluno();
                    umaPerguntaDaAvaliacaoDeAluno.setId(rsAvaliacao.GetInt32("id"));
                    umaPerguntaDaAvaliacaoDeAluno.setAvaliacaoId(rsAvaliacao.GetInt32("AVALIACAO_DO_ALUNO_ID"));
                    umaPerguntaDaAvaliacaoDeAluno.setPerguntaId(rsAvaliacao.GetInt32("pergunta_id"));
                    umaPerguntaDaAvaliacaoDeAluno.setCorreta(rsAvaliacao.GetBoolean("correta"));
                    perguntasDaAvaliacaoDoAluno.Add(umaPerguntaDaAvaliacaoDeAluno);
                }
                db.Close();
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listarDDDD as avaliacoes por aluno. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return perguntasDaAvaliacaoDoAluno;
    }

    public List<Tema> PegarTemasDaAvaliacaoAluno(int avaliacaoId)
    {
        List<Tema> temasDaAvaliacao = new List<Tema>();
        Tema umTemaDaAvaliacao;

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Carregar_Temas_Da_Avaliacao";

            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacaoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umTemaDaAvaliacao = new Tema();
                    umTemaDaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umTemaDaAvaliacao.SetNome(rsAvaliacao.GetString("nome"));
                    umTemaDaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umTemaDaAvaliacao.SetSerie(rsAvaliacao.GetString("serie"));
                    umTemaDaAvaliacao.SetMatId(rsAvaliacao.GetInt32("materia_id"));

                    temasDaAvaliacao.Add(umTemaDaAvaliacao);
                }
                db.Close();
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listar os temas da avaliacao. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return temasDaAvaliacao;
    }

    public AvaliacaoAluno PegarDadosDaAvaliacaoAlunoDeUmAlunoEmUmaAvaliacao(int alunoId, int avaliacaoId)
    {
        AvaliacaoAluno umaAvaliacaoDeAluno = new AvaliacaoAluno();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Carregar_AvaliacaoDoAluno_Por_AvaliacaoeAluno";

            mySQLcmd.Parameters.AddWithValue("LOC_ALUNO_ID", alunoId);
            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacaoId);

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                //enquanto lê cada linha
                while (rsAvaliacao.Read())
                {
                    //criando um aluno para cada linha
                    umaAvaliacaoDeAluno.setId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacaoDeAluno.SetDataRealizacao(rsAvaliacao.GetInt32("data_realizacao"));
                    //umaAvaliacaoDeAluno.SetTema1TotalDePerguntas(rsAvaliacao.GetInt32("tema1_totalperguntas"));
                    //umaAvaliacaoDeAluno.SetTema1TotalDeAcertos(rsAvaliacao.GetInt32("tema1_totalacertos"));
                    //umaAvaliacaoDeAluno.SetTema2TotalDePerguntas(rsAvaliacao.GetInt32("tema2_totalperguntas"));
                    //umaAvaliacaoDeAluno.SetTema2TotalDeAcertos(rsAvaliacao.GetInt32("tema2_totalacertos"));
                    //umaAvaliacaoDeAluno.SetTema3TotalDePerguntas(rsAvaliacao.GetInt32("tema3_totalperguntas"));
                    //umaAvaliacaoDeAluno.SetTema3TotalDeAcertos(rsAvaliacao.GetInt32("tema3_totalacertos"));
                    //umaAvaliacaoDeAluno.SetTema4TotalDePerguntas(rsAvaliacao.GetInt32("tema4_totalperguntas"));
                    //umaAvaliacaoDeAluno.SetTema4TotalDeAcertos(rsAvaliacao.GetInt32("tema4_totalacertos"));
                }
                db.Close();
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listarEEEE as avaliacoes por aluno. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return umaAvaliacaoDeAluno;
    }

    public void Incluir(Avaliacao avaliacao)
    {
        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", avaliacao.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_DATAINICIO", avaliacao.GetDataInicio());
            mySQLcmd.Parameters.AddWithValue("LOC_DATAFIM", avaliacao.GetDataFim());
            mySQLcmd.Parameters.AddWithValue("LOC_SIMULADO", avaliacao.GetSimulado());
            mySQLcmd.Parameters.AddWithValue("LOC_FUNCIONARIO_ID", avaliacao.GetFuncionarioAutor().GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_MATERIA_ID", avaliacao.GetMateria().GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            int lastIndex = 0;
            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();
            //lastIndex = mySQLcmd.ExecuteNonQuery();
            while (rsAvaliacao.Read())
            {
               lastIndex = rsAvaliacao.GetInt32(0);
            }
            
            avaliacao.SetId(lastIndex);
            rsAvaliacao.Close();
            
            //commit da transação
            mySQLTransaction.Commit();

        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na inclusão da pergunta. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na inclusão da pergunta. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }

        InserirAvaliacaoAluno(avaliacao.GetId(), avaliacao.GetAlunos());
        InserirAvaliacaoTema(avaliacao.GetId(), avaliacao.GetTemas());

    }

    public void Alterar(Avaliacao avaliacao)
    {
        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", avaliacao.GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", avaliacao.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_DATAINICIO", avaliacao.GetDataInicio());
            mySQLcmd.Parameters.AddWithValue("LOC_DATAFIM", avaliacao.GetDataFim());
            mySQLcmd.Parameters.AddWithValue("LOC_SIMULADO", avaliacao.GetSimulado());
            mySQLcmd.Parameters.AddWithValue("LOC_FUNCIONARIO_ID", avaliacao.GetFuncionarioAutor().GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_MATERIA_ID", avaliacao.GetMateria().GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro na alteração da pergunta. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            mySQLTransaction.Rollback();
            throw ex;
        }
        finally
        {
            db.Close();
        }

        AlterarAvaliacaoAluno(avaliacao.GetId(), avaliacao.GetAlunos());
        AlterarAvaliacaoTema(avaliacao.GetId(), avaliacao.GetTemas());
    }

    public void Excluir(Avaliacao avaliacao)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", avaliacao.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na exclusão da pergunta. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na exclusão da pergunta. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Avaliacao umaAvaliacao)
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        Materia umaMateria;
        Funcionario umFuncionario;
        List<Aluno> alunos = new List<Aluno>();
        List<Tema> temas = new List<Tema>();

        DAOFactory daoFactory = new DAOFactory();
        MateriaDAO matDAO = daoFactory.getMateriaDAO();
        FuncionarioDAO funcDAO = daoFactory.getFuncionarioDAO();
        AlunoDAO alunoDAO = daoFactory.getAlunoDAO();
        TemaDAO temaDAO = daoFactory.getTemaDAO();

        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umaAvaliacao.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                if (rsAvaliacao.Read())
                {
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));

                    int materia_id = rsAvaliacao.GetInt32("materia_id");
                    int funcionario_id = rsAvaliacao.GetInt32("funcionario_id");

                    db.Close();

                    umaMateria = new Materia();
                    umaMateria.SetId(materia_id);
                    matDAO.Carregar(umaMateria);
                    umaAvaliacao.SetMateria(umaMateria);

                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(funcionario_id);
                    funcDAO.Carregar(umFuncionario);
                    umaAvaliacao.SetFuncionarioAutor(umFuncionario);

                    alunos = alunoDAO.PegarAlunosPorAvaliacao(umaAvaliacao.GetId());
                    umaAvaliacao.SetAlunos(alunos);

                    temas = temaDAO.PegarTemasPorMateria(umaMateria.GetId());
                    umaAvaliacao.SetTemas(temas);
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Pergunta não encontrado.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar uma Pergunta. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    void InserirAvaliacaoAluno(int avaliacao_id, List<Aluno> alunos)
    {
        for (int i = 0; i < alunos.Count; i++)
        {
            //conexão
            MySqlConnection db = Connection.getConnection();

            //transação
            MySqlTransaction mySQLTransaction;
            mySQLTransaction = db.BeginTransaction();

            try
            {
                //comando na conexão para execução da procedure
                MySqlCommand mySQLcmd = db.CreateCommand();

                //setando a procedure do banco
                mySQLcmd.CommandType = CommandType.StoredProcedure;
                mySQLcmd.CommandText = "AvaliacaoDoAluno_Inserir";

                //preenchendo os parametros da procedure
                mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacao_id);
                mySQLcmd.Parameters.AddWithValue("LOC_ALUNO_ID", alunos[i].GetId());
                
                //ligando a transação
                mySQLcmd.Transaction = mySQLTransaction;

                //execução sem retorno
                mySQLcmd.ExecuteNonQuery();

                //commit da transação
                mySQLTransaction.Commit();

            }
            catch (MySqlException ex)
            {
                try
                {
                    //rollback caso haja erro no MySQL
                    mySQLTransaction.Rollback();
                }
                catch (MySqlException ex1)
                {
                    throw new ExcecaoSAG("Erro na inclusão da avaliação. Código " + ex1.ToString());
                }
            }
            catch (ExcecaoSAG ex)
            {
                try
                {
                    //rollback caso haja erro na aplicação
                    mySQLTransaction.Rollback();
                }
                catch (MySqlException ex1)
                {
                    throw new ExcecaoSAG("Erro na inclusão da avaliação. Código " + ex1.ToString());
                }
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }

    void InserirAvaliacaoTema(int avaliacao_id, List<Tema> temas)
    {
        for (int i = 0; i < temas.Count; i++)
        {
            //conexão
            MySqlConnection db = Connection.getConnection();

            //transação
            MySqlTransaction mySQLTransaction;
            mySQLTransaction = db.BeginTransaction();

            try
            {
                //comando na conexão para execução da procedure
                MySqlCommand mySQLcmd = db.CreateCommand();

                //setando a procedure do banco
                mySQLcmd.CommandType = CommandType.StoredProcedure;
                mySQLcmd.CommandText = "TemasAvaliacao_Inserir";

                //preenchendo os parametros da procedure
                mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacao_id);
                mySQLcmd.Parameters.AddWithValue("LOC_TEMA_ID", temas[i].GetId());

                //ligando a transação
                mySQLcmd.Transaction = mySQLTransaction;

                //execução sem retorno
                mySQLcmd.ExecuteNonQuery();

                //commit da transação
                mySQLTransaction.Commit();

            }
            catch (MySqlException ex)
            {
                try
                {
                    //rollback caso haja erro no MySQL
                    mySQLTransaction.Rollback();
                }
                catch (MySqlException ex1)
                {
                    throw new ExcecaoSAG("Erro na inclusão da avaliação. Código " + ex1.ToString());
                }
            }
            catch (ExcecaoSAG ex)
            {
                try
                {
                    //rollback caso haja erro na aplicação
                    mySQLTransaction.Rollback();
                }
                catch (MySqlException ex1)
                {
                    throw new ExcecaoSAG("Erro na inclusão da avaliação. Código " + ex1.ToString());
                }
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }

    void AlterarAvaliacaoAluno(int avaliacao_id, List<Aluno> alunos)
    {
        string arrayDeIdDeAlunos = "";
        for (int i = 0; i < alunos.Count; i++)
        {
            arrayDeIdDeAlunos += alunos[i].GetId().ToString() + "|";
        }

        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "AvaliacaoDoAluno_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacao_id);
            mySQLcmd.Parameters.AddWithValue("LOC_ALUNOS", arrayDeIdDeAlunos);

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();

        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na alteração da avaliação. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na alteração da avaliação. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    void AlterarAvaliacaoTema(int avaliacao_id, List<Tema> temas)
    {
        string arrayDeIdDeTemas = "";
        for (int i = 0; i < temas.Count; i++)
        {
            arrayDeIdDeTemas += temas[i].GetId().ToString() + "|";
        }

        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "TemasAvaliacao_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacao_id);
            mySQLcmd.Parameters.AddWithValue("LOC_TEMAS", arrayDeIdDeTemas);

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();

        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na alteração da avaliação. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na alteração da avaliação. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public int SalvarResultadoAvaliacao(int id_aluno, int id_avaliacao, int dataRealiacao)
    {
        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        int lastIndex = 0;

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "AvaliacaoDoAluno_Preenche_Dados";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", id_avaliacao);
            mySQLcmd.Parameters.AddWithValue("LOC_ALUNO_ID", id_aluno);
            mySQLcmd.Parameters.AddWithValue("LOC_DATA_REALIZACAO", dataRealiacao);

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;
                        
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();
            //lastIndex = mySQLcmd.ExecuteNonQuery();
            while (rsAvaliacao.Read())
            {
                lastIndex = rsAvaliacao.GetInt32(0);
            }
            rsAvaliacao.Close();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro na gravação dos dados da avaliação. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            mySQLTransaction.Rollback();
            throw ex;
        }
        finally
        {
            db.Close();
        }

        return lastIndex;
    }
}
