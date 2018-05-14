using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Newtonsoft.Json;
using System.Threading;

namespace Tillgangar
{
    public partial class Form1 : Form
    {
        static OnlineData onlineData = new OnlineData();
        public Form1()
        {
            InitializeComponent();
            initializeGrid();
        }


        void initializeGrid()
        {
            /*
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Namn";
            dataGridView1.Columns[1].Name = "Antal";
            dataGridView1.Columns[2].Name = "WKN";

            string[] row = new string[] { "Name", "Antal", "WKN" };
            */

        }

        private void button1_Click(object sender, EventArgs e)
        {
           string apikey = textBox1.Text;
           string httpsString;
           double totalAssets = 0d; 

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
              if (row.Cells[2]?.Value != null)
                {
                    string jsonString;
                   
                    Thread.Sleep(2000);
                        httpsString =
                    @"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" + row.Cells[2].Value.ToString() +
                    "&to_currency=" + textBox3.Text + "&apikey=" + textBox1.Text;

                    jsonString = onlineData.getHttData(httpsString);
                    

                    //var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //ser.DeserializeObject(jsonString);
        



                    Equity deserializedProduct = JsonConvert.DeserializeObject<Equity>(jsonString);
                    double valueAssets;
                    valueAssets = Convert.ToDouble(row.Cells[1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(deserializedProduct.exchangeRate.exchangeValue, System.Globalization.CultureInfo.InvariantCulture);
                    totalAssets += valueAssets;
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2]?.Value != null)
                {
                    string jsonString;
                    
                        Thread.Sleep(2000);
                        httpsString = @"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol="
                + row.Cells[2].Value.ToString() + "&interval=60min&apikey=" + row.Cells[2].Value.ToString() + "&outputsize=compact";
                      

                    
                        jsonString = onlineData.getHttData(httpsString);
                    
                    
                    dynamic fyn = JsonConvert.DeserializeObject(jsonString);
                    Stocks deserializedProduct = JsonConvert.DeserializeObject<Stocks>(jsonString);
                    String format = "yyyy-mm-dd";
                    DateTime now = DateTime.Now;
                    string dateyyyy  = now.ToString(format);
                    string dateShort = DateTime.Today.ToShortDateString();
                    string dateNow = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    string dateMax = deserializedProduct.Data.Keys.Max();

                    TimeSeriesJsonClass timeSeriesJson = deserializedProduct.Data[dateMax];
                    //TimeSeriesJsonClass timeSeriesJson =  deserializedProduct.Data[dateyyyy];
                    double valueAssets;
                    valueAssets = Convert.ToDouble(timeSeriesJson.close, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(row.Cells[1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    if (row.Cells[3].Value.ToString().ToLower() != textBox3.Text.ToLower()) // The stocks are in another currency
                    {
                        
                            // too fast
                            Thread.Sleep(2000);
                            httpsString = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" +
                            row.Cells[3].Value.ToString() + "&to_currency=" + textBox3.Text + "&apikey=" + textBox1.Text;


                            jsonString = onlineData.getHttData(httpsString);
                       
                      
                        
                        

                        Equity deserializedCurrency = JsonConvert.DeserializeObject<Equity>(jsonString);

                        valueAssets = valueAssets  * 
                            Convert.ToDouble(deserializedCurrency.exchangeRate.exchangeValue, System.Globalization.CultureInfo.InvariantCulture);


                    }



                    totalAssets += valueAssets;
                }
            }

            textBox2.Text = totalAssets.ToString();





        }



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("..\\...\\..\\tillgangar_att_lasa_in.txt");
                string[] columnnames = file?.ReadLine()?.Split(' ');
                DataTable dt = new DataTable();
                foreach (string c in columnnames)
                {
                    dt.Columns.Add(c);
                }
                string newline;
                while ((newline = file.ReadLine()) != null)
                {
                    DataRow dr = dt.NewRow();
                    string[] values = newline.Split(' ');
                    for (int i = 0; i < values.Length; i++)
                    {
                        dr[i] = values[i];
                    }
                    dt.Rows.Add(dr);
                }
                file.Close();
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dt;
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            System.IO.StreamReader file = new System.IO.StreamReader("..\\...\\..\\tillgangar_att_lasa_in_currency.txt");
            string[] columnnames = file.ReadLine().Split(' ');
            DataTable dt = new DataTable();
            foreach (string c in columnnames)
            {
                dt.Columns.Add(c);
            }
            string newline;
            while ((newline = file.ReadLine()) != null)
            {
                DataRow dr = dt.NewRow();
                string[] values = newline.Split(' ');
                for (int i = 0; i < values.Length; i++)
                {
                    dr[i] = values[i];
                }
                dt.Rows.Add(dr);
            }
            file.Close();
            dataGridView2.Rows.Clear();
            dataGridView2.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("...\\..\\..\\apikey.txt");
            textBox1.Text = file.ReadLine();
            file.Close();
        }
    }
}
