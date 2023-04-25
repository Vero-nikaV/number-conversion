using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Practice_22
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

        /// <summary>
        /// Метод переводящий string в double
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public double Method(string numberString)
        {
            double number = 0;

            //отрицательное или положительное число
            int positive = 1;
            if (numberString[0] == '-') { positive= -1; }
            if (positive == -1) 
                numberString = numberString.Substring(1);

            int separatorNumber = numberString.Length; //определение позиции разделителя
            for (int i = numberString.Length-1; i >=0; i--)
            {
                if (numberString[i] == ',' || numberString[i] == '.')
                {
                    separatorNumber = i;
                    break;
                }
            }
            try
            {
                if (separatorNumber > 308 || numberString.Length - separatorNumber > 324)   //проверка на диапазон
                {
                    throw new Exception("Неправильный диапазон");
                }

                for (int i = 0; i < numberString.Length; i++)
                {
                    if (i != separatorNumber)
                    {
                        int newNumber = numberString[i] - 48;
                        //отработка нечислового символа
                        if (newNumber > 9 || newNumber < 0)

                            throw new Exception();
                        //целая часть
                        if (i < separatorNumber) 
                            number += newNumber * Math.Pow(10, separatorNumber - i - 1);
                       //дробная часть
                        else
                        {
                            int countAfterSepearator = i - separatorNumber;

                            number += newNumber * Math.Pow(10, -countAfterSepearator);

                            //округление для предотвращения ошибок, связанных с переводом в двочиную систему (работает только до 15 цифр после запятой)
                            if (countAfterSepearator < 16)
                                number = Math.Round(number, countAfterSepearator);
                        }
                    }
                }
              
                return number * positive;
                
            }
            catch  //отработка исключения
            {
                BorderRed();
                return 0;
            }
        }


       /// <summary>
       /// Метод, изменяющий границу TextBox
       /// </summary>
       private void BorderRed()
        {
            Input.BorderBrush = Brushes.Red;
            Input.BorderThickness = new Thickness(5);
            MessageBox.Show("Введено некорректное значение");
        }
        /// <summary>
        /// Отработка нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input.BorderBrush = Brushes.Black;
            Input.BorderThickness = new Thickness(1);
            Output.Text = Method(Input.Text).ToString();
           
        }

    }
}
