using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xcom2DamageCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            Calculator cs = new Calculator();
            double AimDubs, GrazeDub, CritDubs, DodgeDubs;
            int LowDamageInt, HighDamageInt, PlusOneInt, CritDamageInt, CritBonusInt, AccuracyInt;
            double.TryParse(Aim.Text, out AimDubs);
            double.TryParse(Graze.Text, out GrazeDub);
            int.TryParse(LowDamage.Text, out LowDamageInt);
            int.TryParse(HighDamage.Text, out HighDamageInt);
            int.TryParse(PlusOne.Text, out PlusOneInt);
            int.TryParse(CritDamage.Text, out CritDamageInt);
            int.TryParse(CritRate.Text, out CritBonusInt);
            int.TryParse(Accuracy.Text, out AccuracyInt);
            double.TryParse(Crit.Text, out CritDubs);
            double.TryParse(Dodge.Text, out DodgeDubs);
            CombatValues currentCV = cs.CalculateCombatValues(new CombatValues(), AimDubs, GrazeDub, CritDubs, DodgeDubs);
            currentCV = cs.CrunchDamage(currentCV, currentCV.currentHitChance, currentCV.currentCritChance, currentCV.currentGraze, LowDamageInt, HighDamageInt, AccuracyInt, PlusOneInt, CritDamageInt, CritBonusInt);
            ToHitChance.Text = (currentCV.currentHitChance * 100).ToString();
            CritChance.Text = (currentCV.currentCritChance * 100).ToString();
            GrazeChance.Text = (currentCV.currentGraze * 100).ToString();
            MissChance.Text = (currentCV.currentMiss * 100).ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void LowDamage_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}