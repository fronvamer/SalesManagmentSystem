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



-- Вставка данных в таблицу Магазины
INSERT INTO Stores (Name, Address) VALUES 
('Гипермаркет "Продукты"', 'Проспект Мира, 10'), 
('Универсам "Семья"', 'Улица Советская, 15');

-- Вставка данных в таблицу Поставщики
INSERT INTO Suppliers (Name, ContactInfo) VALUES 
('АгроПоставка', 'Телефон: +1 (999) 111-11-11'), 
('ПостачальникНапитков', 'Телефон: +1 (999) 222-22-22'),
('ЗерноПлюс', 'Телефон: +1 (999) 333-33-33'), 
('Молочный Гуру', 'Телефон: +1 (999) 444-44-44');

-- Вставка данных в таблицу Товары
INSERT INTO Products (Code, Name, Brand, Category, Image, RetailPrice) VALUES 
('FRU001', 'Яблоки', 'Фермер', 'Овощи и фрукты', NULL, 50.00), 
('FRU002', 'Бананы', 'Тропик', 'Овощи и фрукты', NULL, 60.00),
('DRN001', 'Минеральная вода', 'Аква', 'Напитки', NULL, 35.00),
('BRD001', 'Хлеб', 'Зерно', 'Хлебобулочные', NULL, 25.00),
('MLK001', 'Молоко', 'Молочный путь', 'Молочные продукты', NULL, 45.00),
('CND001', 'Кофе', 'Арабика', 'Напитки', NULL, 150.00),
('SNK001', 'Чипсы', 'Соленый', 'Закуски', NULL, 75.00),
('CNF001', 'Шоколад', 'Сладости', 'Сладости', NULL, 100.00);

-- Вставка данных в таблицу Склад
INSERT INTO Warehouses (StoreID, ProductID, Quantity) VALUES 
(1, 1, 100), 
(1, 2, 120), 
(1, 3, 150), 
(1, 4, 80), 
(2, 1, 60), 
(2, 5, 40), 
(2, 6, 30), 
(2, 7, 90);

-- Вставка данных в таблицу Сотрудники
INSERT INTO Employees (FullName, Position, StoreID) VALUES 
('Алексей Смирнов', 'Управляющий', 1), 
('Марина Петрова', 'Кассир', 1),
('Дмитрий Ковалев', 'Менеджер по продажам', 1),
('Ольга Васильева', 'Кассир', 2),
('Игорь Федоров', 'Продавец', 2);

-- Вставка данных в таблицу Клиенты
INSERT INTO Customers (Name, ContactInfo) VALUES 
('Анна Сергеева', 'Телефон: +1 (999) 555-55-55'), 
('Сергей Ильин', 'Телефон: +1 (999) 666-66-66'),
('Елена Сидорова', 'Телефон: +1 (999) 777-77-77'),
('Александр Морозов', 'Телефон: +1 (999) 888-88-88');

-- Вставка данных в таблицу Продажи
INSERT INTO Sales (CustomerID, StoreID, EmployeeID, TotalAmount) VALUES 
(1, 1, 1, 345.00), 
(2, 2, 4, 275.00),
(3, 1, 3, 190.00), 
(4, 2, 5, 120.00);

-- Вставка данных в таблицу Товары в Продажах
INSERT INTO SaleItems (SaleID, ProductID, Quantity, SalePrice) VALUES 
(1, 1, 3, 50.00),  -- Яблоки
(1, 2, 2, 60.00),  -- Бананы
(1, 3, 5, 35.00),  -- Минеральная вода
(2, 7, 4, 75.00),  -- Чипсы
(2, 5, 2, 45.00),  -- Молоко
(3, 1, 1, 50.00),  -- Яблоки
(3, 4, 2, 25.00),  -- Хлеб
(4, 6, 1, 150.00); -- Кофе

-- Вставка данных в таблицу Инвентаризация
INSERT INTO Inventory (WarehouseID, ProductID, RecordedQuantity, ActualQuantity, Difference) VALUES 
(1, 1, 100, 98, -2), 
(1, 2, 120, 120, 0), 
(1, 3, 150, 145, -5), 
(1, 4, 80, 80, 0), 
(2, 1, 60, 60, 0), 
(2, 5, 40, 39, -1), 
(2, 6, 30, 30, 0), 
(2, 7, 90, 90, 0);

-- Вставка данных в таблицу Скидки
INSERT INTO Discounts (ThresholdAmount, DiscountPercentage) VALUES 
(100.00, 5.00), 
(200.00, 10.00), 
(300.00, 15.00);
-- Вставка данных о продажах за 4 апреля
INSERT INTO Sales (CustomerID, StoreID, EmployeeID, TotalAmount) VALUES 
(1, 1, 1, 289.00),   -- Клиент 1, Магазин 1, Сотрудник 1
(2, 2, 4, 345.50),   -- Клиент 2, Магазин 2, Сотрудник 4
(3, 1, 3, 112.00),   -- Клиент 3, Магазин 1, Сотрудник 3
(4, 2, 5, 215.00);   -- Клиент 4, Магазин 2, Сотрудник 5

-- Вставка данных о товарах, проданных 4 апреля
INSERT INTO SaleItems (SaleID, ProductID, Quantity, SalePrice) VALUES 
(5, 1, 4, 50.00),  -- Продажа Яблок (4 шт)
(5, 3, 2, 35.00),  -- Продажа Минеральной воды (2 шт)
(6, 2, 3, 60.00),  -- Продажа Бананов (3 шт)
(6, 7, 1, 75.00),  -- Продажа Чипсов (1 шт)
(7, 3, 2, 50.00),  -- Продажа Яблок (2 шт)
(7, 4, 1, 25.00),  -- Продажа Хлеба (1 шт)
(8, 6, 1, 150.00); -- Продажа Кофе (1 шт)


-- Вставка данных о продажах за 5 апреля
INSERT INTO Sales (CustomerID, StoreID, EmployeeID, TotalAmount) VALUES 
(1, 1, 2, 320.00),  -- Клиент 1, Магазин 1, Сотрудник 2
(2, 2, 3, 150.00),  -- Клиент 2, Магазин 2, Сотрудник 3
(3, 1, 1, 450.00),  -- Клиент 3, Магазин 1, Сотрудник 1
(4, 2, 5, 275.00);  -- Клиент 4, Магазин 2, Сотрудник 5

-- Вставка данных о товарах, проданных 5 апреля
INSERT INTO SaleItems (SaleID, ProductID, Quantity, SalePrice) VALUES 
(9, 1, 5, 60.00),  -- Продажа Яблок (5 шт)
(9, 2, 3, 40.00),  -- Продажа Груш (3 шт)
(10, 3, 2, 50.00), -- Продажа Минеральной воды (2 шт)
(10, 4, 3, 30.00), -- Продажа Хлеба (3 шт)
(11, 5, 1, 100.00),-- Продажа Молока (1 шт)
(12, 7, 2, 70.00); -- Продажа Чипсов (2 шт)
