CREATE TABLE api_session
(session_id uniqueidentifier NOT NULL PRIMARY KEY,
user_id int NOT NULL FOREIGN KEY REFERENCES [user](user_id),
date_created dateTime NOT NULL,
expires datetime NOT NULL)