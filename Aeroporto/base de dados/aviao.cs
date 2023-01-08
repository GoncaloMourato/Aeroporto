using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Aeroporto.base_de_dados
{
    public class aviao
    {

        public int idaviao { get; set; }

        public string nome { get; set; }

        public string modelo { get; set; }

        public int quantidadeeco { get; set; }

        public int quantidadeclasse { get; set; }

        public int quantidadeemp { get; set; }

        private SQLiteConnection connection;
        private SQLiteCommand command;

        public override string ToString()
        {
            return $"|  {idaviao} | {nome} {modelo} | {quantidadeeco} | {quantidadeclasse} | {quantidadeemp}";
        }


        public void basedados() ///criação da base de dados
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            var path = @"Data\aeronautica.sqlite";
            try
            {

                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");

            }
        }

        public void criartbl() ///criação da tabela aviao
        {
            var path = @"Data\aeronautica.sqlite";
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sqlcommand1 = "create table if not exists aviao (idaviao int,nome varchar(80), " +
                    "modelo varchar(80),quantidadeeco int, quantidadeclasse int, quantidadeemp int)";

                command = new SQLiteCommand(sqlcommand1, connection);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }

        }


        public List<aviao> selectaviao() ///Buscar os aviao da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            List<aviao> laviao = new List<aviao>();
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sql = "select * from aviao";
                command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader(); //Lê cada registo

                while (reader.Read())

                    laviao.Add(new aviao
                    {
                        idaviao = (int)reader["idaviao"],
                        nome = (string)reader["nome"],
                        modelo = (string)reader["modelo"],
                        quantidadeeco = (int)reader["quantidadeeco"],
                        quantidadeclasse = (int)reader["quantidadeclasse"],
                        quantidadeemp = (int)reader["quantidadeemp"]

                    });


                connection.Close();
                return laviao;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
                return null;
            }
        }

        public void adiconaraviao(List<aviao> laviao) ///Adicionar aviões a tabela aviao
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                foreach (var avioes in laviao)
                {
                    string sql = string.Format("insert into aviao (idaviao, nome, modelo,quantidadeeco,quantidadeclasse,quantidadeemp)" +
                        "values ({0},'{1}','{2}',{3},{4},{5})", avioes.idaviao, avioes.nome, avioes.modelo,avioes.quantidadeeco,avioes.quantidadeclasse,avioes.quantidadeemp);
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        public void remover() ///Remove os avioes da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                string sql = "Delete from aviao";

                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        public void editar(aviao editaraviao) ///Edita os avioes da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("update aviao SET  idaviao = {0}, nome = '{1}', modelo = '{2}',quantidadeeco = {3},quantidadeclasse = {4},quantidadeemp = {5} WHERE idaviao = {0}",
                editaraviao.idaviao, editaraviao.nome, editaraviao.modelo,editaraviao.quantidadeeco,editaraviao.quantidadeclasse,editaraviao.quantidadeemp);
            
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(aviao deleteaviao) ///Remove os avioes por o idaviao da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("DELETE FROM aviao WHERE idaviao =" + deleteaviao.idaviao);
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }


    }
}

