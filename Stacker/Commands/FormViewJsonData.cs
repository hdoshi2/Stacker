using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stacker.Commands
{
    public partial class FormViewJsonData : Form
    {
        Dictionary<string, string> DictJSON;

        public FormViewJsonData(Dictionary<string, string> jsonData)
        {
            InitializeComponent();

            dgvConcreteData.Rows.Clear();
            dgvConcreteData.ReadOnly = true;

            DictJSON = jsonData;

            int currentIndex = 0;
            foreach (var item in DictJSON)
            {
                var jsonKey = item.Key;
                var jsonValue = item.Value;

                currentIndex = dgvConcreteData.Rows.Add();

                dgvConcreteData.Rows[currentIndex].Cells[0].Value = jsonKey;
                dgvConcreteData.Rows[currentIndex].Cells[1].Value = jsonValue;
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
