INSERT INTO [user] (user_name) VALUES ('test@test.com')

DECLARE @user_id int

SELECT @user_id = SCOPE_IDENTITY()

INSERT INTO password
(user_id
salt,
password)
VALUES
(@user_id,
'825fea554a7443e98d3049374c15c90f',
)
