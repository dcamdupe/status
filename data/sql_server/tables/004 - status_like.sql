CREATE TABLE status_like
(status_like_id int IDENTITY NOT NULL PRIMARY KEY,
status_id int NOT NULL FOREIGN KEY REFERENCES status(status_id),
ip_address varchar(40) NOT NULL,
date_added datetime NOT NULL,
user_id int NULL FOREIGN KEY REFERENCES [user](user_id))