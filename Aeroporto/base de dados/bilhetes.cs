using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Aeroporto.base_de_dados
{
    public class bilhetes
    {
        public int idbilhete { get; set; }

        public int idvoo { get; set; }

        public int idaviao { get; set; }

        public int bilheteeco { get; set; }

        public string titular { get; set; }

        public int bilheteprimeriaclasse { get; set; }

        public int bilheteempresarial { get; set; }

        public int precototal { get; set; }

        private SQLiteConnection connection;
        private SQLiteCommand command;

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

        public void criartbl()  ///criação da tabela bilhetes
        {
            var path = @"Data\aeronautica.sqlite";
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sqlcommand1 = "create table if not exists bilhetes (idbilhete int,titular varchar(50),idvoo int,idaviao int,bilheteeco int," +
                    "bilheteprimeriaclasse int, bilheteempresarial int, precototal int)";

                command = new SQLiteCommand(sqlcommand1, connection);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }

        }

        public List<bilhetes> selectbilhete() ///Buscar os bilhetes da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            List<bilhetes> lbilhete = new List<bilhetes>();
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sql = "select * from bilhetes";
                command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader(); //Lê cada registo

                while (reader.Read())

                    lbilhete.Add(new bilhetes
                    {
                        idbilhete = (int)reader["idbilhete"],
                        titular = (string)reader["titular"],
                        idvoo = (int)reader["idvoo"],
                        idaviao = (int)reader["idaviao"],
                        bilheteeco = (int)reader["bilheteeco"],
                        bilheteprimeriaclasse = (int)reader["bilheteprimeriaclasse"],
                        bilheteempresarial = (int)reader["bilheteempresarial"],
                        precototal = (int)reader["precototal"]
                    });


                connection.Close();
                return lbilhete;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
                return null;
            }
        }

        public void adicionarbilhete(List<bilhetes> bilhete) ///Adicionar bilhetes a tabela bilhetes
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                foreach (var bilhetes in bilhete)
                {
                    string sql = string.Format("insert into bilhetes (idbilhete, titular,idvoo, idaviao, bilheteeco, bilheteprimeriaclasse,bilheteempresarial,precototal)" +
                        "values ({0},'{1}',{2},{3},{4},{5},{6},{7})", bilhetes.idbilhete, bilhetes.titular,bilhetes.idvoo, bilhetes.idaviao, 
                                                                    bilhetes.bilheteeco, bilhetes.bilheteprimeriaclasse, bilhetes.bilheteempresarial, bilhetes.precototal);
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

        public void remover() ///Remove os bilhetes da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                string sql = "Delete from bilhetes";

                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        public void editar(bilhetes editarbilhetes) ///Edita os bilhetes da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("update bilhetes SET  idbilhete = {0}, titular= '{1}' ,idvoo = {2}, idaviao = {3}, bilheteeco = {4}, bilheteprimeriaclasse = {5}, bilheteempresarial = {6},precototal = {7} WHERE idbilhete = {0}",
                editarbilhetes.idbilhete,editarbilhetes.titular,editarbilhetes.idvoo,editarbilhetes.idaviao,editarbilhetes.bilheteeco,editarbilhetes.bilheteprimeriaclasse,editarbilhetes.bilheteempresarial,editarbilhetes.precototal);

            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(bilhetes deletebilhetes) ///Remove os bilhetes por o idbilhete da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("DELETE FROM bilhetes WHERE idbilhete =" + deletebilhetes.idbilhete);
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }
    }

}

