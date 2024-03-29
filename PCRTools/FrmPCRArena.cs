﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Core.Common;
using Core.PCR;

using OpenCvSharp;
using CvSize = OpenCvSharp.Size;
using CvPoint = OpenCvSharp.Point;

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
            LoadUnits(new int[0]);
        }

        static readonly int UNKNOWN_UNIT_ID = 1000;

        int[] unitIds = new int[] { UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID, UNKNOWN_UNIT_ID };

        string GetUnitName(int unitId)
        {
            if (unitId == UNKNOWN_UNIT_ID)
                return "";
            return Unit.GetName(unitId);
        }

        string GetUnitIconFilePath(int unitId)
        {
            var iconStar6 = Unit.GetIconResource(unitId, 6);
            if (iconStar6.Exists)
                return iconStar6.Fullpath;
            return Unit.GetIconResource(unitId, 3).AssertExists().Fullpath;
        }

        Image GetUnitIconImage(int unitId)
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

        void LoadUnits(IEnumerable<int> unitIds)
        {
            var units = unitIds?.Select(x => Unit.GetUnitById(x)).ToList() ?? new List<Unit>();
            LoadUnits(units);
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
            var names = new List<string>();
            for (var i = 0; i < 5; i++)
            {
                var unitId = unitIds[i];
                var picIcon = Controls["icon" + i] as PictureBox;
                var lblName = Controls["name" + i] as Label;
                picIcon.Image = GetUnitIconImage(unitId);
                var name = GetUnitName(unitId);
                lblName.Text = name;
                if (!string.IsNullOrWhiteSpace(name))
                    names.Add(name);
            }
            txtUnitNames.Text = string.Join(" ", names);
        }

        List<int> GetUnitIds()
        {
            var r = new List<int>();
            foreach (var unitId in unitIds)
            {
                if (unitId != UNKNOWN_UNIT_ID)
                    r.Add(unitId);
            }
            return r;
        }

        private void btnFromClipboard_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
            {
                MessageBox.Show("剪切板中没有可用的图片数据");
                return;
            }

            var image = Clipboard.GetImage();
            var img = new Img(image);
            var units = Arena.FindUnits(img);
            LoadUnits(units);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var ids = GetUnitIds();
            if (ids.Count == 0)
            {
                MessageBox.Show("至少选择一名角色");
                return;
            }
            if (ids.Count < 5 && MessageBox.Show("队伍角色不足5名，继续吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var numIds = ids.ToList();
            var teams = Arena.AttackTeamQuery(new ArenaAttackTeamQueryParams().SetDefenceTeamIds(numIds));
            RenderAttackTeamQuery(teams);
        }

        void RenderAttackTeamQuery(List<ArenaAttackTeam> teams)
        {
            if (teams.Count == 0)
            {
                MessageBox.Show("没有查到对应的进攻队伍");
                return;
            }

            var img = Arena.RenderAttackTeams(teams);

            Cv2.ImShow("QueryResult" + DateTime.Now.ToString(), img.ToMat());
        }

        private void btnFromText_Click(object sender, EventArgs e)
        {
            var nameStr = txtUnitNames.Text;
            var names = nameStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var ids = new List<int>();
            foreach (var name in names)
            {
                var unit = Unit.GetUnitByName(name);
                if (unit != null)
                    ids.Add(unit.Id);
            }
            LoadUnits(ids);
        }
    }
}
