CREATE DATABASE TECHNOVA;
GO
USE TECHNOVA;
GO

-- Tạo bảng Categories
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) NOT NULL,
    CategoryName NVARCHAR(50) NOT NULL,
    Alias NVARCHAR(50) NULL,
    Description NVARCHAR(MAX) NULL,
    Image NVARCHAR(50) NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryID)
);

-- Tạo bảng Suppliers
CREATE TABLE Suppliers (
    SupplierID NVARCHAR(50) PRIMARY KEY,
    SupplierName VARCHAR(100),
    ContactEmail VARCHAR(100),
    Address VARCHAR(200)
);

-- Tạo bảng Employees
CREATE TABLE Employees (
    EmployeeID NVARCHAR(50) NOT NULL,
    FullName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NULL,
    CONSTRAINT PK_Employees PRIMARY KEY (EmployeeID)
);

-- Tạo bảng Departments
CREATE TABLE Departments (
    DepartmentID VARCHAR(7) NOT NULL,
    DepartmentName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Departments PRIMARY KEY (DepartmentID)
);

-- Tạo bảng Pages
CREATE TABLE Pages (
    PageID INT IDENTITY(1,1) NOT NULL,
    PageName NVARCHAR(50) NOT NULL,
    URL NVARCHAR(250) NOT NULL,
    CONSTRAINT PK_Pages PRIMARY KEY (PageID)
);

-- Tạo bảng OrderStatus
CREATE TABLE OrderStatus (
    StatusID INT NOT NULL,
    StatusName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NULL,
    CONSTRAINT PK_OrderStatus PRIMARY KEY (StatusID)
);

-- Tạo bảng Customers
CREATE TABLE Customers (
    CustomerID NVARCHAR(20) NOT NULL,
    Password NVARCHAR(50) NULL,
    FullName NVARCHAR(50) NOT NULL,
    Gender BIT NULL,
    BirthDate DATETIME NULL,
    Address NVARCHAR(60) NULL,
    Phone NVARCHAR(24) NULL,
    Email NVARCHAR(50) NULL,
	FacebookId NVARCHAR(100) NULL,
    Image NVARCHAR(50) NULL,
    IsActive BIT NOT NULL,
    Role INT NOT NULL,
    RandomKey VARCHAR(50) NULL,
    CONSTRAINT PK_Customers PRIMARY KEY (CustomerID)
);

-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) NOT NULL,
    ProductName NVARCHAR(50) NOT NULL,
    Alias NVARCHAR(50) NULL,
    CategoryID INT NOT NULL,
    UnitDescription NVARCHAR(50) NULL,
    UnitPrice FLOAT NULL,
    Image NVARCHAR(50) NULL,
    ManufactureDate DATETIME NOT NULL,
    Discount FLOAT NOT NULL,
    ViewCount INT NOT NULL,
    Description NVARCHAR(MAX) NULL,
    SupplierID NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Products PRIMARY KEY (ProductID)
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) NOT NULL,
    CustomerID NVARCHAR(20) NOT NULL,
    OrderDate DATETIME NOT NULL,
    EstimatedDate DATETIME NULL,
    DeliveryDate DATETIME NULL,
    FullName NVARCHAR(50) NULL,
    Address NVARCHAR(60) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    ShippingMethod NVARCHAR(50) NOT NULL,
    ShippingFee FLOAT NOT NULL,
    OrderStatus INT NOT NULL,
    EmployeeID NVARCHAR(50) NULL,
	Phone NVARCHAR(24) NULL,
    Notes NVARCHAR(50) NULL,
    DiscountID INT NULL,
    CONSTRAINT PK_Orders PRIMARY KEY (OrderID)
);

-- Tạo bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) NOT NULL,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    UnitPrice FLOAT NOT NULL,
    Quantity INT NOT NULL,
    Discount FLOAT NOT NULL,
    CONSTRAINT PK_OrderDetails PRIMARY KEY (OrderDetailID)
);

