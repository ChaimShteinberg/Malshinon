CREATE DATABASE MalshinonDB;
USE MalshinonDB;

CREATE TABLE people (
    name VARCHAR(15) UNIQUE,
    secret_code INT PRIMARY KEY AUTO_INCREMENT
    );
    
CREATE TABLE report (
    reportID INT PRIMARY KEY AUTO_INCREMENT,
    reporter INT,
    target INT,
    ReportText TEXT,
    ReportingTime DATETIME,
    FOREIGN KEY(reporter) REFERENCES people(secret_code),
    FOREIGN KEY (target) REFERENCES people(secret_code)
    );

CREATE TABLE reporter(
    secret_code int,
    rating FLOAT, 
    FOREIGN KEY (secret_code) REFERENCES people(secret_code)
    );
    
CREATE TABLE target(
    secret_code int,
    rating FLOAT, 
    FOREIGN KEY (secret_code) REFERENCES people(secret_code)
    );
    
CREATE TABLE alert(
    target int,
    message text,
    FOREIGN KEY (target) REFERENCES people(secret_code)
    );