CREATE DATABASE MobileShoppedb;
GO
USE MobileShoppedb;
GO

-- tbl_User
CREATE TABLE tbl_User (
    Username NVARCHAR(20) PRIMARY KEY,
    Pwd NVARCHAR(20), -- Password
    EmployeeName NVARCHAR(50),
    Address NVARCHAR(MAX),
    MobileNumber NVARCHAR(20),
    Hint NVARCHAR(50)
);

-- tbl_Company
CREATE TABLE tbl_Company (
    CompanyID NVARCHAR(20) PRIMARY KEY,
    CompanyName NVARCHAR(50)
);

-- tbl_Model
CREATE TABLE tbl_Model (
    ModelID NVARCHAR(20) PRIMARY KEY,
    CompanyId NVARCHAR(20),
    ModelNumber NVARCHAR(50),
    AvailableQty INT,
    FOREIGN KEY (CompanyId) REFERENCES tbl_Company(CompanyId)
);

-- tbl_Transaction
CREATE TABLE tbl_Transaction (
    TransactionID NVARCHAR(20) PRIMARY KEY,
    ModelId NVARCHAR(20),
    Quantity INT,
    Date DATE,
    Amount MONEY,
    FOREIGN KEY (ModelId) REFERENCES tbl_Model(ModelId)
);

-- tbl_Mobile
CREATE TABLE tbl_Mobile (
    ModelId NVARCHAR(20),
    IMEINO NVARCHAR(50) PRIMARY KEY,
    Status NVARCHAR(20),
    Warranty DATE,
    Price MONEY,
    FOREIGN KEY (ModelId) REFERENCES tbl_Model(ModelId)
);

-- tbl_Customer
CREATE TABLE tbl_Customer (
    CustomerID NVARCHAR(20) PRIMARY KEY,
    CustomerName NVARCHAR(50),
    MobileNumber NVARCHAR(20),
    EmailID NVARCHAR(50),
    Address NVARCHAR(MAX)
);

-- tbl_Sales
CREATE TABLE tbl_Sales (
    SalesID NVARCHAR(20) PRIMARY KEY,
    IMEINO INT,
    SalesDate DATE,
    Price MONEY,
    CustomerID NVARCHAR(20),
    FOREIGN KEY (IMEINO) REFERENCES tbl_Mobile(IMEINO),
    FOREIGN KEY (CustomerID) REFERENCES tbl_Customer(CustomerID)
);

INSERT INTO tbl_User VALUES
('user1', '1', 'Nguyen Van A', '123 Le Loi, Q1', '0909123456', N'Tên thú cưng'),
('user2', '1', 'Tran Thi B', '456 Tran Hung Dao, Q5', '0911123456', N'Món ăn yêu thích'),
('admin', '1', 'Le Van C', '789 Cach Mang Thang 8, Q3', '0933123456', N'Trường cấp 1'),
('user3', '1', 'Pham Duy D', '321 Nguyen Thi Minh Khai, Q10', '0944123456', N'Số yêu thích'),
('user4', '1', 'Doan Thi E', '654 Hai Ba Trung, Q1', '0955123456', N'Động vật yêu thích');
GO

INSERT INTO tbl_Company VALUES
('C001', 'Apple'),
('C002', 'Samsung'),
('C003', 'Xiaomi'),
('C004', 'OPPO'),
('C005', 'Nokia');
GO

INSERT INTO tbl_Model VALUES
('M001', 'C001', 'iPhone 13', 10),
('M002', 'C002', 'Galaxy S22', 15),
('M003', 'C003', 'Redmi Note 11', 20),
('M004', 'C004', 'OPPO Reno 8', 12),
('M005', 'C005', 'Nokia G20', 8);
GO

INSERT INTO tbl_Transaction VALUES
('T001', 'M001', 2, '2025-04-01', 30000000),
('T002', 'M002', 3, '2025-04-02', 45000000),
('T003', 'M003', 5, '2025-04-03', 25000000),
('T004', 'M004', 4, '2025-04-04', 28000000),
('T005', 'M005', 1, '2025-04-05', 6000000);
GO

INSERT INTO tbl_Mobile VALUES
('M001', '913220001', 'In Stock', '2026-04-01', 15000000),
('M002', '913220002', 'In Stock', '2026-04-01', 15000000),
('M003', '913220003', 'Sold', '2026-04-01', 10000000),
('M004', '913220004', 'Sold', '2026-04-01', 7000000),
('M005', '913220005', 'In Stock', '2026-04-01', 6000000);
GO

INSERT INTO tbl_Customer VALUES
('CU001', 'Nguyen Thanh', '0981123456', 'thanh@example.com', '123 D1, Binh Thanh'),
('CU002', 'Le Mai', '0982123456', 'mai@example.com', '456 D2, Go Vap'),
('CU003', 'Tran Hieu', '0983123456', 'hieu@example.com', '789 D3, Tan Binh'),
('CU004', 'Pham Linh', '0984123456', 'linh@example.com', '321 D4, Q10'),
('CU005', 'Do Quang', '0985123456', 'quang@example.com', '654 D5, Thu Duc');
GO

