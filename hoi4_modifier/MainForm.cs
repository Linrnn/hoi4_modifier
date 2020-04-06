using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public partial class mainForm : Form
{
    private List<int> _list = new List<int>();

    public mainForm()
    {
        InitializeComponent();
    }

    private void ReadButton_Click(object sender, EventArgs e)
    {
        _list.Clear();
        if (File.Exists(inputRichTextBox.Text))
        {
            printLabel.Text = "开始读取";

            StreamReader reader = new StreamReader(inputRichTextBox.Text);
            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine().ToLower();
                if (Regex.IsMatch(s, "land *; *true")) { _list.Add(int.Parse(Regex.Match(s, @"\d+").Value)); }
            }
            reader.Close();

            printLabel.Text = "结束读取";
        }
        else
        {
            printLabel.Text = "路径不存在";
        }
    }

    private void DelectButton_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(inputRichTextBox.Text))
        {
            printLabel.Text = "开始删除";

            foreach (string path in Directory.GetFiles(inputRichTextBox.Text))
            {
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine(string.Empty);
                writer.Close();
            }

            printLabel.Text = "结束删除";
        }
        else
        {
            printLabel.Text = "路径不存在";
        }
    }

    private void ModifyButton_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(inputRichTextBox.Text))
        {
            if (_list == null || _list.Count == 0)
            {
                printLabel.Text = "未读取\"definition.csv\"文件";
            }
            else
            {
                printLabel.Text = "开始修改";

                foreach (string path in Directory.GetFiles(inputRichTextBox.Text))
                {
                    #region 读取
                    StreamReader reader = new StreamReader(path);
                    string text = reader.ReadToEnd();
                    reader.Close();
                    string id = Regex.Match(text, @"id\s*=\s*(\d+)").Groups[1].Value;
                    string manpower = Regex.Match(text, @"manpower\s*=\s*(\d+)").Groups[1].Value;
                    string state_category = Regex.Match(text, "state_category\\s*=\\s*\"?([_a-z]+)\"?").Groups[1].Value;
                    string resources = Regex.Match(text, @"resources\s*=\s*{\s*([=#:.\w\s]*)\s*}").Groups[1].Value;
                    string owner = Regex.Match(text, @"owner\s*=\s*([A-Z]{3})").Groups[1].Value;
                    MatchCollection victory_points = Regex.Matches(text, @"victory_points\s*=\s*{\s*\d[=#:.\d\s^{}]*\d\s*}");
                    string provinces = Regex.Match(text, @"provinces\s*=\s*{\s*(\d[\s\d^{}]*\d)\s*}").Groups[1].Value;
                    #endregion

                    #region 修改
                    if (Regex.IsMatch("SOV GER ITA POL LIT LAT EST SWI BEL LUX CZE AUS HUN YUG ROM BUL GRE ALB SWE NOR FIN CHI PRC SHX YUN GXC MAN MEN XSM SIK TIB MON TAN RAJ NEP BHU TUR IRQ PER AFG SAU YEM OMA ETH SAF LIB SIA", owner))
                    {
                        owner = "LO1";
                    }
                    else if (Regex.IsMatch("USA CAN MEX GUA HON ELS NIC COS PAN CUB HAI DOM COL VEN ECU PRU BRA BOL PAR URG ARG CHL IRE PHI INS AST NZL", owner))
                    {
                        owner = "LO0";
                    }
                    else if (Regex.IsMatch("ENG FRA SPR POR DEN HOL", owner))
                    {
                        owner = "LO1";
                    }
                    else if (Regex.IsMatch("JAP MAL", owner))
                    {
                        owner = "LO0";
                    }
                    else
                    {
                        owner = "LO1";
                    }
                    #endregion

                    #region 写入
                    StreamWriter writer = new StreamWriter(path);
                    writer.WriteLine("state =");
                    writer.WriteLine("{");
                    writer.WriteLine($"\tid = {id}");
                    writer.WriteLine($"\tname = \"STATE_{id}\"");
                    writer.WriteLine($"\tmanpower = {manpower}");
                    writer.WriteLine($"\tstate_category = {state_category}");
                    if (Regex.IsMatch(text, "impassable *= *yes")) { writer.WriteLine("\timpassable = yes"); }
                    if (Regex.IsMatch(text, "resources"))
                    {
                        writer.WriteLine("\tresources =");
                        writer.WriteLine("\t{");
                        writer.Write($"\t\t{resources}");
                        writer.WriteLine("}");
                    }
                    writer.WriteLine("\thistory =");
                    writer.WriteLine("\t{");
                    writer.WriteLine($"\t\towner = {owner}");
                    bool flag = false;
                    if (Regex.IsMatch(text, "victory_points"))
                    {
                        foreach (Match match in victory_points)
                        {
                            string num = match.Value;
                            writer.WriteLine($"\t\t{num}");
                            if (!flag && _list.Contains(int.Parse(Regex.Match(num, @"\d+").Value))) { flag = true; }
                        }
                    }
                    if (flag)
                    {
                        writer.WriteLine("\t\tbuildings =");
                        writer.WriteLine("\t\t{");
                        foreach (Match match1 in victory_points)
                        {
                            MatchCollection collection = Regex.Matches(match1.Value, @"[.\d]+");
                            for (int i = 0; i < collection.Count; i += 2)
                            {
                                int p_id = int.Parse(collection[i].Value);
                                if (_list.Contains(p_id))
                                {
                                    int lv = 0;
                                    switch ((int)float.Parse(collection[i + 1].Value))
                                    {
                                        case 1: lv = 1; break;
                                        case 3: lv = 2; break;
                                        case 5: lv = 3; break;
                                        case 10: lv = 4; break;
                                        case 15: lv = 5; break;
                                        case 20: lv = 6; break;
                                        case 25: lv = 7; break;
                                        case 30: lv = 8; break;
                                        case 40: lv = 9; break;
                                        case 50: lv = 10; break;
                                    }
                                    if (lv != 0) { writer.WriteLine($"\t\t\t{p_id} = {{ naval_base = {lv} }}"); }
                                }
                            }
                        }
                        writer.WriteLine("\t\t}");
                    }
                    writer.WriteLine("\t\tadd_core_of = LO1");
                    writer.WriteLine("\t\tadd_core_of = LO0");
                    writer.WriteLine("\t}");
                    writer.WriteLine($"\tprovinces = {{ {provinces} }}");
                    writer.Write("}");
                    writer.Close();
                    #endregion
                }

                printLabel.Text = "结束修改";
            }
        }
        else
        {
            printLabel.Text = "路径不存在";
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        inputRichTextBox.Text = null;
        printLabel.Text = null;
        if (_list != null) { _list.Clear(); }
    }
}