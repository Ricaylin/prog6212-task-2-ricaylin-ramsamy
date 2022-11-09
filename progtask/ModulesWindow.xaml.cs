using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using progtask.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace progtask
{
    /// <summary>
    /// Interaction logic for ModulesWindow.xaml
    /// </summary>
    public partial class ModulesWindow : Window
    {
        public ModulesWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ModulesWindow_Loaded);
        }

        public async void ModulesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StudyAppContext studyAppContext = new StudyAppContext();

            List<Module> modules = await studyAppContext.modules.Where(b => b.Username == CurrentProfile.user.Username).ToListAsync();
            ModulesListBox.ItemsSource = modules;
            ModulesComboBox.ItemsSource = modules;
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private async void CreateModuleButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            Module module = new Module();
            module.ModuleCode = ModuleCodeTextBox.Text;

            if(module.ModuleCode.IsNullOrEmpty())
            {
                ErrorTextBlock.Text += "The module code should atleast be 1 letter. ";
            }

            module.ModuleName = ModuleNameTextBox.Text;

            if (module.ModuleName.IsNullOrEmpty())
            {
                ErrorTextBlock.Text += "The module name should atleast be 1 letter. ";
            }

            int credits;
            bool isParsableCredits = Int32.TryParse(CreditsTextBox.Text, out credits);

            if (isParsableCredits)
            {
                module.Credits = credits;
            } else
            {
                ErrorTextBlock.Text += "The number of credits entered is invalid. ";
                return;
            }

            int classhours;
            bool isParsableHours = Int32.TryParse(ClassHoursPerWeekTextBox.Text, out classhours);

            if (isParsableHours)
            {
                module.ClassHoursPerWeek = classhours;
            }
            else
            {
                ErrorTextBlock.Text += "The number of class hours per week entered is invalid. ";
                return;
            }

            DateTime? startDate = StartDateDatePicker.SelectedDate;
            if(startDate.HasValue)
            {
                module.SemesterStartDate = startDate.Value;
            }
            else
            {
                ErrorTextBlock.Text += "The date selected is invalid. ";
                return;
            }

            int numberofweeks;
            bool isParsableWeeks = Int32.TryParse(ClassHoursPerWeekTextBox.Text, out numberofweeks);

            if (isParsableWeeks)
            {
                module.WeeksInSemester = numberofweeks;
            }
            else
            {
                ErrorTextBlock.Text += "The number of weeks in the semester entered is invalid. ";
                return;
            }

            double rsh = ((credits * 10) / numberofweeks) - classhours;
            module.RecommendedHoursToStudy = rsh;
            module.Username = CurrentProfile.user.Username;

            StudyAppContext context = new StudyAppContext();

            try
            {
                await context.modules.AddAsync(module);
                await context.SaveChangesAsync();
                ErrorTextBlock.Text = "Successfully created module " + ModuleCodeTextBox.Text;
                GetModules();
                

            }
            catch (UniqueConstraintException ex)
            {
                ErrorTextBlock.Text = ex.Message;
            }

        }

        public async void GetModules()
        {
            ModulesListBox.Items.Refresh();
            StudyAppContext studyAppContext = new StudyAppContext();

            List<Module> modules = await studyAppContext.modules.Where(b => b.Username == CurrentProfile.user.Username).ToListAsync();
            ModulesListBox.ItemsSource = modules;
            ModulesComboBox.ItemsSource = modules;
        }

        private async void AddTimeButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTimeTextBlock.Text = "";
            int hours;
            bool isParsablehours = Int32.TryParse(TimeStudiedTodayTextBox.Text, out hours);

            StudyAppContext context = new StudyAppContext();

            if (ModulesComboBox.SelectedItem == null)
            {
                ErrorTimeTextBlock.Text += "Please select a module. ";
                return;
            }

            if (isParsablehours)
            {
               
                try
                {
                    var module = await (context.modules.FirstOrDefaultAsync(b => b.Username == CurrentProfile.user.Username && b.ModuleCode == ModulesComboBox.SelectedItem.ToString()));
                    module.HoursStudiedToday = hours;
                    await context.SaveChangesAsync();
                    ErrorTimeTextBlock.Text += "Updated hours. ";
                    GetModules();


                }
                catch (UniqueConstraintException ex)
                {
                    ErrorTimeTextBlock.Text = ex.Message;
                }

            }
            else
            {
               ErrorTimeTextBlock.Text += "The number of hours entered is invalid. ";
               return;
            }
        }
    }
}
