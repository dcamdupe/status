CREATE TABLE status
(status_id int IDENTITY NOT NULL PRIMARY KEY,
message nvarchar(500) NOT NULL,
user_name nvarchar(200) NOT NULL,
date_added datetime NOT NULL)