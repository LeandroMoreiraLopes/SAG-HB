using UnityEngine;
using MySql.Data.MySqlClient;

public class Connection {

    string host = "127.0.0.1";
    string database = "sag-hb";
    string user = "root";
    string password = "admin";
    
    bool pooling = true;
    
    private static Connection conexao = null;
    private MySqlConnection mySQLconn;
    string connectionString;

    private Connection()
    {

        connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";Pooling=";
        if (pooling)
        {
            connectionString += "true;";
        }
        else
        {
            connectionString += "false";
        }
        try
        {
            mySQLconn = new MySqlConnection(connectionString);
            mySQLconn.Open();
            Debug.Log("MySQL Connection: " + mySQLconn.State.ToString());
        }
        catch (MySqlException e)
        {
            Debug.Log("Error: " + e.ToString());
            if (mySQLconn != null)
                mySQLconn.Close();

            Debug.Log("Programa será desligado...");

            //Application.Quit();
        }
    }

    public static MySqlConnection getConnection()
    {
        if (conexao == null)
        {
            conexao = new Connection();
        }

        else
        {
            conexao.mySQLconn.Open();
        }
        return conexao.mySQLconn;
    }

    public void Close()
    {
        try
        {
            mySQLconn.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("Error: " + e.ToString());
        }
    }
}