-- Tạo bảng Promotions
CREATE TABLE Promotions (
    PromotionID INT IDENTITY(1,1) NOT NULL,
    CustomerID NVARCHAR(20) NULL,
    ProductID INT NOT NULL,
    FullName NVARCHAR(50) NULL,
    Email NVARCHAR(50) NOT NULL,
    SentDate DATETIME NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Promotions PRIMARY KEY (PromotionID)
);

-- Tạo bảng Topics
CREATE TABLE Topics (
    TopicID INT IDENTITY(1,1) NOT NULL,
    TopicName NVARCHAR(50) NULL,
    EmployeeID NVARCHAR(50) NULL,
    CONSTRAINT PK_Topics PRIMARY KEY (TopicID)
);

-- Tạo bảng Feedback
CREATE TABLE Feedback (
    FeedbackID NVARCHAR(50) NOT NULL,
    TopicID INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    FeedbackDate DATE NOT NULL,
    FullName NVARCHAR(50) NULL,
    Email NVARCHAR(50) NULL,
    Phone NVARCHAR(50) NULL,
    RequiresResponse BIT NOT NULL,
    Response NVARCHAR(50) NULL,
    ResponseDate DATE NULL,
    CONSTRAINT PK_Feedback PRIMARY KEY (FeedbackID)
);

-- Tạo bảng QandA
CREATE TABLE QandA (
    QandAID INT NOT NULL,
    Question NVARCHAR(50) NOT NULL,
    Answer NVARCHAR(50) NOT NULL,
    SubmitDate DATE NOT NULL,
    EmployeeID NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_QandA PRIMARY KEY (QandAID)
);

-- Tạo bảng Assignments
CREATE TABLE Assignments (
    AssignmentID INT IDENTITY(1,1) NOT NULL,
    EmployeeID NVARCHAR(50) NOT NULL,
    DepartmentID VARCHAR(7) NOT NULL,
    AssignmentDate DATETIME NULL,
    IsActive BIT NULL,
    CONSTRAINT PK_Assignments PRIMARY KEY (AssignmentID)
);

-- Tạo bảng Permissions
CREATE TABLE Permissions (
    PermissionID INT IDENTITY(1,1) NOT NULL,
    DepartmentID VARCHAR(7) NULL,
    PageID INT NULL,
    CanAdd BIT NOT NULL,
    CanEdit BIT NOT NULL,
    CanDelete BIT NOT NULL,
    CanView BIT NOT NULL,
    CONSTRAINT PK_Permissions PRIMARY KEY (PermissionID)
);

-- Tạo bảng Discounts
CREATE TABLE Discounts (
    DiscountID INT IDENTITY(1,1) NOT NULL,
    DiscountCode NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    DiscountType NVARCHAR(20) NOT NULL, -- 'Percentage' or 'Fixed Amount'
    DiscountValue FLOAT NOT NULL, -- Nếu là 'Percentage', giá trị là phần trăm (0-100). Nếu là 'Fixed Amount', giá trị là số tiền giảm.
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NULL,
    IsActive BIT NOT NULL, -- Đánh dấu nếu giảm giá còn hiệu lực
    CONSTRAINT PK_Discounts PRIMARY KEY (DiscountID)
);

-- Thêm khóa ngoại vào bảng Orders
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees (EmployeeID);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_OrderStatus FOREIGN KEY (OrderStatus) REFERENCES OrderStatus (StatusID);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Discounts FOREIGN KEY (DiscountID) REFERENCES Discounts (DiscountID);

-- Thêm khóa ngoại vào bảng Products
ALTER TABLE Products
ADD CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID);

ALTER TABLE Products
ADD CONSTRAINT FK_Products_Suppliers FOREIGN KEY (SupplierID) REFERENCES Suppliers (SupplierID);

-- Thêm khóa ngoại vào bảng OrderDetails
ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES Orders (OrderID);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductID) REFERENCES Products (ProductID);

-- Thêm khóa ngoại vào bảng Promotions
ALTER TABLE Promotions
ADD CONSTRAINT FK_Promotions_Customers FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID);

