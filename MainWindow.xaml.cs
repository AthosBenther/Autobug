using System;
//using System.Drawing;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace autobug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = new Timer();

        List<Autobug> Bugs = new List<Autobug>();
        List<Food> Foods = new List<Food>();
        List<BugLabel> Labels = new List<BugLabel>();
        List<string> Names = new List<string>(new string[]{
            "Adailton",
            "Adauto",
            "Adelino",
            "Agenor",
            "Albino",
            "Amaro",
            "Ambrósio",
            "Anélcio",
            "Arcanjo",
            "Ariovaldo",
            "Aristênio",
            "Arnaldo",
            "Arnóbio",
            "Astolfo",
            "Astrogildo",
            "Balbino",
            "Baltazar",
            "Crescêncio",
            "Deoclides",
            "Diógenes",
            "Dirceu",
            "Durvalino",
            "Edvaldo",
            "Eliseu",
            "Elmo",
            "Eustáquio",
            "Felizardo",
            "Gérson",
            "Gilson",
            "Godofredo",
            "Heraldo",
            "Hilário",
            "Hildebrando",
            "Inocêncio",
            "Jurandir",
            "Leôncio",
            "Leônidas",
            "Ludovico",
            "Militão",
            "Milton",
            "Nelson",
            "Nicanor",
            "Onofre",
            "Orestes",
            "Osmar",
            "Petrônio",
            "Petrúcio",
            "Plácido",
            "Romildo",
            "Severino",
            "Simplício",
            "Teobaldo",
            "Valdemar",
            "Valdomiro",
            "Venâncio",
            "Wando"});
        List<int> UsedNames = new List<int>();


        int bugsMaxQty;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = 15;
            timer.Tick += Timer_Tick;
            try
            {
                Names.AddRange(File.ReadLines("resources/nomes.txt"));
            }
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show(ex.Message);
            }



            bugsMaxQty = 10;
            object syncLock = new object();
            lock (syncLock)
            {
                for (int i = 0; i <= bugsMaxQty * 10; i++)
                {
                    Foods.Add(new Food());
                }

                for (int i = 0; i <= bugsMaxQty; i++)
                {
                    Bugs.Add(new Autobug());
                }
                for (int i = 0; i <= bugsMaxQty; i++)
                {
                    Labels.Add(new BugLabel());
                }
                foreach (BugLabel label in Labels)
                {
                    label.IsOn = chkLabels.IsChecked.Value;
                    label.RelativeAge = chkRelativeAge.IsChecked.Value;
                    Stage.Children.Add(label);
                }

                foreach (Food food in Foods)
                {
                    Stage.Children.Add(food);
                    food.hatch();
                }
                foreach (Autobug bug in Bugs)
                {
                    int nameIndex = Measure2.Random(Names.Count);
                    UsedNames.Add(nameIndex);
                    string newName = Names[nameIndex];
                    bug.Name = newName;
                    Stage.Children.Add(bug);
                    bug.hatch();
                }

            }


            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            foreach (Autobug bug in Bugs)
            {
                bug.act(Foods);

                foreach (Food food in Foods)
                {
                    if (Measure2.Distance(bug.ActualPosition, food.Position) < 0.001) food.shrink(bug.BiteSize);
                    else food.grow();
                }

                Labels[Bugs.IndexOf(bug)].ShowLabel = bug._showLabel;
                Labels[Bugs.IndexOf(bug)].ShowLabelPersistent = bug._showLabelPersistent;
                Labels[Bugs.IndexOf(bug)].BugName = (bug._amDying) ? "MORTO" : bug.Name;
                Labels[Bugs.IndexOf(bug)].BugType = bug.Type;
                Labels[Bugs.IndexOf(bug)].AgeNow = bug.Age;
                Labels[Bugs.IndexOf(bug)].AgeMax = bug.LifeSpan;
                Labels[Bugs.IndexOf(bug)].RelativeAge = chkRelativeAge.IsChecked.Value;
                Labels[Bugs.IndexOf(bug)].IsOn = chkLabels.IsChecked.Value;
                Labels[Bugs.IndexOf(bug)].RenderTransform = new TranslateTransform(bug.pos.X + 50 + bug.Size/2, bug.pos.Y + 25 + bug.Size/2);
                Labels[Bugs.IndexOf(bug)].populate();
            }
        }
    }
}
