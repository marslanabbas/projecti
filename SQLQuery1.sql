CREATE TABLE tblUsers (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Role VARCHAR(20) NOT NULL DEFAULT 'User'
);
GO


CREATE TABLE tblCategories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL
);
GO


CREATE TABLE tblBooks (
    BookID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(150) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    ImageUrl VARCHAR(500),
    CategoryID INT,
    CONSTRAINT FK_Books_Categories FOREIGN KEY (CategoryID) 
    REFERENCES tblCategories(CategoryID)
);
GO


CREATE TABLE tblOrders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserID) 
    REFERENCES tblUsers(UserID)
);
GO


CREATE TABLE tblOrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    BookID INT NOT NULL,
    Quantity INT NOT NULL,
    PricePerUnit DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) 
    REFERENCES tblOrders(OrderID),
    CONSTRAINT FK_OrderDetails_Books FOREIGN KEY (BookID) 
    REFERENCES tblBooks(BookID)
);
GO


INSERT INTO tblUsers (Username, Password, Email, Role) 
VALUES ('admin', 'admin123', 'admin@shop.com', 'Admin');

INSERT INTO tblUsers (Username, Password, Email, Role) 
VALUES ('ali', 'ali123', 'ali@gmail.com', 'User');
GO

INSERT INTO tblOrders (UserID, TotalAmount, Status) 
VALUES (2, 1500.00, 'Pending');

INSERT INTO tblOrders (UserID, TotalAmount, Status) 
VALUES (2, 850.50, 'Shipped');
GO