ALTER TABLE Promotions
ADD CONSTRAINT FK_Promotions_Products FOREIGN KEY (ProductID) REFERENCES Products (ProductID);

-- Thêm khóa ngoại vào bảng Topics
ALTER TABLE Topics
ADD CONSTRAINT FK_Topics_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees (EmployeeID);

-- Thêm khóa ngoại vào bảng Feedback
ALTER TABLE Feedback
ADD CONSTRAINT FK_Feedback_Topics FOREIGN KEY (TopicID) REFERENCES Topics (TopicID);

-- Thêm khóa ngoại vào bảng QandA
ALTER TABLE QandA
ADD CONSTRAINT FK_QandA_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees (EmployeeID);

-- Thêm khóa ngoại vào bảng Assignments
ALTER TABLE Assignments
ADD CONSTRAINT FK_Assignments_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees (EmployeeID);

ALTER TABLE Assignments
ADD CONSTRAINT FK_Assignments_Departments FOREIGN KEY (DepartmentID) REFERENCES Departments (DepartmentID);

-- Thêm khóa ngoại vào bảng Permissions
ALTER TABLE Permissions
ADD CONSTRAINT FK_Permissions_Departments FOREIGN KEY (DepartmentID) REFERENCES Departments (DepartmentID);

ALTER TABLE Permissions
ADD CONSTRAINT FK_Permissions_Pages FOREIGN KEY (PageID) REFERENCES Pages (PageID);


-- Thêm 10 categories vào bảng Categories
INSERT INTO Categories (CategoryName, Alias, Description, Image)
VALUES
('Laptops', 'laptop', 'Various laptop models from top brands', null),
('Smartphones', 'smartphone', 'Smartphones with latest features', null),
('Tablets', 'tablet', 'Tablets with great performance and portability', null),
('Accessories', 'accessories', 'Accessories for all tech products', null),
('Headphones', 'headphones', 'Noise-canceling and wireless headphones', null),
('Cameras', 'cameras', 'High-quality cameras for photography', null),
('Smartwatches', 'smartwatch', 'Smartwatches with health tracking features', null),
('Printers', 'printers', 'Printers for home and office use', null),
('Monitors', 'monitors', 'Monitors for gaming and office work', null),
('Gaming', 'gaming', 'Gaming consoles and accessories', null);

-- Thêm 50 sản phẩm vào bảng Products

ALTER TABLE Products
ALTER COLUMN Image VARCHAR(MAX);

INSERT INTO Suppliers (SupplierID, SupplierName, ContactEmail, Address)
VALUES
('supplier-01', 'Apple Inc.', 'contact@apple.com', 'Cupertino, CA, USA'),
('supplier-02', 'Samsung Electronics', 'support@samsung.com', 'Suwon, South Korea'),
('supplier-03', 'Google LLC', 'sales@google.com', 'Mountain View, CA, USA'),
('supplier-04', 'OnePlus', 'info@oneplus.com', 'Shenzhen, China'),
('supplier-05', 'Xiaomi Corporation', 'support@xiaomi.com', 'Beijing, China'),
('supplier-06', 'Microsoft Corporation', 'surface@microsoft.com', 'Redmond, WA, USA'),
('supplier-07', 'Lenovo Group', 'support@lenovo.com', 'Beijing, China'),
('supplier-08', 'Huawei Technologies', 'info@huawei.com', 'Shenzhen, China'),
('supplier-09', 'Dell Technologies', 'support@dell.com', 'Round Rock, TX, USA'),
('supplier-10', 'HP Inc.', 'sales@hp.com', 'Palo Alto, CA, USA'),
('supplier-11', 'Razer Inc.', 'info@razer.com', 'Irvine, CA, USA'),
('supplier-12', 'ASUS', 'support@asus.com', 'Taipei, Taiwan'),
('supplier-13', 'Sony Corporation', 'audio@sony.com', 'Tokyo, Japan'),
('supplier-14', 'Bose Corporation', 'support@bose.com', 'Framingham, MA, USA'),
('supplier-15', 'Sennheiser', 'info@sennheiser.com', 'Wedemark, Germany'),
('supplier-16', 'JBL (Harman)', 'support@jbl.com', 'Los Angeles, CA, USA'),
('supplier-17', 'Garmin Ltd.', 'sales@garmin.com', 'Olathe, KS, USA'),
('supplier-18', 'Fitbit Inc.', 'info@fitbit.com', 'San Francisco, CA, USA'),
('supplier-19', 'Amazfit (Zepp Health)', 'support@amazfit.com', 'Hefei, China'),
('supplier-20', 'Sony Interactive Entertainment', 'playstation@sony.com', 'San Mateo, CA, USA'),
('supplier-21', 'Microsoft Xbox', 'xbox@microsoft.com', 'Redmond, WA, USA'),
('supplier-22', 'Meta Platforms', 'vr@meta.com', 'Menlo Park, CA, USA'),
('supplier-23', 'Roku Inc.', 'support@roku.com', 'San Jose, CA, USA');

