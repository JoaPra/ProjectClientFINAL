using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectClient.Helpers;

namespace ProjectClient
{
    public partial class FormClient : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private  List<Award> awardsList = new List<Award>(); // nagrody schowamy sobie w liście
        public FormClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
    
            string adres = textBox1.Text; // dane z textboxa Adres
            int port = System.Convert.ToInt16(numericUpDown1.Value); // sparsowane dane z decimala na int z numeric-up-downa Port
            try
            {
                client = new TcpClient(adres, port); //przekazanie adresu i portu do konstruktora obiektu TcpClient
                stream = client.GetStream(); // uruchomienie klasy NetworkStream- przesył i odbieranie danych.
                listBox1.Items.Add("Nawiązano połączenie z pizzerią o adresie " + adres + " na porcie: " + port);

            }
            catch (Exception exception)
            {
                listBox1.Items.Add("Nie można nawiązać połączenia! Błąd: " + exception);
                MessageBox.Show(exception.ToString());

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadAwards(); // ladowanie nagrod do listy
            string Award = RandomAward();

            byte[] text = Encoding.ASCII.GetBytes(textBox2.Text + " - " + Award); // textboxa tekst zostaje buforem
            stream.Write(text, 0,text.Length); // mój bufor tekstowy, liczba bajtów do zapisania
            textBox2.Text += " Gratulacje! Wygrałeś " + Award + " Zamówienie przyjęto.";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private string RandomAward()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 4); // ilosc nagrod po id
            Award getAward = awardsList.Find(p => p.ID == randomNumber);
            return getAward.Name;
            
        }

        private void LoadAwards()
        {
            
            Award sosCzosnkowy = new Award()
            {
                Name = "Sos czosnkowy",
                ID = 1
            };
            Award sosPomidorowy = new Award()
            {
                Name = "Sos pomidorowy",
                ID = 2
            };
            Award puszkaColi = new Award()
            {
                Name = "Puszka Coli",
                ID = 3
            };
            Award sokPomaranczowy = new Award()
            {
                Name = "Sok Pomarańczowy",
                ID = 4
            };
            awardsList.Add(sosCzosnkowy);
            awardsList.Add(sosPomidorowy);
            awardsList.Add(puszkaColi);
            awardsList.Add(sokPomaranczowy);
        }

    }
}
