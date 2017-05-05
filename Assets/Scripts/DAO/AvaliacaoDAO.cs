﻿using System.Collections.Generic;
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
            mySQLcmd.CommandText = "Avaliacao_PegarTodosCompleto";

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
                    matDAO.Carregar(umaMateria);
                    umaAvaliacao.SetMateria(umaMateria);

                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(rsAvaliacao.GetInt32("funcionario_id"));
                    funcDAO.Carregar(umFuncionario);
                    umaAvaliacao.SetFuncionarioAutor(umFuncionario);

                    alunos = alunoDAO.PegarAlunosPorAvaliacao(umaAvaliacao.GetId());
                    umaAvaliacao.SetAlunos(alunos);

                    temas = temaDAO.PegarTemasPorMateria(umaMateria.GetId());
                    umaAvaliacao.SetTemas(temas);

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

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            avaliacao.SetId((int)mySQLcmd.LastInsertedId);
            
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

    public void Carregar(Avaliacao avaliacao)
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

        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Avaliacao_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", avaliacao.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsAvaliacao = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAvaliacao.HasRows)
            {
                while (rsAvaliacao.Read())
                {
                    umaAvaliacao = new Avaliacao();
                    umaAvaliacao.SetId(rsAvaliacao.GetInt32("id"));
                    umaAvaliacao.SetDescricao(rsAvaliacao.GetString("descricao"));
                    umaAvaliacao.SetDataInicio(rsAvaliacao.GetInt32("datainicio"));
                    umaAvaliacao.SetDataFim(rsAvaliacao.GetInt32("datafim"));
                    umaAvaliacao.SetSimulado(rsAvaliacao.GetBoolean("simulado"));

                    umaMateria = new Materia();
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
                mySQLcmd.CommandText = "Avaliacao_Aluno_Inserir";

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
                mySQLcmd.CommandText = "Tema_Avaliacao_Inserir";

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
            mySQLcmd.CommandText = "Avaliacao_Aluno_Alterar";

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
            mySQLcmd.CommandText = "Tema_Avaliacao_Alterar";

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
    }

}
