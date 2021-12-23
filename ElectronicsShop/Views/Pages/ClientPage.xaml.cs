﻿using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ElectronicsShop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientViewModel ClientViewModel = new ClientViewModel();
        public InteractionDataBaseService ClientDataBaseService;
        private readonly string tableName = "[Client]";
        public ClientPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ClientViewModel;
            ClientDataBaseService = new ClientDataBaseService(sqlConnection);
        }

        
    }
}
