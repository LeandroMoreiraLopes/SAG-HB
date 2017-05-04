
using System.Data;
using MySql.Data.MySqlClient;

public class LoginDAO {

    public int Login(Aluno umaPessoa)
    {
        MySqlConnection db = Connection.getConnection();
                
        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();
            mySQLcmd.CommandType = CommandType.Text;
            mySQLcmd.CommandText = "Select LOGIN(@Usuario,@Senha) AS Login";
            mySQLcmd.Parameters.AddWithValue("@Usuario", umaPessoa.GetUsuario());
            mySQLcmd.Parameters.AddWithValue("@Senha", umaPessoa.GetSenha());

            //execução sem retorno
            return (int)mySQLcmd.ExecuteScalar();
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao logar um usuário. Código " + ex.ToString());
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

    public int GetFuncionarioId(Funcionario umFuncionario)
    {
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();
            mySQLcmd.CommandType = CommandType.Text;
            mySQLcmd.CommandText = string.Format("Select * from funcionario where usuario = '{0}' and senha = '{1}';", 
                                                    umFuncionario.GetUsuario(), umFuncionario.GetSenha());
            
            //execução sem retorno
            MySqlDataReader rsFuncionario =  mySQLcmd.ExecuteReader();

            if (rsFuncionario.HasRows)
            {
                while (rsFuncionario.Read())
                {
                    return rsFuncionario.GetInt32("id");
                }
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao logar um usuário. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        return 0;
    }
}