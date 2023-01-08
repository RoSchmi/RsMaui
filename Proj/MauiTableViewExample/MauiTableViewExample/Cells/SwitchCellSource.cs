﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace MauiTableViewExample.Cells
{
    public class SwitchCellSource
    {
        private List<string> names;
        private bool[] namesStates;

        private TableSection section;

        public enum CellAction
        {
            select,
            delete,
            leave
        }

        public SwitchCellSource(List<string> pNames, TableSection pSection)
        {
            this.names = pNames;
            this.section = pSection;
        }

        public void Populate(List<string> pNames)
        {
            names = pNames;
            namesStates = new bool[names.Count];
            for (int i1 = 0; i1 < namesStates.Length; i1++)
            {
                namesStates[i1] = false;
            }

            if (this.names.Count > 0)
            {
                section.Clear();
                foreach (string account in this.names)
                {
                    SwitchCell switchCell = new SwitchCell { Text = account, On = false };
                    section.Add(switchCell);
                    if (account != "")
                    {
                        ToggledEventArgs toggledEventArgs = new ToggledEventArgs(switchCell.On);
                        switchCell.OnChanged += (object sender2, ToggledEventArgs e2) => OnSwitchCell_toggled(this, toggledEventArgs, account);
                        switchCell.Tapped += OnSwitchCell_Tapped;                     
                    }
                }
            }
        }

        private void OnSwitchCell_Tapped(object sender, EventArgs e)
        {
            int index = names.FindIndex(x => x == ((SwitchCell)sender).Text);
            if ((index == -1) || (index >= names.Count))
            {
                throw new Exception("Account not found, this should not occur");
            }
            SwitchCellSourceEventArgs eventArgs = new SwitchCellSourceEventArgs(
            namesStates[index] == true ? CellAction.delete : CellAction.select, ((SwitchCell)sender).Text);

            OnSwitchCellSourceSend(this, eventArgs);
        }

        public void OnSwitchCell_toggled(object sender, ToggledEventArgs toggledEventArgs, string account)
        {

            int index = -1;

            index = names.FindIndex(x => x == account);

            if (index == -1)
            {
                index = names.FindIndex(x => x == account + " (DEL)");
            }

            if ((index == -1) || (index >= names.Count))
            {
                throw new Exception("Account not found, this should not occur");
            }
            if (namesStates[index] == false)
            {
                names[index] += " (DEL)";
                ((SwitchCell)section[index]).Text += " (DEL)";
            }
            else
            {
                if (names[index].Length == account.Length + 6)
                {
                    names[index] = names[index].Substring(0, names[index].Length - 6);
                    ((SwitchCell)section[index]).Text = names[index];
                }
            }
            SwitchCellSourceEventArgs eventArgs = new SwitchCellSourceEventArgs(
            CellAction.leave, account);
            namesStates[index] = !namesStates[index];
            OnSwitchCellSourceSend(this, eventArgs);
        }

        public delegate void switchCellSourceEventhandler(SwitchCellSource sender, SwitchCellSourceEventArgs e);

        /// <summary>
        /// Raised when a message from the SwitchCellSource is received
        /// </summary>
        public event switchCellSourceEventhandler SwitchCellSourceSend;

        private switchCellSourceEventhandler onSwitchCellSourceSend;

        private void OnSwitchCellSourceSend(SwitchCellSource sender, SwitchCellSourceEventArgs e)
        {
            if (this.onSwitchCellSourceSend == null)

            {
                this.onSwitchCellSourceSend = this.OnSwitchCellSourceSend;
            }
            SwitchCellSourceSend(sender, e);
        }

        public class SwitchCellSourceEventArgs : EventArgs
        {
            public SwitchCellSource.CellAction Action
            { get; private set; }

            public string Account
            { get; private set; }

            internal SwitchCellSourceEventArgs(CellAction pAction, string pAccount)
            {
                this.Account = pAccount;
                this.Action = pAction;
            }
        }
    }
}
