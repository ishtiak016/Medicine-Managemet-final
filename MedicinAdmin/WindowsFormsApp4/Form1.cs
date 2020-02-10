using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public String connection = "Data Source=DESKTOP-7UMP360;Initial Catalog=Store;Integrated Security=True";
        SqlDataAdapter adapt;
        DataTable dt;
        private void ConfirmButonForInsert_Click(object sender, EventArgs e)
        {
            String productId = ProductIdTextBox.Text;         
            String productName = MedicineNameTextBox.Text;
            String genericName = GenericNameTextBox.Text;
            String mgfDate = MgfdateTextBox.Text;
            String expDate = ExprDateTextBox.Text;
            String productAmmount = ProductAmmountTextBox.Text;
            String description = DescriptionTextBox.Text;
            byte[] file_byte = ImageToByteArray(pictureBox1.Image);
           
           
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (ConfirmButonForInsert.Text== "Confirm")
                { 
                String q = "INSERT INTO Product (ProductId,ProductName,GenericName, MgfDate,ExpDate, ProductAmmount,Description,MedicineImage )VALUES ('" + productId + "','" + productName+ "','" + genericName + "','" + mgfDate + "','" + expDate + "','" + productAmmount + "','" + description + "','" + file_byte + "')";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("tamak");
            }
                else if(ConfirmButonForInsert.Text == "Update")
                {
                    try
                    {
                        String q = "UPDATE Product SET ProductName='"+ProductName+"' WHERE ProductId='"+productId+"'";
                        SqlCommand cmd = new SqlCommand(q, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Update Success");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else 
                {
                    try
                    {
                        String q = "DELETE from Product  WHERE ProductId='" + productId + "'";
                        SqlCommand cmd = new SqlCommand(q, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Delete Success");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            ProductIdTextBox.Clear();
            MedicineNameTextBox.Clear();
            GenericNameTextBox.Clear();
           MgfdateTextBox.Clear();
          ExprDateTextBox.Clear();
           ProductAmmountTextBox.Clear();
            DescriptionTextBox.Clear();
            
            conn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
         

        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            adapt = new SqlDataAdapter("select * from Product where ProductId like '" + SearchTextBox.Text + "%' or ProductName like '" + SearchTextBox.Text + "%'", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            adapt = new SqlDataAdapter("select * from Product", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;          
            conn.Close();

            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            ProductIdTextBox.Text = selectedRow.Cells[0].Value.ToString();
            MedicineNameTextBox.Text = selectedRow.Cells[1].Value.ToString();
            GenericNameTextBox.Text = selectedRow.Cells[2].Value.ToString();
            MgfdateTextBox.Text = selectedRow.Cells[3].Value.ToString();
            ExprDateTextBox.Text= selectedRow.Cells[4].Value.ToString();
            ProductAmmountTextBox.Text= selectedRow.Cells[5].Value.ToString();
            DescriptionTextBox.Text= selectedRow.Cells[6].Value.ToString();

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Image";
            imageColumn.DataPropertyName = "Data";
            imageColumn.HeaderText = "Image";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Insert(7, imageColumn);
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Columns[2].Width = 100;


            ConfirmButonForInsert.Text = "Update";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfirmButonForInsert.Text = "Delete";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfirmButonForInsert.Text = "Confirm";
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }
        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (MemoryStream ms=new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter= "(*.jpg;*.jpeg;*.bmp)|*.jpg;*jpeg;.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }
    }
}
