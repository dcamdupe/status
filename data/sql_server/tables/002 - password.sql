CREATE TABLE password
(user_id int NOT NULL FOREIGN KEY REFERENCES [user](user_id),	-- TODO: make this primary key
salt varchar(50),
password varchar(100)
CONSTRAINT pk_user_id PRIMARY KEY(user_id))