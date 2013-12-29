﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD
{
    public partial class Wallets : Form
    {
        Boolean hasChanged = false;
        public Wallets()
        {

            InitializeComponent();
        }

        private void Wallets_Load(object sender, EventArgs e)
        {
            
           cmbWallets.Items.Add("Select Wallet From List");
            foreach (string item in Main.coinVersion.Keys)
            {
                cmbWallets.Items.Add(item.ToString());
            }
            cmbWallets.SelectedIndex = 0;

        }

        private void cmbWallets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWallets.SelectedIndex > 0)
            {

                txtName.Text = cmbWallets.Text;
                txtVersion.Text = Main.coinVersion[cmbWallets.Text].ToString();
                txtPayload.Text = Main.coinPayloadByteSize[cmbWallets.Text].ToString();
                txtTransactionFee.Text = Main.coinTransactionFee[cmbWallets.Text].ToString();
                txtMinTransaction.Text = Main.coinMinTransaction[cmbWallets.Text].ToString();
                txtTransactionSize.Text = Main.coinTransactionSize[cmbWallets.Text].ToString();
                txtRPCPort.Text = Main.coinPort[cmbWallets.Text];
                txtRPCIP.Text = Main.coinIP[cmbWallets.Text];
                txtRPCUser.Text = Main.coinUser[cmbWallets.Text];
                txtRPCPassword.Text = Main.coinPassword[cmbWallets.Text];
                if (Main.coinGetRawSupport[cmbWallets.Text]) { chkGetRawSupport.Checked = true; } else { chkGetRawSupport.Checked = false; }
                if (Main.coinFeePerAddress[cmbWallets.Text]) { chkFeePerAddress.Checked = true; } else { chkFeePerAddress.Checked = false; }
                if (Main.coinDisabled[cmbWallets.Text]) { chkDisabled.Checked = true; } else { chkDisabled.Checked = false; }

                txtName.Enabled = true;
                txtVersion.Enabled = true;
                txtPayload.Enabled = true;
                txtTransactionFee.Enabled = true;
                txtMinTransaction.Enabled = true;
                txtTransactionSize.Enabled = true;
                txtRPCPort.Enabled = true;
                txtRPCIP.Enabled = true;
                txtRPCUser.Enabled = true;
                txtRPCPassword.Enabled = true;
                chkFeePerAddress.Enabled = true;
                chkGetRawSupport.Enabled = true;
                chkDisabled.Enabled = true;
                
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;

            }
            else
            {
                txtName.Enabled = false;
                txtVersion.Enabled = false;
                txtPayload.Enabled = false;
                txtTransactionFee.Enabled = false;
                txtMinTransaction.Enabled = false;
                txtTransactionSize.Enabled = false;
                txtRPCPort.Enabled = false;
                txtRPCIP.Enabled = false;
                txtRPCUser.Enabled = false;
                txtRPCPassword.Enabled = false;
                chkFeePerAddress.Enabled = false;
                chkGetRawSupport.Enabled = false;
                chkDisabled.Enabled = false;

                txtName.Text = "";
                txtVersion.Text = "";
                txtPayload.Text = "";
                txtTransactionFee.Text = "";
                txtMinTransaction.Text = "";
                txtTransactionSize.Text = "";
                txtRPCPort.Text = "";
                txtRPCIP.Text = "";
                txtRPCUser.Text = "";
                txtRPCPassword.Text = "";
                chkGetRawSupport.Checked = false;
                chkFeePerAddress.Checked = false;
                chkDisabled.Checked = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e){
            try
            {
                Main.coinVersion.Add(txtName.Text, byte.Parse(txtVersion.Text));
                Main.coinPayloadByteSize.Add(txtName.Text, int.Parse(txtPayload.Text));
                Main.coinTransactionFee.Add(txtName.Text, decimal.Parse(txtTransactionFee.Text));
                Main.coinMinTransaction.Add(txtName.Text, decimal.Parse(txtMinTransaction.Text));
                Main.coinTransactionSize.Add(txtName.Text, int.Parse(txtTransactionSize.Text));
                Main.coinPort.Add(txtName.Text, txtRPCPort.Text);
                Main.coinIP.Add(txtName.Text, txtRPCIP.Text);
                Main.coinUser.Add(txtName.Text, txtRPCUser.Text);
                Main.coinPassword.Add(txtName.Text, txtRPCPassword.Text);
                if (chkGetRawSupport.Checked) { Main.coinGetRawSupport.Add(txtName.Text, true); } else { Main.coinGetRawSupport.Add(txtName.Text, false); }
                if (chkFeePerAddress.Checked) { Main.coinFeePerAddress.Add(txtName.Text, true); } else { Main.coinFeePerAddress.Add(txtName.Text, false); }
                if (chkDisabled.Checked) { Main.coinDisabled.Add(txtName.Text, true); } else { Main.coinDisabled.Add(txtName.Text, false); }
                cmbWallets.Items.Add(txtName.Text);
 
                SaveWallets();
                hasChanged = true;
                cmbWallets.SelectedIndex = cmbWallets.Items.Count -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbWallets.SelectedIndex > 0)
            {
                var indexBeforeDelete = cmbWallets.SelectedIndex;
                cmbWallets.Items.Remove(cmbWallets.SelectedItem);
                cmbWallets.SelectedIndex = indexBeforeDelete - 1;
                hasChanged = true;
                SaveWallets();
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveWallets();
            hasChanged = true;
        }

        private void SaveWallets()
        {
            System.IO.File.Delete("coin.conf");
            System.IO.StreamWriter writeCoinConf = new System.IO.StreamWriter("coin.conf");


            Main.coinVersion[cmbWallets.Text] = byte.Parse(txtVersion.Text);
            Main.coinPayloadByteSize[cmbWallets.Text] = int.Parse(txtPayload.Text);
            Main.coinTransactionFee[cmbWallets.Text] = decimal.Parse(txtTransactionFee.Text);
            Main.coinMinTransaction[cmbWallets.Text] = decimal.Parse(txtMinTransaction.Text);
            Main.coinTransactionSize[cmbWallets.Text] = int.Parse(txtTransactionSize.Text);
            Main.coinPort[cmbWallets.Text] = txtRPCPort.Text;
            Main.coinIP[cmbWallets.Text] = txtRPCIP.Text;
            Main.coinUser[cmbWallets.Text] = txtRPCUser.Text;
            Main.coinPassword[cmbWallets.Text] = txtRPCPassword.Text;
            if (chkGetRawSupport.Checked) { Main.coinGetRawSupport[cmbWallets.Text] = true; } else { Main.coinGetRawSupport[cmbWallets.Text] = false; }
            if (chkFeePerAddress.Checked) { Main.coinFeePerAddress[cmbWallets.Text] = true; } else { Main.coinFeePerAddress[cmbWallets.Text] = false; }
            if (chkDisabled.Checked) { Main.coinDisabled[cmbWallets.Text] = true; } else { Main.coinDisabled[cmbWallets.Text] = false; }


            foreach (string i in cmbWallets.Items)
            {
                if (i != "Select Wallet From List")
                {
                    writeCoinConf.WriteLine(i + " " + Main.coinVersion[i].ToString().Replace(" ", "") + " " + Main.coinPayloadByteSize[i].ToString().Replace(" ", "") + " " + Main.coinTransactionFee[i].ToString().Replace(" ", "") + " " + Main.coinMinTransaction[i].ToString().Replace(" ", "") + " " + Main.coinTransactionSize[i].ToString().Replace(" ", "") + " " + Main.coinPort[i].Replace(" ", "") + " " + Main.coinIP[i].Replace(" ", "") + " " + Main.coinUser[i].Replace(" ", "") + " " + Main.coinPassword[i].Replace(" ", "") + " " + Main.coinGetRawSupport[i].ToString().Replace(" ", "") + " " + Main.coinFeePerAddress[i].ToString().Replace(" ", "") + " " + Main.coinDisabled[i].ToString().Replace(" ", ""));
                }
            }
            writeCoinConf.Close();





        }

        private void Wallets_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hasChanged)
            {
                System.Diagnostics.Process.Start("ADD.EXE");
                Application.Exit();
            }
        }


 
    }
}
