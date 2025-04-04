-- Создание таблицы Магазины
CREATE TABLE Stores (
    StoreID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL
);

-- Создание таблицы Товары
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(50) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Brand NVARCHAR(100) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    Image NVARCHAR(255),  -- Путь к изображению
    RetailPrice DECIMAL(10, 2) NOT NULL
);

-- Создание таблицы Поставщики
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(255) NOT NULL
);

-- Создание таблицы Склад
CREATE TABLE Warehouses (
    WarehouseID INT PRIMARY KEY IDENTITY(1,1),
    StoreID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Создание таблицы Сотрудники
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100) NOT NULL,
    StoreID INT NOT NULL,
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID)
);

-- Создание таблицы Клиенты
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(255) NOT NULL
);

-- Создание таблицы Продажи
CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    StoreID INT NOT NULL,
    EmployeeID INT NOT NULL,
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- Создание таблицы Товары в Продажах
CREATE TABLE SaleItems (
    SaleItemID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    SalePrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Создание таблицы Инвентаризация
CREATE TABLE Inventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    WarehouseID INT NOT NULL,
    ProductID INT NOT NULL,
    RecordedQuantity INT NOT NULL,
    ActualQuantity INT NOT NULL,
    Difference INT NOT NULL,
    FOREIGN KEY (WarehouseID) REFERENCES Warehouses(WarehouseID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Создание таблицы Скидки
CREATE TABLE Discounts (
    DiscountID INT PRIMARY KEY IDENTITY(1,1),
    ThresholdAmount DECIMAL(10, 2) NOT NULL,
    DiscountPercentage DECIMAL(5, 2) NOT NULL
);





-- Вставка данных в таблицу Магазины
INSERT INTO Stores (Name, Address) VALUES
('Супермаркет "Все для дома"', 'ул. Ленина, д. 10, г. Москва'),
('Магазин "Электроника"', 'пр. Мира, д. 5, г. Санкт-Петербург'),
('Продуктовый магазин "Вкусно и точка"', 'ул. Пушкина, д. 20, г. Новосибирск');

-- Вставка данных в таблицу Товары
INSERT INTO Products (Code, Name, Brand, Category, Image, RetailPrice) VALUES
('P001', 'Смартфон Galaxy S21', 'Samsung', 'Электроника', 'images/samsung_s21.jpg', 79999.99),
('P002', 'Ноутбук ThinkPad T14', 'Lenovo', 'Компьютеры', 'images/lenovo_thinkpad.jpg', 69999.50),
('P003', 'Смартфон iPhone 13', 'Apple', 'Электроника', 'images/iphone_13.jpg', 99999.00),
('P004', 'Телевизор OLED 55"', 'LG', 'Бытовая техника', 'images/lg_oled_tv.jpg', 129999.99),
('P005', 'Микроволновая печь', 'Panasonic', 'Бытовая техника', 'images/panasonic_microwave.jpg', 8999.99);

-- Вставка данных в таблицу Поставщики
INSERT INTO Suppliers (Name, ContactInfo) VALUES
('ООО "ТехноПоставка"', '+7 (495) 123-45-67'),
('ЗАО "Электроника-2000"', '+7 (812) 987-65-43'),
('ИП Иванов И.И.', '+7 (913) 555-12-34');

-- Вставка данных в таблицу Склад
INSERT INTO Warehouses (StoreID, ProductID, Quantity) VALUES
(1, 1, 25),
(1, 2, 15),
(1, 4, 10),
(2, 3, 20),
(2, 5, 30),
(3, 2, 8),
(3, 4, 5);

-- Вставка данных в таблицу Сотрудники
INSERT INTO Employees (FullName, Position, StoreID) VALUES
('Иванов Сергей Александрович', 'Менеджер по продажам', 1),
('Петрова Мария Игоревна', 'Кассир', 1),
('Сидоров Алексей Викторович', 'Продавец-консультант', 2),
('Кузнецова Анна Сергеевна', 'Кассир', 2),
('Лебедева Ольга Павловна', 'Продавец', 3);

-- Вставка данных в таблицу Клиенты
INSERT INTO Customers (Name, ContactInfo) VALUES
('Алексей Смирнов', '+7 (900) 123-45-67'),
('Екатерина Петрова', '+7 (916) 234-56-78'),
('Дмитрий Кузнецов', '+7 (918) 345-67-89'),
('Мария Степанова', '+7 (910) 456-78-90'),
('Ольга Михайлова', '+7 (911) 567-89-01');

-- Вставка данных в таблицу Продажи
INSERT INTO Sales (CustomerID, StoreID, EmployeeID, TotalAmount) VALUES
(1, 1, 1, 79999.99),
(2, 2, 3, 99999.00),
(3, 1, 2, 8999.99),
(4, 3, 5, 69999.50),
(5, 2, 4, 129999.99);

-- Вставка данных в таблицу Товары в Продажах
INSERT INTO SaleItems (SaleID, ProductID, Quantity, SalePrice) VALUES
(1, 1, 1, 79999.99),
(2, 3, 1, 99999.00),
(3, 5, 1, 8999.99),
(4, 2, 1, 69999.50),
(5, 4, 1, 129999.99);

-- Вставка нескольких записей в таблицу Инвентаризация
INSERT INTO Inventory (WarehouseID, ProductID, RecordedQuantity, ActualQuantity, Difference)
VALUES
    (1, 1, 10, 15, 5),  -- Warehouse 1, Product 101
    (1, 2, 20, 30, 10),  -- Warehouse 1, Product 102
    (2, 3, 30, 35, 5),  -- Warehouse 2, Product 201
    (2, 4, 25, 30, 5),  -- Warehouse 2, Product 202
    (3, 5, 40, 41, 1);  -- Warehouse 3, Product 301

-- Вставка данных в таблицу Скидки
INSERT INTO Discounts (ThresholdAmount, DiscountPercentage) VALUES
(1000.00, 5.00),
(5000.00, 10.00),
(10000.00, 15.00);
