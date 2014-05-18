INSERT INTO [user] (user_name) VALUES ('valid@test.com')

DECLARE @user_id int

SELECT @user_id = SCOPE_IDENTITY()

-- password represents validPassword
INSERT INTO password
(user_id,
salt,
password)
VALUES
(@user_id,
'$2a$10$w14lcO8AfT9ng5D9FfPz3.',
'$2a$10$w14lcO8AfT9ng5D9FfPz3.UOwaFx2CWmUSYG5HyG/3TPHyy5Itv9m')
