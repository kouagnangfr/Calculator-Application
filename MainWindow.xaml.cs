using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculatrice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string operationText = ""; // Variable pour stocker le texte des opérations.

        private double valeur = 0;
        private string operateur = "";  // Opération en cours
        private double nombre1 = 0;
        private double nombre2;
        string val = "";
        string val2 = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button touche = (Button)sender;
            string fonction = touche.Content.ToString();

            if (fonction == "C")   // Effacer
            {
                nombre1 = 0;
                nombre2 = 0;
                operateur = "";
                operationText = "";
            }
            else if (fonction == "=") // Égalité
            {

                if (operateur == "")
                {
                    // Afficher un message d'erreur si aucun opérateur n'a été sélectionné
                    MessageBox.Show("Sélectionnez d'abord un opérateur et un deuxième nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (nombre2 == 0 && operateur != "÷" &&  operateur != "%")
                {
                    // Afficher un message d'erreur si aucun nombre n'est selectionner
                    MessageBox.Show("Sélectionnez d'abord un deuxième nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Resultat();
                if(operationText.Contains("="))
                {
                    operationText = "" +  nombre1;
                }
                else
                { operationText += "="; }
                
            }
            else if (fonction == "10^x")  // Puissance de 10
            {
                try
                {

                    if (Double.TryParse(nombre1.ToString(), out double input))
                    {

                        double result;
                        if (input == 0)
                        {
                            result = 1;
                            nombre1 = result;
                            operationText = operationText.Substring(0, operationText.Length - 1) + "10^" + input + "=";
                        }
                        else
                        {
                            result = Math.Pow(10, input);
                            nombre1 = result;
                            operationText = "10^" + input + "=";
                        }
                    }
                }
                catch
                { 
                        // Afficher un message d'erreur si aucun nombre n'est selectionner
                        MessageBox.Show("Sélectionnez d'abord un deuxième nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    
                }
                
            }
            else if (fonction == "|x|")  // Valeur absolue
            {
                try
                {
                    if (Double.TryParse(nombre1.ToString(), out double input))
                    {
                        double result;
                        if (input == 0)
                        {
                            result = 0;
                            nombre1 = result;
                            operationText = operationText.Substring(0, operationText.Length - 1) + "|" + input + "|" + "=";
                        }
                        else
                        {
                            result = Math.Abs(input);
                            nombre1 = result;

                            operationText = "|" + input + "|" + "=";
                        }
                    }
                    else
                    {
                        // Afficher un message d'erreur si aucun nombre n'est selectionner
                        MessageBox.Show("Sélectionnez d'abord un  nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                catch
                {
                    // Afficher un message d'erreur si aucun nombre n'est selectionner
                    MessageBox.Show("Sélectionnez d'abord un deuxième nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
            }
            else if (fonction == "+/-")   // Signe
            {
                if (nombre2 != 0)
                {
                    // Si un deuxième nombre est déjà sélectionné, désélectionnez-le
                    nombre1 = nombre2;
                    nombre2 = 0;
                }
                else
                {
                    nombre1 *= -1;
                }
            }
            else if (IsNumeric(fonction))
            {
                if (operateur == "" )  // Si on n'a pas encore cliqué sur un opérateur 
                {
                    val += fonction;
                    nombre1 = double.Parse(val);
                    textBlock.Text = nombre1.ToString();

                }
                else // S'il y a un opérateur avant
                {
                    val2 += fonction;
                    nombre2 = double.Parse(val2);
                    textBlock.Text = nombre2.ToString();

                }
                operationText += fonction;
            }
            else if (IsOperator(fonction))
            {
                operateur = fonction;
                if (operationText.Contains("="))
                {
                    operationText = "" + nombre1 + fonction;
                }
                else
                {
                    operationText += fonction;
                }
                val = "";
                val2 = "";
                    
            }

           MiseAJourEcran();
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }

        private bool IsOperator(string value) // Liste des opérateurs
        {
            return value == "+" || value == "-" || value == "x" || value == "÷" || value == "%";
        }

        private void Resultat()
        {
            switch (operateur)
            {
                case "+": // Addition
                    valeur = nombre1 + nombre2;
                    break;
                case "-": // Soustraction
                    valeur = nombre1 - nombre2;
                    break;
                case "x": // Multiplication
                    valeur = nombre1 * nombre2;
                    break;
                case "÷": // Division
                    if (nombre2 != 0)
                    {
                        valeur = nombre1 / nombre2;
                    }
                    else
                    {
                        MessageBox.Show("Division par zéro impossible.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        nombre1 = 0;
                        nombre2 = 0;
                        operateur = "";
                        MiseAJourEcran();
                        operationText = "=";
                        return;
                    }
                    break;
                case "%": // Modulo
                    if (nombre2 != 0)
                    {
                        valeur = nombre1 % nombre2;
                    }
                    else
                    {
                        MessageBox.Show("Division par zéro impossible.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        nombre1 = 0;
                        nombre2 = 0;
                        operateur = "";
                        MiseAJourEcran();
                        operationText = "=";
                        
                        return; 
                    }
                    break;
                default:
                    valeur = nombre1;
                    break;
            }

            nombre1 = valeur;
            nombre2 = 0;
        }

        private void MiseAJourEcran()
        {
            if(nombre2 == 0)
            {
                textBlock.Text = nombre1.ToString();
            }
            else
            {
                textBlock.Text = nombre2.ToString();
            }
            
            textBlock2.Text = operationText;
        }
    }
}
