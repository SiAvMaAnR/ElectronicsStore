using ElectronicsShop.Models;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Pages
{
    public partial class TypePage : Page
    {
        public TypeViewModel TypeViewModel = new TypeViewModel();
        public InteractionDataBaseService TypeDataBaseService;
        private readonly string tableName = "[Type]";

        public TypePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = TypeViewModel;
            TypeDataBaseService = new TypeDataBaseService(sqlConnection);
        }

        
    }
}
