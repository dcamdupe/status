CREATE TABLE status
(status_id int IDENTITY NOT NULL PRIMARY KEY,
message nvarchar(500) NOT NULL,
user_id int NOT NULL FOREIGN KEY REFERENCES [user](user_id),
date_added datetime NOT NULL)