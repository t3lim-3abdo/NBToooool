using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NBTooool
{
    public partial class neighborTool : Form
    {
        public neighborTool()
        {
            InitializeComponent();
        }
        
        private void generateScript_Click(object sender, EventArgs e)
        {
            NB N = new NB(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text);
            richTextBox1.Text = N.GenerateInitialScript();
            richTextBox1.Text += N.GenerateNBScript();
            richTextBox1.Text += N.GenerateEndScript();
        }

        private void addNBFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileNeighbor = new OpenFileDialog();
            fileNeighbor.AddExtension = true;
            fileNeighbor.DefaultExt = "*.csv";
            fileNeighbor.ShowDialog();
            string[] nbLines = File.ReadAllLines(fileNeighbor.FileName);

            string[] output = new string[nbLines.Count()+2];
            int index = 0;
            NB dummy = new NB("", "", "", "", "", "", "", "", "", "", "", "", "", "");
            output[index] = dummy.GenerateInitialScript();
            index++;
            foreach ( string line in nbLines)
            {
                //Columns will have fixed locations
                string[] property = line.Split(',');
                NB N = new NB(property[0], property[1], property[2], property[3], property[4], property[5], property[6], property[7], property[8], property[9], property[10], property[11], property[12], property[13]);
                output[index] = N.GenerateNBScript();
                index++;
            }
            output[index] = dummy.GenerateEndScript();
            SaveFileDialog sv = new SaveFileDialog();
            sv.ShowDialog();
            File.WriteAllLines(sv.FileName, output);

        }
    }
    public class NB
    {
        private string cellNumber;
        private string relationIndex;
        private string eNB;
        private string CI;
        private string MCC;
        private string MNC;
        private string PCI;
        private string CIO;
        private string TAC;
        private string freqUL;
        private string freqDL;
        private string bwUl;
        private string bwDl;
        private string qCIO;
        private string initalscript = "<nc:rpc xmlns:nc=\"urn:ietf:params:xml:ns:netconf:base:1.0\">\n  <nc:edit-config>\n    <nc:target>\n      <nc:running/>\n    </nc:target>\n    <nc:default-operation>none</nc:default-operation>\n    <nc:config>\n      <mid:managed-element xmlns:mid=\"http://www.samsung.com/global/business/4GvRAN/ns/macro_indoor_dist\">\n        <mid:enb-function>\n          <mid:eutran-generic-cell>";
        private string endScript = "           </ mid:eutran-generic-cell>\n        </mid:enb-function>\n      </mid:managed-element>\n    </nc:config>\n  </nc:edit-config>\n</nc:rpc>";
        public NB(string sourceCellNumber,string sourceRelationIndex,string targeteNB, string targetCI,string targetMCC, string targetMNC, string targetPCI
            , string targetCIO, string targetTAC, string targetFreqUL, string targetFreqDL, string targetBWUL, string targetBWDL, string targetQCIO)
        {
            cellNumber = sourceCellNumber;
            relationIndex = sourceRelationIndex;
            eNB = targeteNB;
            CI = targetCI;
            MCC = targetMCC;
            MNC = targetMNC;
            PCI = targetPCI;
            CIO = targetCIO;
            TAC = targetTAC;
            freqUL = targetFreqUL;
            freqDL = targetFreqDL;
            bwUl = targetBWUL;
            bwDl = targetBWDL;
            qCIO = targetQCIO;
        }
        public string GenerateNBScript()
        {
            string script;
            script = "< mid:eutran - cell - fdd - tdd >\n               < mid:cell - num > " + cellNumber + " </ mid:cell - num >\n               < mid:external - eutran - cell - fdd - logic nc: operation = \"create\" xmlns: nc = \"urn:ietf:params:xml:ns:netconf:base:1.0\" >\n               < mid:relation - index > " + relationIndex + " </ mid:relation - index >\n               < mid:enb - id > " + eNB + " </ mid:enb - id >\n               < mid:target - cell - id > " + CI + " </ mid:target - cell - id >\n               < mid:mcc > " + MCC + " </ mid:mcc >\n               < mid:mnc > " + MNC + " </ mid:mnc >\n               < mid:phy - cell - id > " + PCI + " </ mid:phy - cell - id >\n               < mid:tac > " + TAC + " </ mid:tac >\n               < mid:earfcn - ul > " + freqUL + " </ mid:earfcn - ul >\n               < mid:earfcn - dl > " + freqDL + " </ mid:earfcn - dl >\n               < mid:bandwidth - ul > " + bwUl + " </ mid:bandwidth - ul >\n               < mid:bandwidth - dl > " + bwDl + " </ mid:bandwidth - dl >\n               < mid:individual - offset > " + CIO + " </ mid:individual - offset >\n               < mid:qoffset - cell > " + qCIO + " </ mid:qoffset - cell >\n             </ mid:external - eutran - cell - fdd - logic >\n         </ mid:eutran - cell - fdd - tdd >";
            return script;
        }
        public string GenerateInitialScript()
        {
            return initalscript;
        }
        public string GenerateEndScript()
        {
            return endScript;
        }
    }
}
