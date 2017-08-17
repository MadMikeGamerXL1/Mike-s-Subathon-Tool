using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subathontool
{
    public partial class bitsStoreOptions : Form
    {
        
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        logger writeFile = new logger();
        public bitsStoreOptions()
        {
            InitializeComponent();
        }
        public bool needsSaving = true;
        public bool forceClose = false;
        public void close(string caller)
        {
            if(caller == "main")
            {
                forceClose = true;
                this.Close();
            }
        }
        private void bitsStoreOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (needsSaving)
            {
                DialogResult result = MessageBox.Show("Do you wish to save any changes made? ", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveOptions();
                    if (!forceClose)
                    {
                        e.Cancel = true;
                        this.Hide();
                    }else { e.Cancel = false; }
                }
                else
                {

                    Console.WriteLine("Discarding changes made to bits store");
                    if (!forceClose)
                    {
                        e.Cancel = true;
                        this.Hide();
                    }else { e.Cancel = false; }
                }
            }
            else
            {
                if (!forceClose)
                {
                    e.Cancel = true;
                    this.Hide();
                }
                else
                {
                    e.Cancel = false;
                }
            }
            

        }
        public void saveOptions()
        {
            try
            {


                DataSet dataSet = new DataSet("dataSet");
                writeFile.logFile("Created dataset");
                dataSet.Namespace = "NetFrameWork";
                DataTable table = new DataTable();
                writeFile.logFile("Created datatable");
                DataColumn idColumn = new DataColumn("id", typeof(int));
                DataColumn itemColumn = new DataColumn("name");
                writeFile.logFile("Successfully defined column 'name'");
                DataColumn imageColumn = new DataColumn("image");
                writeFile.logFile("Successfully defined column 'image'");
                DataColumn priceColumn = new DataColumn("price");
                writeFile.logFile("successfully defined column 'price'");
                idColumn.AutoIncrement = true;

                table.TableName = "store";
                table.Columns.Add(idColumn);
                writeFile.logFile("Added ID to column");
                table.Columns.Add(itemColumn);
                writeFile.logFile("Added item to column");
                table.Columns.Add(imageColumn);
                writeFile.logFile("Added image to column");
                table.Columns.Add(priceColumn);
                writeFile.logFile("Added price to column");

                dataSet.Tables.Add(table);
                writeFile.logFile("Added table to dataset");
                for (int i = 0; i < 12; i++)
                {
                    DataRow newRow = table.NewRow();
                    if (i == 0)
                    {

                        newRow["name"] = slot0Name.Text;
                        newRow["image"] = slot0ImagePath.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot0Price.Text;
                    }
                    else if (i == 1)
                    {
                        newRow["name"] = slot1Name.Text;
                        newRow["image"] = slot1Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot1Price.Text;
                    }
                    else if (i == 2)
                    {
                        newRow["name"] = slot2Name.Text;
                        newRow["image"] = slot2Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot2Price.Text;
                    }
                    else if (i == 3)
                    {
                        newRow["name"] = slot3Name.Text;
                        newRow["image"] = slot3Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot3Price.Text;
                    }
                    else if (i == 4)
                    {
                        newRow["name"] = slot4Name.Text;
                        newRow["image"] = slot4Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot4Price.Text;
                    }
                    else if (i == 5)
                    {
                        newRow["name"] = slot5Name.Text;
                        newRow["image"] = slot5Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot5Price.Text;
                    }
                    else if (i == 6)
                    {
                        newRow["name"] = slot6Name.Text;
                        newRow["image"] = slot6Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot6Price.Text;
                    }
                    else if (i == 7)
                    {
                        newRow["name"] = slot7Name.Text;
                        newRow["image"] = slot7Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot7Price.Text;
                    }
                    else if (i == 8)
                    {
                        newRow["name"] = slot8Name.Text;
                        newRow["image"] = slot8Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot8Price.Text;
                    }
                    else if (i == 9)
                    {
                        newRow["name"] = slot9Name.Text;
                        newRow["image"] = slot9Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot9Price.Text;
                    }
                    else if (i == 10)
                    {
                        newRow["name"] = slot10Name.Text;
                        newRow["image"] = slot10Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot10Price.Text;
                    }
                    else if (i == 11)
                    {
                        newRow["name"] = slot11Name.Text;
                        newRow["image"] = slot11Picture.Text.Replace(@"\\", @"\");
                        newRow["price"] = slot11Price.Text;
                    }
                    table.Rows.Add(newRow);

                }
                dataSet.AcceptChanges();
                writeFile.logFile("Serializing object and writing to file...");
                string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
                File.WriteAllText(appdataFolder + "/subathontool/store.json", json);
                writeFile.logFile("Successfully serialized and wrote to file");
                Console.Write("Wrote to file 'store.json' " + Environment.NewLine + json);
                needsSaving = false;
            }
            catch (Exception err)
            {
                writeFile.logFile("Encountered Error saving bits store options - " + err);
                MessageBox.Show("Unexpected error occured saving options - Please try again. If this problem keeps occuring after a restart, please submit a bug report using the built-in Feedback tool.", "Unexpected Error occured - " + err.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loadOptions()
        {
            try
            {
                
                string json = File.ReadAllText(appdataFolder + "/subathontool/store.json");
                DataSet readData = JsonConvert.DeserializeObject<DataSet>(json);
                DataTable readTable = readData.Tables["store"];
                Console.WriteLine($" Total rows in DataSet: {readTable.Rows.Count}");
                foreach (DataRow row in readTable.Rows)
                {
                    Console.WriteLine(row["id"] + " - " + row["name"] + " - " + row["image"] + "[" + row["price"] + " bits]");
                    var id = row["id"].ToString();
                    if (int.Parse(id) == 0)
                    {

                        slot0Name.Text = row["name"].ToString();
                        slot0Price.Text = row["price"].ToString();
                        slot0ImagePath.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 1)
                    {
                        slot1Name.Text = row["name"].ToString();
                        slot1Price.Text = row["price"].ToString();
                        slot1Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 2)
                    {
                        slot2Name.Text = row["name"].ToString();
                        slot2Price.Text = row["price"].ToString();
                        slot2Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 3)
                    {
                        slot3Name.Text = row["name"].ToString();
                        slot3Price.Text = row["price"].ToString();
                        slot3Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 4)
                    {
                        slot4Name.Text = row["name"].ToString();
                        slot4Price.Text = row["price"].ToString();
                        slot4Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 5)
                    {
                        slot5Name.Text = row["name"].ToString();
                        slot5Price.Text = row["price"].ToString();
                        slot5Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 6)
                    {
                        slot6Name.Text = row["name"].ToString();
                        slot6Price.Text = row["price"].ToString();
                        slot6Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 7)
                    {
                        slot7Name.Text = row["name"].ToString();
                        slot7Price.Text = row["price"].ToString();
                        slot7Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 8)
                    {
                        slot8Name.Text = row["name"].ToString();
                        slot8Price.Text = row["price"].ToString();
                        slot8Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 9)
                    {
                        slot9Name.Text = row["name"].ToString();
                        slot9Price.Text = row["price"].ToString();
                        slot9Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 10)
                    {
                        slot10Name.Text = row["name"].ToString();
                        slot10Price.Text = row["price"].ToString();
                        slot10Picture.Text = row["image"].ToString();
                    }
                    if (int.Parse(id) == 11)
                    {
                        slot11Name.Text = row["name"].ToString();
                        slot11Price.Text = row["price"].ToString();
                        slot11Picture.Text = row["image"].ToString();
                    }
                }

            }
            catch(FileNotFoundException err)
            {
                writeFile.logFile("No store json file has been found " + err);
                slot0Name.Text = "[Example] Chance Cube";
                slot0Price.Text = "[Example] 700";
            }
            catch (Exception err)
            {
                writeFile.logFile("Encountered Error loading bits store options - " + err);
                MessageBox.Show("Unexpected error occured loading options - Please try again. If this problem keeps occuring after a restart, please submit a bug report using the built-in Feedback tool.", "Unexpected Error occured - " + err.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bitsStoreOptions_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                loadOptions();
            }
        }
        public class data
        {
            public string name { get; set; }
            public string picture { get; set; }
            public string price { get; set; }
            
        }

        private void saveClose_Click(object sender, EventArgs e)
        {
            saveOptions();
            this.Hide();
            writeFile.logFile("Saving and hiding bits store...");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void slot1Browse_Click(object sender, EventArgs e)
        {
            if(openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot0ImagePath.Text = openImageFileDia.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot1Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot2Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot3Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot4Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot5Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot6Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot7Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot8Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot9Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot10Picture.Text = openImageFileDia.FileName;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (openImageFileDia.ShowDialog() == DialogResult.OK)
            {
                slot11Picture.Text = openImageFileDia.FileName;
            }
        }

        private void bitsStoreOptions_Load(object sender, EventArgs e)
        {

        }
    }
}
