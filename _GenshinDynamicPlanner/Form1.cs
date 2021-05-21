using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Additional Using Statements
using _GenshinDynamicPlanner.Models;

namespace _GenshinDynamicPlanner
{
    public partial class Form1 : Form
    {
        #region [Performance & Tearing Fix]
        //To Fix Performance Issue w/ Dynamically Generated Controls within TableLayoutPanel
        //https://www.codeproject.com/Answers/728560/Remove-flickering-due-to-TableLayoutPanel-Panel-2#answer1
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion


        #region .. code for Flucuring ..

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion
        #endregion


        public Form1()
        {
            //Initiaze Form
            InitializeComponent();

            //SetDoubleBuffered - Performance Fix
            SetDoubleBuffered(flp_CharPlanner);
            SetDoubleBuffered(flp_ItemInv);
            

        }

        //[Object Declaration]
        _GenshinDynamicPlanner.Models.User User = new Models.User();
        _GenshinDynamicPlanner.Models.UserSavedCharacterData usrSavedCharacterData = new Models.UserSavedCharacterData();
        _GenshinDynamicPlanner.Models.UserSavedInventoryData usrSavedInventoryData = new Models.UserSavedInventoryData();
        _GenshinDynamicPlanner.Models.dbWeapon dbWeaponsList = new Models.dbWeapon();
        _GenshinDynamicPlanner.Models.Calcuations genCalc = new Models.Calcuations();

        #region [LoginForm TextBox Settings]
        private void textBox_Email_Enter(object sender, EventArgs e)
        {
            if (textBox_Email.Text == "Email")
            {
                textBox_Email.Text = "";
            }
        }

        private void textBox_Email_Leave(object sender, EventArgs e)
        {
            if (textBox_Email.Text == "")
            {
                textBox_Email.Text = "Email";
            }
        }

        private void textBox_Username_Enter(object sender, EventArgs e)
        {
            if (textBox_Username.Text == "Username")
            {
                textBox_Username.Text = "";
            }
        }

        private void textBox_Username_Leave(object sender, EventArgs e)
        {
            if (textBox_Username.Text == "")
            {
                textBox_Username.Text = "Username";
            }
        }

        private void textBox_Password_Enter(object sender, EventArgs e)
        {
            if (textBox_Password.Text == "Password")
            {
                if (textBox_Password.UseSystemPasswordChar == false)
                {
                    textBox_Password.Text = "";
                    textBox_Password.UseSystemPasswordChar = true;
                }
            }
        }

        private void textBox_Password_Leave(object sender, EventArgs e)
        {
            if (textBox_Password.Text == "")
            {
                textBox_Password.Text = "Password";
                textBox_Password.UseSystemPasswordChar = false;
            }
        }

        private void textBox_ConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (textBox_ConfirmPassword.Text == "Confirm Password")
            {
                if (textBox_ConfirmPassword.UseSystemPasswordChar == false)
                {
                    textBox_ConfirmPassword.Text = "";
                    textBox_ConfirmPassword.UseSystemPasswordChar = true;
                }
            }
        }