-- Thêm nhà cung cấp cho các sản phẩm
INSERT INTO Suppliers (SupplierID, SupplierName, ContactEmail, Address)
VALUES
('supplier-24', 'LG Electronics', 'support@lge.com', 'Seoul, South Korea'),
('supplier-25', 'Epson America', 'support@epson.com', 'Los Alamitos, CA, USA'),
('supplier-26', 'BenQ', 'support@benq.com', 'Taipei, Taiwan'),
('supplier-27', 'Anker Innovations', 'support@anker.com', 'Shenzhen, China'),
('supplier-28', 'Seagate Technology', 'support@seagate.com', 'Shenzhen, China'),
('supplier-29', 'Western Digital', 'support@wd.com', 'San Jose, CA, USA'),
('supplier-30', 'Ring', 'support@ring.com', 'Santa Monica, CA, USA'),
('supplier-31', 'Arlo Technologies', 'support@arlo.com', 'Carlsbad, CA, USA'),
('supplier-32', 'OtterBox', 'support@otterbox.com', 'Fort Collins, CO, USA'),
('supplier-33', 'Anker Innovations', 'support@anker.com', 'Shenzhen, China'),
('supplier-34', 'Belkin', 'support@belkin.com', 'Playa Vista, CA, USA'),
('supplier-35', 'Jabra', 'support@jabra.com', 'Ballerup, Denmark');

