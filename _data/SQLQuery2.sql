
CREATE TABLE tblRodzaj(
IDRodzaj TINYINT CONSTRAINT pk_IDRodzaj PRIMARY KEY
, Rodzaj VARCHAR(20) NOT NULL)

CREATE TABLE tblCennik(
Klasa TINYINT CONSTRAINT pk_Klasa PRIMARY KEY 
, Cena_godz SMALLMONEY NOT NULL  CHECK (Cena_godz > 0)
)

CREATE TABLE tblSprzet (
IDSprzet SMALLINT CONSTRAINT pk_IDSprzet PRIMARY KEY IDENTITY
, IDRodzaj TINYINT NOT NULL
, Marka VARCHAR(30) NOT NULL
, Model VARCHAR(30) NULL
, Klasa TINYINT NOT NULL
)

CREATE TABLE tblKlienci(
IDKlient INT CONSTRAINT pk_IDKlient PRIMARY KEY
,Imie VARCHAR(20) NOT NULL
,Nazwisko VARCHAR(30) NOT NULL
,PESEL CHAR(11) NOT NULL UNIQUE
,Numer_tel CHAR(9) NOT NULL CHECK (LEFT(Numer_tel,1) not like '0')
)

CREATE TABLE tblPracownicy(
IDPracownik INT CONSTRAINT pk_IDPracownik PRIMARY KEY
,Imie VARCHAR(20) NOT NULL
,Nazwisko VARCHAR(30) NOT NULL
,Data_ur DATE NOT NULL CHECK(Data_ur <= GETDATE())
,PESEL CHAR(11) NOT NULL UNIQUE
,Numer_tel CHAR(9) NOT NULL CHECK (LEFT(Numer_tel,1) not like '0')
,Login_prac varchar(20) NOT NULL
,Password_prac varchar(20) CHECK (Len(Password_prac)>5)
)

CREATE TABLE tblWypozyczenia(
IDWypozyczenia INT CONSTRAINT pk_IDWypozyczenia PRIMARY KEY IDENTITY(1,1)
,IDPracownik INT NOT NULL
,IDSprzet SMALLINT NOT NULL
,IDKlient INT NOT NULL
,Data_Wyp SMALLDATETIME NOT NULL
,Liczba_godz_wyp INT NOT NULL CHECK (Liczba_godz_wyp > 0)
)

ALTER TABLE tblSprzet 
ADD CONSTRAINT FK_tblSprzet_IDRodzaj 
FOREIGN KEY (IDRodzaj) 
REFERENCES tblRodzaj(IDRodzaj)
ON DELETE CASCADE ON UPDATE CASCADE; 

ALTER TABLE tblSprzet 
ADD CONSTRAINT FK_tblSprzet_Klasa
FOREIGN KEY (Klasa) 
REFERENCES tblCennik(Klasa)
ON DELETE CASCADE ON UPDATE CASCADE; 

ALTER TABLE tblWypozyczenia 
ADD CONSTRAINT FK_tblWypozyczenia_IDPracownik
FOREIGN KEY (IDPracownik) 
REFERENCES tblPracownicy(IDPracownik)
ON DELETE CASCADE ON UPDATE CASCADE; 

ALTER TABLE tblWypozyczenia 
ADD CONSTRAINT FK_tblWypozyczenia_IDKlient
FOREIGN KEY (IDKlient) 
REFERENCES tblKlienci(IDKlient)
ON DELETE CASCADE ON UPDATE CASCADE; 

ALTER TABLE tblWypozyczenia 
ADD CONSTRAINT FK_tblWypozyczenia_IDSprzet
FOREIGN KEY (IDSprzet) 
REFERENCES tblSprzet(IDSprzet)
ON DELETE CASCADE ON UPDATE CASCADE; 

 
insert into tblRodzaj values (1,'narty');
insert into tblRodzaj values(2,'snowboard');
insert into tblRodzaj values(3,'kijki');
insert into tblRodzaj values(4,'kask');
insert into tblRodzaj values(5,'google');
insert into tblRodzaj values(6,'buty narciarskie');
insert into tblRodzaj values(7,'buty snowboardowe');
insert into tblRodzaj values(8,'ochraniacze');

insert into tblCennik values(1,10);
insert into tblCennik values(2,20);
insert into tblCennik values(3,30);
insert into tblCennik values(4,40);
insert into tblCennik values(5,60);
insert into tblCennik values(6,80);

insert into tblSprzet values(1,'Volkl','Racetiger SL',6);
insert into tblSprzet values(1,'Nordica','Doberman',6);
insert into tblSprzet values(1,'AK','RED',6);
insert into tblSprzet values(2,'Burton','CustomX',5);
insert into tblSprzet values(2,'Rossignol','Distinct',5);
insert into tblSprzet values(3,'LEKI',NULL,4);
insert into tblSprzet values(4,'POC','ORBIT X',4);
insert into tblSprzet values(5,'HEAD','HyperView',3);
insert into tblSprzet values(6,'HEAD','Raptor LTD',4);
insert into tblSprzet values(7,'Burton','Freestyle',3);
insert into tblSprzet values(8,'LEKI',NULL,2);

insert into tblKlienci values(1,'Jakub','Kolber','11111111111','111111111');
insert into tblKlienci values(2,'Bartosz','Książek','22222222222' ,'222222222');
insert into tblKlienci values(3,'Mateusz','K','33333333333','333333333');
insert into tblKlienci values(4,'Bartosz','Jelonek','44444444444','444444444');
insert into tblKlienci values(5,'Konstanty','Ukca','55555555555','555555555');
insert into tblKlienci values(6,'Krzysztof','Logi','66666666666','666666666');

insert into tblPracownicy values(1,'Jan','Smith','1971-01-20','12341234567', '111111111','smithj', 'Start123++');
insert into tblPracownicy values(2,'Michał','Kowal','1969-02-12','12341334567', '222222222', 'kowalm', 'Start123++');
insert into tblPracownicy values(3,'Anna','Anusz','1970-01-01','12341234561','333333333','anusza','Start123++');
insert into tblPracownicy values(4,'Marek','Chiński','1966-06-07','12341234569', '444444444','chinskim','Start123++');
insert into tblPracownicy values(5,'Karol','Gates','1992-02-01','42341234567', '555555555','gatesk','Start123++');
insert into tblPracownicy values(6,'Grzegorz','Potocki','1984-07-02','12741234567', '666666666', 'potockig','Start123++');

insert into tblWypozyczenia values(1,4,1,GETDATE(),6)
insert into tblWypozyczenia values(2,7,6,GETDATE(),10)
insert into tblWypozyczenia values(4,9,4,GETDATE(),48)
insert into tblWypozyczenia values(3,4,5,GETDATE(),24)
insert into tblWypozyczenia values(6,1,3,GETDATE(),12)
insert into tblWypozyczenia values(5,10,2,GETDATE(),4)