
CREATE TABLE EmployeeMarketing(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name  VARCHAR(45),
    email VARCHAR (55),
    password VARCHAR(55),
    role VARCHAR(45)
)

DROP TABLE `EmployeeMarketing`

CREATE Table Redemption(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    buys_id VARCHAR(45),
    fecha_redencion DATETIME,
    Coupon_id INT,
    user_id INT
    
)

DROP TABLE `Redemption`

CREATE TABLE Coupons(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    description VARCHAR(45),
    creation_date DATE,
    activation_date DATE,
    expiration_date DATE,
    discount DECIMAL,
    Status ENUM("activo","desactivado","redimido","eliminado") ,
    use_type ENUM("limitado", "ilimitado") ,
    Quantity_uses INT,
    discount_type ENUM("NET","PERCENTUAL"),
    creator_employee_id INT
)

DROP TABLE `Coupons`

CREATE table  Users(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    lastname VARCHAR(55),
    email VARCHAR (55),
    document_type ENUM("Cedula","Pasaporte","Cedula Extranjeria"),
    document_number INT
)

DROP TABLE `Users`

CREATE Table coupon_sent(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    user_id  INT,
    coupon_id INT
)

DROP TABLE coupon_sent



ALTER TABLE Coupons ADD FOREIGN KEY (creator_employee_id) REFERENCES EmployeeMarketing(id)

ALTER TABLE Redemption ADD FOREIGN KEY (Coupon_id) REFERENCES Coupons(id)

ALTER TABLE Redemption ADD FOREIGN KEY (user_id) REFERENCES Users(id)

ALTER TABLE coupon_sent  ADD FOREIGN KEY (coupon_id) REFERENCES Coupons(id)

ALTER TABLE coupon_sent  ADD FOREIGN KEY (user_id) REFERENCES Users(id)




CREATE TABLE CouponCategories (
    CategoryId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(55)
);


drop Table CouponCategories;


ALTER TABLE Coupons
ADD CategoryId INT,
ADD FOREIGN KEY (CategoryId) REFERENCES CouponCategories(CategoryId);

INSERT INTO CouponCategories  (`Name`)
VALUES ('Cumpleaños');


INSERT INTO CouponCategories (`Name`)
VALUES 
    ('Descuento por tiempo limitado'),
    ('Cupón de primera compra'),
    ('Cupón de lealtad'),
    ('Cupón de envío gratis'),
    ('Cupón de descuento por cantidad'),
    ('Cupón de temporada'),
    ('Cupón de descuento por referidos'),
    ('Cupón de descuento para estudiantes'),
    ('Cupón de descuento para empleados'),
    ('Cupón de aniversario'),
    ('Cupón de descuento por compra recurrente');


-- Verificar todos los cupones y sus categorías
SELECT c.id, c.name, c.creator_employee_id, e.Name AS EmployeeName, c.CategoryId, cc.Name AS CategoryName
FROM Coupons c
LEFT JOIN EmployeeMarketing e ON c.creator_employee_id = e.id
LEFT JOIN CouponCategories cc ON c.CategoryId = cc.CategoryId;

-- Verificar todos los cupones de un empleado específico
SELECT c.id, c.name, c.creator_employee_id, e.Name AS EmployeeName, c.CategoryId, cc.Name AS CategoryName
FROM Coupons c
LEFT JOIN EmployeeMarketing e ON c.creator_employee_id = e.id
LEFT JOIN CouponCategories cc ON c.CategoryId = cc.CategoryId
WHERE c.creator_employee_id = [id];


/* -- Crear tabla EmployeeMarketing
CREATE TABLE IF NOT EXISTS EmployeeMarketing(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name  VARCHAR(45),
    email VARCHAR(55),
    password VARCHAR(55),
    role ENUM('marketing', 'ventas', 'administrativo', 'otro')
);

-- Crear tabla Coupons
CREATE TABLE IF NOT EXISTS Coupons(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    description VARCHAR(45),
    creation_date DATE,
    activation_date DATE,
    expiration_date DATE,
    discount DECIMAL,
    Status ENUM('activo', 'desactivado', 'redimido', 'eliminado'),
    use_type ENUM('limitado', 'ilimitado'),
    Quantity_uses INT,
    discount_type ENUM('NET', 'PERCENTUAL'),
    creator_employee_id INT,
    CategoryId INT,
    FOREIGN KEY (creator_employee_id) REFERENCES EmployeeMarketing(id),
    FOREIGN KEY (CategoryId) REFERENCES CouponCategories(CategoryId)
);

-- Crear tabla Redemption
CREATE TABLE IF NOT EXISTS Redemption(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    buys_id VARCHAR(45),
    fecha_redencion DATETIME,
    Coupon_id INT,
    user_id INT,
    FOREIGN KEY (Coupon_id) REFERENCES Coupons(id),
    FOREIGN KEY (user_id) REFERENCES Users(id)
);

-- Crear tabla Users
CREATE TABLE IF NOT EXISTS Users(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    lastname VARCHAR(55),
    email VARCHAR(55),
    document_type ENUM('Cedula', 'Pasaporte', 'Cedula Extranjeria'),
    document_number INT
);

-- Crear tabla coupon_sent
CREATE TABLE IF NOT EXISTS coupon_sent(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    coupon_id INT,
    FOREIGN KEY (coupon_id) REFERENCES Coupons(id),
    FOREIGN KEY (user_id) REFERENCES Users(id)
);

-- Crear tabla CouponCategories
CREATE TABLE IF NOT EXISTS CouponCategories (
    CategoryId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(55)
);

-- Insertar datos en CouponCategories
INSERT INTO CouponCategories (Name) VALUES 
    ('Cumpleaños'),
    ('Descuento por tiempo limitado'),
    ('Cupón de primera compra'),
    ('Cupón de lealtad'),
    ('Cupón de envío gratis'),
    ('Cupón de descuento por cantidad'),
    ('Cupón de temporada'),
    ('Cupón de descuento por referidos'),
    ('Cupón de descuento para estudiantes'),
    ('Cupón de descuento para empleados'),
    ('Cupón de aniversario'),
    ('Cupón de descuento por compra recurrente');
 */