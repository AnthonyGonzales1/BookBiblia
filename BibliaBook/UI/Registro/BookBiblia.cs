using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliaBook.Entidades;
using BibliaBook.BLL;

namespace BibliaBook
{
    public partial class BookBiblia : Form
    {
        Biblia book = new Biblia();
        public BookBiblia()
        {
            InitializeComponent();
            LlenarComboBox();
        }

        private Biblia LlenaClase()
        {
            if (IdnumericUpDown.Value == 0)
            {
                book.BibliaId = 0;
            }
            else
            {
                book.BibliaId = Convert.ToInt32(IdnumericUpDown.Value);
            }

            book.TipoId = Convert.ToInt32(TipocomboBox.Text);
            book.Descripcion = DescripciontextBox.Text;
            book.Siglas = SiglastextBox.Text;

            return book;
        }

        private void Clear()
        {
            IdnumericUpDown.Value = 0;
            DescripciontextBox.Clear();
            SiglastextBox.Clear();
            TipocomboBox.Text = string.Empty;

        }

        private bool Validar(int error)
        {
            bool paso = false;
            
            if (error == 1 && IdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(IdnumericUpDown,
                   "Debe ingresar un ID");
                paso = true;
            }

            if (error == 2 && TipocomboBox.Text == string.Empty)
            {
                errorProvider.SetError(TipocomboBox,
                   "Debe ingresar un Tipo");
                paso = true;
            }
            
            if (error == 2 && DescripciontextBox.Text == string.Empty)
            {
                errorProvider.SetError(DescripciontextBox,
                   "Debe ingresar una Descripcion");
                paso = true;
            }

            if (error == 2 && SiglastextBox.Text == string.Empty)
            {
                errorProvider.SetError(SiglastextBox,
                   "Debe ingresar las Siglas");
                paso = true;
            }

            return paso;
        }

        private void LlenarComboBox()
        {
            TipocomboBox.Items.Clear();
            foreach (var item in BLL.BibliaBookBLL.GetList(x => true))
            {
                TipocomboBox.Items.Add(item.TipoId);
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(2))
            {
                MessageBox.Show("Llenar Campos vacios");
                errorProvider.Clear();
                return;
            }
            else
            {
                book = LlenaClase();
                if (IdnumericUpDown.Value == 0 )
                {
                    if (BLL.BibliaBookBLL.Guardar(book))
                    {
                        MessageBox.Show("Guardado!", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo Guardar!!");
                    }
                }
                else
                {
                    var result = MessageBox.Show("Seguro de Modificar?", "+Libros",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (BLL.BibliaBookBLL.Modificar(LlenaClase()))
                        {
                            MessageBox.Show("Modificado!!");
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo Guardar!!");
                        }
                    }
                }
            }
        }
        
        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Llenar Campo Id");
                return;
            }

            if (BLL.BibliaBookBLL.Eliminar(Convert.ToInt32(IdnumericUpDown.Value)))
            {
                MessageBox.Show("Eliminado!!");
                Clear();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar!!");
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Favor de llenar la casilla para poder Buscar");
            }
            else
            {
                int id = Convert.ToInt32(IdnumericUpDown.Value);
                book = BLL.BibliaBookBLL.Buscar(id);

                if (book != null)
                {
                    IdnumericUpDown.Value = book.BibliaId;
                    DescripciontextBox.Text = book.Descripcion.ToString();
                    SiglastextBox.Text = book.Siglas.ToString();
                    TipocomboBox.Text = book.TipoId.ToString();
                }
                else
                {
                    MessageBox.Show("No Fue Encontrado!", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                errorProvider.Clear();
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}

