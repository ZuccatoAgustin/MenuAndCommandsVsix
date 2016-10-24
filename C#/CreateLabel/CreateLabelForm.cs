using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;

namespace Microsoft.Samples.VisualStudio.MenuCommands.CreateLabel
{
    public partial class CreateLabelForm : Form
    {
        internal TextSelection textSelection;

        public CreateLabelForm()
        {
            InitializeComponent();
        }

        public string FileSelection { get; internal set; }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {

            try
            {
                ModelRofex mr = new ModelRofex();

                var t = new Traducciones() { KEY = this.txt_key.Text, LenguageID = 1, Value = this.txt_value.Text };
                mr.Entry(t).State = System.Data.Entity.EntityState.Added;

                //mr.Traducciones.Add(t);
                var traEng = this.txt_value.Text;
                if (!string.IsNullOrEmpty(this.txt_value2.Text))
                {
                    traEng = this.txt_value2.Text;
                }


                mr.Traducciones.Add(new Traducciones() { KEY = this.txt_key.Text, LenguageID = 2, Value = traEng });
                mr.SaveChanges();

                White();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.ToString().Contains("IX_Traducciones"))
                {
                    var result =MessageBox.Show("la key ya estaba cargada! escribir igual?", "key duplicada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        White();
                    }                   
                }
            }
            catch (Exception ex)
            {

            }
           
        }

        private void White()
        {
            if (this.checkBox1.Checked)
            {
                if (this.checkBox2.Checked)
                {
                    textSelection.Text = string.Format("<% TraducirDeault \"{0}\", \"{1}\" ", this.txt_key.Text, this.txt_value.Text);
                }
                else
                {
                    textSelection.Text = string.Format("<% Traducir(\"{0}\") ", this.txt_key.Text);
                }
            }
            this.Close();
        }

        private void CreateLabelForm_Load(object sender, EventArgs e)
        {
            var  text = textSelection.Text;
            
            this.txt_key.Text = text.TrimStart().TrimEnd().Replace(" ", "").Replace("_", "");

            this.txt_value.Text = text.TrimStart().TrimEnd();

        }
    }
}
