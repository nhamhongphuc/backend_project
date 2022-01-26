CREATE TABLE ProductType
(
	ProductTypeID INT NOT NULL PRIMARY KEY AUTO_INCREMENT ,
    ProductTypeName NVARCHAR(50) NOT NULL
);

CREATE TABLE Supplier
(
	SupplierID INT NOT NULL PRIMARY KEY AUTO_INCREMENT ,
    SupplierName NVARCHAR(50) NOT NULL,
    SupplierAddress NVARCHAR(50) NOT NULL
);

CREATE TABLE Product
(
	ProductID INT NOT NULL PRIMARY KEY AUTO_INCREMENT ,
	SupplierID INT NOT NULL,
    ProductTypeID INT NOT NULL,
    ProductName NVARCHAR(50) NOT NULL,
    Price DOUBLE  NOT NULL,
    IMG_URL varchar(150),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID),
    FOREIGN KEY (ProductTypeID) REFERENCES ProductType(ProductTypeID)
);

CREATE TABLE User
(
	UserID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	UserName  NVARCHAR(50) NOT NULL,
    UserMail VARCHAR(40) NOT NULL,
    UserBirthdate DATE,
    UserGender INT,
    UserAddress NVARCHAR(60) NOT NULL
);

CREATE TABLE Account
(
	AccountID VARCHAR(40) NOT NULL PRIMARY KEY,
	AccountPassword  VARCHAR(20) NOT NULL,
    UserID INT NOT NULL,
    IsAdmin BOOLEAN,
    CreatedDate DATE,
    IsActive BOOLEAN,
	FOREIGN KEY (UserID) REFERENCES User(UserID)
); 