INSERT INTO tbl_Sales VALUES
('S001', 913220003, '2025-04-10', 10000000, 'CU001'),
('S002', 913220004, '2025-04-11', 7000000, 'CU002'),
('S003', 913220005, '2025-04-12', 6000000, 'CU003'),
('S004', 913220001, '2025-04-13', 15000000, 'CU004'),
('S005', 913220002, '2025-04-14', 15000000, 'CU005');
GO

 --drop database MobileShoppedb;

-- Disable all foreign key constraints
-- EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";

-- Enable all foreign key constraints
-- EXEC sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL";

-- PROCEDURES
-- CREATE PROCEDURE sp_Login
--     @UserName VARCHAR(20),
--     @PWD VARCHAR(20)
-- AS
-- BEGIN
--     SELECT * FROM tbl_User 
--     WHERE UserName = @UserName AND PWD = @PWD
-- END
-- GO

-- CREATE PROCEDURE sp_AddCompany
--     @CompanyId VARCHAR(20),
--     @CName VARCHAR(20)
-- AS
-- BEGIN
--     INSERT INTO tbl_Company (CompanyId, CName)
--     VALUES (@CompanyId, @CName)
-- END
-- GO

-- CREATE PROCEDURE sp_AddModel
--     @ModelId VARCHAR(20),
--     @CompanyId VARCHAR(20),
--     @ModelNumber VARCHAR(20),
--     @AvailableQty INT
-- AS
-- BEGIN
--     INSERT INTO tbl_Model (ModelId, CompanyId, ModelNumber, AvailableQty)
--     VALUES (@ModelId, @CompanyId, @ModelNumber, @AvailableQty)
-- END
-- GO

-- CREATE PROCEDURE sp_AddMobile
--     @ModelId VARCHAR(20),
--     @IMEINO VARCHAR(50),
--     @Status VARCHAR(20),
--     @Warranty DATE,
--     @Price MONEY
-- AS
-- BEGIN
--     INSERT INTO tbl_Mobile (ModelId, IMEINO, Status, Warranty, Price)
--     VALUES (@ModelId, @IMEINO, @Status, @Warranty, @Price)
-- END
-- GO

-- CREATE PROCEDURE sp_UpdateStock
--     @ModelId VARCHAR(20),
--     @Quantity INT
-- AS
-- BEGIN
--     UPDATE tbl_Model
--     SET AvailableQty = AvailableQty + @Quantity
--     WHERE ModelId = @ModelId
-- END
-- GO

-- CREATE PROCEDURE sp_SalesReport_ByDate
--     @Date DATE
-- AS
-- BEGIN
--     SELECT * FROM tbl_Sales
--     WHERE CONVERT(DATE, SalesDate) = @Date
-- END
-- GO

-- CREATE PROCEDURE sp_SalesReport_DateToDate
--     @FromDate DATE,
--     @ToDate DATE
-- AS
-- BEGIN
--     SELECT * FROM tbl_Sales
--     WHERE SalesDate BETWEEN @FromDate AND @ToDate
-- END
-- GO

-- CREATE PROCEDURE sp_AddUser
--     @UserName VARCHAR(20),
--     @PWD VARCHAR(20),
--     @EmployeeName VARCHAR(20),
--     @Address VARCHAR(MAX),
--     @MobileNumber VARCHAR(20),
--     @Hint VARCHAR(50)
-- AS
-- BEGIN
--     INSERT INTO tbl_User (UserName, PWD, EmployeeName, Address, MobileNumber, Hint)
--     VALUES (@UserName, @PWD, @EmployeeName, @Address, @MobileNumber, @Hint)
-- END
-- GO

-- CREATE PROCEDURE sp_AddCustomer
--     @CustomerID VARCHAR(20),
--     @CustomerName VARCHAR(20),
--     @MobileNumber VARCHAR(20),
--     @EmailID VARCHAR(20),
--     @Address VARCHAR(MAX)
-- AS
-- BEGIN
--     INSERT INTO tbl_Customer (CustomerID, CustomerName, MobileNumber, EmailID, Address)
--     VALUES (@CustomerID, @CustomerName, @MobileNumber, @EmailID, @Address)
-- END
-- GO

-- CREATE PROCEDURE sp_SellMobile
--     @SlsId VARCHAR(20),
--     @IMEINO VARCHAR(50),
--     @SalesDate DATE,
--     @Price MONEY,
--     @CustomerID VARCHAR(20)
-- AS
-- BEGIN
--     INSERT INTO tbl_Sales (SlsId, IMEINO, SalesDate, Price, CustomerID)
--     VALUES (@SlsId, @IMEINO, @SalesDate, @Price, @CustomerID)

--     -- Cập nhật trạng thái máy đã bán
--     UPDATE tbl_Mobile
--     SET Status = 'Sold'
--     WHERE IMEINO = @IMEINO
-- END
-- GO

-- CREATE PROCEDURE sp_ViewStock
-- AS
-- BEGIN
--     SELECT m.ModelId, m.ModelNumber, c.CName, m.AvailableQty
--     FROM tbl_Model m
--     INNER JOIN tbl_Company c ON m.CompanyId = c.CompanyId
-- END
-- GO

-- CREATE PROCEDURE sp_SearchCustomerByIMEI
--     @IMEINO VARCHAR(50)
-- AS
-- BEGIN
--     SELECT c.*
--     FROM tbl_Customer c
--     INNER JOIN tbl_Sales s ON c.CustomerID = s.CustomerID
--     WHERE s.IMEINO = @IMEINO
-- END
-- GO
