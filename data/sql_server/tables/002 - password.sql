CREATE TABLE password
(user_id int NOT NULL FOREIGN KEY REFERENCES [user](user_id),
salt varchar(50),
password varbinary(100))