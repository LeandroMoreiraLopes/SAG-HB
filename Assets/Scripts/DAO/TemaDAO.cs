using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class TemaDAO {

    public List<Tema> PegarTodos()
    {
        List<Tema> temas = new List<Tema>();
        Tema umTema;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Tema_PegarTodos";

            //execução sem retorno
            MySqlDataReader rsTema = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsTema.HasRows)
            {
                //enquanto lê cada linha
                while (rsTema.Read())
                {
                    //criando um aluno para cada linha
                    umTema = new Tema();
                    umTema.SetId(rsTema.GetInt32("id"));
                    umTema.SetNome(rsTema.GetString("nome"));
                    umTema.SetDescricao(rsTema.GetString("descricao"));
                    umTema.SetSerie(rsTema.GetString("serie"));
                    umTema.SetMatId(rsTema.GetInt32("materia_id"));

                    temas.Add(umTema);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listar os temas. Código " + ex.ToString());
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
        return temas;
    }

    public void Incluir(Tema tema)
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
            mySQLcmd.CommandText = "Tema_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_NOME", tema.GetNome());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", tema.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_SERIE", tema.GetSerie());
            mySQLcmd.Parameters.AddWithValue("LOC_MATERIA_ID", tema.GetMatId());

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
                throw new ExcecaoSAG("Erro na inclusão do tema. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão do tema. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }

    }

    public void Alterar(Tema tema)
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
            mySQLcmd.CommandText = "Tema_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", tema.GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_NOME", tema.GetNome());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", tema.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_SERIE", tema.GetSerie());
            mySQLcmd.Parameters.AddWithValue("LOC_MATERIA_ID", tema.GetMatId());

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
            throw new ExcecaoSAG("Erro na alteração do tema. Código " + ex.ToString());
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
    }

    public void Excluir(Tema tema)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Tema_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", tema.GetId());

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
                throw new ExcecaoSAG("Erro na exclusão do tema. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na exclusão do tema. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Tema umTema)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Tema_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umTema.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsTema = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsTema.HasRows)
            {
                while (rsTema.Read())
                {
                    umTema.SetNome(rsTema.GetString("nome"));
                    umTema.SetDescricao(rsTema.GetString("descricao"));
                    umTema.SetSerie(rsTema.GetString("serie"));
                    umTema.SetMatId(rsTema.GetInt32("materia_id"));
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Tema não encontrado.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar um tema. Código " + ex.ToString());
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

}