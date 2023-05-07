using System.IO;
using System.IO.IsolatedStorage;


namespace test_project
{
    public partial class Form1 : Form
    {
        public event EventHandler<string> FileLoad;
        public event EventHandler CancelDisplay;

        private string FilePath = Directory.GetCurrentDirectory();
        private bool isFile = false;
        private string currentNameSelected = "";

        private string final_file = "";

        public string getSelectedFile()
        {
            return this.final_file;
        }

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the file display
            filePathTextBox.Text = FilePath;
            insertFiles();
        }

        public void insertFiles()
        {
            // clear the current files
            deleteCurrentEntries();

            // add the files from the current directory
            DirectoryInfo fileList;

            try
            {
                fileList = new DirectoryInfo(FilePath);
                FileInfo[] files = fileList.GetFiles();
                DirectoryInfo[] directories = fileList.GetDirectories();

                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".edf")
                    {
                        listView.Items.Add(file.Name, 1);
                    }

                    else
                    {
                        listView.Items.Add(file.Name, 2);
                    }
                }

                foreach (DirectoryInfo dir in directories)
                {
                    listView.Items.Add(dir.Name, 0);
                }
            }

            catch (Exception e)
            {
                errorBox.Text = e.Message;
            }
        }

        public void loadButtonAction()
        {
            FilePath = filePathTextBox.Text;
            insertFiles();
            isFile = false;
        }

        private void deleteCurrentEntries()
        {
            listView.Items.Clear();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            deleteCurrentEntries();
            loadButtonAction();
        }



        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentNameSelected = e.Item.Text;


            FileAttributes fileAttr = File.GetAttributes(FilePath + @"\" + currentNameSelected);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                filePathTextBox.Text = FilePath + @"\" + currentNameSelected;
            }

            else
            {
                FileInfo fileInfo = new FileInfo(FilePath + @"\" + currentNameSelected);
                fileNameLabel.Text = fileInfo.Name;
                fileTypeLabel.Text = fileInfo.Extension;

                if (fileInfo.Extension == ".edf")
                {
                    errorBox.Text = "Valid Environmental Data File Extension!";
                }

                else
                {
                    errorBox.Text = "Invalid Environmental Data File Extension!";
                }

            }
        }

        private void cancelFileButton_Click(object sender, EventArgs e)
        {
            CancelDisplay?.Invoke(this, EventArgs.Empty);
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            string selectFile = FilePath + @"\" + currentNameSelected;
            FileInfo fileInfo = new FileInfo(selectFile);

            if (fileInfo.Extension == ".edf")
            {
                final_file = selectFile;
                FileLoad?.Invoke(this, final_file);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            string curr_loc = filePathTextBox.Text;
            string prev_loc = "";
            int length = curr_loc.Length;
            int last_slash = curr_loc.LastIndexOf(@"\");

            // no slashes found, must be in the root directory
            if (last_slash == -1)
            {
                errorBox.Text = "No folder to move up into!";
            }

            // slash is at the end... remove slash and try again
            else if (last_slash == length)
            {
                curr_loc = curr_loc.Substring(0, (last_slash - 1));
                last_slash = curr_loc.LastIndexOf(@"\");

                if (last_slash == -1)
                {
                    errorBox.Text = "No folder to move up into!";
                }

                else
                {
                    prev_loc = curr_loc.Substring(0, last_slash);
                    filePathTextBox.Text = prev_loc;
                    errorBox.Text = "Select a valid .edf file to view environmental data";

                    deleteCurrentEntries();
                    loadButtonAction();
                }
            }

            // slash is not at the end, keep only the beginning elements and research
            else
            {
                prev_loc = curr_loc.Substring(0, last_slash);
                filePathTextBox.Text = prev_loc;
                errorBox.Text = "Select a valid .edf file to view environmental data";

                deleteCurrentEntries();
                loadButtonAction();
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Setup the file display
            filePathTextBox.Text = FilePath;
            insertFiles();
        }
    }
}