-- Điện thoại
INSERT INTO Products (ProductName, Alias, CategoryID, UnitDescription, UnitPrice, Image, ManufactureDate, Discount, ViewCount, Description, SupplierID)
VALUES
('Apple iPhone 14', 'iphone-14', 2, '6.1-inch display, 128GB storage', 799.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746452863/26c4bfbc-9755-4191-9ffe-133fbd64bca2_gkyhgp.jpg', '2023-09-01', 10.0, 2500, 'Latest iPhone with A15 Bionic chip', 'supplier-01'),
('Samsung Galaxy S23', 'samsung-galaxy-s23', 2, '6.1-inch display, 128GB storage', 849.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746452992/e5726dbe-3a80-448b-89e5-4945f1868a4a_dolwtl.jpg', '2023-02-15', 8.0, 2300, 'Flagship smartphone from Samsung', 'supplier-02'),
('Google Pixel 7', 'google-pixel-7', 2, '6.3-inch display, 128GB storage', 599.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453076/ca2d3d3d-26ed-4519-9fad-f5691bfd566d_pmk9tu.jpg', '2023-10-01', 15.0, 1800, 'Android phone with clean UI and great camera', 'supplier-03'),
('OnePlus 11', 'oneplus-11', 2, '6.7-inch display, 256GB storage', 699.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453225/Smart_choice_for_all__gwx167.jpg', '2023-07-01', 12.0, 2000, 'Flagship killer with great value', 'supplier-04'),
('Xiaomi Mi 13 Pro', 'xiaomi-mi-13-pro', 2, '6.73-inch display, 256GB storage', 799.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453225/Xiaomi_Mi_13_Ultra_5G_512GB_16GB_Factory_Unlocked_ckrrne.jpg', '2023-08-01', 5.0, 2200, 'Premium flagship from Xiaomi', 'supplier-05'),

-- Máy tính bảng
('Apple iPad Pro 12.9-inch', 'ipad-pro-12-9', 3, '12.9-inch display, 256GB storage', 1099.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453490/The_Apple_iPad_Pro_gives_you_the_ultimate_iPad_g9ye5w.jpg', '2023-06-01', 10.0, 1800, 'Best tablet for productivity and creativity', 'supplier-01'),
('Samsung Galaxy Tab S8 Ultra', 'galaxy-tab-s8-ultra', 3, '14.6-inch display, 128GB storage', 1099.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453490/Galaxy_Tab_S8_Ultra_for_Designers_What_to_expect_from_Samsung_s_Next_Big_Thing_ccafh7.jpg', '2023-05-15', 8.0, 1600, 'Large screen tablet with top performance', 'supplier-02'),
('Microsoft Surface Pro 9', 'surface-pro-9', 3, '13-inch display, 128GB storage', 999.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453490/O_dispositivo_2-em-1_mais_flex%C3%ADvel_e_potente_vkqmfn.jpg', '2023-09-10', 7.0, 1500, 'Convertible laptop with tablet features', 'supplier-06'),
('Lenovo Tab P11 Pro', 'lenovo-tab-p11-pro', 3, '11.5-inch display, 128GB storage', 499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453490/5f4bd44a-3389-4759-ae45-674dc7f62180_hdjdik.jpg', '2023-04-01', 5.0, 1400, 'Affordable Android tablet', 'supplier-07'),
('Huawei MatePad Pro', 'huawei-matepad-pro', 3, '12.6-inch display, 128GB storage', 799.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746453490/Huawei_MatePad_Pro_features_a_tall_13_2-inch_wzbmpw.jpg', '2023-03-01', 10.0, 1200, 'Premium Android tablet', 'supplier-08'),

-- Laptop
('MacBook Air M2', 'macbook-air-m2', 1, '13.6-inch display, 256GB SSD', 1199.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746454054/The_Newest_MacBook_Air_2022_Mockups_10_eursxa.jpg', '2023-07-20', 5.0, 2500, 'Lightweight laptop with powerful M2 chip', 'supplier-01'),
('Dell XPS 13 Plus', 'dell-xps-13-plus', 1, '13.4-inch display, 256GB SSD', 1499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746454048/e9cf525a-2045-44a2-96a4-85bd34d39602_culrz0.jpg', '2023-08-10', 10.0, 2200, 'Sleek ultrabook with excellent performance', 'supplier-09'),
('HP Spectre x360', 'hp-spectre-x360', 1, '13.3-inch display, 512GB SSD', 1599.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746454049/HP_Spectre_x360_m9x9aq.jpg', '2023-09-01', 8.0, 2100, 'Premium 2-in-1 laptop with great battery life', 'supplier-10'),
('Razer Blade 15', 'razer-blade-15', 1, '15.6-inch display, 1TB SSD', 1999.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746454048/Razer_Blade_15_Basmodell_2020___Gaming_Laptop_med_qo9i94.jpg', '2023-06-15', 10.0, 2300, 'Powerful gaming laptop with RTX graphics', 'supplier-11'),
('Asus ZenBook 14', 'asus-zenbook-14', 1, '14-inch display, 512GB SSD', 1299.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746454048/a4a96006-14b7-4b38-b5cd-8f79eecafdd6_vhk5zw.jpg', '2023-07-01', 7.0, 2000, 'Ultraportable laptop for work and play', 'supplier-12'),

-- Tai nghe
('Sony WH-1000XM5', 'sony-wh-1000xm5', 5, 'Over-ear, noise-canceling, wireless headphones', 349.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455188/Sony_WH-1000XM5_Wireless_Noise-Canceling_hlfdlg.jpg', '2023-08-15', 12.0, 2200, 'Industry-leading noise-canceling headphones', 'supplier-13'),
('Apple AirPods 3', 'airpods-3', 5, 'In-ear, wireless earbuds', 179.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455186/Refurbished_Apple_AirPods_3rd_Gen_-_Grade_B_zpwhhq.jpg', '2023-06-10', 5.0, 2700, 'Affordable AirPods with great sound', 'supplier-17'),
('Bose QuietComfort 45', 'bose-quietcomfort-45', 5, 'Over-ear, noise-canceling, wireless headphones', 329.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455185/Bluetooth_Over_Ear_Headphones_with_Up_to_24_Hours_lduhgv.jpg', '2023-05-20', 10.0, 1500, 'Comfortable noise-canceling headphones', 'supplier-14'),
('Sennheiser Momentum 4', 'sennheiser-momentum-4', 5, 'Over-ear, noise-canceling, wireless headphones', 379.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455185/Maximum_audio_resolution_with_Sennheiser_Signature_je22cm.jpg', '2023-04-25', 7.0, 1200, 'Premium noise-canceling headphones', 'supplier-15'),
('JBL Live 660NC', 'jbl-live-660nc', 5, 'Over-ear, wireless headphones', 179.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455183/JBL_Live_660NC___Wireless_over-ear_NC_ks8odb.jpg', '2023-02-01', 10.0, 1000, 'Affordable wireless headphones with noise cancellation', 'supplier-16'),

-- Đồng hồ thông minh
('Apple Watch Series 8', 'apple-watch-series-8', 7, 'Smartwatch with ECG and fitness tracking', 399.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455729/Amazon_com__Apple_Watch_Series_8_GPS_41mm_Smart_ut1dld.jpg', '2023-09-01', 10.0, 2500, 'Premium smartwatch with health features', 'supplier-01'),
('Samsung Galaxy Watch 5 Pro', 'galaxy-watch-5-pro', 7, 'Smartwatch with GPS and fitness tracking', 499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455729/The_Official_Samsung_Galaxy_Watch_5_40mm_Model_qzy7ui.jpg', '2023-08-10', 8.0, 2200, 'Rugged smartwatch with long battery life', 'supplier-02'),
('Garmin Venu 2', 'garmin-venu-2', 7, 'Smartwatch with advanced fitness features', 399.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455729/PRICES_MAY_VARY__22mm_Compatible_Models_Wanme_merors.jpg', '2023-07-20', 15.0, 2000, 'Fitness-focused smartwatch with heart rate tracking', 'supplier-17'),
('Fitbit Sense 2', 'fitbit-sense-2', 7, 'Smartwatch with health and fitness tracking', 299.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455729/a47bc3bf-0e84-4019-adac-d5bc1dbd484f_jnygez.jpg', '2023-06-01', 10.0, 1900, 'Health-focused smartwatch with stress tracking', 'supplier-18'),
('Amazfit GTR 4', 'amazfit-gtr-4', 7, 'Smartwatch with AMOLED display', 229.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746455729/Amazfit_GTR_4_Racetrack_Grey_Smartwatch_zmhh0j.jpg', '2023-05-10', 12.0, 1700, 'Affordable smartwatch with long battery life', 'supplier-19'),

-- Các sản phẩm khác
('Sony PlayStation 5', 'ps5', 10, 'Gaming console with 4K UHD', 499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456177/Sony_PlayStation_5_-_Pro___bol_d1l6a3.jpg', '2023-05-01', 10.0, 3000, 'Next-gen gaming console with amazing graphics', 'supplier-20'),
('Xbox Series X', 'xbox-series-x', 10, 'Gaming console with 4K UHD, 1TB storage', 499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456178/cdb0709b-5c45-49b4-99b8-5543f865b414_ussf62.jpg', '2023-06-10', 12.0, 3500, 'Powerful gaming console for next-gen games', 'supplier-21'),
('Oculus Quest 2', 'oculus-quest-2', 10, 'Virtual Reality headset', 299.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456176/Oculus_quest_2_advanced_all-in-one_virtual_reality_vflqlu.jpg', '2023-07-10', 8.0, 2700, 'Wireless VR headset for gaming', 'supplier-22'),
('Roku Ultra', 'roku-ultra', 10, 'Streaming media player with 4K UHD', 99.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456177/The_ultimate_streaming_device_just_got_even_b9isvy.jpg', '2023-05-15', 5.0, 2500, 'Streaming device with 4K and Dolby Vision', 'supplier-23'),
('Google Chromecast with Google TV', 'chromecast-google-tv', 10, 'Streaming device with 4K UHD', 49.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456177/Check_out_this_listing_I_just_found_on_Poshmark_hcsskr.jpg', '2023-04-20', 12.0, 2200, 'Affordable 4K streaming device', 'supplier-03');

-- Thêm 20 sản phẩm khác vào bảng Products

-- Máy tính để bàn
INSERT INTO Products (ProductName, Alias, CategoryID, UnitDescription, UnitPrice, Image, ManufactureDate, Discount, ViewCount, Description, SupplierID)
VALUES
('Apple iMac 24-inch', 'imac-24-inch', 1, '24-inch display, 256GB storage', 1299.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456704/Apple_iMac_with_Retina_4_5K_Display_24-inch_M3_ffglda.jpg', '2023-06-01', 10.0, 2000, 'All-in-one desktop with M1 chip', 'supplier-01'),
('Dell Inspiron Desktop', 'dell-inspiron-desktop', 1, 'Intel Core i5, 8GB RAM, 512GB SSD', 599.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456704/Today_s_deal__2017_Dell_Inspiron_High_Performance_jlambp.jpg', '2023-05-10', 5.0, 1500, 'Affordable desktop for home use', 'supplier-09'),
('HP Pavilion Desktop', 'hp-pavilion-desktop', 1, 'Intel Core i7, 16GB RAM, 1TB HDD', 799.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746456704/f7748707-ea41-4d1a-b452-c141e9ea0b51_t5dove.jpg', '2023-07-15', 8.0, 1800, 'Mid-range desktop with great performance', 'supplier-10'),

-- Màn hình máy tính
('LG UltraWide Monitor', 'lg-ultrawide-monitor', 9, '34-inch, 144Hz, 1440p', 399.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457041/LG_34UC97_IPS_Curved_Ultrawide_Monitor_hgjbie.jpg', '2023-08-01', 7.0, 1600, 'Ultra-wide monitor for productivity and gaming', 'supplier-24'),
('Samsung Odyssey G7', 'samsung-odyssey-g7', 9, '27-inch, 240Hz, 1440p', 599.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457041/Monitor_Gamer_Samsung_Odyssey_G7_26_9_QLED_Curvo_qcn7ri.jpg', '2023-06-25', 10.0, 2000, 'Curved gaming monitor with fast refresh rate', 'supplier-02'),
('Dell UltraSharp U2720Q', 'dell-ultrasharp-u2720q', 9, '27-inch, 4K UHD, IPS', 499.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457042/Detailed_Review_and_Buying_Guide_of_Dell_gbj6i1.jpg', '2023-07-10', 5.0, 1200, '4K UHD monitor for designers and professionals', 'supplier-09'),

-- Máy chiếu
('Epson Home Cinema 2150', 'epson-home-cinema-2150', 8, '1080p, wireless projector', 799.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457718/371df4b0-8369-4f65-9ae0-7052a100134c_j9qgeb.jpg', '2023-05-20', 10.0, 1000, 'Full HD home theater projector', 'supplier-25'),
('BenQ TK800M', 'benq-tk800m', 8, '4K UHD, 3000 lumens', 1199.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457715/5a892b74-c430-4a39-9a7d-3b5a92295beb_ck1ft7.jpg', '2023-04-01', 12.0, 1100, '4K projector for movies and gaming', 'supplier-26'),
('Anker Nebula Capsule', 'anker-nebula-capsule', 8, 'HD, portable projector', 359.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457714/AmazonSmile__Anker_NEBULA_Capsule_II_Smart_rw1lli.jpg', '2023-06-15', 8.0, 1400, 'Portable mini projector with Android OS', 'supplier-27'),

-- Thiết bị lưu trữ
('Seagate Backup Plus 2TB', 'seagate-backup-plus-2tb', 4, '2TB external HDD', 59.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457712/Seagate_Backup_Plus_Slim_2TB_Portable_Hard_Drive_fnciux.jpg', '2023-05-30', 10.0, 2500, 'External hard drive for backup and storage', 'supplier-28'),
('Samsung T7 1TB', 'samsung-t7-1tb', 4, '1TB external SSD', 109.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457711/SAMSUNG_T7_Touch_Portable_SSD_1TB_-_Up_to_1050_jqjjmm.jpg', '2023-04-15', 15.0, 2200, 'Fast external SSD for high-speed transfers', 'supplier-02'),
('WD My Passport 4TB', 'wd-my-passport-4tb', 4, '4TB external HDD', 109.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746457711/WD_4TB_My_Passport_Wireless_Pro_Portable_external_rcciyc.jpg', '2023-06-10', 10.0, 2000, 'External storage with large capacity', 'supplier-29'),

-- Camera an ninh
('Ring Video Doorbell 4', 'ring-video-doorbell-4', 6, '1080p HD video doorbell', 199.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458125/Ring_Video_Doorbell_Satin_Nickel_bundle_with_Ring_xtxxab.jpg', '2023-07-01', 5.0, 1800, 'Smart doorbell with video and two-way audio', 'supplier-30'),
('Nest Cam Indoor', 'nest-cam-indoor', 6, '1080p HD indoor camera', 129.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458037/Google_Nest_Cam_Indoor_Security_Camera_NC1102AU_bnz46v.jpg', '2023-06-01', 12.0, 1500, 'Indoor security camera with smart features', 'supplier-03'),
('Arlo Pro 4', 'arlo-pro-4', 6, '4K UHD security camera', 249.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458035/Arlo_Pro_4_Spotlight_Camera_nuupkl.jpg', '2023-05-15', 10.0, 1700, 'Outdoor security camera with 4K UHD resolution', 'supplier-31'),

-- Phụ kiện điện thoại
('OtterBox Defender Series Case', 'otterbox-defender-case', 4, 'Phone case for iPhone 14', 49.99, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458034/Take_on_every_adventure_with_confidence_when_you_xqnpxb.jpg', '2023-04-20', 8.0, 1800, 'Rugged case to protect your phone', 'supplier-32'),
('Anker PowerCore 10000', 'anker-powercore-10000', 4, 'Portable charger, 10000mAh', 29.99, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458033/Powerbank_Anker_PowerCore_10K_Noir_10000_mAh_ulgbm8.jpg', '2023-05-10', 12.0, 2200, 'Compact power bank for on-the-go charging', 'supplier-33'),
('Belkin Boost Up Wireless Charger', 'belkin-boost-up-wireless-charger', 4, 'Wireless charging pad', 39.99, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458030/acfefeec-8bc9-4aa1-adca-d68f9e4850e5_qufggr.jpg', '2023-06-01', 10.0, 1500, 'Wireless charger for fast charging', 'supplier-34'),

-- Tai nghe Bluetooth
('Jabra Elite 75t', 'jabra-elite-75t', 5, 'True wireless earbuds with ANC', 149.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458029/b28844af-b6b5-4228-b7dd-9b0a88eb39c8_lduyrv.jpg', '2023-07-10', 10.0, 1900, 'Compact and powerful true wireless earbuds', 'supplier-35'),
('Sony WF-1000XM4', 'sony-wf-1000xm4', 5, 'True wireless earbuds with noise canceling', 279.00, 'https://res.cloudinary.com/dxup3irlk/image/upload/v1746458029/Ecouteurs_SONY_WF-C510_Noir_Autonomie_totale_gcukhh.jpg', '2023-08-15', 15.0, 2000, 'Noise-canceling earbuds with premium sound', 'supplier-13');

INSERT INTO OrderStatus (StatusID, StatusName) VALUES
(1, 'Pending'),
(2, 'Processing'),
(3, 'Shipped'),
(4, 'Delivered');