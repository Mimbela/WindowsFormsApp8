using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//ejercicio 10.12
namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        //pongo aquí la cadena de conexión porque combo y tabla las usarán
        string cadena = ConfigurationManager.ConnectionStrings["cadenaNorthwind"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarCombo(); //`lo primero que hago y genero método
        }

        private void cargarCombo()
        {
            string consulta = "select SupplierID, CompanyName from Suppliers"; //escribo la consulta
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, cadena);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            //CREAMOS COMBO
            //añadp fila vacía
            DataRow fila = tabla.NewRow();
            fila["CompanyName"].Equals(String.Empty); //lo relleno vacío
            fila["SupplierID"].Equals (0);
            tabla.Rows.Add(fila);

            //ordeno la tabla
            tabla.DefaultView.Sort = tabla.Columns[1].ColumnName;

            //PRopiedad por delante y por detrás porque es combo
            comboBox1.DisplayMember = tabla.Columns[1].ColumnName; //por delante
            comboBox1.ValueMember = tabla.Columns[0].ColumnName;//por detrás
            comboBox1.DataSource = tabla; //suministros los datos



        }
        //cuando selecciono de la combo una opcion
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboBox1.SelectedIndex)>0) //esto es para que cargue, sino da fallo
            {
            int valor = (int)comboBox1.SelectedValue; //capturo lo que está seleccionado y lo casteo
            CargarGrid(valor); //doy a bombilla para cargar metodo
            }
           
        }

        private void CargarGrid(int valor)
        {
            string consulta = "select ProductName , QuantityPerUnit , UnitPrice, UnitsInStock from Products WHERE SupplierID = '" + valor + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, cadena);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);// ajusta el tamaño de la celda
            
        }
    }
}
//Ejercicio propuesto(10.12)
//Crear Aplicación de un formulario con una combo y un dataGridView.
//La combo cargará todos los proveedores(TABLA : Suppliers)(CAMPOS : SupplierID y CompanyName).
//Con el valor seleccionado se cargará un dataGridView con todos los productos de ese proveedor(TABLA: Products)
//    (CAMPOS: ProductName,QuantityPerUnit,UnitPrice y UnitsInStock)
