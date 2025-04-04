using Microsoft.Win32; // Для OpenFileDialog
using SalesManagmentSystem.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SalesManagmentSystem
{
    public partial class MainWindow : Window
    {
        private string imagePath = string.Empty; // Для хранения пути к загруженному изображению

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadStores();
            LoadSuppliers();
            LoadEmployees();
            LoadCustomers();
            LoadStoresForSales(); // Загрузка магазинов для вкладки Продажи
            LoadEmployeesForSales(); // Загрузка сотрудников для вкладки Продажи
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        
        }
        private void btnAddSale_Click(object sender, RoutedEventArgs e)
        {
            var sale = new Sales
            {
                CustomerID = (int)cmbCustomer.SelectedValue,
                StoreID = (int)cmbStore.SelectedValue,
                EmployeeID = (int)cmbEmployee.SelectedValue,
                SaleDate = DateTime.Now,
                TotalAmount = CalculateTotalAmount() // Предположим, есть метод для расчета
            };

            using (var context = new ModelStore())
            {
                context.Sales.Add(sale);
                context.SaveChanges();
            }

            MessageBox.Show("Продажа добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void LoadStoresForSales()
        {
            using (var context = new ModelStore())
            {
                var stores = context.Stores.ToList();
                cmbStore.ItemsSource = stores;
                cmbStore.DisplayMemberPath = "Name"; // Убедитесь, что поле "Name" существует в классе Stores
                cmbStore.SelectedValuePath = "StoreID"; // Убедитесь, что поле "StoreID" существует в классе Stores
            }
        }



        private void LoadEmployeesForSales()
        {
            using (var context = new ModelStore())
            {
                var employees = context.Employees.ToList();
                cmbEmployee.ItemsSource = employees;
                cmbEmployee.DisplayMemberPath = "FullName"; // Убедитесь, что поле "FullName" существует в классе Employees
                cmbEmployee.SelectedValuePath = "EmployeeID"; // Убедитесь, что поле "EmployeeID" существует в классе Employees
            }
        }

        private decimal CalculateTotalAmount()
        {
            decimal total = 0m;

            var saleItemsList = gridSaleItems.ItemsSource as List<SaleItems>;
            if (saleItemsList != null)
            {
                foreach (var item in saleItemsList)
                {
                    total += item.Quantity * item.SalePrice;
                }
            }

            txtTotalAmount.Text = total.ToString("C");
            return total;
        }
        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                imagePath = dialog.FileName;
                imgProduct.Source = new BitmapImage(new Uri(imagePath)); // Отображаем изображение
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var product = new Products
            {
                Code = txtCode.Text,
                Name = txtName.Text,
                Brand = txtBrand.Text,
                Category = txtCategory.Text,
                RetailPrice = decimal.Parse(txtPrice.Text),
                Image = imagePath // Сохраняем путь к изображению
            };

            using (var context = new ModelStore())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }

            LoadProducts();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var context = new ModelStore())
            {
                var products = context.Products.ToList();
                gridProducts.ItemsSource = products;
                gridSaleItems.ItemsSource = products;   
            }
        }

        private void btnAddStore_Click(object sender, RoutedEventArgs e)
        {
            var store = new Stores
            {
                Name = txtStoreName.Text,
                Address = txtStoreAddress.Text
            };

            using (var context = new ModelStore())
            {
                context.Stores.Add(store);
                context.SaveChanges();
            }

            LoadStores();
        }

        private void LoadStores()
        {
            using (var context = new ModelStore())
            {
                var stores = context.Stores.ToList();
                gridStores.ItemsSource = stores;
            }
        }

        private void LoadCustomers()
        {
            using (var context = new ModelStore())
            {
                var customers = context.Customers.ToList();
                cmbCustomer.ItemsSource = customers;
                cmbCustomer.DisplayMemberPath = "Name"; // Убедитесь, что поле "Name" существует в классе Customers
                cmbCustomer.SelectedValuePath = "CustomerID"; // Убедитесь, что поле "CustomerID" существует в классе Customers
            }
        }
        private void LoadEmployees()
        {
            using (var context = new ModelStore())
            {
                var employees = context.Employees.ToList();
                gridEmployees.ItemsSource = employees;
            }
        }


        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var supplier = new Suppliers
            {
                Name = txtSupplierName.Text,
                ContactInfo = txtSupplierContact.Text
            };

            using (var context = new ModelStore())
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
            }

            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            using (var context = new ModelStore())
            {
                var suppliers = context.Suppliers.ToList();
                gridSuppliers.ItemsSource = suppliers;
            }
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employees
            {
                FullName = txtEmployeeName.Text,
                Position = txtEmployeePosition.Text,
                StoreID = int.Parse(txtStoreID.Text)
            };

            using (var context = new ModelStore())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }

            LoadEmployees();
        }

       

        private void cmbCustomer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
