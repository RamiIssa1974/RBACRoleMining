namespace RBACRoleMining.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenDatasetGenerator_Click(object sender, EventArgs e)
        {
            var form = new DatasetGeneratorForm();
            form.ShowDialog();
        } 
        private void btnRunAlgorithms_Click(object sender, EventArgs e)
        {
            var form = new RoleMiningForm();
            form.ShowDialog();
        }
    }
}
