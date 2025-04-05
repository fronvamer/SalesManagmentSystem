using Microsoft.Win32; 
using SalesManagmentSystem.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SalesManagmentSystem
{
    public partial class MainWindow : Window
    {
        private string imagePath = string.Empty; 
        private decimal totalAmount = 0m;
        private List<SaleItems> saleItems = new List<SaleItems>();
        private Warehouses selectedWarehouse; 

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadStores();
            LoadSuppliers();
            LoadEmployees();
            LoadCustomers();
            LoadStoresForSales(); 
            LoadProductsForSales(); 
            LoadEmployeesForSales();
            LoadMainWarehouses();
            LoadWarehouseProducts();
        }
        private void LoadMainWarehouses()
        {
            using (var context = new ModelStore())
            {
                var warehouses = context.Warehouses.ToList();
                cmbMainWarehouse.ItemsSource = warehouses; 
                cmbMainWarehouse.DisplayMemberPath = "Name"; 
                cmbMainWarehouse.SelectedValuePath = "WarehouseID"; 
            }
        }

        private void LoadProductsForSales()
        {
            using (var context = new ModelStore())
            {
                var products = context.Products.ToList();
                cmbProducts.ItemsSource = products;
                cmbProducts.DisplayMemberPath = "Name"; 
                cmbProducts.SelectedValuePath = "ProductID"; 
            }
        }
        private void cmbProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (cmbProducts.SelectedItem is Products selectedProduct)
            //{
            //    
            //    MessageBox.Show($"Выбран товар: {selectedProduct.Name}, Цена: {selectedProduct.RetailPrice}");
            //}
        }

      

        private void btnAddSale_Click(object sender, RoutedEventArgs e)
        {
            totalAmount = CalculateTotalAmount();
            var sale = new Sales
            {
                CustomerID = (int)cmbCustomer.SelectedValue,
                StoreID = (int)cmbStore.SelectedValue,
                EmployeeID = (int)cmbEmployee.SelectedValue,
                SaleDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            using (var context = new ModelStore())
            {
                context.Sales.Add(sale);
                context.SaveChanges();


                foreach (var item in saleItems)
                {
                    var saleItem = new SaleItems
                    {
                        SaleID = sale.SaleID, 
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        SalePrice = item.SalePrice
                    };
                    context.SaleItems.Add(saleItem);

            
                    var warehouseItem = context.Warehouses.FirstOrDefault(w => w.StoreID == sale.StoreID && w.ProductID == item.ProductID);
                    if (warehouseItem != null && warehouseItem.MinQuantity >= item.Quantity)
                    {
                        warehouseItem.MinQuantity -= item.Quantity;
                       
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Недостаточное количество товара на складе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return; 
                    }
                }

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
                cmbStore.DisplayMemberPath = "Name"; 
                cmbStore.SelectedValuePath = "StoreID"; 
            }
        }



        private void LoadEmployeesForSales()
        {
            using (var context = new ModelStore())
            {
                var employees = context.Employees.ToList();
                cmbEmployee.ItemsSource = employees;
                cmbEmployee.DisplayMemberPath = "FullName"; 
                cmbEmployee.SelectedValuePath = "EmployeeID"; 
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
                imgProduct.Source = new BitmapImage(new Uri(imagePath)); 
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
                Image = imagePath 
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
                cmbCustomer.DisplayMemberPath = "Name"; 
                cmbCustomer.SelectedValuePath = "CustomerID"; 
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

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.Now;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.Now;

            using (var context = new ModelStore())
            {
                var reportData = (from sale in context.Sales
                                  where sale.SaleDate >= startDate && sale.SaleDate <= endDate
                                  join store in context.Stores on sale.StoreID equals store.StoreID
                                  join employee in context.Employees on sale.EmployeeID equals employee.EmployeeID
                                  join saleItem in context.SaleItems on sale.SaleID equals saleItem.SaleID
                                  join product in context.Products on saleItem.ProductID equals product.ProductID
                                  select new
                                  {
                                      SaleDate = sale.SaleDate,
                                      StoreName = store.Name,
                                      EmployeeName = employee.FullName,
                                      ProductName = product.Name,
                                      Quantity = saleItem.Quantity,
                                      TotalAmount = saleItem.SalePrice * saleItem.Quantity
                                  }).ToList();

                gridSalesReport.ItemsSource = reportData;
            }
        }
        private void btnGenerateComparisonReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate1 = dpStartDate1.SelectedDate ?? DateTime.Now;
            DateTime endDate1 = dpEndDate1.SelectedDate ?? DateTime.Now;
            DateTime startDate2 = dpStartDate2.SelectedDate ?? DateTime.Now;
            DateTime endDate2 = dpEndDate2.SelectedDate ?? DateTime.Now;

            using (var context = new ModelStore())
            {
                var reportData = (from sale in context.Sales
                                  where (sale.SaleDate >= startDate1 && sale.SaleDate <= endDate1) ||
                                        (sale.SaleDate >= startDate2 && sale.SaleDate <= endDate2)
                                  join saleItem in context.SaleItems on sale.SaleID equals saleItem.SaleID
                                  join product in context.Products on saleItem.ProductID equals product.ProductID
                                  group saleItem by product.Name into g
                                  select new
                                  {
                                      ProductName = g.Key,
                                      SalesPeriod1 = g.Where(x => x.Sales.SaleDate >= startDate1 && x.Sales.SaleDate <= endDate1).Sum(x => x.Quantity),
                                      SalesPeriod2 = g.Where(x => x.Sales.SaleDate >= startDate2 && x.Sales.SaleDate <= endDate2).Sum(x => x.Quantity)
                                  }).ToList();

                gridComparisonReport.ItemsSource = reportData;
            }
        }

        private void gridComparisonReport_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in gridComparisonReport.Items)
            {
                var row = gridComparisonReport.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var dataRow = (dynamic)item; 
                    if (dataRow.SalesPeriod1 > dataRow.SalesPeriod2)
                    {
                        row.Background = new SolidColorBrush(Colors.Red);
                    }
                    else if (dataRow.SalesPeriod1 < dataRow.SalesPeriod2)
                    {
                        row.Background = new SolidColorBrush(Colors.Green);
                    }
                }
            }
        }


        private void HighlightRows()
        {
            foreach (var item in gridComparisonReport.Items)
            {
                var row = gridComparisonReport.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null) 
                {
                    var dataRow = (dynamic)item; 
                    if (dataRow.SalesPeriod1 > dataRow.SalesPeriod2)
                    {
                        row.Background = new SolidColorBrush(Colors.Red);
                    }
                    else if (dataRow.SalesPeriod1 < dataRow.SalesPeriod2)
                    {
                        row.Background = new SolidColorBrush(Colors.Green);
                    }
                }
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dpSearchStartDate.SelectedDate ?? DateTime.Now;
            DateTime endDate = dpSearchEndDate.SelectedDate ?? DateTime.Now;

            using (var context = new ModelStore())
            {
                var results = (from sale in context.Sales
                               where sale.SaleDate >= startDate && sale.SaleDate <= endDate
                               join saleItem in context.SaleItems on sale.SaleID equals saleItem.SaleID
                               join product in context.Products on saleItem.ProductID equals product.ProductID
                               group new { saleItem, product } by product.Name into g
                               let profit = g.Sum(x => (x.saleItem.SalePrice - x.product.RetailPrice) * x.saleItem.Quantity)
                               orderby profit descending
                               select new
                               {
                                   ProductName = g.Key,
                                   Profit = profit
                               }).ToList();

                gridSearchResults.ItemsSource = results;
            }
        }


        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Products)cmbProducts.SelectedItem;

            if (selectedProduct != null)
            {
                decimal markupPercentage;


                switch (selectedProduct.Brand)
                {
                    case "Apple":
                        markupPercentage = 0.15m;
                        break;
                    case "Lenovo":
                        markupPercentage = 0.10m;
                        break;
                    case "Samsung":
                        markupPercentage = 0.05m;
                        break;
                    case "LG":
                        markupPercentage = 0.02m;
                        break;
                    case "Panasonic":
                        markupPercentage = 0.07m;
                        break;
                    default:
                        markupPercentage = 0.00m;
                        break;
                }

                decimal salePriceWithMarkup = selectedProduct.RetailPrice * (1 + markupPercentage);

                var saleItem = new SaleItems
                {
                    ProductID = selectedProduct.ProductID,
                    Quantity = 1,
                    SalePrice = salePriceWithMarkup
                };

                saleItems.Add(saleItem);
                gridSaleItems.ItemsSource = null;
                gridSaleItems.ItemsSource = saleItems;
                CalculateTotalAmount();
            }
        }

        private void cmbMainWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadWarehouseProducts();
        }

        private void LoadWarehouseProducts()
        {
            if (selectedWarehouse != null)
            {
                using (var context = new ModelStore())
                {
                    var products = context.Products
                        .Join(context.Warehouses.Where(w => w.WarehouseID == selectedWarehouse.WarehouseID),
                            p => p.ProductID,
                            w => w.ProductID,
                            (p, w) => new
                            {
                                p.Code,
                                p.Name,
                                MinQuantity = w.MinQuantity,
                                MaxQuantity = w.MaxQuantity,
                     
                            }).ToList();

                    gridWarehouseProducts.ItemsSource = products;
                }
            }
        }


        private void btnTransferToStore_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("В разработке :)");
        }


       

        private void cmbCustomer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
