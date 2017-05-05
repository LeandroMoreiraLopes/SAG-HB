using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class AlunoDAO {

    public List<Aluno> PegarTodos()
    {
        List<Aluno> alunos = new List<Aluno>();
        Aluno umAluno;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Aluno_PegarTodos";

            //mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsAluno = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAluno.HasRows)
            {
                //enquanto lê cada linha
                while (rsAluno.Read())
                {
                    //criando um aluno para cada linha
                    umAluno = new Aluno();
                    umAluno.SetId(rsAluno.GetInt32("id"));
                    umAluno.SetMatricula(rsAluno.GetInt32("matricula"));
                    umAluno.SetNomeCompleto(rsAluno.GetString("nomecompleto"));
                    umAluno.SetNascimento(rsAluno.GetInt32("nascimento"));
                    umAluno.SetCpf(rsAluno.GetString("cpf"));
                    umAluno.SetTelefone(rsAluno.GetInt32("telefone"));
                    umAluno.SetCelular(rsAluno.GetInt32("celular"));
                    umAluno.SetUsuario(rsAluno.GetString("usuario"));
                    umAluno.SetSenha(rsAluno.GetString("senha"));
                    umAluno.SetEmail(rsAluno.GetString("email"));

                    alunos.Add(umAluno);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            //rollback caso haja erro no MySQL
            //mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro ao listar os alunos. Código " + ex.ToString());
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
        return alunos;
    }

    public List<Aluno> PegarAlunosPorAvaliacao(int avaliacao_id)
    {
        List<Aluno> alunos = new List<Aluno>();
        Aluno umAluno;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Carregar_Alunos_Da_Avaliacao";

            mySQLcmd.Parameters.AddWithValue("LOC_AVALIACAO_ID", avaliacao_id);

            //execução sem retorno
            MySqlDataReader rsAluno = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAluno.HasRows)
            {
                //enquanto lê cada linha
                while (rsAluno.Read())
                {
                    //criando um aluno para cada linha
                    umAluno = new Aluno();
                    umAluno.SetId(rsAluno.GetInt32("id"));
                    umAluno.SetMatricula(rsAluno.GetInt32("matricula"));
                    umAluno.SetNomeCompleto(rsAluno.GetString("nomecompleto"));
                    umAluno.SetNascimento(rsAluno.GetInt32("nascimento"));
                    umAluno.SetCpf(rsAluno.GetString("cpf"));
                    umAluno.SetTelefone(rsAluno.GetInt32("telefone"));
                    umAluno.SetCelular(rsAluno.GetInt32("celular"));
                    umAluno.SetUsuario(rsAluno.GetString("usuario"));
                    umAluno.SetSenha(rsAluno.GetString("senha"));
                    umAluno.SetEmail(rsAluno.GetString("email"));

                    alunos.Add(umAluno);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            //rollback caso haja erro no MySQL
            //mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro ao listar os alunos. Código " + ex.ToString());
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
        return alunos;
    }

    public void Incluir(Aluno aluno)
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
            mySQLcmd.CommandText = "Aluno_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_MATRICULA", aluno.GetMatricula());
            mySQLcmd.Parameters.AddWithValue("LOC_NOMECOMPLETO", aluno.GetNomeCompleto());
            mySQLcmd.Parameters.AddWithValue("LOC_NASCIMENTO", aluno.GetNascimento());
            mySQLcmd.Parameters.AddWithValue("LOC_CPF", aluno.GetCpf());
            mySQLcmd.Parameters.AddWithValue("LOC_TELEFONE", aluno.GetTelefone());
            mySQLcmd.Parameters.AddWithValue("LOC_CELULAR", aluno.GetCelular());
            mySQLcmd.Parameters.AddWithValue("LOC_USUARIO", aluno.GetUsuario());
            mySQLcmd.Parameters.AddWithValue("LOC_SENHA", aluno.GetSenha());
            mySQLcmd.Parameters.AddWithValue("LOC_EMAIL", aluno.GetEmail());

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
                throw new ExcecaoSAG("Erro na inclusão do aluno. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão do aluno. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally {
            db.Close();
        }

    }

    public void Alterar(Aluno aluno)
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
            mySQLcmd.CommandText = "Aluno_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_MATRICULA", aluno.GetMatricula());
            mySQLcmd.Parameters.AddWithValue("LOC_NOMECOMPLETO", aluno.GetNomeCompleto());
            mySQLcmd.Parameters.AddWithValue("LOC_NASCIMENTO", aluno.GetNascimento());
            mySQLcmd.Parameters.AddWithValue("LOC_CPF", aluno.GetCpf());
            mySQLcmd.Parameters.AddWithValue("LOC_TELEFONE", aluno.GetTelefone());
            mySQLcmd.Parameters.AddWithValue("LOC_CELULAR", aluno.GetCelular());
            mySQLcmd.Parameters.AddWithValue("LOC_USUARIO", aluno.GetUsuario());
            mySQLcmd.Parameters.AddWithValue("LOC_SENHA", aluno.GetSenha());
            mySQLcmd.Parameters.AddWithValue("LOC_EMAIL", aluno.GetEmail());
            mySQLcmd.Parameters.AddWithValue("LOC_ID", aluno.GetId());

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
            throw new ExcecaoSAG("Erro na alteração do aluno. Código " + ex.ToString());
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

    public void Excluir(Aluno aluno)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Aluno_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", aluno.GetId());

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
                throw new ExcecaoSAG("Erro na exclusão do aluno. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão do aluno. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Aluno umAluno)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Aluno_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umAluno.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsAluno = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAluno.HasRows)
            {
                while (rsAluno.Read())
                {
                    //criando um aluno para cada linha
                    umAluno.SetMatricula(rsAluno.GetInt32("matricula"));
                    umAluno.SetNomeCompleto(rsAluno.GetString("nomecompleto"));
                    umAluno.SetNascimento(rsAluno.GetInt32("nascimento"));
                    umAluno.SetCpf(rsAluno.GetString("cpf"));
                    umAluno.SetTelefone(rsAluno.GetInt32("telefone"));
                    umAluno.SetCelular(rsAluno.GetInt32("celular"));
                    umAluno.SetUsuario(rsAluno.GetString("usuario"));
                    umAluno.SetSenha(rsAluno.GetString("senha"));
                    umAluno.SetEmail(rsAluno.GetString("email"));
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Aluno não encontrado.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar um aluno. Código " + ex.ToString());
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
