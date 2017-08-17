using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subathontool
{
    public partial class bitsStore : Form
    {
        bitsStoreOptions options = new bitsStoreOptions();
        public bitsStore()
        {
            InitializeComponent();
        }
        public void setStoreItems()
        {
            options.loadOptions();
            if (options.slot0Name.Text != "" && options.slot0Name.Text != null)
            {
                slot0Btn.Enabled = true;
                slot0Btn.Text = options.slot0Name.Text;
                if (options.slot0ImagePath.Text != null && options.slot0ImagePath.Text != "")
                {
                    try
                    {
                        Console.WriteLine("Found path " + options.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        Image slot0Image = new Bitmap(options.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        slot0Btn.BackgroundImage = slot0Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot0ImagePath.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
            if (options.slot1Name.Text != "" && options.slot1Name.Text != null)
            {
                slot1Btn.Enabled = true;
                slot1Btn.Text = options.slot1Name.Text;

                if (options.slot1Picture.Text != null && options.slot1Picture.Text != "")
                {
                    try {
                        Image slot1Image = new Bitmap(options.slot1Picture.Text.Replace(@"\\", @"\"));
                        slot1Btn.BackgroundImage = slot1Image;
                    }
                    catch(System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot1Picture.Text.Replace(@"\\", @"\") +  "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                        
                }
            }
            if (options.slot2Name.Text != "" && options.slot2Name.Text != null)
            {
                slot2Btn.Enabled = true;
                slot2Btn.Text = options.slot2Name.Text;
                if (options.slot2Picture.Text != null && options.slot2Picture.Text != "")
                {
                    try
                    {
                        Image slot2Image = new Bitmap(options.slot2Picture.Text.Replace(@"\\", @"\"));
                        slot2Btn.BackgroundImage = slot2Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot2Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
            if (options.slot3Name.Text != "" && options.slot3Name.Text != null)
            {
                slot3Btn.Enabled = true;
                slot3Btn.Text = options.slot3Name.Text;
                if (options.slot3Picture.Text != null && options.slot3Picture.Text != "")
                {
                    Image slot3Image = new Bitmap(options.slot3Picture.Text.Replace(@"\\", @"\"));
                    slot3Btn.BackgroundImage = slot3Image;
                }
            }
            if (options.slot4Name.Text != "" && options.slot4Name.Text != null)
            {
                slot4Btn.Enabled = true;
                slot4Btn.Text = options.slot4Name.Text;
                if(options.slot4Picture.Text != null && options.slot4Picture.Text != "")
                {
                    try
                    {
                        Image slot4Image = new Bitmap(options.slot4Picture.Text.Replace(@"\\", @"\"));
                        slot4Btn.BackgroundImage = slot4Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot4Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
                
            }
            if (options.slot5Name.Text != "" && options.slot5Name.Text != null)
            {
                slot5Btn.Enabled = true;
                slot5Btn.Text = options.slot5Name.Text;
                if (options.slot5Picture.Text != null && options.slot5Picture.Text != "")
                {
                    try
                    {
                        Image slot5Image = new Bitmap(options.slot5Picture.Text.Replace(@"\\", @"\"));
                        slot5Btn.BackgroundImage = slot5Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot5Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                }
            }
            if (options.slot6Name.Text != "" && options.slot6Name.Text != null)
            {
                slot6Btn.Enabled = true;
                slot6Btn.Text = options.slot6Name.Text;
                if (options.slot6Picture.Text != null && options.slot6Picture.Text != "")
                {
                    try
                    {
                        Image slot6Image = new Bitmap(options.slot6Picture.Text.Replace(@"\\", @"\"));
                        slot6Btn.BackgroundImage = slot6Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot6Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
            if (options.slot7Name.Text != "" && options.slot7Name.Text != null)
            {
                slot7Btn.Enabled = true;
                slot7Btn.Text = options.slot7Name.Text;
                if (options.slot7Picture.Text != null && options.slot7Picture.Text != "")
                {
                    try
                    {
                        Image slot7Image = new Bitmap(options.slot7Picture.Text.Replace(@"\\", @"\"));
                        slot7Btn.BackgroundImage = slot7Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot7Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (options.slot8Name.Text != "" && options.slot8Name.Text != null)
            {
                slot8Btn.Enabled = true;
                slot8Btn.Text = options.slot8Name.Text;
                if (options.slot8Picture.Text != null && options.slot8Picture.Text != "")
                {
                    try
                    {
                        Image slot8Image = new Bitmap(options.slot8Picture.Text.Replace(@"\\", @"\"));
                        slot8Btn.BackgroundImage = slot8Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot8Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (options.slot9Name.Text != "" && options.slot9Name.Text != null)
            {
                slot9Btn.Enabled = true;
                slot9Btn.Text = options.slot9Name.Text;
                if (options.slot9Picture.Text != null && options.slot9Picture.Text != "")
                {
                    try
                    {
                        Image slot9Image = new Bitmap(options.slot9Picture.Text.Replace(@"\\", @"\"));
                        slot9Btn.BackgroundImage = slot9Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot9Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
            if (options.slot10Name.Text != "" && options.slot10Name.Text != null)
            {
                slot10Btn.Enabled = true;
                slot10Btn.Text = options.slot10Name.Text;
                if (options.slot10Picture.Text != null && options.slot10Picture.Text != "")
                {
                    try
                    {
                        Image slot10Image = new Bitmap(options.slot10Picture.Text.Replace(@"\\", @"\"));
                        slot10Btn.BackgroundImage = slot10Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot10Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
            if (options.slot11Name.Text != "" && options.slot11Name.Text != null)
            {
                slot11Btn.Enabled = true;
                slot11Btn.Text = options.slot11Name.Text;
                if (options.slot11Picture.Text != null && options.slot11Picture.Text != "")
                {
                    try
                    {
                        Image slot11Image = new Bitmap(options.slot11Picture.Text.Replace(@"\\", @"\"));
                        slot11Btn.BackgroundImage = slot11Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + options.slot11Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
        }
        private void bitsStore_Load(object sender, EventArgs e)
        {
            bitsIcon.Start();
            /**
            icons[0] = new Icon("");
            icons[1] = new Icon(subathontool.Properties.Resources._1.ToString());
            icons[2] = new Icon(subathontool.Properties.Resources._2.ToString());
            icons[3] = new Icon(subathontool.Properties.Resources._3.ToString());
            icons[4] = new Icon(subathontool.Properties.Resources._4.ToString());
            icons[5] = new Icon(subathontool.Properties.Resources._5.ToString());
            icons[6] = new Icon(subathontool.Properties.Resources._6.ToString());
            icons[7] = new Icon(subathontool.Properties.Resources._7.ToString());
            icons[8] = new Icon(subathontool.Properties.Resources._8.ToString());
            icons[9] = new Icon(subathontool.Properties.Resources._9.ToString());
            icons[10] = new Icon(subathontool.Properties.Resources._10.ToString());
            icons[11] = new Icon(subathontool.Properties.Resources._11.ToString());
            icons[12] = new Icon(subathontool.Properties.Resources._12.ToString());
            icons[13] = new Icon(subathontool.Properties.Resources._13.ToString());
            icons[14] = new Icon(subathontool.Properties.Resources._14.ToString());
            icons[15] = new Icon(subathontool.Properties.Resources._15.ToString());
            icons[16] = new Icon(subathontool.Properties.Resources._16.ToString());
            icons[17] = new Icon(subathontool.Properties.Resources._17.ToString());
            icons[18] = new Icon(subathontool.Properties.Resources._18.ToString());
            icons[19] = new Icon(subathontool.Properties.Resources._19.ToString());
            icons[20] = new Icon(subathontool.Properties.Resources._20.ToString());
            icons[21] = new Icon(subathontool.Properties.Resources._21.ToString());
            icons[22] = new Icon(subathontool.Properties.Resources._22.ToString());
            icons[23] = new Icon(subathontool.Properties.Resources._23.ToString());
            icons[24] = new Icon(subathontool.Properties.Resources._24.ToString());
            icons[25] = new Icon(subathontool.Properties.Resources._25.ToString());
            icons[26] = new Icon(subathontool.Properties.Resources._26.ToString());
            icons[27] = new Icon(subathontool.Properties.Resources._27.ToString());
            icons[28] = new Icon(subathontool.Properties.Resources._28.ToString());
            icons[29] = new Icon(subathontool.Properties.Resources._29.ToString());
            icons[30] = new Icon(subathontool.Properties.Resources._30.ToString());
            icons[31] = new Icon(subathontool.Properties.Resources._31.ToString());
            icons[32] = new Icon(subathontool.Properties.Resources._32.ToString());
            icons[33] = new Icon(subathontool.Properties.Resources._33.ToString());
            icons[34] = new Icon(subathontool.Properties.Resources._34.ToString());
            icons[35] = new Icon(subathontool.Properties.Resources._35.ToString());
            icons[36] = new Icon(subathontool.Properties.Resources._36.ToString());
            icons[37] = new Icon(subathontool.Properties.Resources._37.ToString());
            icons[38] = new Icon(subathontool.Properties.Resources._38.ToString());
            icons[39] = new Icon(subathontool.Properties.Resources._39.ToString());
            icons[40] = new Icon(subathontool.Properties.Resources._40.ToString());
            icons[41] = new Icon(subathontool.Properties.Resources._41.ToString());
            icons[42] = new Icon(subathontool.Properties.Resources._42.ToString());
            icons[43] = new Icon(subathontool.Properties.Resources._43.ToString());
            icons[44] = new Icon(subathontool.Properties.Resources._44.ToString());
            icons[45] = new Icon(subathontool.Properties.Resources._45.ToString());
            icons[46] = new Icon(subathontool.Properties.Resources._46.ToString());
            icons[47] = new Icon(subathontool.Properties.Resources._47.ToString());
            icons[48] = new Icon(subathontool.Properties.Resources._48.ToString());
            icons[49] = new Icon(subathontool.Properties.Resources._49.ToString());
            icons[50] = new Icon(subathontool.Properties.Resources._50.ToString());
            icons[51] = new Icon(subathontool.Properties.Resources._51.ToString());
            icons[52] = new Icon(subathontool.Properties.Resources._52.ToString());
            icons[53] = new Icon(subathontool.Properties.Resources._53.ToString());
            icons[54] = new Icon(subathontool.Properties.Resources._54.ToString());
            icons[55] = new Icon(subathontool.Properties.Resources._55.ToString());
            icons[56] = new Icon(subathontool.Properties.Resources._56.ToString());
            icons[57] = new Icon(subathontool.Properties.Resources._57.ToString());
            icons[58] = new Icon(subathontool.Properties.Resources._58.ToString());
            icons[59] = new Icon(subathontool.Properties.Resources._59.ToString());
            icons[60] = new Icon(subathontool.Properties.Resources._60.ToString());
            icons[61] = new Icon(subathontool.Properties.Resources._61.ToString());
            icons[62] = new Icon(subathontool.Properties.Resources._62.ToString());
            icons[63] = new Icon(subathontool.Properties.Resources._63.ToString());
            icons[64] = new Icon(subathontool.Properties.Resources._64.ToString());
            icons[65] = new Icon(subathontool.Properties.Resources._65.ToString());
            icons[66] = new Icon(subathontool.Properties.Resources._66.ToString());
            icons[67] = new Icon(subathontool.Properties.Resources._67.ToString());
            icons[68] = new Icon(subathontool.Properties.Resources._68.ToString());
            icons[69] = new Icon(subathontool.Properties.Resources._69.ToString());
            icons[70] = new Icon(subathontool.Properties.Resources._70.ToString());
            icons[71] = new Icon(subathontool.Properties.Resources._71.ToString());
            icons[72] = new Icon(subathontool.Properties.Resources._72.ToString());
            icons[73] = new Icon(subathontool.Properties.Resources._73.ToString());
            icons[74] = new Icon(subathontool.Properties.Resources._74.ToString());
            icons[75] = new Icon(subathontool.Properties.Resources._75.ToString());
            icons[76] = new Icon(subathontool.Properties.Resources._76.ToString());
            icons[77] = new Icon(subathontool.Properties.Resources._77.ToString());
            icons[78] = new Icon(subathontool.Properties.Resources._78.ToString());
            icons[79] = new Icon(subathontool.Properties.Resources._79.ToString());
            icons[80] = new Icon(subathontool.Properties.Resources._80.ToString());
            icons[81] = new Icon(subathontool.Properties.Resources._81.ToString());
            icons[82] = new Icon(subathontool.Properties.Resources._82.ToString());
            icons[83] = new Icon(subathontool.Properties.Resources._83.ToString());
            icons[84] = new Icon(subathontool.Properties.Resources._84.ToString());
            icons[85] = new Icon(subathontool.Properties.Resources._85.ToString());
            icons[86] = new Icon(subathontool.Properties.Resources._86.ToString());
            icons[87] = new Icon(subathontool.Properties.Resources._87.ToString());
            icons[88] = new Icon(subathontool.Properties.Resources._88.ToString());
            icons[89] = new Icon(subathontool.Properties.Resources._89.ToString());
            icons[90] = new Icon(subathontool.Properties.Resources._90.ToString()); **/
            

        }

        private void bitsStore_VisibleChanged(object sender, EventArgs e)
        {

            if (Visible)
            {
                setStoreItems();
            }

        }

        private void bitsStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
        public int currentIcon = 0;
        private Icon[] icons = new Icon[91];
        private void bitsIcon_Tick(object sender, EventArgs e)
        {
            string img = $"{Application.StartupPath}/data/icons/{currentIcon}.ico";
            this.Icon = Icon.ExtractAssociatedIcon(img);
            currentIcon++;
            if (currentIcon == 90)
            {
                currentIcon = 0;
                 
            }
        }
    }
}
