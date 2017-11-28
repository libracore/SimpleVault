using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleVault
{
    public partial class MainForm : Form
    {
        #region global variables
        // flag to track changes
        private bool isChanged = false;
        // filename storage
        private string filename = string.Empty;
        // password
        private string password = string.Empty;
        #endregion

        #region constructor
        public MainForm(string file)
        {
            InitializeComponent();

            filename = file;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (filename != string.Empty)
            {
                loadFile();
            }
        }
        #endregion

        #region menu handlers
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isChanged)
            {
                if (MessageBox.Show("There are unsaved changes. Really make a new document?", "SimpleVault", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    newContent();
                }
            }
            else
            {
                newContent();
            }
        }

        private void newContent()
        {
            txtContent.Text = string.Empty;
            filename = string.Empty;
            password = string.Empty;
            txtContent.Text = string.Empty;
            isChanged = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isChanged)
            {
                if (MessageBox.Show("There are unsaved changes. Really open a document?", "SimpleVault",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    openFile();
                }
            }
            else
            {
                openFile();
            }
        }

        private void openFile()
        {
            // flush previous password
            password = string.Empty;
            // open file dialog
            OpenFileDialog dlgOpen = new OpenFileDialog
            {
                Filter = "Encrypted text file (*.tenc)|*.tenc",
                Title = "Select file to open"
            };
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                filename = dlgOpen.FileName;
                loadFile();
            }
        }

        /// <summary>
        /// Load the file with stored filename
        /// </summary>
        private void loadFile()
        {
            PasswordEntry dlgPassword = new PasswordEntry();
            if (dlgPassword.ShowDialog() == DialogResult.OK)
            {
                password = dlgPassword.Password.Text;

                // decrypt
                txtContent.Text = Encryption.Decrypt(filename, password);
                isChanged = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((filename != string.Empty) && (password != string.Empty))
            {
                save();
            }
            else
            {
                saveAs();
            }
        }

        private void save()
        {
            if (!Encryption.Encrypt(txtContent.Text, password, filename))
            {
                MessageBox.Show("Encryption failed.", "SimpleVault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                isChanged = false;
            }
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void saveAs()
        {
            // flush previous password
            password = string.Empty;
            // open file dialog
            SaveFileDialog dlgSave = new SaveFileDialog
            {
                Filter = "Encrypted text file (*.tenc)|*.tenc",
                Title = "Select file name",
                FileName = "newfile.tenc"
            };
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                filename = dlgSave.FileName;
                PasswordEntry dlgPassword = new PasswordEntry();
                if (dlgPassword.ShowDialog() == DialogResult.OK)
                {
                    password = dlgPassword.Password.Text;

                    // encryp
                    save();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox myAbout = new AboutBox();
            myAbout.ShowDialog();
        }

        #endregion

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }
    }
}
