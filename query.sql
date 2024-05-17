CREATE TABLE EmployeeMarketing(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name  VARCHAR(45),
    email VARCHAR (55),
    password VARCHAR(55),
    role VARCHAR(45)
)




CREATE TABLE coupon_category(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR (45),
    description VARCHAR(255),
    state ENUM('active', 'inactive'),
    creation_date DATETIME,
    creator_category INT
    
)


CREATE Table Redemption(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    buys_id VARCHAR(45),
    fecha_redencion DATETIME,
    EmployeeMarketing_id INT,
    Coupon_id INT,
    user_id INT
    
)



CREATE TABLE Coupon(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    description VARCHAR(45),
    creation_date DATE,
    activation_date DATE,
    redemption_date DATE,
    expiration_date DATE,
    discount DECIMAL,
    Status ENUM("activo","desactivado","redimido","eliminado") ,
    use_type ENUM("limitado", "ilimitado") ,
    Quantity_uses INT,
    discount_type ENUM("NET","PERCENTUAL"),
    creator_employee_id INT,
    coupon_category INT
)


CREATE table  Users(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45),
    lastname VARCHAR(55),
    email VARCHAR (55),
    document_type ENUM("Cedula","Pasaporte","Cedula Extranjeria"),
    document_number INT
)

CREATE Table coupon_sent(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    user_id  INT,
    coupon_id INT
)

ALTER TABLE coupon_category ADD FOREIGN KEY (creator_category) REFERENCES EmployeeMarketing(id)


ALTER TABLE Coupon ADD FOREIGN KEY (creator_employee_id) REFERENCES EmployeeMarketing(id)

ALTER TABLE Coupon ADD FOREIGN KEY (coupon_category) REFERENCES coupon_category(id)

ALTER TABLE Redemption ADD FOREIGN KEY (Coupon_id) REFERENCES Coupon(id)

ALTER TABLE Redemption ADD FOREIGN KEY (EmployeeMarketing_id) REFERENCES EmployeeMarketing(id)

ALTER TABLE Redemption ADD FOREIGN KEY (user_id) REFERENCES Users(id)

ALTER TABLE coupon_sent  ADD FOREIGN KEY (coupon_id) REFERENCES Coupon(id)
