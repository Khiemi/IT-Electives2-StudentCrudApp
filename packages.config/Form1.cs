using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentCrudApp
{
    public partial class Form1 : Form
    {
        private readonly StudentRepository _studentRepository = new StudentRepository();

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            bSave.Click += Save_Click;
            bUpdate.Click += Update_Click;
            bDelete.Click += Delete_Click;
            bClear.Click += Clear_Click;
            tbSearch.TextChanged += tbSearch_TextChanged;
            bSearch.Click += tbSearch_TextChanged;
            dgvShow.CellClick += dgvShow_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            dgvShow.DataSource = _studentRepository.GetAll();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbStudentID.Text) ||
                string.IsNullOrWhiteSpace(tbStudentName.Text) ||
                string.IsNullOrWhiteSpace(cbProgram.Text))
            {
                MessageBox.Show(
                    "Please fill in Student ID, Student Name, and Program.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                int rowsAffected = _studentRepository.Insert(
                    tbStudentID.Text.Trim(),
                    tbStudentName.Text.Trim(),
                    cbProgram.Text.Trim());

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                    ClearInputs();
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow selectedRow = dgvShow.Rows[e.RowIndex];
            tbStudentID.Text = selectedRow.Cells["StudentId"].Value?.ToString() ?? string.Empty;
            tbStudentName.Text = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;
            cbProgram.Text = selectedRow.Cells["Course"].Value?.ToString() ?? string.Empty;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                int rowsAffected = _studentRepository.Update(
                    tbStudentID.Text.Trim(),
                    tbStudentName.Text.Trim(),
                    cbProgram.Text.Trim());

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("No matching student record was found to update.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string studentId = tbStudentID.Text.Trim();
            if (string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Please select or enter a Student ID to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Delete this student?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                int rowsAffected = _studentRepository.Delete(studentId);
                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Student deleted successfully ({rowsAffected} row removed).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("No matching student record was removed.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = tbSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadStudents();
                }
                else
                {
                    dgvShow.DataSource = _studentRepository.Search(keyword);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            tbStudentID.Clear();
            tbStudentName.Clear();
            cbProgram.SelectedIndex = -1;
            cbProgram.Text = string.Empty;
            cbGender.SelectedIndex = -1;
            cbGender.Text = string.Empty;
            tbSearch.Clear();
            dgvShow.ClearSelection();
        }
    }
}