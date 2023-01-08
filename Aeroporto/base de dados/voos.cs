using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Aeroporto.base_de_dados
{
    public class voos
    {
        public int idvoo { get; set; }  
        public int idaviao { get; set; }
        public string origem { get; set; }
        public string destino { get; set; }
        public string data { get; set; }
        public string hora { get; set; }
        public int quanteco { get; set; }
        public int quantclasse { get; set; }
        public int quantemp { get; set; }



        private SQLiteConnection connection;
        private SQLiteCommand command;

        public void basedados()         ///criação da base de dados
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

        public void criartbl()      ///criação da tabela voos
        {
            var path = @"Data\aeronautica.sqlite";
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sqlcommand1 = "create table if not exists voos (idvoo int,idaviao int,origem varchar(80),destino varchar(80),data varchar(50),hora varchar(50)," +
                                        "quanteco int, quantclasse int, quantemp int)";

                command = new SQLiteCommand( sqlcommand1 , connection);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }

        }

        public List<voos> selectvoo() ///Buscar os voos da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            List<voos> lvoos = new List<voos>();
            try
            {
                connection = new SQLiteConnection("DataSource=" + path);
                connection.Open();
                string sql = "select * from voos";
                command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader(); //Lê cada registo

                while (reader.Read())

                    lvoos.Add(new voos
                    {
                        idvoo = (int)reader["idvoo"],
                        idaviao = (int)reader["idaviao"],
                        origem = (string)reader["origem"],
                        destino = (string)reader["destino"],
                        data = (string)reader["data"],
                        hora = (string)reader["hora"],
                        quanteco = (int)reader["quanteco"],
                        quantclasse = (int)reader["quantclasse"],
                        quantemp = (int)reader["quantemp"]
                    });


                connection.Close();
                return lvoos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
                return null;
            }
        }


        public void adicionarvoo(List<voos> voo) ///Adicionar voos a tabela voos
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                foreach (var voos in voo)
                {
                    string sql = string.Format("insert into voos (idvoo,idaviao, origem, destino,data, hora,quanteco, quantclasse, quantemp)" +
                        "values ({0},{1},'{2}','{3}','{4}','{5}',{6},{7},{8})", voos.idvoo, voos.idaviao, voos.origem, voos.destino, voos.data, voos.hora,
                                                                                voos.quanteco, voos.quantclasse, voos.quantemp);
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

        public void remover() ///Remover os voos da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            try
            {
                string sql = "Delete from voos";

                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        public void editar(voos editarvoo) ///Edita os voos da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("update voos SET  idvoo = {0}, idaviao = {1}, origem = '{2}', destino = '{3}',data = '{4}',hora = '{5}'," +
                                       "quanteco = {6}, quantclasse = {7}, quantemp = {8} WHERE idvoo = {0}",
                                       editarvoo.idvoo, editarvoo.idaviao, editarvoo.origem, editarvoo.destino, editarvoo.data, 
                                       editarvoo.hora,editarvoo.quanteco,editarvoo.quantclasse,editarvoo.quantemp);

            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(voos deletevoos)  ///Remove os voos por o idvoo da base de dados
        {
            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            string sql = string.Format("DELETE FROM voos WHERE idvoo =" + deletevoos.idvoo);
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }


        public void pesquisarvoo(List<voos> voosencontrados) ///Pesquisa os voos na base de dados
        {

            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();

            foreach(var pesquisarvoo in voosencontrados)
            {
                string sql = string.Format("SELECT * FROM voos WHERE origem ='" + pesquisarvoo.origem + "'or destino ='" + pesquisarvoo.destino + "'or data ='" + pesquisarvoo.data + "'");
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }

            connection.Close();

        }

    }
       

}