        private void textBox_ConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (textBox_ConfirmPassword.Text == "")
            {
                textBox_ConfirmPassword.Text = "Confirm Password";
                textBox_ConfirmPassword.UseSystemPasswordChar = false;
            }
        }



        #endregion

        #region [LoginForm Buttons (BottomRow)]
        private void btnLFRegister_Click(object sender, EventArgs e)
        {
            //[Button Colors]
            btnLFRegister.BackColor = Color.FromArgb(54, 49, 60);
            btnLFLogin.BackColor = Color.FromArgb(35, 32, 39);
            btnLFAccountRecovery.BackColor = Color.FromArgb(35, 32, 39);
            //
            //[Register UI]
            //TextBoxes
            textBox_Email.Visible = true;
            textBox_Password.Visible = true;
            textBox_ConfirmPassword.Visible = true;

            //Buttons
            btnLFRegisterConfirm.Visible = true;
            btnLFLoginConfirm.Visible = false;
            btnLFAccountRecoveryConfirm.Visible = false;

            //Reset Textboxes
            textBox_Email.Text = "Email";
            textBox_Username.Text = "Username";
            textBox_Password.Text = "Password";
            textBox_ConfirmPassword.Text = "Confirm Password";
            textBox_Password.UseSystemPasswordChar = false;
            textBox_ConfirmPassword.UseSystemPasswordChar = false;
        }

        private void btnLFLogin_Click(object sender, EventArgs e)
        {
            //[Button Colors]
            btnLFRegister.BackColor = Color.FromArgb(35, 32, 39);
            btnLFLogin.BackColor = Color.FromArgb(54, 49, 60);
            btnLFAccountRecovery.BackColor = Color.FromArgb(35, 32, 39);
            //
            //[Login UI]
            //TextBoxes
            textBox_Email.Visible = false;
            textBox_Password.Visible = true;
            textBox_ConfirmPassword.Visible = false;

            //Buttons
            btnLFRegisterConfirm.Visible = false;
            btnLFLoginConfirm.Visible = true;
            btnLFAccountRecoveryConfirm.Visible = false;

            //Reset Textboxes
            textBox_Username.Text = "Username";
            textBox_Password.Text = "Password";
            textBox_Password.UseSystemPasswordChar = false;
        }

        private void btnLFAccountRecovery_Click(object sender, EventArgs e)
        {
            //[Button Colors]
            btnLFRegister.BackColor = Color.FromArgb(35, 32, 39);
            btnLFLogin.BackColor = Color.FromArgb(35, 32, 39);
            btnLFAccountRecovery.BackColor = Color.FromArgb(54, 49, 60);
            //
            //[Login UI]
            //TextBoxes
            textBox_Email.Visible = true;
            textBox_Password.Visible = false;
            textBox_ConfirmPassword.Visible = false;

            //Buttons
            btnLFRegisterConfirm.Visible = false;
            btnLFLoginConfirm.Visible = false;
            btnLFAccountRecoveryConfirm.Visible = true;

            //Reset Textboxes
            textBox_Email.Text = "Email";
            textBox_Username.Text = "Username";
        }
        #endregion







        #region [LoginForm Confirmation/Functional Buttons]
        private void btnLFRegisterConfirm_Click(object sender, EventArgs e)
        {
            //_GenshinDynamicPlanner.Models.User User = new Models.User();
            User.email = textBox_Email.Text;
            User.username = textBox_Username.Text;
            User.password = textBox_Password.Text;
            User.confirmPassword = textBox_ConfirmPassword.Text;
            

        }

        private void btnLFLoginConfirm_Click(object sender, EventArgs e)
        {
            User.username = textBox_Username.Text;
            User.password = textBox_Password.Text;
            
            
            
            if (User.userLogin() == false)
            {
                DialogResult Error = MessageBox.Show("Username or password is incorrect!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult Success = MessageBox.Show("Login Success!", "System", MessageBoxButtons.OK);

                //tableLayoutPanel_LoginForm.Visible = false;
                tabControl1.TabPages.Remove(tabPage_LoginForm);

                //Add it back vv
                //tabControl1.TabPages.Insert(0, tabPage_LoginForm);



                //functioncall to initialize table here


            }

            Console.WriteLine("FORM USER ID: " + User.userID);

            //Call function to build tablelayoutpanel here? yes
            //initializeTable(User.userID);

            usrSavedInventoryData.updateUserInvData(User.userID);

            generateItemInventory();

            dbWeaponsList.getdbWeaponsList();

            initializeTable(User.userID);
        }

        private void btnLFAccountRecoveryConfirm_Click(object sender, EventArgs e)
        {
            //Not Implemented For This Project
        }
        #endregion




        #region [TableLayoutPanel Character Planner => tlp_CharPlanner]
        public void initializeTable(int userID)
        {
            flp_CharPlanner.Controls.Clear();  //Clear First
            flp_CharPlanner.AutoScroll = true;

            usrSavedCharacterData.getUserCharacterData(userID);
            int charCount = usrSavedCharacterData.userCharList.Count;

            //flp_CharPlanner.
            for (int i = 0; i < charCount; i++)
            {
                
                flp_CharPlanner.Controls.Add(genCharPanel(i));
            }
            flp_CharPlanner.Controls.Add(genAddNewPanel());




        }

        public FlowLayoutPanel genAddNewPanel()
        {
            FlowLayoutPanel addNewPanel = new FlowLayoutPanel();
            //addNewPanel.Size = new Size(882, 253);
            //addNewPanel.Width = 882;
            //addNewPanel.AutoSize = true;
            addNewPanel.Width = 947;  //882
            addNewPanel.BorderStyle = BorderStyle.FixedSingle;

            Button addNewButton = new Button();
            addNewButton.Text = "Add New Character";
            addNewButton.AutoSize = true;
            
            //Buton/Panel Height/Positioning
            addNewPanel.Height = addNewButton.Height.GetHashCode() + 15;
            //addNewButton.Location = new Point(3, 3);

            addNewPanel.Padding = new Padding(3, 3, 3, 3);

            //ComboBox + Save Button
            ComboBox cbbx = new ComboBox();
            cbbx.Font = new Font("Arial", 11);

            //set datasource to chars list;
            //cbbx.DataSource
            //cbbx.Items.Add([Select Character]"});
            //cbbx.Text = "Select Character";  SEE BELOW >>//CBBX Default Text (Post-Uneditable Change)<<
            cbbx.Width = 250;
            cbbx.DropDownStyle = ComboBoxStyle.DropDownList;
            
            

            Button addNewSaveButton = new Button();
            addNewSaveButton.Text = "Add";
            addNewSaveButton.AutoSize = true;
            Button addNewCancelButton = new Button();
            addNewCancelButton.Text = "Cancel";
            addNewCancelButton.AutoSize = true;



            cbbx.Visible = false;
            addNewSaveButton.Visible = false;
            addNewCancelButton.Visible = false;

            //[OnClick Events]
            addNewButton.MouseClick += delegate
            {
                addNewButton.Visible = false;

                cbbx.Visible = true;
                addNewSaveButton.Visible = true;
                addNewCancelButton.Visible = true;

                
                usrSavedCharacterData.getUserAddNewCharList(User.userID);
                cbbx.Items.Clear();  //Clear here & in Function Above ^

                //CBBX Default Text (Post-Uneditable Change)
                string dText = "Select Character";
                cbbx.Items.Insert(0, dText);
                //cbbx.Items[9999] = "Select Character";
                //cbbx.Items.Add("Select Character");
                cbbx.SelectedIndex = 0;
                Console.WriteLine("SELECTEDITEM: " + cbbx.SelectedItem);
                //Console.WriteLine("selectedvalue: " + cbbx.SelectedValue);  <<This returns nothing...

                cbbx.MouseClick += delegate
                {
                    if (cbbx.SelectedIndex == 0 && cbbx.SelectedItem == dText)
                    {
                        cbbx.Items.RemoveAt(0);
                    }
                };
                cbbx.Leave += delegate {
                    Console.WriteLine("SELECTEDINDEX LEAVE: " + cbbx.SelectedIndex);
                    Console.WriteLine("SELECTEDITEM LEAVE: " + cbbx.SelectedItem);
                    if (cbbx.SelectedIndex == 0 && cbbx.SelectedItem == "")
                    {
                        cbbx.Items.Insert(0, dText);
                        cbbx.SelectedIndex = 0;
                    }
                };

                for (int i=0;i< usrSavedCharacterData.addNewCharList.Count; i++)
                {
                    UserSavedCharacterData temp = (UserSavedCharacterData)usrSavedCharacterData.addNewCharList[i];
                    cbbx.Items.Add(i+1 + ". " + temp.Name);
                }
                //cbbx.DataSource = usrSavedCharacterData.addNewCharList;
                //cbbx.Dat
            };

            addNewSaveButton.MouseClick += delegate
            {
                //[Get Actual charID of char in DB]
                //Possibly a better way to do this
                int charIndex = cbbx.SelectedIndex;
                int actualcharID = -1;
                for (int i = 0; i < usrSavedCharacterData.addNewCharList.Count; i++)
                {

                    UserSavedCharacterData temp = (UserSavedCharacterData)usrSavedCharacterData.addNewCharList[i];
                    if (charIndex == i)
                    {
                        actualcharID = temp.charID;
                    }
                }
                Console.WriteLine("ACTUALCHARID: " + actualcharID);
                //

                //Call Function to add character to User Records
                usrSavedCharacterData.addCharRecord(User.userID, actualcharID);
                //Refresh Table
                initializeTable(User.userID);
            };

            addNewCancelButton.MouseClick += delegate
            {
                addNewButton.Visible = true;

                cbbx.Visible = false;
                addNewSaveButton.Visible = false;
                addNewCancelButton.Visible = false;
            };



            addNewPanel.Controls.Add(addNewButton);
            addNewPanel.Controls.Add(cbbx);
            addNewPanel.Controls.Add(addNewSaveButton);
            addNewPanel.Controls.Add(addNewCancelButton);

            return addNewPanel;
        }

        //gen char panel
        public Panel genCharPanel(int rowNum)
        {
            UserSavedCharacterData temp = (UserSavedCharacterData)usrSavedCharacterData.userCharList[rowNum];
            
            //[Char Panel]
            Panel charPanel = new Panel();
            charPanel.Size = new Size(947, 253);  //882, 253  //947, 253
            charPanel.BorderStyle = BorderStyle.FixedSingle;

            //[Del Char Record Button]
            Button delCharRecButton = new Button();
            delCharRecButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            delCharRecButton.ForeColor = System.Drawing.Color.Crimson;
            delCharRecButton.AutoSize = true;
            delCharRecButton.Text = "❌";
            delCharRecButton.Size = new Size(35, 27);
            delCharRecButton.Location = new Point(898, 12);  //833, 12
            delCharRecButton.MouseClick += delegate
            {
                DialogResult result = MessageBox.Show("Delete Character Record?", "Confirmation", MessageBoxButtons.YesNoCancel);  //YesNoCancel -> YesNo
                if (result == DialogResult.Yes)
                {
                    //...
                    usrSavedCharacterData.delCharRecord(User.userID, temp.charID);
                    initializeTable(User.userID);  //Refresh
                }
                else if (result == DialogResult.No)
                {
                    //...
                    return;
                }
                else
                {
                    //...
                    return;
                }
            };

            //[Name Label]
            Label nameLabel = new Label();
            nameLabel.Location = new Point(8, 221);  //12, 221
            nameLabel.AutoSize = true;
            //nameLabel.Text = "NAME";
            nameLabel.Font = new Font("Impact", 14);
            nameLabel.Text = temp.Name;

            //[Large Photo (Char)]
            PictureBox pctbxChar = new PictureBox();
            pctbxChar.Size = new Size(112, 200);
            //pctbx.SizeMod
            pctbxChar.Location = new Point(12, 12);
            pctbxChar.SizeMode = PictureBoxSizeMode.Zoom;  //Zoom not StretchImage to keep aspect ratio
            //pctbx.SizeMode = PictureBoxSizeMode.AutoSize;
            pctbxChar.ImageLocation = temp.imgURLchar;



            //[Talent PictureBoxes]
            PictureBox pctbxT1 = new PictureBox();
            PictureBox pctbxT2 = new PictureBox();
            PictureBox pctbxT3 = new PictureBox();
            pctbxT1.Size = new Size(50, 50);
            pctbxT2.Size = new Size(50, 50);
            pctbxT3.Size = new Size(50, 50);
            pctbxT1.Location = new Point(130, 12);  //300, 12
            pctbxT2.Location = new Point(130, 68);
            pctbxT3.Location = new Point(130, 124);
            pctbxT1.SizeMode = PictureBoxSizeMode.Zoom;
            pctbxT2.SizeMode = PictureBoxSizeMode.Zoom;
            pctbxT3.SizeMode = PictureBoxSizeMode.Zoom;
            pctbxT1.ImageLocation = temp.imgURLt1;
            pctbxT2.ImageLocation = temp.imgURLt2;
            pctbxT3.ImageLocation = temp.imgURLt3;

            //[Talent Labels]
            Label t1Label = new Label();
            Label t2Label = new Label();
            Label t3Label = new Label();
            t1Label.AutoSize = true;
            t2Label.AutoSize = true;
            t3Label.AutoSize = true;
            t1Label.Font = new Font("Impact", 14);
            t2Label.Font = new Font("Impact", 14);
            t3Label.Font = new Font("Impact", 14);
            t1Label.Location = new Point(182, 12);
            t2Label.Location = new Point(182, 68);
            t3Label.Location = new Point(182, 124);
            t1Label.Text = temp.talent1Name;
            t2Label.Text = temp.talent2Name;
            t3Label.Text = temp.talent3Name;

            
            //[Weapon Controls Logic]
            #region [addWeapon Controls + Functionality]
            //Dropdown (ComboBox) for weapon select
            ComboBox wpcbbx = new ComboBox();
            wpcbbx.Font = new Font("Arial", 11);

            //set datasource to chars list;
            //cbbx.DataSource
            //cbbx.Items.Add([Select Character]"});
            //wpcbbx.Text = "Select Weapon";
            wpcbbx.Width = 195;  //212, 21
            wpcbbx.Visible = false;
            //POSITION NEEDS TO BE SET
            wpcbbx.Location = new Point(698, 47);  //633, 47
            wpcbbx.DropDownStyle = ComboBoxStyle.DropDownList;



            //[Add Weapon Button Logic]
            Button addWeapon = new Button();
            addWeapon.Size = new Size(35, 27);  //96, 29
            addWeapon.Location = new Point(698, 12);  //633, 12
            //toggleCalc.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            addWeapon.AutoSize = true;
            addWeapon.Text = "Add Weapon";
            //addWeapon.Visible = false;  //Maybe Not Needed?, check visibility section below...
            addWeapon.Visible = true;

            //[Add Weapon Save/Cancel Button]
            Button addWeaponSave = new Button();
            Button addWeaponCancel = new Button();
            addWeaponSave.Size = new Size(88, 24);
            addWeaponSave.Location = new Point(698, 74);  //633, 74
            //addWeaponSave.AutoSize = true;
            addWeaponSave.Text = "Save";
            addWeaponCancel.Size = new Size(88, 24);
            addWeaponCancel.Location = new Point(791, 74); //735, 74  //726, 74
            //addWeaponCancel.AutoSize = true;
            addWeaponCancel.Text = "Cancel";

            addWeaponSave.Visible = false;
            addWeaponCancel.Visible = false;

            //[Delete Weapon]
            Button delWeapon = new Button();
            delWeapon.Size = new Size(35, 27);  //96, 29
            delWeapon.Location = new Point(687, 12);  //633, 12  //622, 12
            //toggleCalc.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            delWeapon.AutoSize = true;
            delWeapon.Text = "Delete Weapon";
            delWeapon.Visible = false;  //Maybe Not Needed?, check visibility section below...

            //[Weapon Controls Visibility Logic]
            if (temp.wepID == -1)
            {
                addWeapon.Visible = true;
                delWeapon.Visible = false;
            }
            else
            {
                addWeapon.Visible = false;
                delWeapon.Visible = true;

                //function get wepInfo?
                dbWeaponsList.getWeaponData(temp.wepID);
                dbWeapon tempWep = (dbWeapon)dbWeaponsList.userCharWeaponData[0];

                //[Weapon Display Items]
                Label wepNameLabel = new Label();
                wepNameLabel.AutoSize = true;
                wepNameLabel.Font = new Font("Impact", 14);
                wepNameLabel.Location = new Point(684, 47);  //619, 47
                wepNameLabel.Text = tempWep.name;
                //wepNameLabel.Visible = true;

                PictureBox pctbxWEP = new PictureBox();
                pctbxWEP.Size = new Size(100, 100);
                pctbxWEP.Location = new Point(684, 74);  //619, 74
                pctbxWEP.SizeMode = PictureBoxSizeMode.Zoom;
                pctbxWEP.ImageLocation = tempWep.imgURL;

                charPanel.Controls.Add(wepNameLabel);
                charPanel.Controls.Add(pctbxWEP);
            }
            addWeapon.MouseClick += delegate
            {
                wpcbbx.Visible = true;
                addWeaponSave.Visible = true;
                addWeaponCancel.Visible = true;

                //Add Weapons To CBBX
                wpcbbx.Items.Clear();
                string dText = "Select Weapon";
                wpcbbx.Items.Insert(0, dText);
                wpcbbx.SelectedIndex = 0;
                Console.WriteLine("SELECTEDITEM: " + wpcbbx.SelectedItem);
                wpcbbx.MouseClick += delegate
                {
                    if (wpcbbx.SelectedIndex == 0 && wpcbbx.SelectedItem == dText)
                    {
                        wpcbbx.Items.RemoveAt(0);
                    }
                };
                wpcbbx.Leave += delegate {
                    Console.WriteLine("SELECTEDINDEX LEAVE: " + wpcbbx.SelectedIndex);
                    Console.WriteLine("SELECTEDITEM LEAVE: " + wpcbbx.SelectedItem);
                    if (wpcbbx.SelectedIndex == 0 && wpcbbx.SelectedItem == "")
                    {
                        wpcbbx.Items.Insert(0, dText);
                        wpcbbx.SelectedIndex = 0;
                    }
                };

                int listCount = 1;
                for (int i = 1; i < dbWeaponsList.dbWepList.Count; i++)
                {
                    dbWeapon tempWep = (dbWeapon)dbWeaponsList.dbWepList[i];
                    //UserSavedCharacterData temp = (UserSavedCharacterData)usrSavedCharacterData.addNewCharList[i];
                    //wpcbbx.Items.Add(i + 1 + ". " + tempWep.name);
                    if (temp.weaponType == tempWep.wepType)  //List Correct Weapon Types for ea character
                    {
                        wpcbbx.Items.Add(listCount + ". " + tempWep.name);
                        listCount++;
                    }
                }
            };
            addWeaponSave.MouseClick += delegate
            {
                //Visibility
                addWeapon.Visible = false;
                wpcbbx.Visible = false;
                addWeaponSave.Visible = false;
                addWeaponCancel.Visible = false;
                delWeapon.Visible = true;  //Maybe not needed...

                //[Function Call]
                //[Get Actual wepID of wep in DB]
                //Possibly a better way to do this, char is like this above, in addNewChar
                string wepName = (string)wpcbbx.SelectedItem;
                wepName = wepName.Substring(wepName.IndexOf(" ") + 1);
                Console.WriteLine("wepName: " + wepName);
                int actualWepID = -1;
                for (int i = 0; i < dbWeaponsList.dbWepList.Count; i++)
                {

                    dbWeapon tempWep = (dbWeapon)dbWeaponsList.dbWepList[i];
                    if (wepName == tempWep.name)  //List Correct Weapon Types for ea character
                    {
                        Console.WriteLine("SELECTEDITEM IF STATMENT: " + wpcbbx.SelectedItem);
                        actualWepID = tempWep.wepID;
                    }
                }
                //int actualWepID = wpcbbx.SelectedIndex.;
                Console.WriteLine("actualWepID: " + actualWepID);

                //Call Function to add charWep to User Records
                usrSavedCharacterData.addCharWeaponRecord(User.userID, temp.charID, actualWepID);

                //Refresh
                initializeTable(User.userID);  //Refresh
            };
            addWeaponCancel.MouseClick += delegate
            {
                //Visibility
                addWeapon.Visible = true;
                wpcbbx.Visible = false;
                addWeaponSave.Visible = false;
                addWeaponCancel.Visible = false;

                //Function Call?
            };
            delWeapon.MouseClick += delegate
            {
                /*
                //PROBABLY NOT NEEDED
                wpcbbx.Visible = true;
                addWeaponSave.Visible = true;
                addWeaponCancel.Visible = true;*/

                //Function Call
                DialogResult result = MessageBox.Show("Delete Character's Weapon Record?", "Confirmation", MessageBoxButtons.YesNoCancel);  //YesNoCancel -> YesNo
                if (result == DialogResult.Yes)
                {
                    //...
                    usrSavedCharacterData.delCharWeaponRecord(User.userID, temp.charID);
                    initializeTable(User.userID);  //Refresh
                }
                else if (result == DialogResult.No)
                {
                    //...
                    return;
                }
                else
                {
                    //...
                    return;
                }
                //call db set back to -1
                //Refresh
                //initializeTable(User.userID);  //Refresh
            };
            #endregion

            //(Enable/Disable) Toggle Calculation Button
            #region [Toggle Calculation Buttons]
            Button toggleCalc = new Button();
            toggleCalc.Size = new Size(35, 27);
            toggleCalc.Location = new Point(782, 12);  //717, 12
            //toggleCalc.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            toggleCalc.AutoSize = true;
            toggleCalc.Text = "Enable Calculations";

            if (temp.toggleCalc == 1)  //toggleCalc Enabled
            {
                //Move Other Elements
                //Call Calc Function

                toggleCalc.Text = "Disable Calculations";
                /*toggleCalc.MouseClick += delegate
                {

                };*/
                //addWeapon.Visible = true;  //Probably redundant, look above...

                //[Controls for Calc]
                NumericUpDown numBoxCharLevel = new NumericUpDown();
                numBoxCharLevel.Name = "charLevel";
                numBoxCharLevel.Width = 50;
                numBoxCharLevel.Location = new Point(186, 221);  //130, 192
                numBoxCharLevel.Value = temp.charLevel;  //INITIAL VALUE
                
                NumericUpDown numBoxCharLevelDesired = new NumericUpDown();
                numBoxCharLevelDesired.Name = "charDesiredLevel";
                numBoxCharLevelDesired.Width = 50;
                numBoxCharLevelDesired.Location = new Point(242, 221);
                numBoxCharLevelDesired.Value = temp.charDesiredLevel;  //INITIAL VALUE
                
                NumericUpDown numBoxT1Level = new NumericUpDown();
                numBoxT1Level.Name = "t1Level";
                numBoxT1Level.Width = 50;
                numBoxT1Level.Location = new Point(186, 42);
                numBoxT1Level.Value = temp.t1Level;  //INITIAL VALUE
                
                NumericUpDown numBoxT1LevelDesired = new NumericUpDown();
                numBoxT1LevelDesired.Name = "t1DesiredLevel";
                numBoxT1LevelDesired.Width = 50;
                numBoxT1LevelDesired.Location = new Point(242, 42);
                numBoxT1LevelDesired.Value = temp.t1DesiredLevel;  //INITIAL VALUE
                
                NumericUpDown numBoxT2Level = new NumericUpDown();
                numBoxT2Level.Name = "t2Level";
                numBoxT2Level.Width = 50;
                numBoxT2Level.Location = new Point(186, 98);
                numBoxT2Level.Value = temp.t2Level;  //INITIAL VALUE
                
                NumericUpDown numBoxT2LevelDesired = new NumericUpDown();
                numBoxT2LevelDesired.Name = "t2DesiredLevel";
                numBoxT2LevelDesired.Width = 50;
                numBoxT2LevelDesired.Location = new Point(242, 98);
                numBoxT2LevelDesired.Value = temp.t2DesiredLevel;  //INITIAL VALUE
                
                NumericUpDown numBoxT3Level = new NumericUpDown();
                numBoxT3Level.Name = "t3Level";
                numBoxT3Level.Width = 50;
                numBoxT3Level.Location = new Point(186, 154);
                numBoxT3Level.Value = temp.t3Level;  //INITIAL VALUE
                
                NumericUpDown numBoxT3LevelDesired = new NumericUpDown();
                numBoxT3LevelDesired.Name = "t3DesiredLevel";
                numBoxT3LevelDesired.Width = 50;
                numBoxT3LevelDesired.Location = new Point(242, 154);
                numBoxT3LevelDesired.Value = temp.t3DesiredLevel;  //INITIAL VALUE
                
                if (temp.wepID != -1)
                {
                    NumericUpDown numBoxWepLevel = new NumericUpDown();
                    numBoxWepLevel.Name = "wepLevel";
                    numBoxWepLevel.Width = 50;
                    numBoxWepLevel.Location = new Point(684, 221);  //725, 74  //619, 221
                    numBoxWepLevel.Value = temp.wepLevel;  //INITIAL VALUE
                    
                    NumericUpDown numBoxWepLevelDesired = new NumericUpDown();
                    numBoxWepLevelDesired.Name = "wepDesiredLevel";
                    numBoxWepLevelDesired.Width = 50;
                    numBoxWepLevelDesired.Location = new Point(740, 221);  //781, 74  //675, 221
                    numBoxWepLevelDesired.Value = temp.wepDesiredLevel;  //INITIAL VALUE

                    numBoxWepLevel.ValueChanged += delegate {
                        int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxWepLevel.Name, numBoxWepLevelDesired.Name, (int)numBoxWepLevel.Value, (int)numBoxWepLevelDesired.Value);
                        if (flag == 0)
                        {
                            numBoxWepLevelDesired.Value += 1;
                            numBoxWepLevel.Value = numBoxWepLevelDesired.Value;
                        }
                    };
                    numBoxWepLevelDesired.ValueChanged += delegate {
                        int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxWepLevel.Name, numBoxWepLevelDesired.Name, (int)numBoxWepLevel.Value, (int)numBoxWepLevelDesired.Value);
                        if (flag == 0)
                        {
                            numBoxWepLevel.Value = numBoxWepLevelDesired.Value;
                        }
                    };

                    /*FlowLayoutPanel flp_wepMaterialPanel = new FlowLayoutPanel();
                    SetDoubleBuffered(flp_wepMaterialPanel);
                    flp_wepMaterialPanel.Size = new Size(139, 164);
                    flp_wepMaterialPanel.Location = new Point(796, 74);  //731, 74
                    flp_wepMaterialPanel.WrapContents = true;*/
                    //call function to get contents?


                    //[Set Min/Max Values & Add Controls]
                    numBoxWepLevel.Minimum = 1;
                    numBoxWepLevel.Maximum = 90;
                    numBoxWepLevelDesired.Minimum = 1;
                    numBoxWepLevelDesired.Maximum = 90;
                    charPanel.Controls.Add(numBoxWepLevel);
                    charPanel.Controls.Add(numBoxWepLevelDesired);
                    //charPanel.Controls.Add(flp_wepMaterialPanel);
                }
                //[Set Min/Max Values & Add Controls]
                numBoxCharLevel.Minimum = 1;
                numBoxCharLevel.Maximum = 90;
                numBoxCharLevelDesired.Minimum = 1;
                numBoxCharLevelDesired.Maximum = 90;
                numBoxT1Level.Minimum = 1;
                numBoxT1Level.Maximum = 10;
                numBoxT1LevelDesired.Minimum = 1;
                numBoxT1LevelDesired.Maximum = 10;
                numBoxT2Level.Minimum = 1;
                numBoxT2Level.Maximum = 10;
                numBoxT2LevelDesired.Minimum = 1;
                numBoxT2LevelDesired.Maximum = 10;
                numBoxT3Level.Minimum = 1;
                numBoxT3Level.Maximum = 10;
                numBoxT3LevelDesired.Minimum = 1;
                numBoxT3LevelDesired.Maximum = 10;

                /*FlowLayoutPanel flp_charMaterialPanel = new FlowLayoutPanel();
                SetDoubleBuffered(flp_charMaterialPanel);
                flp_charMaterialPanel.Size = new Size(357, 203);  //311, 203
                flp_charMaterialPanel.Location = new Point(322, 35);
                flp_charMaterialPanel.WrapContents = true;
                flp_charMaterialPanel.AutoScroll = true;*/

                //[genCalc Functions]
                int purplesNeeded = genCalc.calcRangesEXPTablePurples((int)numBoxCharLevel.Value, (int)numBoxCharLevelDesired.Value);
                usrSavedInventoryData.getUserInvEXPBooks(User.userID);
                int numPurples = 0;
                int numBlues = 0;
                int numGreens = 0;
                for (int i = 0; i <= 2; i++)
                {
                    UserSavedInventoryData invBooks = (UserSavedInventoryData)usrSavedInventoryData.userInvEXPBooks[i];
                    if (i == 0)
                    {
                        numGreens = invBooks.count;
                    }
                    if (i == 1)
                    {
                        numBlues = invBooks.count;
                    }
                    if (i == 2)
                    {
                        numPurples = invBooks.count;
                    }
                }
                Console.WriteLine("form1 - BOOKS: purple " + numPurples + "  blue " + numBlues + "  green  " + numGreens);
                int[] bookArray = genCalc.calcEXPBookDistribution(purplesNeeded, numPurples, numBlues, numGreens);


                //generateCharMaterialItemPanel(bookArray,);
                /*flp_charMaterialPanel.Controls.Add(generateItemPanel(2));
                flp_charMaterialPanel.Controls.Add(generateItemPanel(1));
                flp_charMaterialPanel.Controls.Add(generateItemPanel(0));
                flp_charMaterialPanel.Controls.Add(generateItemPanel(temp.ascElementalMat));
                flp_charMaterialPanel.Controls.Add(generateItemPanel(temp.ascElementalBossMat));*/




                //[Add onValueChanged Events]
                numBoxCharLevel.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxCharLevel.Name, numBoxCharLevelDesired.Name, (int)numBoxCharLevel.Value, (int)numBoxCharLevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxCharLevelDesired.Value += 1;
                        numBoxCharLevel.Value = numBoxCharLevelDesired.Value;
                    }
                };
                numBoxCharLevelDesired.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxCharLevel.Name, numBoxCharLevelDesired.Name, (int)numBoxCharLevel.Value, (int)numBoxCharLevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxCharLevel.Value = numBoxCharLevelDesired.Value;
                    }
                };
                numBoxT1Level.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT1Level.Name, numBoxT1LevelDesired.Name, (int)numBoxT1Level.Value, (int)numBoxT1LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT1LevelDesired.Value += 1;
                        numBoxT1Level.Value = numBoxT1LevelDesired.Value;
                    }
                };
                numBoxT1LevelDesired.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT1Level.Name, numBoxT1LevelDesired.Name, (int)numBoxT1Level.Value, (int)numBoxT1LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT1Level.Value = numBoxT1LevelDesired.Value;
                    }
                };
                numBoxT2Level.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT2Level.Name, numBoxT2LevelDesired.Name, (int)numBoxT2Level.Value, (int)numBoxT2LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT2LevelDesired.Value += 1;
                        numBoxT2Level.Value = numBoxT2LevelDesired.Value;
                    }
                };
                numBoxT2LevelDesired.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT2Level.Name, numBoxT2LevelDesired.Name, (int)numBoxT2Level.Value, (int)numBoxT2LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT2Level.Value = numBoxT2LevelDesired.Value;
                    }
                };
                numBoxT3Level.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT3Level.Name, numBoxT3LevelDesired.Name, (int)numBoxT3Level.Value, (int)numBoxT3LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT3LevelDesired.Value += 1;
                        numBoxT3Level.Value = numBoxT3LevelDesired.Value;
                    }
                };
                numBoxT3LevelDesired.ValueChanged += delegate {
                    int flag = usrSavedCharacterData.updateLevel(User.userID, temp.charID, numBoxT3Level.Name, numBoxT3LevelDesired.Name, (int)numBoxT3Level.Value, (int)numBoxT3LevelDesired.Value);
                    if (flag == 0)
                    {
                        numBoxT3Level.Value = numBoxT3LevelDesired.Value;
                    }
                };

                //[Add Controls]
                charPanel.Controls.Add(numBoxCharLevel);
                charPanel.Controls.Add(numBoxCharLevelDesired);
                charPanel.Controls.Add(numBoxT1Level);
                charPanel.Controls.Add(numBoxT1LevelDesired);
                charPanel.Controls.Add(numBoxT2Level);
                charPanel.Controls.Add(numBoxT2LevelDesired);
                charPanel.Controls.Add(numBoxT3Level);
                charPanel.Controls.Add(numBoxT3LevelDesired);
                //charPanel.Controls.Add(flp_charMaterialPanel);
            }
            else  //toggleCalc Disabled
            {
                //Move Other Elements

                toggleCalc.Text = "Enable Calculations";
                /*toggleCalc.MouseClick += delegate
                {
                };*/
                //addWeapon.Visible = false;  //Not redundant... Look above...
            }
            int nextValue = temp.toggleCalc;
            toggleCalc.MouseClick += delegate
            {
                Console.WriteLine("NEXTVALUE: " + nextValue);
                nextValue = updateToggleCalc(User.userID, temp.charID, nextValue);
                if (nextValue == 1)  //Enabled
                {
                    //toggleCalc.Text = "Disable Calculations";
                    //addWeapon.Visible = true;  //Not redundant... Look above...

                    //Refresh
                    initializeTable(User.userID);  //Refresh
                }
                else  //Disabled
                {
                    //toggleCalc.Text = "Enable Calculations";
                    //addWeapon.Visible = false;  //Not redundant... Look above...

                    //Refresh
                    initializeTable(User.userID);  //Refresh
                }
            };
            #endregion

            //[Adding Controls To Panel]
            #region [charPanel.Controls.Add(...)]
            //[Add Controls]
            charPanel.Controls.Add(delCharRecButton);
            charPanel.Controls.Add(nameLabel);
            charPanel.Controls.Add(pctbxChar);

            //[PictureBoxTalents]
            charPanel.Controls.Add(pctbxT1);
            charPanel.Controls.Add(pctbxT2);
            charPanel.Controls.Add(pctbxT3);

            //[Talent Name Labels]
            charPanel.Controls.Add(t1Label);
            charPanel.Controls.Add(t2Label);
            charPanel.Controls.Add(t3Label);

            //[Toggle CalcButton]
            charPanel.Controls.Add(toggleCalc);

            //[Weapon Controls (addWeapon)]
            charPanel.Controls.Add(addWeapon);
            charPanel.Controls.Add(wpcbbx);
            charPanel.Controls.Add(addWeaponSave);
            charPanel.Controls.Add(addWeaponCancel);
            charPanel.Controls.Add(delWeapon);
            #endregion

            return charPanel;
        }

        //toggleCalc Event Function
        public int updateToggleCalc(int userID, int charID, int toggleCalc)
        {
            int newToggleCalc;
            //usrSavedCharacterData.updateToggleCalc(User.userID, temp.charID, 0);
            if (toggleCalc == 1)  //toggleCalc Enabled
            {
                //Disable
                newToggleCalc = 0;
            }
            else  //toggleCalc Disabled
            {
                //Enable
                newToggleCalc = 1;
            }
            usrSavedCharacterData.updateToggleCalc(User.userID, charID, newToggleCalc);
            return newToggleCalc;
        }

        /*public Panel generateCharMaterialItemPanel(int[] bookArray, )
        {
            Panel a = new Panel();
            return a;
        }*/

        #endregion
        #region [Dynamically Generated Controls (Generation + Styling)]







        #endregion




        #region [TableLayoutPanel Item Inventory => tlp_ItemInv]


        public void generateItemInventory()
        {
            //test
            usrSavedInventoryData.getUserInventoryData(User.userID);

            //Clear
            flp_ItemInv.Controls.Clear();

            //flowTableLayout Properties (In-Case)
            flp_ItemInv.WrapContents = true;
            flp_ItemInv.AutoScroll = true;
            flp_ItemInv.AutoSize = true;

            //add items
            

            int itemcount = usrSavedInventoryData.userInvList.Count;
            int labelRow = 0;
            int labelType = 1;
            //bool firstOccurrence = true;

            for (int i = 0; i < itemcount; i++)
            {
                UserSavedInventoryData temp = (UserSavedInventoryData)usrSavedInventoryData.userInvList[i];
                Console.WriteLine(temp.name);
                if (i == labelRow && labelType == temp.itemType)
                {
                    if (i != 0)
                    {
                        //BREAK AFTER LAST ITEM
                        Label lblhidden = new Label();
                        lblhidden.Size = new Size(0, 0);
                        lblhidden.Width = 0;
                        lblhidden.Height = 0;
                        lblhidden.Margin = new Padding(0, 0, 0, 0);
                        
                        flp_ItemInv.Controls.Add(lblhidden);
                        flp_ItemInv.SetFlowBreak(lblhidden, true);
                        //flowLayoutPanel1.SetFlowBreak(generateItemPanel(), true);
                    }
                    
                    //ACTUAL LABEL
                    Label lbl = new Label();
                    //lbl.Text = "TESTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTtttttttttttttttttttttttttTTTT";
                    
                    lbl.Text = temp.itemTypeString;
                    lbl.Font = new Font("Impact", 70, FontStyle.Underline);
                    
                    //tableLayoutPanel4.Controls.Add(lbl, 0, labelRow);
                    //lbl.Size = new Size(0, 0);
                    lbl.AutoSize = true;
                    lbl.Margin = new Padding(0, 0, 0, 0);

                    flp_ItemInv.Controls.Add(lbl);

                    Label lblhidden2 = new Label();
                    lblhidden2.Size = new Size(0, 0);
                    lblhidden2.Width = 0;
                    lblhidden2.Height = 0;
                    lblhidden2.Margin = new Padding(0, 0, 0, 0);
                    
                    flp_ItemInv.Controls.Add(lblhidden2);
                    flp_ItemInv.SetFlowBreak(lblhidden2, true);

                    //flp_ItemInv.SetFlowBreak(lbl, true);

                    labelType++;
                }

                flp_ItemInv.Controls.Add(generateItemPanel(i));
                labelRow++;
            }
        }

        
        public Panel generateItemPanel(int itemNumber)
        {
            UserSavedInventoryData temp = (UserSavedInventoryData)usrSavedInventoryData.userInvList[itemNumber];
            Console.WriteLine(temp.name);
            Console.WriteLine(temp.count);

            //Panel itemPanel = new Panel();
            FlowLayoutPanel flp_itemPanel = new FlowLayoutPanel();
            SetDoubleBuffered(flp_itemPanel);

            flp_itemPanel.Size = new Size(100, 300);
            flp_itemPanel.Margin = new Padding(0, 0, 0, 0);

            flp_itemPanel.WrapContents = true;
            //itemPanel.Wrap = true;
            //itemPanel.AutoSize = true;
            
            //commentoutforflowlayoutpanel
            //itemPanel.Dock = DockStyle.Fill;

            //itemPanel.Margin.All = 0;
            //itemPanel.Margin.All = (0, 0, 0, 0);

            //[PictureBox]
            PictureBox pctbx = new PictureBox();
            pctbx.Size = new Size(50, 50);
            pctbx.Location = new Point(25, 0);

            pctbx.SizeMode = PictureBoxSizeMode.Zoom;  //Zoom not StretchImage to keep aspect ratio

            //pctbx.ImageLocation = "https://static.wikia.nocookie.net/gensin-impact/images/a/aa/Item_Cor_Lapis.png/revision/latest/scale-to-width-down/256?cb=20201202043432";
            pctbx.ImageLocation = temp.imgURL;


            //tableLayoutPanel2.Controls.Add(pctbx, r, c);

            //[ITEM NAME]
            Label lbl = new Label();
            //lbl.Location = new Point(25, 70);
            lbl.AutoSize = true;
            lbl.Text = temp.name;
            
            
            //lbl.Text = "ITEM NAME";
            //lbl.Text = string.Format("({0}, {1})", r, c);
            //tableLayoutPanel2.Controls.Add(lbl, r, c);


            //[ItemCount (Label + TextBox)]
            Label lblc = new Label();
            //lblc.Location = new Point(25, 100);
            lbl.AutoSize = true;
            lblc.Text = "[Count]";
            //lbl.Text = string.Format("({0}, {1})", r, c);
            //tableLayoutPanel2.Controls.Add(lbl, r, c);

            //WRONG
            //TextBox txtbx = new TextBox();
            ////txtbx.Location = new Point(25, 120);
            ////txtbx.Dock = new DockStyle(fill);
            //txtbx.Width = 50;
            //txtbx.Text = temp.count;

            //Right
            NumericUpDown numBox = new NumericUpDown();
            numBox.Name = temp.itemID.ToString();  //Set name of TextBox
            numBox.Width = 50;
            numBox.Value = temp.count;
            //numBox.ValueChanged += new EventHandler(itemCount_Changed);
            //EventHandler itemCount_Changed = null;
            numBox.ValueChanged += delegate {
                usrSavedInventoryData.updateUserItemCount(User.userID, Int32.Parse(numBox.Name), (int)numBox.Value);
                //CALL MATH FUNCTIONS LATER HERE
                //////////
            };
            /*numBox.Leave += delegate {
                usrSavedInventoryData.updateUserItemCount(User.userID, Int32.Parse(numBox.Name), (int)numBox.Value);
            };*/


            flp_itemPanel.Controls.Add(pctbx);
            flp_itemPanel.Controls.Add(lbl);
            flp_itemPanel.Controls.Add(lblc);
            //flp_itemPanel.Controls.Add(txtbx);
            flp_itemPanel.Controls.Add(numBox);

            return flp_itemPanel;
        }

        /*private void itemCount_Changed(object sender, EventArgs e)
        {

            usrSavedInventoryData.updateUserItemCount(User.userID, );
        }*/
        #endregion








    }
}
