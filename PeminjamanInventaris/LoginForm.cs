﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeminjamanInventaris
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string serverName = "CHLUNIDIA";
            string username = txtUserID.Text;
            string password = txtPassword.Text;

            string connectionString = "Data Source=" + serverName + ";Initial Catalog=inventaris;User ID=sa;Password=Chluni2350719";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM petugas WHERE username = @username AND kata_sandi = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            // Login successful
                            MessageBox.Show("Berhasil login!");
                            MainMenuForm mainMenu = new MainMenuForm();
                            mainMenu.Show();
                            this.Hide();
                        }
                        else
                        {
                            // Login failed
                            MessageBox.Show("Username atau password salah.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    string errorMessage = "Gagal terhubung dengan database:\n";
                    foreach (SqlError error in ex.Errors)
                    {
                        errorMessage += "Message: " + error.Message + "\n";
                    }
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
