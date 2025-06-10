CREATE DATABASE MalshinonDB;
USE MalshinonDB;

CREATE TABLE people (
    name VARCHAR(15) UNIQUE,
    secret_code INT PRIMARY KEY AUTO_INCREMENT
    );
    
CREATE TABLE repot (
    reporter INT,
    target INT,
    ReportText TEXT,
    ReportingTime DATETIME,
    FOREIGN KEY(reporter) REFERENCES people(secret_code),
    FOREIGN KEY (target) REFERENCES people(secret_code)
    );