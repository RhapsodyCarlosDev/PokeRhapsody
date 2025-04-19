using System.Windows.Forms;

namespace Pokemon
{
    public partial class Pokedex : Form
    {
        public Pokedex()
        {
            InitializeComponent();
        }

        public void MostrarImagen(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                PBCartas.Image = System.Drawing.Image.FromFile(imagePath);
            }
        }
    }
}