CREATE TABLE SearchHistory
(
	SearchHistoryID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    AccountID VARCHAR(40) NOT NULL,
    SearchContent NVARCHAR(50) NOT NULL,
    SearchDate DATETIME,
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE Review
(
	ProductID INT NOT NULL AUTO_INCREMENT,
    AccountID VARCHAR(40) NOT NULL ,
    Ranking INT NOT NULL,
    Comment NVARCHAR(80),
    CreatedDate DATETIME,
    PRIMARY KEY (ProductID,AccountID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE Cart 
(
	CartID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	AccountID VARCHAR(40) NOT NULL ,
    CartCapacity INT NOT NULL,
    CartTotal DOUBLE NOT NULL,
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE CartDetail 
(
	CartID INT NOT NULL,
    ProductID INT NOT NULL,
    Capacity INT NOT NULL,
    Money DOUBLE NOT NULL,
    AddDate DATETIME NOT NULL,
    PRIMARY KEY (CartID, ProductID),
    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE  Address
(
	AddressID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    AccountID VARCHAR(40) NOT NULL,
    Diachi  NVARCHAR(60) NOT NULL,
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE  Order_
(
	OrderID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    	AddressID INT NOT NULL,
    	AccountID VARCHAR(40) NOT NULL,
    	CreatedDate DATETIME,
    	Status_ INT NOT NULL,
    	Total DOUBLE NOT NULL,
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
    	FOREIGN KEY (AddressID) REFERENCES Address(AddressID)
);

CREATE TABLE OrderDetail 
(
	OrderID INT NOT NULL AUTO_INCREMENT,
    ProductID INT NOT NULL,
	Capacity INT NOT NULL,
    Money DOUBLE NOT NULL,
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Order_(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

INSERT INTO supplier VALUES(1,'Adidas','USA');
INSERT INTO supplier VALUES(2,'Nike','USA');

INSERT INTO producttype VALUES(1,'Shoe');
INSERT INTO producttype VALUES(2,'Shirt');

INSERT INTO product VALUES(1,1,2,"Own the run tee",750000,"https://drive.google.com/file/d/1SyFDHvXwM7qw12I30cfucIJdUGdi_Ljp/view?usp=sharing");
INSERT INTO product VALUES(2,2,1,"Air Jordan XXXVI",5535000,"https://drive.google.com/file/d/1EVmu5ETMZX3-PF5muxrWX7WQJkeb-PN6/view?usp=sharing");
INSERT INTO user VALUES(1,'Nhâm hồng Phúc','nhamphuc414@gm.com','2001-1-14',1,"Bình Dương");
INSERT INTO account VALUES('hongphuc414','12345',1,0,'2021-11-14',0);
/*Insert product type*/
select * from producttype;
INSERT INTO producttype(ProductTypeName) VALUES('Giày bóng đá');
INSERT INTO producttype(ProductTypeName) VALUES('Giày thể thao');
INSERT INTO producttype(ProductTypeName) VALUES('Quần áo bóng đá');
INSERT INTO producttype(ProductTypeName) VALUES('Bó gối bóng đá');
INSERT INTO producttype(ProductTypeName) VALUES('Túi thể thao');
/*Insert supplier*/
select * FROM supplier;
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Aolikes','Quan 1 tp HCM');
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Super Sonic','Thuận An, Bình Dương');
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Mira Sky','Dĩ An, Bình Dương');
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Jogarbola','Quận 2 tp HCM');
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Fasten','Dĩ An, Bình Dương');
INSERT INTO SUPPLIER(supplierName, SupplierAddress) VALUES('Clash','Thuận An, Bình Dương');

/*Insert product*/
select * from product;
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/file/d/1ZOfFSKXYxwLYY-3R3YobFDs9ZEAEh5Ia/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/file/d/14NWj7UQqWZHGgi4ZpGmmGOlvJ1n_phvL/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/file/d/1iQrBi5xvA__fIgsJLMHzWyt7r7sMBG2w/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/file/d/1Rs1kPbNWVGKjr4NxfzwhVF9Em3d6i0XM/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/file/d/1BrYrRbnTuteII-0QLTONEt4uP6ysFzzT/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,4,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/file/d/1OXKt9pRk9Z0ujzMGEkgZL-5KdxFx7o01/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,5,'Giày thể thao Mira Sky S1',699000,'https://drive.google.com/file/d/17WCHnpRWTuA_AAKclh4hZhDjAy5U2YrL/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,5,'Giày thể thao Mira Sky S2',699000,'https://drive.google.com/file/d/1rWs2Tpqnve88Voew5s_f7hYKRQEQQJfg/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,5,'Giày thể thao Mira Sky S3',699000,'https://drive.google.com/file/d/1tatbg0hYlyMY3OK22lHZf0Foy1wPkaoD/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,5,'Giày thể thao Mira Sky S4',699000,'https://drive.google.com/file/d/1856Uqg6U368BZE8bGvb6o_8kf72mMss8/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,5,'Giày thể thao Jogarbola Boost M1',790000,'https://drive.google.com/file/d/1k3EMHq2KqBv1dKtOALghtGGN--XHl32m/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,5,'Giày thể thao Jogarbola Boost M2',790000,'https://drive.google.com/file/d/197i8V9ozl1f-1M-4ySrj1q-rFgFXwSG8/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 001 TF',490000,'https://drive.google.com/file/d/1Z51aMyFkgilZf59Vw_e1fvOGWm9EJ0Fn/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 002 TF',490000,'https://drive.google.com/file/d/18aNPsDhIsYpTA1N3Zy4HKxwtyvvVaDBt/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 003 TF',490000,'https://drive.google.com/file/d/1lt6x_oau9sPltALMRqUZdG003yQxVaxc/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 004 TF',490000,'https://drive.google.com/file/d/1CnPXIabERaj2B_loYOQpX4VNYM4rQygO/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 005 TF',490000,'https://drive.google.com/file/d/1t8-Hl88BNc8_jeNn_WUQwy9B_EaETsZD/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày Jogarbola 006 TF',490000,'https://drive.google.com/file/d/19zY78L4147wGSBm9gR7XBxCgGtZ8EL8-/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',170000,'https://drive.google.com/file/d/1i0sK0sgOAQLeTbTvna-4j9agznGEzifX/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/file/d/17E6WMTRQNKg3A1x6Opin0AXcqZpKnYaK/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/file/d/1KqAgXWT-hrtBR7GT2iedLOSw2fTHFbQz/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/file/d/178NU-qW_ulyjHqFgj70lwuXWSku9NdE4/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/file/d/13dUMP5Xe4chQa_TzLONPx-9Z468hm0rs/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,6,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/file/d/1brMP2EC4yGbHj5sQvu12aSSuGU9IJDsX/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,8,'Túi đựng giày 2 ngăn X',99000,'https://drive.google.com/file/d/1nUzAukjJTQh_hQmpYoeFJLMuPrUTf1Fz/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,8,'Túi đựng giày 2 ngăn X',99000,'https://drive.google.com/file/d/1DbQRG6ySY851s_p_xx-ZBWjwYQJZgnZr/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,8,'Túi rút thể thao FLASH',119000,'https://drive.google.com/file/d/13MCwol7Sa1V3VVWFVJBJ9x5SmKatJ1Mx/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,8,'Túi rút thể thao FLASH',119000,'https://drive.google.com/file/d/11qnZd8Lwzp58mrci9tFYiXtMVmsmKSgz/view?usp=sharing');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,8,'Túi rút thể thao FLASH',119000,'https://drive.google.com/file/d/1ElJR_wvSjybdmPbPVyUUuO5-hKfG11Rs/view?usp=sharing');


INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/uc?export=view&id=1ZOfFSKXYxwLYY-3R3YobFDs9ZEAEh5Ia');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/uc?export=view&id=14NWj7UQqWZHGgi4ZpGmmGOlvJ1n_phvL');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic S 2021 IC',540000,'https://drive.google.com/uc?export=view&id=1iQrBi5xvA__fIgsJLMHzWyt7r7sMBG2w');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/uc?export=view&id=1Rs1kPbNWVGKjr4NxfzwhVF9Em3d6i0XM');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/uc?export=view&id=1BrYrRbnTuteII-0QLTONEt4uP6ysFzzT');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(5,3,'Giày Pan Sonic L 2021 IC',540000,'https://drive.google.com/uc?export=view&id=1OXKt9pRk9Z0ujzMGEkgZL-5KdxFx7o01');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,4,'Giày thể thao Mira Sky S1',699000,'https://drive.google.com/uc?export=view&id=17WCHnpRWTuA_AAKclh4hZhDjAy5U2YrL');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,4,'Giày thể thao Mira Sky S2',699000,'https://drive.google.com/uc?export=view&id=1rWs2Tpqnve88Voew5s_f7hYKRQEQQJfg');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,4,'Giày thể thao Mira Sky S3',699000,'https://drive.google.com/uc?export=view&id=1tatbg0hYlyMY3OK22lHZf0Foy1wPkaoD');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(6,4,'Giày thể thao Mira Sky S4',699000,'https://drive.google.com/uc?export=view&id=1856Uqg6U368BZE8bGvb6o_8kf72mMss8');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày thể thao Jogarbola Boost M1',790000,'https://drive.google.com/uc?export=view&id=1k3EMHq2KqBv1dKtOALghtGGN--XHl32m');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,4,'Giày thể thao Jogarbola Boost M2',790000,'https://drive.google.com/uc?export=view&id=197i8V9ozl1f-1M-4ySrj1q-rFgFXwSG8');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 001 TF',490000,'https://drive.google.com/uc?export=view&id=1Z51aMyFkgilZf59Vw_e1fvOGWm9EJ0Fn');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 002 TF',490000,'https://drive.google.com/uc?export=view&id=18aNPsDhIsYpTA1N3Zy4HKxwtyvvVaDBt');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 003 TF',490000,'https://drive.google.com/uc?export=view&id=1lt6x_oau9sPltALMRqUZdG003yQxVaxc');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 004 TF',490000,'https://drive.google.com/uc?export=view&id=1CnPXIabERaj2B_loYOQpX4VNYM4rQygO');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 005 TF',490000,'https://drive.google.com/uc?export=view&id=1t8-Hl88BNc8_jeNn_WUQwy9B_EaETsZD');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(7,3,'Giày Jogarbola 006 TF',490000,'https://drive.google.com/uc?export=view&id=19zY78L4147wGSBm9gR7XBxCgGtZ8EL8-');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',170000,'https://drive.google.com/uc?export=view&id=1i0sK0sgOAQLeTbTvna-4j9agznGEzifX');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/uc?export=view&id=17E6WMTRQNKg3A1x6Opin0AXcqZpKnYaK');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/uc?export=view&id=1KqAgXWT-hrtBR7GT2iedLOSw2fTHFbQz');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/uc?export=view&id=178NU-qW_ulyjHqFgj70lwuXWSku9NdE4');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/uc?export=view&id=13dUMP5Xe4chQa_TzLONPx-9Z468hm0rs');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(8,5,'Quần áo bóng đá CP Fasten',490000,'https://drive.google.com/uc?export=view&id=1brMP2EC4yGbHj5sQvu12aSSuGU9IJDsX');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,7,'Túi đựng giày 2 ngăn X',99000,'https://drive.google.com/uc?export=view&id=1nUzAukjJTQh_hQmpYoeFJLMuPrUTf1Fz');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,7,'Túi đựng giày 2 ngăn X',99000,'https://drive.google.com/uc?export=view&id=1DbQRG6ySY851s_p_xx-ZBWjwYQJZgnZr');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,7,'Túi rút thể thao FLASH',119000,'https://drive.google.com/uc?export=view&id=13MCwol7Sa1V3VVWFVJBJ9x5SmKatJ1Mx');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,7,'Túi rút thể thao FLASH',119000,'https://drive.google.com/uc?export=view&id=11qnZd8Lwzp58mrci9tFYiXtMVmsmKSgz');
INSERT INTO product(SupplierID, ProductTypeID, ProductName, Price, IMG_URL) VALUES(9,7,'Túi rút thể thao FLASH',119000,'https://drive.google.com/uc?export=view&id=1ElJR_wvSjybdmPbPVyUUuO5-hKfG11Rs');



/*Procedure tạo user, account, address, cart*/
DELIMITER $$
CREATE PROCEDURE CreateUser(IN name_ nvarchar(50), IN mail varchar(40), IN birth date, IN gender int, IN address nvarchar(60), IN pass varchar(20))
BEGIN
	DECLARE userid_tmp INT DEFAULT 0;
	INSERT INTO User(UserName, UserMail, UserBirthdate, UserGender, UserAddress)  VALUES(name_, mail, birth, gender, address);
    
    SELECT UserID INTO userid_tmp 
    FROM User
    WHERE UserName = name_ AND UserMail = mail;
    INSERT INTO account VALUES(mail, pass, userid_tmp, 0, CURDATE(), 0);
    
    INSERT INTO address(AccountID, DiaChi) VALUES(mail, address);
    
    INSERT INTO cart(AccountID, CartCapacity, CartTotal) VALUES(mail, 0, 0);
END; $$
DELIMITER ;
call CreateUser('Le Ngo Quoc Tuan', 'tuanle@gmail.com', '1999-5-12', 1, 'Biên Hòa, Đồng Nai', '12345');
call CreateUser('Nham Hong Phuc', 'hongphuc@gmail.com', '2001-1-14', 1, 'Tân Uyên, Bình Dương', '12345');
call CreateUser('Bui Ta Loc', 'taloc@gmail.com', '2001-5-12', 1, 'Lý Sơn, Quảng Ngãi', '12345');
call CreateUser('Tran Khoa', 'trankhoa@gmail.com', '2001-5-12', 1, 'An Giang', '12345');

insert into order_(AddressID , AccountID, CreatedDate , status_, total) values(1,'letuan@mail.com','2021-12-21',1,1080000);
insert into orderdetail VALUES(1,30,1,5400000);
insert into orderdetail VALUES(1,31,1,5400000);

insert into order_(AddressID , AccountID, CreatedDate , status_, total) values(2,'hongphuc@mail.com','2021-12-21',1,1080000);
insert into orderdetail VALUES(2,30,1,5400000);
insert into orderdetail VALUES(2,31,1,5400000);

insert into order_(AddressID , AccountID, CreatedDate , status_, total) values(3,'taloc@mail.com','2021-12-21',1,1080000);
insert into orderdetail VALUES(3,32,1,5400000);
insert into orderdetail VALUES(3,33,1,5400000);

insert into order_(AddressID , AccountID, CreatedDate , status_, total) values(4,'trankhoa@mail.com','2021-12-21',1,980000);
insert into orderdetail VALUES(4,42,1,4900000);
insert into orderdetail VALUES(4,43,1,4900000);

insert into cartdetail values(1,45,1,490000,'2021-12-25');
insert into cartdetail values(1,46,1,490000,'2021-12-25');
update cart set cartcapacity =2, carttotal = 980000 where cartid=1;

insert into cartdetail values(2,38,1,699000,'2021-12-25');
insert into cartdetail values(2,39,1,699000,'2021-12-25');
update cart set cartcapacity =2, carttotal = 1398000 where cartid=2;