
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


using Core.Common;
using Core.PCR;


namespace PCRTools
{
    public partial class FrmPCRArena : Form
    {
        public FrmPCRArena()
        {
            InitializeComponent();
        }

        private void FrmPCRArena_Load(object sender, EventArgs e)
        {
            LoadUnits(null);
        }


        static readonly string UNKNOWN_UNIT_ID = "1000";

        string[] unitIds = new string[] { UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID };

        string GetUnitName(string unitId)
        {
            if (unitId == UNKNOWN_UNIT_ID)
                return "";
            return Unit.GetName(unitId);
        }

        string GetUnitIconFilePath(string unitId)
        {
            var iconStar6 = Unit.GetIconResource(unitId, 6);
            if (iconStar6.Exists)
                return iconStar6.Fullpath;
            return Unit.GetIconResource(unitId, 3).AssertExists().Fullpath;
        }

        Image GetUnitIconImage(string unitId)
        {
            return Image.FromFile(GetUnitIconFilePath(unitId));
        }

        void ResetUnits()
        {
            for (var i = 0; i < 5; i++)
            {
                unitIds[i] = UNKNOWN_UNIT_ID;
            }
        }

        void LoadUnits(IEnumerable<Unit> units)
        {
            ResetUnits();
            if (units != null)
            {
                var index = 0;
                foreach (var unit in units)
                {
                    unitIds[index++] = unit.Id;
                    if (index >= 5)
                        break;
                }
            }
            for (var i = 0; i < 5; i++)
            {
                var unitId = unitIds[i];
                var icon = Controls["icon" + i] as PictureBox;
                var name = Controls["name" + i] as Label;
                icon.Image = GetUnitIconImage(unitId);
                name.Text = GetUnitName(unitId);
            }
        }

        private void btnFromClipboard_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
            {
                MessageBox.Show("剪切板中没用图片数据");
                return;
            }

            var image = Clipboard.GetImage();
            var img = new Img(image);
            var units = Arena.FindUnits(img);
            LoadUnits(units);
        }
    }
}
